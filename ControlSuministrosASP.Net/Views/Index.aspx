<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ControlSuministrosASP.Net.Views.Index" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            var datosStock = [
                {
                    value: <%= SumSinStock %>,
                    color: "#F7464A",
                    highlight: "#FF5A5E",
                    label: "Debajo Stock Minimo"
                },
                {
                    value:<%= SumConStock %>,
                    color: "#86B559",
                    highlight: "#9AC949",
                    label: "Con Stock"
                }
            ];
            var datosVencimiento = [
                {
                    value:<%= SumVencidos %>,
                    color: "crimson",
                    highlight: "#ff6666",
                    label: "Vencidos"
                },
                {
                    value:<%= SumVigentes %>,
                    color: "#2c5697",
                    highlight: "#4297d7",
                    label: "Vigentes"
                }
            ];
            var opciones = {
                animationSteps: 200
            };
            try {
                // Get context with jQuery - using jQuery's .get() method.
                var ctx = $("#myChart").get(0).getContext("2d");
                var ctx2 = $("#myChartVenc").get(0).getContext("2d");
                // This will get the first returned node in the jQuery collection.
                var StockChart = new Chart(ctx).Pie(datosStock, opciones);
                var VencimientoChart = new Chart(ctx2).Pie(datosVencimiento, opciones);
            } catch (ex) { }
        });
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="page_content" runat="server">
    <fieldset>
        <legend>Acceso Rapido</legend>
        <label class="label">Suministro</label>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
        <asp:DropDownList ID="indexSuministro" runat="server" AutoPostBack="true" OnSelectedIndexChanged="indexSuministro_SelectedIndexChanged" />
        <asp:UpdatePanel runat="server" ID="panelSuministro" UpdateMode="Always">
            <ContentTemplate>
                <div style="align-content: center">
                    <% if(SumSeleccionado == null) { %>
                        Selecciona un suministro para ver mas informacion.
                        <% } else {
                                if(SumSeleccionado.getUltimoIngreso() == null) { %>
                        No hay ingresos registrados del suministro seleccionado.
                        <% } else { %>
                    <label class="label" style="width: 50%">Ulimo Ingreso</label>
                    <input id="ultimoingreso" class="output" style="width: 50%" value="<%= SumSeleccionado.UltimoIngreso.ToShortDateString() %>" /><br />
                    <label class="label" style="width: 50%">Stock Actual</label>
                    <input id="stock" class="output" style="width: 50%" value="<%= SumSeleccionado.StockActual %> <%= SumSeleccionado.UnidadSuministro.NombreUnidad %>"/>
                    <%}
                        } %>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </fieldset>
    <fieldset>
        <legend>Graficos</legend>
        <div class="contenedor-5050">
            <canvas id="myChart" width="300" height="200" class="contenedor-50"></canvas>
            <canvas id="myChartVenc" width="300" height="200" class="contenedor-50"></canvas>
        </div>
        <div class="contenedor-5050">
            <div class="contenedor-5050">
                <div class="contenedor-50 leyenda-grafico" style="background-color: #F7464A;">Debajo de Stock</div>
                <div class="contenedor-50 leyenda-grafico" style="background-color: #86B559">Con Stock</div>
            </div>
            <div class="contenedor-5050">
                <div class="contenedor-50 leyenda-grafico" style="background-color: crimson;">Vencidos</div>
                <div class="contenedor-50 leyenda-grafico" style="background-color: #4297d7">Vigentes</div>
            </div>
        </div>
    </fieldset>
    <fieldset>
        <legend>Detalles</legend>
        <div class="contenedor-5050">
            <div class="contenedor-50">
                <div class="label">Debajo de minimo</div>
                <br />
                <asp:Repeater runat="server" ID="TablaSuministrosStockMinimo">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th class="label" style="width: 54%">Suministro</th>
                                <th class="label" style='width: 20%;'>Stock</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: left">
                                <%#Eval("NombreSuministro") %>
                            </td>
                            <td style="text-align: right">
                                <%#Eval("StockActual") %> <%#Eval("UnidadSuministro.NombreUnidad")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <div class="contenedor-50">
                <div class="label">Lotes vencidos</div>
                <br />
                <table>
                    <tr>
                        <th class="label" style="width: 79%;">Suministro</th>
                        <th class="label" style='width: 20%;'>Vencimiento</th>
                    </tr>

                    <% foreach(var suministro in ListaSuministrosStockVencido) {
                            foreach(var lote in DicLotesVencidos[suministro.SuministroId]) {
                    %>
                    <tr>
                        <td style="text-align: left"><%= suministro.NombreSuministro %></td>
                        <td style="text-align: right"><%= lote.VencimientoLote.Value.ToShortDateString() %></td>
                    </tr>
                    <% }

                        } %>
                </table>
            </div>
        </div>
    </fieldset>
    <asp:Button ID="Button1" runat="server" Text="mail" OnClick="Button1_Click" />
</asp:Content>
