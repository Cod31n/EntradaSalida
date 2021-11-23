<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Solicitudes.aspx.vb" Inherits="Entrada_Salida.Baja1" %>

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
          top: 480px;
          left: 900px;
        }
      #lblFinVacaciones{
          position: absolute;
          top:500px;
          left: 900px
      }
      #btnSetVacaciones{
          margin-top: 10px;
          width: 150px
      }
      #btnCancelarVacaciones{
          margin-left: 10px;
          color: darkslateblue
      }
      
      .card{
          width: 59%;      
          margin-left: 21%;
          border: groove;
         
      }
      .card-body{
          color:darkslateblue;
          width: 60%;
          margin-left: 20%;
         border: groove
      }
      #lblError{
          color:red
      }
      #btnVolver{
          position:absolute;
          top: 101px;
          margin-left:1445px;
         
         
      }
      #btnSetVacaciones{
          color:darkslateblue
      }
      #comboSolicitud{
          color:darkslateblue
      }
      #btnSolicitudes {
            position: absolute;
            top: 193px;
            left: 590px;
            
        }
      /*.form-bajaCorta #btnConfirmar{
            margin-left: 400px
      }*/
    </style>
</head>
<body>
    <div class="container">
        
        <form id="form1" runat="server" enctype="multipart/form-data">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Resources/cabeceraSolicitudes.PNG" />
            <asp:ImageButton ID="btnVolver" runat="server" ImageUrl="~/Resources/volver.PNG" />
            <asp:Button ID="btnSolicitudes" runat="server" Text="SOLICITUDES" Height="44px" Width="190px" />
        <div class="card">
            
            <div class="card-body">
            
            <div class="solicitud">
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
                        <asp:ListItem Text="Baja Temporal" Value="baja temporal"></asp:ListItem>
                        <asp:ListItem Text="Baja Larga Duración" Value="baja larga"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                </div>
                <div class="divIntervaloBaja">
                    <asp:Calendar ID="calBaja" runat="server"></asp:Calendar>
                </div>
                <div class="descripcion">
                    <asp:Label ID="lblDescripcion" runat="server" Text="Descripción:"></asp:Label>
                    <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
                </div>
                <div class="archivo">
                    <asp:Label ID="lblArchivo" runat="server" Text="Adjunta tu parte médico aquí:"></asp:Label><br/>
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
                <asp:Button ID="btnSetVacaciones" runat="server" Text="Establecer Fecha Inicio" />
                <asp:Button ID="btnCancelarVacaciones" runat="server" Text="Cancelar" />
                <asp:HiddenField ID="contadorCal" value="0" runat="server" />
                <asp:HiddenField ID="hdnfechaIni" runat="server" />
                <asp:HiddenField ID="hdnfechaF" runat="server" />
            </div>
            <div class="errores" id="divErrores" runat="server" visible="false">
                <asp:Label ID="lblError" runat="server" ></asp:Label>
            </div>
   
            </div>
        </div>
             </form>
    </div>
</body>
</html>
