<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Baja.aspx.vb" Inherits="Entrada_Salida.Baja1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        #btnBajaLarga{
           margin-left:40px
            
        }
        .row-fluid{
            margin-top:10px
        }       
        .formBaja .descripcion{
            margin-top:50px
        }
        .formBaja .archivo{
            margin-top:50px
        }
      #calVacaciones{
          position:relative;
          top: 20px;
            left: 0px;
            width: 237px;
        }
      #lblInicioVacaciones{
          position: absolute;
          top: 380px;
          left: 253px;
        }
      #lblFinVacaciones{
          position: absolute;
          top:400px;
          left: 253px
      }
      #btnSetVacaciones{
          margin-top: 10px;
          width: 140px
      }
      #btnCancelarVacaciones{
          margin-left: 20px
      }
      /*.form-bajaCorta #btnConfirmar{
            margin-left: 400px
      }*/
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="solicitud">
            <asp:Label ID="lbl" runat="server" Text="Tipo de solicitud a realizar: "></asp:Label><br />
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
                <asp:RadioButtonList ID="radioTipoBaja" runat="server">
                    <asp:ListItem Text="Baja Temporal" Value="temporal"></asp:ListItem>
                    <asp:ListItem Text="Baja Larga Duración" Value="larga"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
        </div>
            <div class="descripcion">
                <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:"></asp:Label>
                <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
            </div>
            <div class="archivo">
                <asp:Label ID="lblArchivo" runat="server" Text="Adjunta tu parte médico aquí:"></asp:Label><br />
                <asp:FileUpload ID="flParte" runat="server" />
            </div>
            <br />
            <asp:Button ID="btnConfirmarBaja" runat="server" Text="Confirmar" />
        </div>
        <div class="formVacaciones" id="divFormVacaciones" runat="server" visible="false">
            <asp:Label ID="lblVacaciones" runat="server" Text="Elige los días de vacaciones que quieres:"></asp:Label>
            <asp:Label ID="lblInicioVacaciones" runat="server" Text="Inicio vacaciones: "></asp:Label>
            <asp:Label ID="lblFinVacaciones" runat="server" Text="Fin vacaciones: " ></asp:Label>
            <asp:Calendar ID="calVacaciones" runat="server"></asp:Calendar><br />
            <asp:Button ID="btnSetVacaciones" runat="server" Text="Elegir Fecha Inicio" />
            <asp:Button ID="btnCancelarVacaciones" runat="server" Text="Cancelar" />
            <asp:HiddenField ID="contadorCal" value="0" runat="server" />
            <asp:HiddenField ID="hdnfechaIni" runat="server" />
            <asp:HiddenField ID="hdnfechaF" runat="server" />
        </div>
    </form>
</body>
</html>
