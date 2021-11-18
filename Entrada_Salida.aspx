<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Entrada_Salida.aspx.vb" Inherits="Entrada_Salida.Entrada_Salida" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #form1 {
            height: 80px;
            width: 1260px;
        }
    </style>
</head>
<body>
       
    <form id="form1" runat="server" style="position:relative">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/cabecera.png" Height="89px" Width="1697px" />
        <div id="saludo" visible="false" runat="server" style="left: 500px; top:300px; background-color:ghostwhite; position:absolute; z-index:2;height: 105px; width: 400px; border-style:solid">
                    <asp:Label ID="Label1" runat="server" Text="¡Hola! Deberías registrar tu entrada pulsando el botón de 'Entrar'"></asp:Label>
                <br />
                    <asp:Button ID="btnCerrarSaludo" style="position:center" runat="server" Text="Cerrar"/>
        </div>
        <div id="horaEntrada" visible="false" runat="server" style="left: 500px; top:300px;background-color:ghostwhite; position:absolute; z-index:2; height: 105px; width: 400px; border-style:solid">
                    <asp:Label ID="lblHoraEntrada" runat="server"></asp:Label>
            <br />
                    <asp:Button ID="btnCerrarHoraEntrada" runat="server" Text="Cerrar" />
        </div>
        <div id="tiempoTrabajado" visible="false" runat="server" style="left: 500px; top:300px;background-color:ghostwhite; position:absolute; z-index:2;height: 105px; width: 400px; border-style:solid">
             <asp:Label ID="lblTiempoTrabajado" runat="server"></asp:Label>
                    <br />
                    <asp:Button ID="btnCancelarSalida" runat="server" Text="Cancelar" />
                    <asp:Button ID="btnConfirmarSalida" runat="server" Text="Confirmar" />
        </div>
        <div style="width:135%; background-color:gainsboro; height: 755px;z-index:1">
           
            <asp:Table ID="Table1" runat="server" Width="1669px" >
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
                        <asp:Label ID="lblEntrada" runat="server" Text="Entrar"/>   
                            <asp:ImageButton ID="Entrada" runat="server" Height="40px" ImageUrl="~/Resources/entrar.png" Width="40px" data-toggle="tooltip" data-placement="bottom" title="Botón para registrar entrada"  />
                     
                        <asp:Label ID="lblSalida" runat="server" Text="Salir" Visible="false"/>
                            <asp:ImageButton ID="btnSalida" runat="server" ImageUrl="~/Resources/salir.jpg" Height="40px" Width="40px" Visible="False"  data-toggle="tooltip" data-placement="bottom" title="Botón para registrar salida" />
                        
                    </asp:TableCell></asp:TableRow><asp:TableRow>
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
                    </asp:TableCell></asp:TableRow><asp:TableRow>
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
                    </asp:TableCell></asp:TableRow><asp:TableRow>
                    <asp:TableCell>
                        <asp:ImageButton ID="ImageButton19" runat="server" ImageUrl="~/Resources/obrasproperty.PNG"  />
                    </asp:TableCell></asp:TableRow></asp:Table></div><asp:HiddenField ID="Hidden" runat="server" />
    </form>
</body>
</html>
