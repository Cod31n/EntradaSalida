<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PerfilUsuario.aspx.vb" Inherits="Entrada_Salida.Baja" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        #btnSolicitudes {
            position: absolute;
            top: 185px;
            left: 580px;
            
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >
        
            
        
        <div style="position:relative">
            <asp:Button ID="btnSolicitudes" runat="server" Text="SOLICITUDES" Height="44px" Width="190px" />
            <asp:ImageButton ID="btnVolver" runat="server" style="position:absolute; margin-left:76.6%; margin-top:5%" ImageUrl="~/Resources/volver.PNG" />
            <asp:Image ID="perfilUSu" runat="server" ImageUrl="~/Resources/perfilUsuario.PNG"/>
        </div>
        
    </form>
</body>
</html>
