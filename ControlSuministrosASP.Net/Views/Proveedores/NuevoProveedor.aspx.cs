using ControlSuministrosASP.Net.Models.proveedor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlSuministrosASP.Net.Views.Proveedores {
    public partial class NuevoProveedor : System.Web.UI.Page {
        private static ControladorProveedor cProveedor = ControladorProveedor.getInstancia();

        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void btnRegistrarProveedor_Click(object sender, EventArgs e) {
            int idproveedor = cProveedor.CrearProveedor(inputNombreProveedor.Text, inputContactoProveedor.Text);
            if (idproveedor != -1) Response.Redirect("/Views/Proveedores/ListadoProveedores.aspx");
        }
    }
}