<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ListaRegistros.aspx.vb" Inherits="Entrada_Salida.ListaRegistros" %>
<%@ Import Namespace="System.web" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        .card{
            border:groove;
            position:relative;
            left:400px;
            width:59%;
            height:600px
            
        }
        #div2{

        }
        #lboxRegistros{
            border :dotted;            
        }
        #calInicio{
            position: absolute;
            top:50px;
            left: 30%
        }
        #lblCalInicio{
            position:absolute;
            top:15px;
            left: 30%
        }
        #calFin{
            position:absolute;
            top:50px;
            left: 50%
        }
        #lblCalFin{
            position:absolute;
            top:15px;
            left: 50%
        }
        #btnBuscar{
            position:absolute;
            top:35%
            
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/cabeceraSolicitudes.PNG" />
            <div class="card">
                <div id="divFiltros" runat="server">
                    <div id="div2">
                        <asp:Label ID="lblIdRegistro" runat="server" Text="Id Registro"></asp:Label>
                        <asp:TextBox ID="txtIdRegistro" runat="server"></asp:TextBox><br/>
                        <asp:Label ID="lblIdUSuario" runat="server" Text="Id Usuario"></asp:Label>
                        <asp:TextBox ID="txtIdUsuario" runat="server"></asp:TextBox><br />
                        <asp:DropDownList ID="comboTipoSolicitud" runat="server">
                            <asp:ListItem Text="Tipo Solicitud" Value="Tipo Solicitud"></asp:ListItem>
                            <asp:ListItem Value="Jornada Laboral" Text="Jornada Laboral"></asp:ListItem>
                            <asp:ListItem Value="Baja Temporal" Text="Baja Temporal"></asp:ListItem>
                            <asp:ListItem Value="Baja Larga" Text="Baja Larga"></asp:ListItem>
                            <asp:ListItem Value="Vacaciones" Text="Vacaciones"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                <asp:Label ID="lblCalInicio" runat="server" Text="Fecha Inicio"></asp:Label>
                <asp:Calendar ID="calInicio" runat="server" ></asp:Calendar>
                <asp:Label ID="lblCalFin" runat="server" Text="Fecha Fin"></asp:Label>
                <asp:Calendar ID="calFin" runat="server"></asp:Calendar><br/>
                
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />
            </div>
                <div id="divSelect" runat="server" visible="false">

                <asp:ListBox ID="lboxRegistros" runat="server" Rows="20"></asp:ListBox>
            </div>
            </div>
    </form>
</body>
</html>
