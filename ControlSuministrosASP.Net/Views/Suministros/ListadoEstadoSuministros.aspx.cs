using ControlSuministrosASP.Net.Models.operario;
using ControlSuministrosASP.Net.Models.suministro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace ControlSuministrosASP.Net.Views.Suministros {
    public partial class ListadoEstadoSuministros : System.Web.UI.Page {
        private static ControladorSuministro cSuministro = ControladorSuministro.getInstancia();
        protected static Operario OperarioLogueado {get; set;}
        protected Operario Mlogin;
        public static IList<Suministro> ListaSuministros { get; set; }
        public static IDictionary<string, Suministro> DicSuministro { get; set; }
        public static IDictionary<int, Suministro> DicIdSuministros { get; set; }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                OperarioLogueado = (Operario)Session["Operario"];
                ListaSuministros = new List<Suministro>();
                DicSuministro = new Dictionary<string, Suministro>();
                ListaSuministros = cSuministro.ListarSuministros(true);
                foreach (var suministro in ListaSuministros) {
                    DicSuministro.Add(suministro.NombreSuministro + " (" + suministro.ProveedorSuministro.NombreProveedor + ")", suministro);
                }
                
                TablaSuministros.DataSource = ListaSuministros;
                TablaSuministros.DataBind();
            }
        }

        protected void FiltrarLista(object sender, EventArgs e) {
            if (!NombreSuministroFiltro.Text.Equals(String.Empty)) {

                ListaSuministros.Clear();
                foreach (var nombre in DicSuministro.Keys) {
                    if (nombre.ToLower().Contains(NombreSuministroFiltro.Text.ToLower())) {
                        ListaSuministros.Add(DicSuministro[nombre]);
                    }
                }
            }
            else {
                ListaSuministros = new List<Suministro>(DicSuministro.Values);
            }
            TablaSuministros.DataSource = ListaSuministros;
            TablaSuministros.DataBind();
        }
    }
}