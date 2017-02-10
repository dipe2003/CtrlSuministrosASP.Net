using ControlSuministrosASP.Net.Models.proveedor;
using ControlSuministrosASP.Net.Models.suministro;
using ControlSuministrosASP.Net.Models.suministro.unidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlSuministrosASP.Net.Views.Suministros {
    public partial class EditarSuministro : System.Web.UI.Page {
        private static ControladorSuministro cSum = ControladorSuministro.getInstancia();
        private static ControladorProveedor cProv = ControladorProveedor.getInstancia();
        private static ControladorUnidad cUnidad = ControladorUnidad.getInstancia();
        private static int idSuministro = 0;
        private static Suministro sum = null;
        protected void Page_Load(object sender, EventArgs e) {
            if(!IsPostBack) {
                idSuministro = int.Parse(Request.Params["id"]);
                try {
                    sum = cSum.BuscarSuministro(idSuministro);
                } catch(Exception) {
                    Response.Redirect("/Views/Errores/Error404.aspx");
                }
                if(sum == null) {
                    Response.Redirect("/Views/Errores/Error404.aspx");
                } else {
                    editNombreSuministro.Text = sum.NombreSuministro;
                    editDescripcionSuministro.Text = sum.DescripcionSuministro;
                    editCodigoSAPSuministro.Text = sum.CodigoSAPSuministro;
                    editCantidadStockMinimo.Text = sum.StockMinimoVigente.CantidadStockMinimo.ToString();
                    checkVigente.Checked = sum.Vigente;

                    selectUnidad.DataSource = cUnidad.ListarUnidades();
                    selectUnidad.DataTextField = "NombreUnidad";
                    selectUnidad.DataValueField = "UnidadId";
                    selectUnidad.SelectedValue = sum.UnidadSuministro.UnidadId.ToString();
                    selectUnidad.DataBind();

                    String[] tipoSuministro = { "Material", "Reactivo Químico", "Medio de Ensayo" };
                    selectTipoSuministro.DataSource = tipoSuministro;
                    switch(sum.TipoSuministro) {
                        case "Material":
                            selectTipoSuministro.SelectedValue = "Material";
                            break;

                        case "MedioEnsayo":
                            selectTipoSuministro.SelectedValue = "Medio de Ensayo";
                            break;

                        case "ReactivoQuimico":
                            selectTipoSuministro.SelectedValue = "Reactivo Químico";
                            break;
                    }
                    selectTipoSuministro.DataBind();

                    selectProveedor.DataSource = cProv.ListarDicProveedores();
                    selectProveedor.DataTextField = "Key";
                    selectProveedor.DataValueField = "Value";
                    selectProveedor.SelectedValue = sum.ProveedorSuministro.ProveedorId.ToString();
                    selectProveedor.DataBind();
                }
            }

        }

        protected void btnGuardarSuministro_Click(object sender, EventArgs e) {
            String tipo = sum.TipoSuministro;
            Suministro editSum = null;
            switch(tipo) {
                case "Material":
                    editSum = new Material();
                    break;

                case "MedioEnsayo":
                    editSum = new MedioEnsayo();
                    break;

                case "ReactivoQuimico":
                    editSum = new ReactivoQuimico();
                    break;
            }
            editSum.SuministroId = idSuministro;
            editSum.Vigente = checkVigente.Checked;
            editSum.NombreSuministro = editNombreSuministro.Text;
            editSum.DescripcionSuministro = editDescripcionSuministro.Text;
            editSum.CodigoSAPSuministro = editCodigoSAPSuministro.Text;

            int res = cSum.ActualizarSuministro(editSum, int.Parse(selectProveedor.SelectedValue), int.Parse(selectUnidad.SelectedValue), float.Parse(editCantidadStockMinimo.Text));
            if(res != -1) {
                Response.Redirect("/Views/Suministros/ListadoSuministros.aspx");
            }
        }
    }
}