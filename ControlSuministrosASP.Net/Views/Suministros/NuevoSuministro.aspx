<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.Master" AutoEventWireup="true" CodeBehind="NuevoSuministro.aspx.cs"
    Inherits="ControlSuministrosASP.Net.Views.Suministros.NuevoSuministro" UnobtrusiveValidationMode="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_content" runat="server">
    <fieldset>
        <legend>Agregar Suministro</legend>
        <div class="label">Tipo de Suministro</div>
        <asp:RadioButtonList RepeatDirection="Horizontal" ID="selectTipoSuministro" CssClass="select-opt" runat="server" />

        <div class="label">Nombre de Suministro</div>
        <asp:TextBox runat="server" ID="inputNombreSuministro" CssClass="input" placeholder="Nombre del suministro." />

        <div class="label">Descripcion de Suministro</div>
        <asp:TextBox runat="server" ID="inputDescripcionSuministro" CssClass="input" placeholder="Descripcion del suministro." />

        <div class="label">Codigo SAP</div>
        <asp:TextBox runat="server" ID="inputCodigoSAPSuministro" CssClass="input" placeholder="Codigo de suministro en sistema SAP" />

        <div class="label">Unidad</div>
        <asp:RadioButtonList ID="selectUnidad" runat="server" cssclass="select-opt" RepeatDirection="Horizontal" />

        <div class="label">Stock Minimo</div>
        <asp:TextBox runat="server" ID="inputCantidadStockMinimo" CssClass="input" placeholder="Ingrese cantidad" TextMode="Number" />
        <asp:TextBox runat="server" ID="inputFechaVigenteStockMinimo" CssClass="datepicker input" TextMode="Date" placeholder="Ingrese fecha de vigencia" />

        <div class="label">Proveedor</div>
        <asp:DropDownList runat="server" ID="selectProveedor" CssClass="select-opt-menu" Style="width: 97%" />

        <div class="div-boton">
            <asp:LinkButton runat="server" ID="btnRegistrarSuministro" CssClass="boton-input" OnClick="btnRegistrarSuministro_Click">Registrar</asp:LinkButton>
        </div>

        <div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputNombreSuministro" ErrorMessage="No se ingreso nombre de suministro." CssClass="error" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputDescripcionSuministro" ErrorMessage="No se ingreso descripcion de suministro." CssClass="error" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputCodigoSAPSuministro" ErrorMessage="No se ingreso codigo de suministro." CssClass="error" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputCantidadStockMinimo" ErrorMessage="No se ingreso cantidad de stock minimo." CssClass="error" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputFechaVigenteStockMinimo" ErrorMessage="No se ingreso fecha de vigencia de stock minimo de suministro." CssClass="error" />
        </div>
    </fieldset>
</asp:Content>
