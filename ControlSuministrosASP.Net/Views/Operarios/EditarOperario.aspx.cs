using ControlSuministrosASP.Net.Models.operario;
using ControlSuministrosASP.Net.Models.operario.permiso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlSuministrosASP.Net.Views.Operarios {
    public partial class EditarOperario : System.Web.UI.Page {
        private static ControladorOperario cOperario = ControladorOperario.getInstancia();
        private static ControladorPermiso cPermiso = ControladorPermiso.getInstancia();
        public static int idOperario{get; set;}
        public Operario login { get; set; }
        private static IDictionary<string, Permiso> permisos = new Dictionary<string, Permiso>();
        private static string permisoOperario;
        protected void Page_Load(object sender, EventArgs e) {
            idOperario = int.Parse(Request.Params["id"]);
            login = (Operario)Session["Operario"];
            if(!login.PermisoOperario.NombrePermiso.Equals("Administrador") && login.OperarioId != idOperario){
                Response.Redirect("/Views/Errores/Error500.aspx");
            }
            if (!IsPostBack) {
                try {                    
                    if (idOperario != 0) {
                        Operario op = cOperario.BuscarOperario(idOperario);
                        permisoOperario = op.PermisoOperario.NombrePermiso;
                        editApellidoOperario.Text = op.ApellidoOperario;
                        editCorreoOperario.Text = op.CorreoOperario;
                        editNombreOperario.Text = op.NombreOperario;
                        editAlertas.Checked = op.RecibeAlertas;
                        IList<Permiso> listPermisos = cPermiso.ListarPermisos();
                        permisos.Clear();
                        foreach (var permiso in listPermisos) {
                            permisos.Add(permiso.NombrePermiso, permiso);
                        }
                        editPermisoOperario.DataSource = permisos.Keys;
                        editPermisoOperario.SelectedValue = permisoOperario;
                        editPermisoOperario.DataBind();
                        if (!login.PermisoOperario.NombrePermiso.Equals("Administrador")) { editPermisoOperario.Enabled = false; }
                    }
                } catch (NullReferenceException ex) {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        protected void btnEditarOperario_Click(object sender, EventArgs e) {
            int id = cOperario.ModificarDatosOperario(idOperario, editNombreOperario.Text, editApellidoOperario.Text, editCorreoOperario.Text, editAlertas.Checked);            
            if (id != -1){
                if (!permisoOperario.Equals(editPermisoOperario.SelectedValue)) {
                    Permiso permiso = cPermiso.BuscarPermiso((permisos[editPermisoOperario.SelectedValue]).PermisoId);
                    cOperario.AgregarPermiso(id, permiso);
                }
                Response.Redirect("/Views/Operarios/ListadoOperarios.aspx");
            }else{
                Response.Write("<div class='error'>No se pudo actualizar</div>");
            }
        }

        protected void validarPassActual(object source, ServerValidateEventArgs args) {
            args.IsValid = cOperario.ValidarOperario(idOperario, args.Value);
        }
    }
}