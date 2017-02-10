using ControlSuministrosASP.Net.Models.proveedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlSuministrosASP.Net.Views.Proveedores {
    public partial class EditarProveedor : System.Web.UI.Page {
        private static ControladorProveedor cProveedor = ControladorProveedor.getInstancia();
        private static int IdProveedor { get; set; }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                try {
                    IdProveedor = int.Parse(Request.Params["id"]);
                    Proveedor proveedor = cProveedor.BuscarProveedor(IdProveedor);
                    editNombreProveedor.Text = proveedor.NombreProveedor;
                    editContactoProveedor.Text = proveedor.ContactoProveedor;
                }catch(Exception ex) {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        protected void btnEditarProveedor_Click(object sender, EventArgs e) {
            if (cProveedor.ModificarDatosProveedor(IdProveedor, editNombreProveedor.Text, editContactoProveedor.Text) != -1) Response.Redirect("/Views/Proveedores/ListadoProveedores.aspx");
        }
    }
}