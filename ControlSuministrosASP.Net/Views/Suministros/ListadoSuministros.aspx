<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.Master" AutoEventWireup="true" CodeBehind="ListadoSuministros.aspx.cs" 
    Inherits="ControlSuministrosASP.Net.Views.Suministros.ListadoSuministros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_content" runat="server">
    <fieldset>
        <legend>Listado Suministros Registrados</legend>
        <% if (OperarioLogueado.PermisoOperario.NombrePermiso.Equals("Administrador")) { %>
        <div class="div-boton">
            <asp:LinkButton ID="btnNuevoSuministro" runat="server" class="boton-input" OnClick="btnNuevoSuministro_Click">
                            Nuevo
            </asp:LinkButton>
        </div>
        <% } %>
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
                                <th class="label" style="width: 15%">Tipo</th>
                                <th class="label" style="width: 38%">Suministro</th>
                                <th class="label" style="width: 18%">Proveedor</th>
                                <th class="label" style="width: 10%">Codigo SAP</th>
                                <th class="label" style="width: 10%">Stock Minimo</th>
                                <th class="label" style="width: 5%">En Uso</th>
                                <th class="label" style="width: 2%"></th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:left"><%#Eval("TipoSuministro") %> </td>
                            <td style="text-align:left"><%#Eval("NombreSuministro") %> </td>
                            <td style="text-align:left"><%#Eval("ProveedorSuministro.NombreProveedor") %></td>
                            <td><%#Eval("CodigoSAPSuministro") %></td>
                            <td><%#Eval("StockMinimoVigente.CantidadStockMinimo") %> <%#Eval("UnidadSuministro.NombreUnidad")%></td>
                            <td><%# (bool)Eval("Vigente") ? "Si" : "No" %></td>
                            <td><% if (OperarioLogueado.PermisoOperario.NombrePermiso.Equals("Administrador")) { %>
                                <a href="EditarSuministro.aspx?id=<%#Eval("SuministroId") %>">
                                    <img height="20" src="/Content/Images/edit_icon.ico" />
                                </a>
                                <%}
                                    else { %>
                                <img height="20" src="/Content/Images/edit_icon.ico" title="Solo disponible para Administrador" />
                                <%} %>
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
