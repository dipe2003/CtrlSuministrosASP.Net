<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Error500.aspx.cs" Inherits="ControlSuministrosASP.Net.Views.Errores.Error500" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
        <link rel="stylesheet" href="/Content/CSS/base.css"/>
        <link rel="stylesheet" href="/Content/CSS/login.css" />
        <link rel="shortcut icon" type="image/x-icon" href="/Content/Images/error_icon.ico"/>
        <title>Error 500</title>
    </head>
    <body>           
            <div id="linea" class="fondo-azul">
                <div id="titulo" class="fondo-azul">
                    <img height="80" src="/Content/Images/error.png"/><br/>
                    <b>No Autorizado</b>
                </div>
                <div id="contenedor" >
                    <label class="output" >Error 500</label>
                    <label class="output">No tiene permiso para acceder a esta página.</label>                  
                </div>
            </div>
        <meta http-equiv="refresh" content="10;url=/Views/Index.aspx"/>
    </body>
</html>
