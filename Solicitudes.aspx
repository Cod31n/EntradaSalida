<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Solicitudes.aspx.vb" Inherits="Entrada_Salida.Baja1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>        
    </style>
    <link href="CssSolicitudes.css" rel="stylesheet" />
</head>
<body>
    <div class="container">        
        <form id="form1" runat="server" enctype="multipart/form-data">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/cabeceraSolicitudes.PNG" />
            <asp:ImageButton ID="btnVolver" runat="server" ImageUrl="~/Resources/volver.PNG" />
            <asp:Button ID="btnSolicitudes" runat="server" Text="SOLICITUDES" Height="44px" Width="190px" />
            <div class="card">            
                <div class="card-body">            
                    <div id="divTipoSolicitud">
                        <asp:Label ID="lblSolicitud" runat="server" Text="Tipo de solicitud a realizar: "></asp:Label><br />
                        <asp:DropDownList ID="comboSolicitud" runat="server" AutoPostBack="true">
                            <asp:ListItem text="Elige tipo de solicitud"></asp:ListItem>
                            <asp:ListItem Value="baja" Text="Baja"></asp:ListItem>
                            <asp:ListItem Value="vacaciones" Text="Vacaciones"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                <div class="formBaja" id="divFormBaja" runat="server" visible="false">
                    <div class="tipoBaja" id="divTipoBaja" runat="server">
                        <asp:Label ID="lblTipoBaja" runat="server" Text="¿Qué tipo de baja quieres solicitar?"></asp:Label>
                        <div class="row-fluid">      
                            <asp:RadioButtonList ID="radioTipoBaja" runat="server" >
                                <asp:ListItem Text="Baja Temporal" Value="Baja temporal"></asp:ListItem>
                                <asp:ListItem Text="Baja Larga Duración" Value="Baja larga"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div id="divIntervaloBaja">
                        <asp:Label ID="lblBaja" runat="server" Text="Selecciona los días de baja:"></asp:Label>
                        <asp:Calendar ID="calBaja" runat="server"></asp:Calendar>                        
                        <asp:Label ID="lblIniBaja" runat="server" Text="Inicio baja: "></asp:Label>
                        <asp:Label ID="lblFinBaja" runat="server" Text="Fin baja: " ></asp:Label>
                        <asp:ImageButton ID="btnSetBaja" ImageUrl="~/Resources/establecerFechaIni.PNG" runat="server" tooltip="Establecer Fecha Inicio" />
                        <asp:ImageButton ID="btnCancelarBaja" ImageUrl="~/Resources/resetearDef.PNG" runat="server" tooltip="Resetear Fechas" />
                        <asp:HiddenField ID="contadorBaja" value="0" runat="server" />
                        <asp:HiddenField ID="hdnFechaIniBaja" runat="server" />
                        <asp:HiddenField ID="hdnFechaFinBaja" runat="server" />
                    </div>
                    <div id="divDescripcion">
                        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:"></asp:Label>
                        <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
                    </div>
                    <div id="divArchivo">
                        <asp:Label ID="lblArchivo" runat="server" Text="Adjunta tu parte médico aquí:"></asp:Label><br/>
                        <asp:FileUpload ID="flParte" runat="server" />
                        <asp:ImageButton ID="btnConfirmarBaja" ImageUrl="~/Resources/btnGuardar.PNG" runat="server" />
                    </div>
                    <br />                
                </div>
                <div class="formVacaciones" id="divFormVacaciones" runat="server" visible="false">
                    <asp:Label ID="lblVacaciones" runat="server" Text="Selecciona los días de vacaciones:"></asp:Label>
                    <asp:Label ID="lblInicioVacaciones" runat="server" Text="Inicio vacaciones: "></asp:Label>
                    <asp:Label ID="lblFinVacaciones" runat="server" Text="Fin vacaciones: " ></asp:Label>
                    <asp:Calendar ID="calVacaciones" runat="server" ></asp:Calendar><br />
                    <asp:ImageButton ID="btnSetVacaciones" ImageUrl="~/Resources/establecerFechaIni.PNG" runat="server" Enabled="false" tooltip="Establecer Fecha Inicio" />
                    <asp:ImageButton ID="btnCancelarVacaciones" ImageUrl="~/Resources/cncel.PNG" runat="server" Height="50px" Width="75px" />
                    <asp:HiddenField ID="contadorCal" value="0" runat="server" />
                    <asp:HiddenField ID="hdnfechaIni" runat="server" />
                    <asp:HiddenField ID="hdnfechaF" runat="server" />
                </div>
                <div id="divErrores" runat="server" visible="false">
                    <%--<asp:Label ID="lblError" runat="server" ></asp:Label>--%>
                    <asp:BulletedList ID="listErrores" runat="server"></asp:BulletedList>
                </div>
                <div id="divExito" runat="server" visible="false">
                    <asp:Label ID="lblExito" runat="server" ></asp:Label>
                </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
