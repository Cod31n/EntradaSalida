Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient

Public Class Entrada_Salida
    Inherits System.Web.UI.Page

    Private Sub Entrada_Salida_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            saludo.Visible = True
        End If

    End Sub
    Public Function RegistrarEntrada() As Integer
        Dim Errores As String = ""
        'Dim B_CadenaConexionBBDD As String = "Server=PC-TECNICO;Database=mibd;User Id=DALKOM2\tecnico;Password=seKom2019@"
        Dim B_CadenaConexionBBDD As String = "Persist Security Info=False;Integrated Security=true;  
                                                    Initial Catalog=mibd;Server=MSSQL1; Persist Security Info=False;Integrated Security=SSPI;  
                                                    database=mibd;server=(local); Persist Security Info=False;Trusted_Connection=True;  
                                                    database=mibd;server=(local)"

        'Validación parametros de entradas
        'If idProvincia <= 0 Then
        '    Errores = "Debes especificar idProvincia"
        '    Return Errores
        'End If



        'Conexion SQL2000
        Dim ConexionAbierta As Boolean = False
        Dim comando As New System.Data.SqlClient.SqlCommand
        Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
        Dim cadenaSQL As String =
        $"       
        insert into Registro_Entrada_Salida(idUsuario,inicio_fecha,tipo_solicitud,creado_idUsuario,creado_fecha,eliminado)values(@idUsuario,@entrada,@tipo,@idUsuario,@fechaCreado,@eliminado);SELECT SCOPE_IDENTITY()        

        "



        Try


            Dim idUSuario As System.Data.SqlClient.SqlParameter
            idUSuario = New System.Data.SqlClient.SqlParameter
            idUSuario.ParameterName = "idUsuario"
            idUSuario.Value = 28850
            comando.Parameters.Add(idUSuario)


            Dim entry = New System.Data.SqlClient.SqlParameter
            entry.ParameterName = "entrada"
            entry.Value = DateTime.Now
            comando.Parameters.Add(entry)

            Dim registro = New SqlParameter
            registro.ParameterName = "tipo"
            registro.Value = "jornada laboral"
            comando.Parameters.Add(registro)

            Dim creadoFecha = New SqlParameter
            creadoFecha.ParameterName = "fechaCreado"
            creadoFecha.Value = DateTime.Now
            comando.Parameters.Add(creadoFecha)

            Dim eliminado = New SqlParameter
            eliminado.ParameterName = "eliminado"
            eliminado.Value = 0
            comando.Parameters.Add(eliminado)


            comando.Connection = miCon
            comando.CommandText = cadenaSQL 'nombre del S.P.

            miCon.Open()
            ConexionAbierta = True

            Dim r = comando.ExecuteScalar
            Debug.WriteLine(r.ToString)


            Errores = r.ToString
            Return Convert.ToInt32(r)
        Catch ex As Exception


            Throw ex
        Finally
            If ConexionAbierta Then miCon.Close()
            comando = Nothing
            miCon = Nothing
        End Try
    End Function


    Public Function RegistrarSalida(idReg As Integer) As String
        'VALIDAR PARAMETROS DE ENTRADA!!!!
        Dim Errores As String = ""
        Dim B_CadenaConexionBBDD As String = "Persist Security Info=False;Integrated Security=true;  
                                                    Initial Catalog=mibd;Server=MSSQL1" &
                                                "Persist Security Info=False;Integrated Security=SSPI;  
                                                    database=mibd;server=(local)" &
                                                "Persist Security Info=False;Trusted_Connection=True;  
                                                    database=mibd;server=(local)"

        Dim ConexionAbierta As Boolean = False
        Dim cSelect As New System.Data.SqlClient.SqlCommand
        Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
        Dim cadenaSQL As String =
        $"
      
        update Registro_Entrada_Salida set fin_fecha = @salida where id =@id
        

        "

        Try

            Dim salida = New SqlParameter
            salida.ParameterName = "salida"
            salida.Value = DateTime.Now
            cSelect.Parameters.Add(salida)

            Dim id = New SqlParameter
            id.ParameterName = "id"
            id.Value = idReg
            cSelect.Parameters.Add(id)


            cSelect.Connection = miCon
            cSelect.CommandText = cadenaSQL
            miCon.Open()
            ConexionAbierta = True

            Dim r As Integer = cSelect.ExecuteScalar



            Errores = r.ToString
            Return Errores
        Catch ex As Exception
            Throw ex
        Finally
            If ConexionAbierta Then miCon.Close()
            cSelect = Nothing
            miCon = Nothing
        End Try

    End Function

    'Public Function ObtenerTiempoTrabajado(id As Integer) As String
    '    Dim Errores As String = ""
    '    Dim B_CadenaConexionBBDD As String = "Persist Security Info=False;Integrated Security=true; Initial Catalog=mibd;Server=MSSQL1;Persist Security Info=False;Integrated Security=SSPI; database=mibd;server=(local); Persist Security Info=False;Trusted_Connection=True; database=mibd;server=(local)"

    '    Dim ConexionAbierta As Boolean = False
    '    Dim cSelect As New System.Data.SqlClient.SqlCommand
    '    Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
    '    Dim cadenaSQL As String =
    '    $"
    '        select tiempo_trabajado from Registro_Entrada_Salida where id = {id}
    '    "
    '    Try
    '        cSelect.Connection = miCon
    '        cSelect.CommandText = cadenaSQL
    '        miCon.Open()
    '        ConexionAbierta = True

    '        Dim r As SqlDataReader = cSelect.ExecuteReader

    '        If r.HasRows Then

    '            r.Read()
    '            Dim tiempoT As String = r.GetString(0)
    '            Debug.WriteLine("Tiempo trabajado: " & tiempoT)
    '            Return tiempoT
    '        End If





    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        If ConexionAbierta Then miCon.Close()
    '        cSelect = Nothing
    '        miCon = Nothing
    '    End Try

    'End Function
    Private Sub Entrada_Click(sender As Object, e As ImageClickEventArgs) Handles Entrada.Click
        Hidden.Value = Me.RegistrarEntrada()
        HoraEntrada.Visible = True
        Dim entry As Date = DateTime.Now
        Entrada.Visible = False

        btnSalida.Visible = True


        lblHoraEntrada.Text = "Has entrado a las " & entry.Hour.ToString & " horas y " & entry.Minute.ToString & " minutos"

    End Sub


    Private Sub Salida_Click(sender As Object, e As ImageClickEventArgs) Handles btnSalida.Click
        Me.RegistrarSalida(Hidden.Value)
        Dim hora As String = DateTime.Now.Hour
        Dim min As String = DateTime.Now.Minute
        tiempoTrabajado.Visible = True

        lblTiempoTrabajado.Text = "Son las " & hora & " horas y " & min & " minutos ¿Seguro que quieres salir?"


    End Sub

    Private Sub CerrarSaludo_Click(sender As Object, e As EventArgs) Handles btnCerrarSaludo.Click
        saludo.Visible = False
    End Sub

    Private Sub cerrarHoraEntrada_Click(sender As Object, e As EventArgs) Handles btnCerrarHoraEntrada.Click
        horaEntrada.Visible = False
    End Sub

    Private Sub btnCancelarSalida_Click(sender As Object, e As EventArgs) Handles btnCancelarSalida.Click
        Dim Errores As String = ""
        tiempoTrabajado.Visible = False
        Dim B_CadenaConexionBBDD As String = "Persist Security Info=False;Integrated Security=true;  
                                                    Initial Catalog=mibd;Server=MSSQL1; Persist Security Info=False;Integrated Security=SSPI;  
                                                    database=mibd;server=(local); Persist Security Info=False;Trusted_Connection=True; ; database=mibd;server=(local)"

        Dim ConexionAbierta As Boolean = False
        Dim cSelect As New System.Data.SqlClient.SqlCommand
        Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
        Dim cadenaSQL As String =
        $"
       
        update Registro_Entrada_Salida set fin_fecha = null where id =@id
        
        "

        Try

            Dim id = New SqlParameter
            id.ParameterName = "id"
            id.Value = Hidden.Value
            cSelect.Parameters.Add(id)

            cSelect.Connection = miCon
            cSelect.CommandText = cadenaSQL
            miCon.Open()
            ConexionAbierta = True


            Dim r As Integer = cSelect.ExecuteScalar



            Errores = r.ToString

        Catch ex As Exception
            Throw ex
        Finally
            If ConexionAbierta Then miCon.Close()
            cSelect = Nothing
            miCon = Nothing
        End Try
    End Sub

    Private Sub btnConfirmarSalida_Click(sender As Object, e As EventArgs) Handles btnConfirmarSalida.Click
        tiempoTrabajado.Visible = False
        Response.Redirect("https://www.mybuildingvf.com/Vodafone")
    End Sub

    Private Sub btnCabecera_Click(sender As Object, e As ImageClickEventArgs) Handles btnCabecera.Click
        Response.Redirect("PerfilUsuario")
    End Sub
End Class