<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.Master" AutoEventWireup="true" CodeBehind="NuevoOperario.aspx.cs"
    Inherits="ControlSuministrosASP.Net.Views.Operarios.NuevoOperario" UnobtrusiveValidationMode="None" %>

<asp:Content ID="contentNuevoOperario" ContentPlaceHolderID="page_content" runat="server">
    <fieldset>
        <legend>Nuevo Operario</legend>
        <div class="label">NUmero de Operario</div>
        <asp:TextBox runat="server" ID="inputNumeroOperario" class="input" required="true" placeholder="Numero del operario" />

        <div class="label">Nombre de Operario</div>
        <asp:TextBox runat="server" ID="inputNombreOperario" class="input" required="true" placeholder="Nombre del operario" />

        <div class="label">Apellido de Operario</div>
        <asp:TextBox runat="server" ID="inputApellidoOperario" class="input" placeholder="Apellido del operario" />

        <div class="label">Correo de Operario</div>
        <asp:TextBox runat="server" ID="inputCorreoOperario" class="input" placeholder="Correo del operario" />

        <div class="label">Contraseña Nueva</div>
        <asp:TextBox runat="server" TextMode="Password" ID="inputPassNuevoOperario" class="input" placeholder="Contraseña Nueva (dejar vacio para no cambiar)" />

        <div class="label">Confirmar Contraseña</div>
        <asp:TextBox runat="server" TextMode="Password" ID="inputRePassOperario" class="input" placeholder="Repeticion de contraseña (debe coincidir con la contraseña)" />

        <div class="label">Permiso del Operario</div>
        <asp:RadioButtonList runat="server" ID="inputPermisoOperario" RepeatDirection="Horizontal" >
        </asp:RadioButtonList>
        <div class="div-boton">
            <asp:LinkButton runat="server" ID="btnRegOperario" class="boton-input" Text="Guardar" OnClick="btnRegOperario_Click">
            </asp:LinkButton>
        </div>
        <div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputNombreOperario" ErrorMessage="No se ingreso nombre de operario." CssClass="error" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputApellidoOperario" ErrorMessage="No se ingreso apellido de operario." class="error" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputCorreoOperario" ErrorMessage="No se ingreso correo del operario" class="error" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputPassNuevoOperario" ErrorMessage="No se ingreso contraseña del operario" class="error" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputRePassOperario" ErrorMessage="No se ingreso contraseña del operario" class="error" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="inputPermisoOperario" ErrorMessage="No se selecciono permiso." class="error" />
            <asp:CompareValidator runat="server" ControlToValidate="inputRePassOperario" ControlToCompare="inputPassNuevoOperario" Type="String" ErrorMessage="Las contraseñas no coinciden." />
        </div>
    </fieldset>
</asp:Content>
