using ControlSuministrosASP.Net.Models.operario;
using ControlSuministrosASP.Net.Models.proveedor;
using ControlSuministrosASP.Net.Models.suministro;
using ControlSuministrosASP.Net.Models.suministro.lote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlSuministrosASP.Net.Views.Suministros {
    public partial class IngresoSuministro : System.Web.UI.Page {
        private static ControladorSuministro cSuministro = ControladorSuministro.getInstancia();
        private static ControladorProveedor cProveedor = ControladorProveedor.getInstancia();

        private static ControladorIngresoSalida cIngreso = ControladorIngresoSalida.getInstancia();
        private static ControladorLote cLote = ControladorLote.getInstancia();

        private static IDictionary<int, string> Suministros;
        private static IDictionary<int, Suministro> dicSuministros;
        private static IList<Proveedor> Proveedores;
        private static Suministro suministroSeleccionado;

        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                IList<Suministro> sums = cSuministro.ListarSuministros(true);
                dicSuministros = cSuministro.ListarMapSuministrosFull();
                Suministros = new Dictionary<int, string>();
                foreach(Suministro item in sums) {
                    Suministros.Add(item.SuministroId, item.NombreSuministro);
                }                
                selectSuministroIngreso.DataSource = Suministros;
                selectSuministroIngreso.DataTextField = "Value";
                selectSuministroIngreso.DataValueField = "Key";
                selectSuministroIngreso.DataBind();
                selectSuministroIngreso.Items.Add(new ListItem("--- Selecciona Suministro ---", "select", true));
                selectSuministroIngreso.SelectedIndex = selectSuministroIngreso.Items.Count - 1;
                Proveedores = cProveedor.ListarProveedores();
            }
        }

        protected void btnRegistrarIngresoSuministro_Click(object sender, EventArgs e) {
            int idOperarioIngreso = (int)Session["IdOperario"];
            int ingreso = -1;
            int lote = -1;
            if(selectNumeroLoteSuministro.SelectedValue.Equals("nuevo")) {
                DateTime fecha = DateTime.MinValue;
                if(checkPerecedero.Checked) fecha = DateTime.Parse(inputFechaVencimiento.Text);
                lote = cLote.CrearLote(fecha, inputNumeroLoteSuministro.Text, suministroSeleccionado.SuministroId);
                if(lote != -1) {
                    cLote.AgregarLoteSuministro(lote, suministroSeleccionado.SuministroId);
                }
            } else {
                lote = int.Parse(selectNumeroLoteSuministro.SelectedValue);
            }
            ingreso = cIngreso.CrearIngreso(DateTime.Today, float.Parse(inputCantidadIngresoSuministro.Text), inputFacturaIngresoSuministro.Text, lote, idOperarioIngreso, inputObservacionesIngresoSuministro.Text, suministroSeleccionado.SuministroId);
            if(ingreso != -1) {
                Response.Redirect("/Views/Index.aspx");
            }
        }

        protected void selectSuministroIngreso_SelectedIndexChanged(object sender, EventArgs e) {
            if(!selectSuministroIngreso.SelectedValue.Equals("select")) {
                suministroSeleccionado = dicSuministros[int.Parse(selectSuministroIngreso.SelectedValue)];
                selectProveedorIngreso.Text = suministroSeleccionado.ProveedorSuministro.NombreProveedor;
                IList<Lote> lotes = suministroSeleccionado.LotesSuministros;
                selectNumeroLoteSuministro.DataSource = lotes;
                selectNumeroLoteSuministro.DataTextField = "NumeroLote";
                selectNumeroLoteSuministro.DataBind();
                selectNumeroLoteSuministro.Items.Add(new ListItem("-- Nuevo Lote ---", "nuevo", true));
                inputNumeroLoteSuministro.Visible = false;
                outputUnidadIngresoSuministro.Text = suministroSeleccionado.UnidadSuministro.NombreUnidad;
            } else {
                outputUnidadIngresoSuministro.Text = string.Empty;
                selectProveedorIngreso.Text = string.Empty;
                selectNumeroLoteSuministro.Items.Clear();
                selectNumeroLoteSuministro.Items.Add(new ListItem("--Selecciona Suministro ---", "nuevo", true));
            }
        }

        protected void selectNumeroLoteSuministro_SelectedIndexChanged(object sender, EventArgs e) {
            if(!selectNumeroLoteSuministro.SelectedValue.Equals("nuevo")) {
                inputNumeroLoteSuministro.Visible = false;
            } else {
                inputNumeroLoteSuministro.Visible = true;
            }
        }

        protected void cantidad_ServerValidate(object source, ServerValidateEventArgs args) {
            if(float.Parse(inputCantidadIngresoSuministro.Text) <= 0) args.IsValid = false;
        }

        protected void selectCalendar_SelectedIndexChanged(object sender, EventArgs e) {
            if(checkPerecedero.Checked) {
                inputFechaVencimiento.Enabled = true;
            } else {
                inputFechaVencimiento.Enabled = false;
                inputFechaVencimiento.Text = string.Empty;
            }
        }
    }
}