<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Entrada_Salida.aspx.vb" Inherits="Entrada_Salida.Entrada_Salida" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">       
    </style>
    <link href="CssEntradaSalida.css" rel="stylesheet" />
</head>
<body style="background-color:gainsboro">
       
    <form id="form1" runat="server" style="">
        <asp:ImageButton ID="btnCabecera" runat="server" ImageUrl="~/Resources/cabecera.png" Height="89px" Width="1913"/>
        
        <div id="divSaludo" visible="false" runat="server" class="">
                    <asp:Label ID="lblSaludo" runat="server" Text="¡Hola! Deberías registrar tu entrada pulsando el botón de 'Entrar'"></asp:Label>
                <br />
                    <asp:ImageButton ID="btnCerrarSaludo" ImageUrl="~/Resources/btnCerrar.PNG" runat="server" />
        </div>
        <div id="divHoraEntrada" visible="false" runat="server">
                    <asp:Label ID="lblHoraEntrada" runat="server"></asp:Label>
            <br />
                    <asp:ImageButton ID="btnCerrarHoraEntrada" ImageUrl="~/Resources/btnCerrar.PNG" runat="server"/>
        </div>
        <div id="divTiempoTrabajado" visible="false" runat="server">
             <asp:Label ID="lblTiempoTrabajado" runat="server"></asp:Label>
                    <br />
                    <asp:Button ID="btnCancelarSalida" runat="server" Text="Cancelar" />
                    <asp:Button ID="btnConfirmarSalida" runat="server" Text="Confirmar" />
        </div>
        <div id="divTabla">           
            <asp:Table ID="Table1" runat="server" Width="1280px" >
                <asp:TableHeaderRow>
                    <asp:TableCell ColumnSpan="7">
                        <asp:ImageButton ID="ImageButton20" runat="server" ImageUrl="~/Resources/ayuda.PNG" ImageAlign="Right" Height="60" Width="60" />
                    </asp:TableCell>
                </asp:TableHeaderRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Resources/centros.PNG" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Resources/realestate.PNG"  />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/Resources/espacios.PNG" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/Resources/licencias.PNG" />
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/Resources/paqueterias.PNG"/>
                    </asp:TableCell>
                    <asp:TableCell>
                        <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/Resources/controleconomico.PNG"  />
                    </asp:TableCell>
                    <asp:TableCell>                        
                            <asp:ImageButton ID="Entrada" runat="server" ImageUrl="~/Resources/BotonEntada.PNG" data-toggle="tooltip" data-placement="bottom" title="Botón para registrar entrada"  />
                            <asp:ImageButton ID="btnSalida" runat="server" ImageUrl="~/Resources/BotonSalida.PNG" Visible="False"  data-toggle="tooltip" data-placement="bottom" title="Botón para registrar salida" />                        
                    </asp:TableCell>                    
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/Resources/flota.PNG"  />
                    </asp:TableCell><asp:TableCell>
                        <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/Resources/mantenimiento.PNG"  />
                    </asp:TableCell><asp:TableCell>
                        <asp:ImageButton ID="ImageButton9" runat="server" ImageUrl="~/Resources/auditorias.PNG"  />
                    </asp:TableCell><asp:TableCell>
                        <asp:ImageButton ID="ImageButton10" runat="server" ImageUrl="~/Resources/obrastiendas.PNG" />
                    </asp:TableCell><asp:TableCell>
                        <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="~/Resources/parking.PNG"  />
                    </asp:TableCell><asp:TableCell>
                        <asp:ImageButton ID="ImageButton12" runat="server" ImageUrl="~/Resources/siniestros.PNG" />
                    </asp:TableCell>
                    <asp:TableCell><asp:ImageButton ID="btnBuscar" runat="server"  ImageUrl="~/Resources/buscar.png"/></asp:TableCell>
                               </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:ImageButton ID="ImageButton13" runat="server" ImageUrl="~/Resources/healthsafety.PNG" />
                    </asp:TableCell><asp:TableCell>
                        <asp:ImageButton ID="ImageButton14" runat="server" ImageUrl="~/Resources/doccompartidos.PNG" />
                    </asp:TableCell><asp:TableCell>
                        <asp:ImageButton ID="ImageButton15" runat="server" ImageUrl="~/Resources/informes.PNG" />
                    </asp:TableCell><asp:TableCell>
                        <asp:ImageButton ID="ImageButton16" runat="server" ImageUrl="~/Resources/sekom.PNG"/>
                    </asp:TableCell><asp:TableCell>
                        <asp:ImageButton ID="ImageButton17" runat="server" ImageUrl="~/Resources/opcos.PNG"/>
                    </asp:TableCell><asp:TableCell>
                        <asp:ImageButton ID="ImageButton18" runat="server" ImageUrl="~/Resources/serviciosaux.PNG"  />
                    </asp:TableCell></asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:ImageButton ID="ImageButton19" runat="server" ImageUrl="~/Resources/obrasproperty.PNG"  />
                    </asp:TableCell>
                    </asp:TableRow>
              </asp:Table>
        </div>
        <asp:HiddenField ID="Hidden" runat="server" />
    </form>
</body>
</html>
