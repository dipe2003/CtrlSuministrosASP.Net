<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Main.Master" AutoEventWireup="true" CodeBehind="InfoSuministro.aspx.cs" Inherits="ControlSuministrosASP.Net.Views.Suministros.InfoSuministro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="page_content" runat="server">
    <fieldset>
        <legend>Detalles de Movimientos del Suministro</legend>
        <div class="label">Descripcion</div>
        <div style="text-align: left;">
            <asp:TextBox ID="descripcionSuministro" class="output" runat="server" />
        </div>
        <br />
        <div class="label">Codigo SAP</div>
        <div style="text-align: left;">
            <asp:TextBox ID="codigoSAPSuministro" runat="server" class="output" />
        </div>
        <br />
        <div class="contenedor-5050" style="margin-left: -2%;">
            <div class="contenedor-50" style="margin-left: -1%;">
                <div class="label">Vigencia</div>
                <div style="text-align: left; margin-left: 1%;">
                    <asp:TextBox ID="vigenciaSuministro" runat="server" class="output" />
                </div>
            </div>
            <div class="contenedor-50" style="margin-left: -1%;">
                <div class="label">Stock Actual</div>
                <div style="text-align: left; margin-left: 1%;">
                    <%if(suministroSeleccionado.DebajoStockMinimo) { %>
                    <div class='output alerta'>Debajo Stock Minimo</div>
                    <% } else { %>
                    <div>Con Stock</div>
                    <% } %>
                </div>
            </div>
        </div>
        <fieldset>
            <legend>Movimientos de Stock</legend>
            <div class="contenedor-5050">
                <div class="contenedor-60">
                    <div class="label" style="width: 99%; margin-left: 0.5%;">Ingresos</div>
                    <table class="tabla-infoSuministro">
                        <tr>
                            <th class="label" style="width: 30%">Lote</th>
                            <th class="label" style="width: 10%">Fecha</th>
                            <th class="label" style="width: 10%">Cantidad</th>
                            <th class="label" style="width: 20%">Vencimiento</th>
                            <th class="label" style="width: 20%">Operario</th>
                        </tr>

                        <% foreach(var lote in suministroSeleccionado.LotesSuministros) {
                                foreach(var ingreso in lote.IngresosLote) { %>
                        <tr>
                            <td><%= lote.NumeroLote %> </td>
                            <td><%= ingreso.FechaIngreso.Value.ToShortDateString() %> </td>
                            <td><%= ingreso.CantidadIngreso %> </td>
                            <% if(lote.EstaVencido() && lote.getCantidadStock() > 0) { %>
                            <td class="div-output" style="background-color: red; color: whitesmoke;">
                                <div><%= lote.VencimientoLote.Value.ToShortDateString() %></div>
                            </td>
                            <% } else { %>
                            <td class="div-output">
                                <div><%= lote.VencimientoLote.Value.ToShortDateString() %></div>
                            </td>
                            <% } %>
                            <td><%= ingreso.OperarioIngresoSuministro.OperarioId  %> </td>
                            <%  } %>
                        </tr>
                        <tr style="border-top: 1px !important; background-color: azure">
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Stock Lote:</td>
                            <%if(lote.EstaVencido() && lote.getCantidadStock() > 0) { %>
                            <td class="div-output" style="background-color: red; color: whitesmoke;">
                                <div><%= lote.getCantidadStock() %></div>
                            </td>
                            <% } else { %>
                            <td class="div-output">
                                <div><%= lote.getCantidadStock() %></div>
                            </td>
                            <% }%>
                        </tr>
                        <%   } %>
                    </table>
                </div>
                <div class="contenedor-40">
                    <div class="label" style="width: 99%; margin-left: 0.5%;">Salidas</div>
                    <table class="tabla-infoSuministro">
                        <tr>
                            <th class="label" style="width: 30%">Lote</th>
                            <th class="label" style="width: 10%">Cantidad</th>
                            <th class="label" style="width: 20%">Operario</th>
                        </tr>
                        <% foreach(var lote in suministroSeleccionado.LotesSuministros) {
                                foreach(var salida in lote.SalidasLote) { %>
                        <tr>
                            <td class="div-output">
                                <div><%= lote.NumeroLote %></div>
                            </td>
                            <td class="div-output">
                                <div><%= salida.CantidadSalida %></div>
                            </td>
                            <td class="div-output">
                                <div><%= salida.OperarioSalidaSuministro.OperarioId %></div>
                            </td>
                        </tr>
                              <%  }
                            } %>
                    </table>
                </div>
            </div>
        </fieldset>
    </fieldset>
</asp:Content>
