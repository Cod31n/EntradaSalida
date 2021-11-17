Imports System.Drawing
Imports System.Windows.Forms

Public Class Entrada_Salida
    Inherits System.Web.UI.Page

    Private Sub Entrada_Click(sender As Object, e As ImageClickEventArgs) Handles Entrada.Click
        Dim entry As Date = DateTime.Now
        Hidden.Value = entry
        Entrada.Visible = False
        LabelEntrada.Visible = False
        Salida.Visible = True
        LabelSalida.Visible = True
        Dim entrando As New Form With {
            .FormBorderStyle = FormBorderStyle.FixedDialog,
            .BackColor = Color.White,
            .Top = 0,
            .Left = 0,
            .Width = 500
        }
        Dim texto As New Label() With {
                .Text = "Has entrado a las " & entry.Hour.ToString & " horas y " & entry.Minute.ToString & " minutos",
                .Left = 20,
                .Top = 20,
                .Width = 500
            }
        entrando.Controls.Add(texto)
        entrando.ShowDialog()

    End Sub
    Public Function SolicitudKitMiCasaV2_Obtener_EnvioDias(idProvincia As Integer) As String
        Dim Errores As String = ""
        Dim B_CadenaConexionBBDD As String = ""


        'Validación parametros de entradas
        If idProvincia <= 0 Then
            Errores = "Debes especificar idProvincia"
            Return Errores
        End If



        'Conexion SQL2000
        Dim ConexionAbierta As Boolean = False
        Dim cSelect As New System.Data.SqlClient.SqlCommand
        Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
        Dim cadenaSQL As String =
$"
        select top 1  
        from SolicitudesKitMiCasaV2_envio p 
        where p.idProvincia=@idProvincia
"



        Try
            Dim par As System.Data.SqlClient.SqlParameter
            par = New System.Data.SqlClient.SqlParameter
            par.ParameterName = "idProvincia"
            par.Value = idProvincia
            cSelect.Parameters.Add(par)


            'par = New System.Data.SqlClient.SqlParameter
            'par.ParameterName = "idProvincia"
            'par.Value = idProvincia
            'cSelect.Parameters.Add(par)


            cSelect.Connection = miCon
            cSelect.CommandText = cadenaSQL 'nombre del S.P.
            'cSelect.CommandType = CommandType.Text 'cSelect.CommandType = CommandType.StoredProcedure
            miCon.Open()
            ConexionAbierta = True
            Dim r As String = cSelect.ExecuteNonQuery 'ExecuteScalar,ExecuteNonQuery, ExecuteReader (datareader)
            miCon.Close()

            ''Prueba a devolor un valor nulo.
            'Request.QueryString("")
            Errores = r
            Return Errores
        Catch ex As Exception


            'Throw
        Finally
            If ConexionAbierta Then miCon.Close()
            cSelect = Nothing
            miCon = Nothing
        End Try
    End Function


    Private Sub Salida_Click(sender As Object, e As ImageClickEventArgs) Handles Salida.Click
        Dim salida As Date = DateTime.Now
        Dim entrada As Date = DateTime.Parse(Hidden.Value)

        Dim tiempo = DateDiff(DateInterval.Second, entrada, salida)

        Dim saliendo As New Form With {
            .FormBorderStyle = FormBorderStyle.FixedDialog,
            .BackColor = Color.White,
            .Top = 0,
            .Left = 0,
            .Width = 500
        }
        Dim texto As New Label() With {
                .Text = "Llevas " & tiempo.ToString & " segundos trabajando ¿Seguro que quieres salir?",
                .Left = 20,
                .Top = 20,
                .Width = 500
            }
        Dim okey As New Button With {
            .Text = "Salir",
            .Left = 130,
            .Top = 100
        }
        AddHandler okey.Click, Sub(sender2, args)
                                   Response.Redirect("https://www.mybuildingvf.com/Vodafone")
                                   'System.Diagnostics.Process.Start(")
                                   saliendo.Close()

                               End Sub

        Dim cerrar As New Button With {
                .Text = "Cancelar",
                .Top = 100,
                .Left = 50
            }
        AddHandler cerrar.Click, Sub(sender3, args)
                                     saliendo.Close()
                                 End Sub

        saliendo.Controls.Add(texto)
        saliendo.Controls.Add(okey)
        saliendo.Controls.Add(cerrar)
        saliendo.ShowDialog()



    End Sub

    Private Sub Entrada_Salida_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Dim saludo As New Form With {
                .FormBorderStyle = FormBorderStyle.FixedDialog,
                .BackColor = Color.White,
                .Top = 500,
                .Left = 500
            }
            Dim texto As New Label() With {
                .Text = "¡Hola! Deberías registrar tu entrada",
                 .Left = 20,
                 .Height = 70,
                .Top = 20
            }
            Dim cerrar As New Button With {
                .Text = "Cerrar",
                .Left = 100,
                .Top = 100
            }

            AddHandler cerrar.Click, Sub(sender2, args)
                                         saludo.Close()
                                     End Sub

            saludo.Controls.Add(texto)
            saludo.Controls.Add(cerrar)
            saludo.ShowDialog()
        End If
    End Sub
End Class