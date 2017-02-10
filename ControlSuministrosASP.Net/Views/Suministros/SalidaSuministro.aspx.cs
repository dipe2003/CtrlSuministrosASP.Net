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
    public partial class SalidaSuministro : System.Web.UI.Page {
        private static ControladorSuministro cSuministro = ControladorSuministro.getInstancia();
        private static ControladorProveedor cProveedor = ControladorProveedor.getInstancia();

        private static ControladorIngresoSalida cSalida = ControladorIngresoSalida.getInstancia();
        private static ControladorLote cLote = ControladorLote.getInstancia();

        private static IDictionary<int, string> Suministros;
        private static IDictionary<int, Suministro> dicSuministros;
        private static IList<Proveedor> Proveedores;
        private static Suministro suministroSeleccionado;

        private static IDictionary<int, Lote> lotesSuministroSeleccionado;

        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                IList<Suministro> sums = cSuministro.ListarSuministros(true);
                dicSuministros = cSuministro.ListarMapSuministrosFull();
                Suministros = new Dictionary<int, string>();
                foreach(Suministro item in sums) {
                    Suministros.Add(item.SuministroId, item.NombreSuministro);
                }
                selectSuministroSalida.DataSource = Suministros;
                selectSuministroSalida.DataTextField = "Value";
                selectSuministroSalida.DataValueField = "Key";
                selectSuministroSalida.DataBind();
                selectSuministroSalida.Items.Add(new ListItem("--- Selecciona Suministro ---", "select", true));
                selectSuministroSalida.SelectedIndex = selectSuministroSalida.Items.Count - 1;
                Proveedores = cProveedor.ListarProveedores();
            }
        }

        protected void selectSuministroSalida_SelectedIndexChanged(object sender, EventArgs e) {
            if(!selectSuministroSalida.SelectedValue.Equals("select")) {
                suministroSeleccionado = dicSuministros[int.Parse(selectSuministroSalida.SelectedValue)];
                selectProveedorSalida.Text = suministroSeleccionado.ProveedorSuministro.NombreProveedor;
                IDictionary<int, string> lotes = new Dictionary<int, string>();
                lotesSuministroSeleccionado = new Dictionary<int, Lote>();
                IList<Lote> tmpLotes = suministroSeleccionado.LotesSuministros;
                foreach(Lote lote in tmpLotes) {
                    if(lote.getCantidadStock() > 0) {
                        lotesSuministroSeleccionado.Add(lote.LoteId, lote);
                        if(lote.Perecedero) {
                            lotes.Add(lote.LoteId, lote.NumeroLote + " - Vto: " + lote.VencimientoLote.Value.ToShortDateString());
                        } else {
                            lotes.Add(lote.LoteId, lote.NumeroLote + " - Vto: ---");
                        }
                    }
                }
                selectNumeroLoteSuministro.DataSource = lotes;
                selectNumeroLoteSuministro.DataTextField = "Value";
                selectNumeroLoteSuministro.DataValueField = "Key";
                selectNumeroLoteSuministro.DataBind();
                selectNumeroLoteSuministro.Items.Insert(0, new ListItem("--Selecciona Lote ---", "select", true));
                outputUnidadSalidaSuministro.Text = suministroSeleccionado.UnidadSuministro.NombreUnidad;
            } else {
                outputUnidadSalidaSuministro.Text = string.Empty;
                selectProveedorSalida.Text = string.Empty;
                selectNumeroLoteSuministro.Items.Clear();
                selectNumeroLoteSuministro.Items.Add(new ListItem("--Selecciona Suministro ---", "nuevo", true));
            }
        }

        protected void btnRegistrarSalidaSuministro_Click(object sender, EventArgs e) {
            int IdOperarioSalida = (int)Session["IdOperario"];
            int idLote = int.Parse(selectNumeroLoteSuministro.SelectedValue);
            int res = cSalida.CrearSalida(DateTime.Today, float.Parse(inputCantidadSalidaSuministro.Text), idLote, IdOperarioSalida, inputObservacionesSalidaSuministro.Text);
            if(res != -1) {
                Response.Redirect("/Views/Index.aspx");
            }
        }

        /// <summary>
        /// Comprueba que el valor ingresado para dar de baja de stock no sea negativo o mayor al saldo disponible del suministro seleccionado.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void cantidad_ServerValidate(object source, ServerValidateEventArgs args) {
            float stock = lotesSuministroSeleccionado[int.Parse(selectNumeroLoteSuministro.SelectedValue)].getCantidadStock();
            float cant = float.Parse(inputCantidadSalidaSuministro.Text);
            if(cant <= 0 || (stock- cant) < 0 ) args.IsValid = false;
        }

        protected void inputCantidadSalidaSuministro_TextChanged(object sender, EventArgs e) {
            float cant = float.Parse(inputCantidadSalidaSuministro.Text);
            if(cant >= 0) {
                float stock = lotesSuministroSeleccionado[int.Parse(selectNumeroLoteSuministro.SelectedValue)].getCantidadStock();
                outputNuevoStock.Text = (stock - cant).ToString() + " " + suministroSeleccionado.UnidadSuministro.NombreUnidad;
            } else {
                outputNuevoStock.Text = "Error: cantidad de salida negativa.";
            }
            
        }

        protected void selectNumeroLoteSuministro_SelectedIndexChanged(object sender, EventArgs e) {
            if(selectNumeroLoteSuministro.SelectedValue.Equals("select")) {
                outputStockActual.Text = string.Empty;
                outputNuevoStock.Text = string.Empty;
            } else {
                outputStockActual.Text = lotesSuministroSeleccionado[int.Parse(selectNumeroLoteSuministro.SelectedValue)].getCantidadStock().ToString() + " " + suministroSeleccionado.UnidadSuministro.NombreUnidad;
            }
        }
    }
}