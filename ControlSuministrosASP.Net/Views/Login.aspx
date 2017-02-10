<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ControlSuministrosASP.Net.Views.Login" UnobtrusiveValidationMode="None"  %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="/Content/CSS/Base.css" />
    <link rel="stylesheet" href="/Content/CSS/login.css" />
    <link rel="icon" href="/Content/Images/favicon.ico" />
    <title>Login: Control de Suministros</title>
</head>
<body>
    <form id="login" runat="server">
        <div style="text-align: left;">
            <div>                
                <asp:RequiredFieldValidator ID="mensajeNumero" runat="server" ErrorMessage="Debe ingresar un numero de operario" ControlToValidate="login:inputNumOperario" CssClass="error"></asp:RequiredFieldValidator>                
                <asp:RequiredFieldValidator ID="mensajePass" runat="server" ErrorMessage="Debe ingresar una contraseña" ControlToValidate="login:inputPass" CssClass="error"></asp:RequiredFieldValidator>                
            </div>
        </div>
        <div id="linea" class="fondo-azul">
            <div id="titulo" class="fondo-azul">
                <img height="80" src="../Content/Images/lock_fixed.png" /><br />
                <b>Ingresar</b>
            </div>
            <div id="contenedor">                
                <asp:TextBox runat="server" ID="inputNumOperario" class="Texto" placeholder="Numero de Operario" required="required"  /><br />
                <asp:TextBox runat="server" ID="inputPass" class="Texto" placeholder="Password" required="required" TextMode="Password" /><br />
                <asp:Button runat="server" id="btnLogin" class="boton-input" style="width: 30%;height: 50px;" Text="Ingresar" OnClick="btnLogin_Click"  />
            </div>
        </div>        
    </form>
</body>
</html>
