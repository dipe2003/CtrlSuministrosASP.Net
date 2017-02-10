using ControlSuministrosASP.Net.Models.notificaciones;
using ControlSuministrosASP.Net.Models.operario;
using ControlSuministrosASP.Net.Models.operario.permiso;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlSuministrosASP.Net.Views {
    public partial class Login : System.Web.UI.Page {
        private static ControladorOperario cOperario = ControladorOperario.getInstancia();
        //private static ControladorPermiso cPermiso = new ControladorPermiso();
        protected void Page_Load(object sender, EventArgs e) {
        }

        protected void btnLogin_Click(object sender, EventArgs e) {
            try {
                int IdOperario = int.Parse(inputNumOperario.Text);
                string Password = inputPass.Text;
                if (cOperario.BuscarOperario(IdOperario) != null && cOperario.ValidarOperario(IdOperario, Password)) {
                    Operario operario = cOperario.BuscarOperario(IdOperario);
                    Session["Operario"] = operario;
                    Session["IdOperario"] = operario.OperarioId;
                    FormsAuthentication.RedirectFromLoginPage(operario.NombreOperario, false);                    
                }
                else {
                    Response.Redirect("/Views/Errores/Error500.aspx");
                }
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
                mensajeNumero.Text = "Los datos ingresados no son correctos.";
            }
        }   
    }
}