<%@ Page MasterPageFile="/Views/Main.Master" Language="C#" AutoEventWireup="true" CodeBehind="EditarOperario.aspx.cs" Inherits="ControlSuministrosASP.Net.Views.Operarios.EditarOperario" UnobtrusiveValidationMode="None" %>

<asp:Content ID="contentEditarOperario" ContentPlaceHolderID="page_content" runat="server">
    <fieldset>
        <legend>Editar Operario</legend>
        <div class="label">Nombre de Operario</div>
        <asp:TextBox runat="server" ID="editNombreOperario" class="input" placeholder="Nombre del operario"/>

        <div class="label">Apellido de Operario</div>
        <asp:TextBox runat="server" ID="editApellidoOperario" class="input" placeholder="Apellido del operario"/>

        <div class="label">Contraseña Actual</div>
        <asp:TextBox TextMode="Password" runat="server" ID="editPassActualOperario" class="input" placeholder="Contraseña Actual (dejar vacio para no cambiar)" />

        <div class="label">Correo de Operario</div>
        <asp:TextBox runat="server" ID="editCorreoOperario" class="input" placeholder="Correo del operario" />

        <div class="label">Contraseña Nueva</div>
        <asp:TextBox runat="server" TextMode="Password" ID="editPassNuevoOperario" class="input" placeholder="Contraseña Nueva (dejar vacio para no cambiar)" />

        <div class="label">Confirmar Contraseña</div>
        <asp:TextBox runat="server" TextMode="Password" ID="editRePassOperario" class="input" placeholder="Repeticion de contraseña (debe coincidir con la contraseña)"/>
        
        <div class="label">Alertas</div>
        <asp:CheckBox Text="Recibe Alertas" id="editAlertas" runat="server" />

        <div class="label">Permiso del Operario</div>
        <asp:RadioButtonList runat="server" ID="editPermisoOperario" RepeatDirection="Horizontal" Enabled="true" ToolTip="Solo disponible para Administrador" />
        
        <div class="div-boton">
            <asp:LinkButton runat="server" ID="btnEditarOperario" class="boton-input" Text="Guardar" OnClick="btnEditarOperario_Click">
            </asp:LinkButton>
        </div>
            <div>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="editNombreOperario" ErrorMessage="No se ingreso nombre de operario." CssClass="error"  />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="editApellidoOperario" ErrorMessage="No se ingreso apellido de operario." class="error" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="editCorreoOperario" ErrorMessage="No se ingreso correo del operario" class="error" />
                <asp:CompareValidator runat="server" ControlToValidate="editRePassOperario" ControlToCompare="editPassNuevoOperario" Type="String" ErrorMessage="Las contraseñas no coinciden." />
                <asp:CustomValidator runat="server" OnServerValidate="validarPassActual" ErrorMessage="La conraseña actual no es correcta" ControlToValidate="editPassActualOperario" />
            </div>
    </fieldset>
</asp:Content>
