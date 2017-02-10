using ControlSuministrosASP.Net.Models.proveedor;
using ControlSuministrosASP.Net.Models.suministro;
using ControlSuministrosASP.Net.Models.suministro.unidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlSuministrosASP.Net.Views.Suministros {
    public partial class NuevoSuministro : System.Web.UI.Page {
        private static ControladorSuministro cSuministro = ControladorSuministro.getInstancia();
        private static ControladorProveedor cProveedor = ControladorProveedor.getInstancia();
        private static ControladorUnidad cUnidad = ControladorUnidad.getInstancia();

        private static IDictionary<string, int> Proveedores;
        private static IList<string> TipoSuministro;
        private static IDictionary<string, int> Unidades;
        
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                Proveedores = new Dictionary<string, int>();
                Unidades = new Dictionary<string, int>();
                TipoSuministro = new List<string>();

                Proveedores = cProveedor.ListarDicProveedores();
                IList<Unidad> unidades = cUnidad.ListarUnidades();
                foreach (var unit in unidades) {
                    Unidades.Add(unit.NombreUnidad, unit.UnidadId);
                }

                TipoSuministro.Add("Medio de Cultivo");
                TipoSuministro.Add("Reactivo Quimico");
                TipoSuministro.Add("Material");

                selectTipoSuministro.DataSource = TipoSuministro;
                selectTipoSuministro.DataBind();

                selectProveedor.DataSource = Proveedores;
                selectProveedor.DataTextField = "Key";
                selectProveedor.DataValueField = "Value";
                selectProveedor.DataBind();

                selectUnidad.DataSource = Unidades;
                selectUnidad.DataTextField = "Key";
                selectUnidad.DataValueField = "Value";
                selectUnidad.DataBind();
            }
        }

        protected void btnRegistrarSuministro_Click(object sender, EventArgs e) {
            int unidad = Unidades[selectUnidad.SelectedValue];
            int proveedor = Proveedores[selectProveedor.SelectedValue];
            string tipo = String.Empty;
            switch (selectTipoSuministro.SelectedValue) {
                case "Material":
                    tipo = "Material";
                    break;

                case "Medio de Cultivo":
                    tipo = "MedioCultivo";
                    break;

                case "Reactivo Quimico":
                    tipo = "Reactivo";
                    break;
            }
            int idSuministro = cSuministro.CrearSuministro(inputNombreSuministro.Text, inputDescripcionSuministro.Text, inputCodigoSAPSuministro.Text,unidad, proveedor, tipo);
            if (idSuministro != -1) {
                float cantidad = float.Parse(inputCantidadStockMinimo.Text);
                DateTime fecha = DateTime.Parse(inputFechaVigenteStockMinimo.Text);
                cSuministro.RegistrarStockMinimoSuministro(cantidad, fecha, idSuministro);
                Response.Redirect("/Views/Suministros/ListadoSuministros.aspx");
            }
        }
    }
}