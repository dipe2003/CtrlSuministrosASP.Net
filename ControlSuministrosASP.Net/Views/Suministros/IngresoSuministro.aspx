<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.Master" AutoEventWireup="true" CodeBehind="IngresoSuministro.aspx.cs"
    Inherits="ControlSuministrosASP.Net.Views.Suministros.IngresoSuministro" UnobtrusiveValidationMode="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Nuevo Lote</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_content" runat="server">
    <fieldset>
        <legend>Ingreso de Suministros</legend>
        <fieldset>
            <legend>Suministro</legend>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
            <div class="label">Suministro</div>
            <asp:DropDownList ID="selectSuministroIngreso" CssClass="select-opt-menu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="selectSuministroIngreso_SelectedIndexChanged">
            </asp:DropDownList>

            <div class="label">Proveedor</div>
            <asp:UpdatePanel runat="server" ID="panelProveedor" UpdateMode="Always">
                <ContentTemplate>
                    <div style="text-align: left;">
                        <asp:TextBox ID="selectProveedorIngreso" CssClass="output" TextMode="SingleLine" ReadOnly="true" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>

        <fieldset>
            <legend>Lote</legend>
            <div class="label">Numeros de Lote</div>
            <asp:UpdatePanel runat="server" ID="panelNumeroLote" UpdateMode="Always">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="selectNumeroLoteSuministro" AutoPostBack="true" CssClass="select-opt-menu" OnSelectedIndexChanged="selectNumeroLoteSuministro_SelectedIndexChanged">
                        <asp:ListItem Text="-- Selecciona Suministro --" Selected="True" />
                    </asp:DropDownList>
                    <asp:TextBox runat="server" ID="inputNumeroLoteSuministro" CssClass="input" TextMode="SingleLine" Visible="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="label">Cantidad de Ingreso</div>
            <asp:UpdatePanel ID="panelCantidad" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div style="text-align: left;">
                        <asp:TextBox runat="server" ID="inputCantidadIngresoSuministro" Style="width: 50%" CssClass="input" placeholder="Cantidad a ingresar" TextMode="Number" />
                        <asp:TextBox ReadOnly="true" runat="server" ID="outputUnidadIngresoSuministro" Style="width: 20%" CssClass="output" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="label">Factura</div>
            <asp:TextBox runat="server" ID="inputFacturaIngresoSuministro" CssClass="input" placeholder="Numero de Factura" />

            <div class="label">Fecha de Vencimiento</div>
            <asp:UpdatePanel ID="panelFechaVencimiento" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div style="text-align: left;">
                        <asp:CheckBox ID="checkPerecedero" runat="server" Style="margin-left: 2px;" AutoPostBack="true" Text="Perecedero" OnCheckedChanged="selectCalendar_SelectedIndexChanged" />
                        <asp:TextBox ID="inputFechaVencimiento" runat="server" TextMode="Date" Style="width: 50%" CssClass="input" Enabled="false" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>


            <div class="label">Observaciones</div>
            <asp:TextBox runat="server" ID="inputObservacionesIngresoSuministro" CssClass="input" placeholder="Observaciones para este ingreso" TextMode="MultiLine" />

        </fieldset>

        <div class="div-boton">
            <asp:LinkButton runat="server" ID="btnRegistrarIngresoSuministro" class="boton-input" OnClick="btnRegistrarIngresoSuministro_Click">
                Registrar
            </asp:LinkButton>
        </div>

        <div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputCantidadIngresoSuministro" ErrorMessage="Debe ingresar cantidad." CssClass="error" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputFacturaIngresoSuministro" ErrorMessage="Debe ingresar numero de factura." CssClass="error" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputNumeroLoteSuministro" ErrorMessage="Debe ingresar numero de lote." class="error" />
            <asp:CustomValidator runat="server" ControlToValidate="inputCantidadIngresoSuministro" ErrorMessage="La cantidad ingresada no es correcta." OnServerValidate="cantidad_ServerValidate" Display="Dynamic" />
        </div>

    </fieldset>
</asp:Content>
