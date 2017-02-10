using ControlSuministrosASP.Net.Models.operario;
using ControlSuministrosASP.Net.Models.proveedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlSuministrosASP.Net.Views.Proveedores {
    public partial class ListadoProveedores : System.Web.UI.Page {
        private static ControladorProveedor cProveedor = ControladorProveedor.getInstancia();
        public static Operario login { get; set; }
        public static IList<Proveedor> ListaProveedores { get; set; }
        public static IDictionary<string, Proveedor> DicProveedores { get; set; }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                login = (Operario)Session["Operario"];
                ListaProveedores = new List<Proveedor>();
                DicProveedores = new Dictionary<string, Proveedor>();

                ListaProveedores = cProveedor.ListarProveedores();
                foreach (var proveedor in ListaProveedores) {
                    DicProveedores.Add(proveedor.NombreProveedor +  "-" + proveedor.ProveedorId.ToString(), proveedor);
                }
                TablaProveedores.DataSource = ListaProveedores;
                TablaProveedores.DataBind();
            }
        }

        protected void FiltrarLista(object sender, EventArgs e) {
            if (!NombreProveedorFiltro.Text.Equals(String.Empty)) {
                ListaProveedores.Clear();
                foreach (var nombre in DicProveedores.Keys) {
                    if (nombre.ToLower().Contains(NombreProveedorFiltro.Text.ToLower())) {
                        ListaProveedores.Add(DicProveedores[nombre]);
                    }
                }
            }
            else {
                ListaProveedores = new List<Proveedor>(DicProveedores.Values);
            }
            TablaProveedores.DataSource = ListaProveedores;
            TablaProveedores.DataBind();
        }

        protected void btnNuevoProveedor_Click(object sender, EventArgs e) {
            Response.Redirect("/Views/Proveedores/NuevoProveedor.aspx");
        }

    }
}