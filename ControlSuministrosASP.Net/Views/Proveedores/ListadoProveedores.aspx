<%@ Page Title="" Language="C#" MasterPageFile="/Views/Main.Master" AutoEventWireup="true" CodeBehind="ListadoProveedores.aspx.cs" Inherits="ControlSuministrosASP.Net.Views.Proveedores.ListadoProveedores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="page_content" runat="server">
    <fieldset>
        <legend>Listado Proveedores Registrados</legend>
        <% if (login.PermisoOperario.NombrePermiso.Equals("Administrador")) { %>
        <div class="div-boton">
            <asp:linkbutton id="btnNuevoProveedor" runat="server" class="boton-input" onclick="btnNuevoProveedor_Click">
                            Nuevo
            </asp:linkbutton>
        </div>
        <% } %>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"  />
        <table>
            <tr>
                <td>
                    <div class="label">Filtro: </div>
                </td>
                <td>
                    <asp:textbox id="NombreProveedorFiltro" textmode="SingleLine" ontextchanged="FiltrarLista" runat="server" >
                    </asp:textbox>
                </td>
            </tr>
        </table>

        <asp:UpdatePanel runat="server" id="panelTablaSuministros" updatemode="Always" >
             <ContentTemplate>
        <asp:Repeater runat="server" ID="TablaProveedores">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th class="label" style="width: 35%">Nombre</th>
                        <th class="label" style="width: 60%">Contacto</th>
                        <th class="label" style="width: 2%"></th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%#Eval("NombreProveedor") %> </td>
                    <td><%#Eval("ContactoProveedor") %></td>
                    <td><% if (login.PermisoOperario.NombrePermiso.Equals("Administrador")) { %>
                        <a href="EditarProveedor.aspx?id=<%#Eval("ProveedorId") %>" >
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
