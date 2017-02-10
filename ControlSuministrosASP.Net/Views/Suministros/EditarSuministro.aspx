<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.Master" AutoEventWireup="true" CodeBehind="EditarSuministro.aspx.cs" Inherits="ControlSuministrosASP.Net.Views.Suministros.EditarSuministro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_content" runat="server">
    <fieldset>
        <legend>Agregar Suministro</legend>
        <div class="label">Tipo de Suministro</div>
        <asp:RadioButtonList RepeatDirection="Horizontal" ID="selectTipoSuministro" CssClass="select-opt" runat="server" Enabled="false" />

        <div class="label">Nombre de Suministro</div>
        <asp:TextBox runat="server" ID="editNombreSuministro" CssClass="input" placeholder="Nombre del suministro." />

        <div class="label">Descripcion de Suministro</div>
        <asp:TextBox runat="server" ID="editDescripcionSuministro" CssClass="input" placeholder="Descripcion del suministro." />

        <div class="label">Codigo SAP</div>
        <asp:TextBox runat="server" ID="editCodigoSAPSuministro" CssClass="input" placeholder="Codigo de suministro en sistema SAP" />

        <div class="label">Unidad</div>
        <asp:RadioButtonList ID="selectUnidad" runat="server" cssclass="select-opt" RepeatDirection="Horizontal" />

        <div class="label">Stock Minimo</div>
        <asp:TextBox runat="server" ID="editCantidadStockMinimo" CssClass="input" placeholder="Ingrese cantidad" TextMode="Number" />

        <div class="label">Proveedor</div>
        <asp:DropDownList runat="server" ID="selectProveedor" CssClass="select-opt-menu" Style="width: 97%" />

        <div class="label">Vigencia</div>
        <asp:CheckBox TextAlign="Left" Text="En  Uso" ID="checkVigente" runat="server" />

        <div class="div-boton">
            <asp:LinkButton runat="server" ID="btnGuardarSuministro" CssClass="boton-input" onClick="btnGuardarSuministro_Click">Guardar</asp:LinkButton>
        </div>

        <div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="editNombreSuministro" ErrorMessage="No se ingreso nombre de suministro." CssClass="error" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="editDescripcionSuministro" ErrorMessage="No se ingreso descripcion de suministro." CssClass="error" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="editCodigoSAPSuministro" ErrorMessage="No se ingreso codigo de suministro." CssClass="error" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="editCantidadStockMinimo" ErrorMessage="No se ingreso cantidad de stock minimo." CssClass="error" />            
        </div>
    </fieldset>
</asp:Content>
