using ControlSuministrosASP.Net.Models.notificaciones;
using ControlSuministrosASP.Net.Models.suministro;
using ControlSuministrosASP.Net.Models.suministro.lote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlSuministrosASP.Net.Views {
    public partial class Index : System.Web.UI.Page {
        private static ControladorSuministro cSum = ControladorSuministro.getInstancia();
        private static ControladorLote cLote = ControladorLote.getInstancia();

        public static Suministro SumSeleccionado;
        public String SumVencidos;
        public String SumVigentes;
        public String SumSinStock;
        public String SumConStock;

        public IList<Suministro> ListaSuministrosDebajoStock;
        public IList<Suministro> ListaSuministrosStockVencido;
        public IDictionary<int, IList<Lote>> DicLotesVencidos = new Dictionary<int, IList<Lote>>();
        public static IList<Suministro> ListaTodosSuministros;

        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                ListaTodosSuministros = cSum.ListarSuministros(true);
                indexSuministro.DataSource = ListaTodosSuministros;
                indexSuministro.DataTextField = "NombreSuministro";
                indexSuministro.DataValueField = "SuministroId";
                indexSuministro.DataBind();
                indexSuministro.Items.Add(new ListItem("--- Selecciona Suministro ---", "select", true));
                indexSuministro.SelectedIndex = indexSuministro.Items.Count - 1;
                SumSeleccionado = null;
            }
            ListaSuministrosStockVencido = cSum.getSuministrosConLotesVencidos(true);
            foreach(Suministro suministro in ListaSuministrosStockVencido) {
                if(suministro.TieneLotesVencidos) DicLotesVencidos.Add(suministro.SuministroId, suministro.getLotesVencidosEnStock());
            }

            ListaSuministrosDebajoStock = cSum.GetSuministrosDebajoDeStock(true);
            TablaSuministrosStockMinimo.DataSource = ListaSuministrosDebajoStock;
            TablaSuministrosStockMinimo.DataBind();

            int totalVencidos = cSum.getIdsSuministrosConLotesVencidos(true).Count();
            int totalReg = cSum.GetTotalSuministrosDebajoStockMinimo()[0];
            int totalSinStock = cSum.GetTotalSuministrosDebajoStockMinimo()[1];

            SumVencidos = totalVencidos.ToString();
            SumVigentes = (totalReg - totalVencidos).ToString();
            SumSinStock = totalSinStock.ToString();
            SumConStock = (totalReg - totalSinStock).ToString();
        }

        protected void Button1_Click(object sender, EventArgs e) {
            if(IsPostBack) {
                using(var db = ControlSuministrosASP.Net.Models.ContextoBD.SuministrosDBContext.getInstancia()) {
                    Dictionary<String, Propiedad> propiedades = db.Propiedades.ToDictionary(propiedad => propiedad.Nombre);
                    SendMail mail = new SendMail { FromAddress = propiedades["mail_from"].Valor, Port = int.Parse(propiedades["mail_port"].Valor), FromPassword = propiedades["mail_pass"].Valor, Host = propiedades["mail_host"].Valor };
                    mail.EnviarMail("mensaje para prueba de control de suministros.", "dipejperez@gmail.com", "ctrl de suministros");
                }
            }
        }

        protected void indexSuministro_SelectedIndexChanged(object sender, EventArgs e) {
            if(!indexSuministro.SelectedValue.Equals("select")) {
                SumSeleccionado = cSum.BuscarSuministro(int.Parse(indexSuministro.SelectedValue));
            } else {
                SumSeleccionado = null;
            }
        }
    }
}