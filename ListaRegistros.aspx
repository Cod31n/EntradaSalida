<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ListaRegistros.aspx.vb" Inherits="Entrada_Salida.ListaRegistros" %>
<%@ Import Namespace="System.web" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>        
    </style>
    <link href="CssListaRegistros.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/cabeceraSolicitudes.PNG" />
        <asp:ImageButton ID="btnVolver" runat="server" ImageUrl="~/Resources/volver.PNG" />
            <div class="card">
                <div id="card-body">
                    <div id="divTipoResultado" runat="server">
                        <asp:DropDownList ID="drpTipoResultado" runat="server" AutoPostBack="true">
                        <asp:ListItem Text="Tipo de resultado"></asp:ListItem>
                        <asp:ListItem Text="Tabla" Value="tabla" tooltip="El resultado se muestra como una tabla"></asp:ListItem>
                        <asp:ListItem Text="Lista" Value="lista" tooltip="El resultado se muestra como una lista"></asp:ListItem>
                        <asp:ListItem Text="Detalle" Value="detalle" tooltip="Se muestran los detalles del registro encontrado"></asp:ListItem>
                    </asp:DropDownList>
                    </div>
                    <div id="divBusquedaId" runat="server">
                            <asp:Label ID="lblDivId" runat="server" Text="Busca o elimina un registro"></asp:Label>
                            <asp:Label ID="lblIdRegistro" runat="server" Text="Id Registro"></asp:Label>
                            <asp:TextBox ID="txtIdRegistro" runat="server" Enabled="false"></asp:TextBox><br/><br/>
                            <asp:ImageButton ID="btnBuscarDetalle" Enabled="false" ImageUrl="~/Resources/btnbuscar.PNG" runat="server" Text="Buscar por ID" />
                            <asp:ImageButton ID="btnEliminarRegistro" Enabled="false" ImageUrl="~/Resources/btnEliminar.PNG" runat="server" />
                        </div>
                    <div id="divFiltros" runat="server">                    
                        <div id="div2">
                        
                                <asp:Label ID="lblIdUsuario" runat="server" Text="Id Usuario"></asp:Label>
                                <asp:TextBox ID="txtIdUsuario" Enabled="false" runat="server"></asp:TextBox><br/><br/>
                        
                            <asp:DropDownList ID="comboTipoSolicitud" Enabled="false" runat="server">
                                <asp:ListItem Text="Tipo Solicitud" Value="Tipo Solicitud"></asp:ListItem>
                                <asp:ListItem Value="Jornada Laboral" Text="Jornada Laboral"></asp:ListItem>
                                <asp:ListItem Value="Baja Temporal" Text="Baja Temporal"></asp:ListItem>
                                <asp:ListItem Value="Baja Larga" Text="Baja Larga"></asp:ListItem>
                                <asp:ListItem Value="Vacaciones" Text="Vacaciones"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <asp:Label ID="lblCalInicio" runat="server" Text="Fecha Inicio"></asp:Label>
                        <asp:Calendar ID="calInicio" runat="server" Enabled="false" ></asp:Calendar>
                        <asp:Label ID="lblCalFin" runat="server" Enabled="false" Text="Fecha Fin"></asp:Label>
                        <asp:Calendar ID="calFin" runat="server"></asp:Calendar><br/>
                        <div id="divBotones" runat="server">
                            <asp:ImageButton ID="btnResetCalendarios" ImageUrl="~/Resources/resetearDef.PNG" runat="server" Enabled="false" ToolTip="Resetear Calendarios" />
                            <asp:ImageButton ID="btnBuscar" ImageUrl="~/Resources/btnbuscar.PNG" Enabled="false" runat="server"/>
                        
                        
                            
                        </div>
                    </div>
                
                    <div id="divSelect" runat="server" visible="false">
                    <%--<asp:ListBox ID="lboxRegistros" runat="server" Rows="20"></asp:ListBox>--%>
                        <asp:Table ID="tblRegistros" runat="server" Height="100px" GridLines="Horizontal" BorderWidth="2" CellPadding="1"></asp:Table>
                    </div>
                    <div id="divSelectList" runat="server" visible="false">
                        <asp:BulletedList ID="listRegistros" runat="server"></asp:BulletedList>
                    </div>
                    <div id="divSelectId" runat="server" visible="false">
                        <asp:Label ID="lblIdUsu" runat="server" Text="Id del usuario: "></asp:Label><br/>
                        <asp:Label ID="lblTipoSol" runat="server" Text="Tipo solicitud realizada: "></asp:Label><br/>
                        <asp:Label ID="lblEstadoSol" runat="server" Text="Estado solicitud: "></asp:Label><br/>
                        <asp:Label ID="lblFechaIni" runat="server" Text="Fecha inicio: "></asp:Label><br/>
                        <asp:Label ID="lblFechaFin" runat="server" Text="Fecha fin: "></asp:Label><br/>
                    </div>
                    <div class="card" id="divError" runat="server" visible="false">
                        <asp:Label ID="lblError" runat="server" Text="No se ha encontrado ningún resultado"></asp:Label><br/>
                        <asp:ImageButton ID="btnCerrarError" ImageUrl="~/Resources/cerrarSmall.PNG" runat="server" />
                    </div>
                </div>
            </div>
    </form>
</body>
</html>
