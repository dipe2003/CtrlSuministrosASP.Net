<%@ Page MasterPageFile="/Views/Main.Master" Language="C#" AutoEventWireup="true" CodeBehind="ListadoOperarios.aspx.cs" Inherits="ControlSuministrosASP.Net.Views.Operarios.ListadoOperarios" %>

<asp:Content ContentPlaceHolderID="page_content" runat="server">
    <fieldset>
        <legend>Listado Operarios Registrados</legend>
        <% if (login.PermisoOperario.NombrePermiso.Equals("Administrador")) { %>
        <div class="div-boton">
            <asp:LinkButton ID="btnNuevoOperario" runat="server" class="boton-input" OnClick="btnNuevoOperario_Click" >
                            Nuevo
            </asp:LinkButton>
        </div>
         <% } %>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"  />
        <table>
            <tr>
                <td>
                    <div class="label">Filtro: </div>
                </td>
                <td>
                    <asp:TextBox ID="NombreOperarioFiltro" TextMode="SingleLine" OnTextChanged="FiltrarLista" runat="server">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel runat="server" id="panelTablaSuministros" updatemode="Always" >
             <ContentTemplate>
        <asp:Repeater runat="server" ID="TablaOperarios">
            <HeaderTemplate>
                <table>
                    <tr>
                        <th class="label" style="width: 10%">Numero</th>
                        <th class="label" style="width: 50%">Nombre</th>
                        <th class="label" style="width: 20%">Permiso</th>
                        <th class="label" style="width: 15%">Alertas</th>
                        <th class="label" style="width: 2%"></th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td><%#Eval("OperarioId") %></td>
                    <td><%#Eval("NombreOperario") %> <%#Eval("ApellidoOperario") %></td>
                    <td><%#Eval("PermisoOperario.NombrePermiso") %></td>
                    <td><%#Eval("RecibeAlertas") %></td>
                    <td><% if (login.PermisoOperario.NombrePermiso.Equals("Administrador")) { %>
                        <a href="EditarOperario.aspx?id=<%#Eval("OperarioId") %>" >
                        <img height="20" src="/Content/Images/edit_icon.ico" />
                        </a>
                        <%} else { %>
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


