<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.Master" AutoEventWireup="true" CodeBehind="ListadoEstadoSuministros.aspx.cs"
    Inherits="ControlSuministrosASP.Net.Views.Suministros.ListadoEstadoSuministros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_content" runat="server">
    <fieldset>
        <legend>Estado de los Suministros Registrados</legend>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
        <table>
            <tr>
                <td>
                    <div class="label">Filtro: </div>
                </td>
                <td>
                    <asp:TextBox ID="NombreSuministroFiltro" TextMode="SingleLine" OnTextChanged="FiltrarLista" AutoPostBack="true" runat="server">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel runat="server" ID="panelTablaSuministros" UpdateMode="Always">
            <ContentTemplate>
                <asp:Repeater runat="server" ID="TablaSuministros">
                    <HeaderTemplate>
                        <table>
                            <tr>
                                <th class="label" style="width: 1%"></th>
                                <th class="label" style="width: 54%">Suministro</th>
                                <th class="label" style="width: 20%">Proveedor</th>
                                <th class="label" style="width: 10%">Ultimo Ingreso</th>
                                <th class="label" style="width: 10%">Stock Actual</th>
                                <th class="label" style="width: 5%">Mas Info</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# ((bool)Eval("DebajoStockMinimo") || (bool)Eval("TieneLotesVencidos")) ? "<img height='20' src='/Content/Images/warning_icon.ico' title='Se requiere atencion' />" : "<img height='20' src='/Content/Images/ok_icon.ico' title='OK' />" %></td>
                            <td style="text-align:left"><%#Eval("NombreSuministro") %> </td>
                            <td style="text-align:left"><%#Eval("ProveedorSuministro.NombreProveedor") %></td>
                            <td><%#(((DateTime)(Eval("UltimoIngreso"))).Equals(DateTime.MinValue)) ? "---" : ((DateTime)(Eval("UltimoIngreso"))).ToShortDateString() %></td>
                            <td><%#Eval("StockActual") %> <%#Eval("UnidadSuministro.NombreUnidad")%></td>
                            <td>
                                <a href="/Views/Suministros/InfoSuministro.aspx?id=<%#Eval("SuministroId") %>">
                                    <div class="boton-input boton-tabla">
                                        ...
                                    </div>
                                </a>

                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </ContentTemplate>
        </asp:UpdatePanel>
    </fieldset>
</asp:Content>
