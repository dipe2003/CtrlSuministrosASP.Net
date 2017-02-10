using ControlSuministrosASP.Net.Models.operario;
using ControlSuministrosASP.Net.Models.operario.permiso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlSuministrosASP.Net.Views.Operarios {
    public partial class NuevoOperario : System.Web.UI.Page {
        private static ControladorPermiso cPermiso = ControladorPermiso.getInstancia();
        private static ControladorOperario cOperario = ControladorOperario.getInstancia();
        private static IDictionary<string, Permiso> permisos = new Dictionary<string, Permiso>();

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack){                
                IList<Permiso> listPermisos = cPermiso.ListarPermisos();
                permisos.Clear();
                foreach (var permiso in listPermisos){
                    permisos.Add(permiso.NombrePermiso, permiso);
                }
                inputPermisoOperario.DataSource = permisos.Keys;   
                inputPermisoOperario.DataBind();
            }
        }

        protected void btnRegOperario_Click(object sender, EventArgs e) {
            if (IsPostBack) {
                int idOperario = cOperario.CrearOperario(int.Parse(inputNumeroOperario.Text), inputNombreOperario.Text, inputApellidoOperario.Text, inputPassNuevoOperario.Text, inputCorreoOperario.Text);
                Permiso permiso = cPermiso.BuscarPermiso(permisos[inputPermisoOperario.SelectedValue].PermisoId);
                int res = cOperario.AgregarPermiso(idOperario, permiso);
                if (res != -1) {
                    Response.Redirect("/Views/Operarios/ListadoOperarios.aspx");
                }
                else {
                    Response.Write("<div class='error'>No se pudo registrar</div>");
                }
            }
        }
    }
}