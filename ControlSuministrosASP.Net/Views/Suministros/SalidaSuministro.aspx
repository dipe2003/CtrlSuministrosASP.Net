<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.Master" AutoEventWireup="true" CodeBehind="SalidaSuministro.aspx.cs" Inherits="ControlSuministrosASP.Net.Views.Suministros.SalidaSuministro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_content" runat="server">
    <fieldset>
        <legend>Salida de Suministros</legend>
        <fieldset>
            <legend>Suministro</legend>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
            <div class="label">Suministro</div>
            <asp:DropDownList ID="selectSuministroSalida" CssClass="select-opt-menu" runat="server" AutoPostBack="true" OnSelectedIndexChanged="selectSuministroSalida_SelectedIndexChanged">
            </asp:DropDownList>

            <div class="label">Proveedor</div>
            <asp:UpdatePanel runat="server" ID="panelProveedor" UpdateMode="Always">
                <ContentTemplate>
                    <div style="text-align: left;">
                        <asp:TextBox ID="selectProveedorSalida" CssClass="output" TextMode="SingleLine" ReadOnly="true" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>

        <fieldset>
            <legend>Lote</legend>
            <div class="label">Numeros de Lote</div>
            <asp:UpdatePanel runat="server" ID="panelNumeroLote" UpdateMode="Always">
                <ContentTemplate>
                    <asp:DropDownList runat="server" ID="selectNumeroLoteSuministro" CssClass="select-opt-menu" AutoPostBack="true" OnSelectedIndexChanged="selectNumeroLoteSuministro_SelectedIndexChanged">
                        <asp:ListItem Text="-- Selecciona Suministro --" Selected="True" />
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="label">Cantidad de Salida</div>
            <asp:UpdatePanel ID="panelCantidad" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div style="text-align: left;">
                        <asp:TextBox runat="server" ID="inputCantidadSalidaSuministro" Style="width: 50%" CssClass="input" placeholder="Cantidad de salida" TextMode="Number" AutoPostBack="true" OnTextChanged="inputCantidadSalidaSuministro_TextChanged" />
                        <asp:TextBox ReadOnly="true" runat="server" ID="outputUnidadSalidaSuministro" Style="width: 20%" CssClass="output" />
                    </div>
                    <div class="label">Stock Actual</div>
                    <div style="text-align: left;">
                        <asp:TextBox runat="server" ID="outputStockActual" CssClass="output" Style="width: 25%" />
                    </div>
                    <div class="label">Nuevo Stock:</div>
                    <div style="text-align: left;">
                        <asp:TextBox runat="server" ID="outputNuevoStock" CssClass="output" Style="width: 25%" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            <div class="label">Observaciones</div>
            <asp:TextBox runat="server" ID="inputObservacionesSalidaSuministro" CssClass="input" placeholder="Observaciones para esta salida" TextMode="MultiLine" />

        </fieldset>

        <div class="div-boton">
            <asp:LinkButton runat="server" ID="btnRegistrarSalidaSuministro" class="boton-input" OnClick="btnRegistrarSalidaSuministro_Click">
                Registrar
            </asp:LinkButton>
        </div>

        <div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputCantidadSalidaSuministro" ErrorMessage="Debe ingresar cantidad." CssClass="error" />
            <asp:CustomValidator runat="server" ControlToValidate="inputCantidadSalidaSuministro" ErrorMessage="La cantidad ingresada para dar de baja no es correcta." CssClass="error" OnServerValidate="cantidad_ServerValidate" Display="Dynamic" />
        </div>

    </fieldset>
</asp:Content>
