<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.Master" AutoEventWireup="true" CodeBehind="NuevoProveedor.aspx.cs" 
    Inherits="ControlSuministrosASP.Net.Views.Proveedores.NuevoProveedor" UnobtrusiveValidationMode="None" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_content" runat="server">
    <fieldset>
        <legend>Agregar Proveedor</legend>
        <div class="label">Nombre de Proveedor</div>
        <asp:TextBox runat="server" ID="inputNombreProveedor" class="input" placeholder="Nombre del proveedor" />
        <div class="label">Contacto</div>
        <asp:TextBox runat="server" ID="inputContactoProveedor" class="input" placeholder="Datos de contacto con el proveedor" />

        <div class="div-boton">
            <asp:LinkButton runat="server" ID="btnRegistrarProveedor" class="boton-input" OnClick="btnRegistrarProveedor_Click" >Registrar</asp:LinkButton>
        </div>

        <div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputNombreProveedor" class="error" ErrorMessage="No se ingreso Nombre." />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputContactoProveedor" class="error" ErrorMessage="No se ingreso contacto" />
        </div>
    </fieldset>
</asp:Content>
