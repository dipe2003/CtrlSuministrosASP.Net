using ControlSuministrosASP.Net.Models.operario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlSuministrosASP.Net.Views {
    public partial class Main : System.Web.UI.MasterPage {
        public static Operario OperarioLogueado { get; }
        public int idOperarioLogueado { get; set; }
        
        protected void Page_Load(object sender, EventArgs e) {
            Operario OperarioLogueado = (Operario)Session["Operario"];
            if (OperarioLogueado != null) {               
                NombreLogueado.Text = OperarioLogueado.NombreOperario + " " + OperarioLogueado.ApellidoOperario;
                PermisoLogueado.Text = OperarioLogueado.PermisoOperario.NombrePermiso;
                idOperarioLogueado = OperarioLogueado.OperarioId;
            }
            else {
                Response.Redirect("/Views/Errores/Error500.aspx");
            }
        }

        protected void btnSalir_Click(object sender, EventArgs e) {
            Session["Operario"] = null;
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        protected void btnEditarOperario_Click(object sender, EventArgs e) {
            if (IsPostBack) {
                Response.Redirect("/Views/Operarios/EditarOperario.aspx?id=" + idOperarioLogueado.ToString());
            }
        }
    }
}