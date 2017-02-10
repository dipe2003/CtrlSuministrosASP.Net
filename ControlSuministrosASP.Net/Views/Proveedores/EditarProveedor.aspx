<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.Master" AutoEventWireup="true" CodeBehind="EditarProveedor.aspx.cs" 
    Inherits="ControlSuministrosASP.Net.Views.Proveedores.EditarProveedor" UnobtrusiveValidationMode="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_content" runat="server">
    <fieldset>
        <legend>Editar Proveedor</legend>
        <div class="label">Nombre de Proveedor</div>
        <asp:TextBox runat="server" ID="editNombreProveedor" class="input" placeholder="Nombre del proveedor" />
        <div class="label">Contacto</div>
        <asp:TextBox runat="server" ID="editContactoProveedor" class="input" placeholder="Datos de contacto con el proveedor" />

        <div class="div-boton">
            <asp:LinkButton runat="server" ID="btnEditarProveedor" class="boton-input" OnClick="btnEditarProveedor_Click">Guardar</asp:LinkButton>
        </div>

        <div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="editNombreProveedor" class="error" ErrorMessage="No se ingreso Nombre." />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="editContactoProveedor" class="error" ErrorMessage="No se ingreso contacto" />
        </div>
    </fieldset>
</asp:Content>
