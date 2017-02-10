using ControlSuministrosASP.Net.Models.suministro;
using ControlSuministrosASP.Net.Models.suministro.lote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlSuministrosASP.Net.Views.Suministros {
    public partial class InfoSuministro : System.Web.UI.Page {
        public static Suministro suministroSeleccionado;
        public static IDictionary<int, IList<Ingreso>> ingresosSuministro;

        private static ControladorSuministro cSuministro = ControladorSuministro.getInstancia();

        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                int idsuministro = int.Parse(Request.Params["id"]);
                suministroSeleccionado = cSuministro.BuscarSuministro(idsuministro);
                descripcionSuministro.Text = suministroSeleccionado.DescripcionSuministro;
                codigoSAPSuministro.Text = suministroSeleccionado.CodigoSAPSuministro;
                if(suministroSeleccionado.Vigente) {
                    vigenciaSuministro.Text = "En Uso";
                } else {
                    vigenciaSuministro.Text = "No se Utiliza";
                }
                ingresosSuministro = new Dictionary<int, IList<Ingreso>>();
                foreach(Lote item in suministroSeleccionado.LotesSuministros) {
                    ingresosSuministro.Add(item.LoteId, item.IngresosLote);
                }               
                                
            }
        }
    }
}