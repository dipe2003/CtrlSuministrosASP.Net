using ControlSuministrosASP.Net.Models.operario;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlSuministrosASP.Net.Views.Operarios {
    public partial class ListadoOperarios : System.Web.UI.Page {
        private static ControladorOperario cOperario = ControladorOperario.getInstancia();
        public static Operario login { get; set; }
        public static IList<Operario> ListaOperario { get; set; }
        public static IDictionary<string, Operario> DicOperario { get; set; }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                login = (Operario)Session["Operario"];
                ListaOperario = new List<Operario>();
                DicOperario = new Dictionary<string, Operario>();

                ListaOperario = cOperario.ListarOperarios();
                foreach (var operario in ListaOperario) {
                    DicOperario.Add(operario.NombreOperario + " " + operario.ApellidoOperario + "-" + operario.OperarioId.ToString(), operario);
                }
                TablaOperarios.DataSource = ListaOperario;
                TablaOperarios.DataBind();
            }
            
        }

        protected void FiltrarLista(object sender, EventArgs e) {
            if (!NombreOperarioFiltro.Text.Equals(String.Empty)) {
                ListaOperario.Clear();
                foreach (var nombre in DicOperario.Keys) {
                    if (nombre.ToLower().Contains(NombreOperarioFiltro.Text.ToLower())) {
                        ListaOperario.Add(DicOperario[nombre]);
                    }
                }            
            }
            else {
                ListaOperario = new List<Operario>(DicOperario.Values);
            }
            TablaOperarios.DataSource = ListaOperario;
            TablaOperarios.DataBind();
        }

        protected void btnNuevoOperario_Click(object sender, EventArgs e) {
            Response.Redirect("/Views/Operarios/NuevoOperario.aspx");
        }
    }
}