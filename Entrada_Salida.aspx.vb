﻿Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient

Public Class Entrada_Salida
    Inherits System.Web.UI.Page

    Private Sub Entrada_Salida_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            saludo.Visible = True

            Dim oES As New EntradaSalida

            oES.obtener_lista()


            'Dim idRegistro As Integer = 0

            ''lectura
            'Dim oES As New EntradaSalida(idRegistro)
            'Debug.WriteLine($"idRegistro={oES.ID}")
            'Debug.WriteLine($"idUsuario={oES.idUsuario}")

            ''guardar
            'Dim oES2 As New EntradaSalida()
            'oES2.idUsuario = 1
            'oES2.inicio_fecha = Now()

            'oES2.guardar_registro()


            'Dim datos As New EntradaSalida.datos
            'datos.idUsuario = 1
            'datos.inicio_fecha = Now

            'oES2.guardar_registro(datos)

            'Dim filtro As New EntradaSalida.Filtro
            'filtro.idUsuario = 1
            'filtro.inicio_fecha = Now
            'oES2.obtener_lista(filtro)
        End If

    End Sub


    'Public Function RegistrarEntrada() As Integer
    '    Dim Errores As String = ""
    '    'Dim B_CadenaConexionBBDD As String = "Server=PC-TECNICO;Database=mibd;User Id=DALKOM2\tecnico;Password=seKom2019@"
    '    Dim B_CadenaConexionBBDD As String = "Persist Security Info=False;Integrated Security=true;  
    '                                                Initial Catalog=mibd;Server=MSSQL1; Persist Security Info=False;Integrated Security=SSPI;  
    '                                                database=mibd;server=(local); Persist Security Info=False;Trusted_Connection=True;  
    '                                                database=mibd;server=(local)"

    '    'Validación parametros de entradas
    '    'If idProvincia <= 0 Then
    '    '    Errores = "Debes especificar idProvincia"
    '    '    Return Errores
    '    'End If



    '    'Conexion SQL2000
    '    Dim ConexionAbierta As Boolean = False
    '    Dim comando As New System.Data.SqlClient.SqlCommand
    '    Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
    '    Dim cadenaSQL As String =
    '    $"       
    '    insert into Registro_Entrada_Salida(idUsuario,inicio_fecha,tipo_solicitud,creado_idUsuario,creado_fecha,eliminado)values(@idUsuario,@entrada,@tipo,@idUsuario,@fechaCreado,@eliminado);SELECT SCOPE_IDENTITY()        

    '    "



    '    Try

    '        Dim registroEntrada As New EntradaSalida(0, 28850, DateTime.Now, "jornada laboral", 28850, DateTime.Now, 0)

    '        If registroEntrada.validar_propiedades() = True Then




    '            Dim parametro As System.Data.SqlClient.SqlParameter
    '            parametro = New System.Data.SqlClient.SqlParameter
    '            parametro.ParameterName = "idUsuario"
    '            parametro.Value = registroEntrada.idUsuario
    '            comando.Parameters.Add(parametro)


    '            parametro = New System.Data.SqlClient.SqlParameter
    '            parametro.ParameterName = "entrada"
    '            parametro.Value = registroEntrada.inicio_fecha
    '            comando.Parameters.Add(parametro)

    '            parametro = New SqlParameter
    '            parametro.ParameterName = "tipo"
    '            parametro.Value = registroEntrada.tipo_solicitud
    '            comando.Parameters.Add(parametro)

    '            parametro = New SqlParameter
    '            parametro.ParameterName = "fechaCreado"
    '            parametro.Value = registroEntrada.creado_fecha
    '            comando.Parameters.Add(parametro)

    '            parametro = New SqlParameter
    '            parametro.ParameterName = "eliminado"
    '            parametro.Value = registroEntrada.eliminado
    '            comando.Parameters.Add(parametro)


    '            comando.Connection = miCon
    '            comando.CommandText = cadenaSQL 'nombre del S.P.

    '            miCon.Open()
    '            ConexionAbierta = True

    '            Dim r = comando.ExecuteScalar
    '            Debug.WriteLine(r.ToString)
    '            Errores = r.ToString
    '            Return Convert.ToInt32(r)
    '        Else
    '            Return 0
    '        End If




    '    Catch ex As Exception


    '        Throw ex
    '    Finally
    '        If ConexionAbierta Then miCon.Close()
    '        comando = Nothing
    '        miCon = Nothing
    '    End Try
    'End Function


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



        Catch ex As Exception
            Throw ex
        Finally

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
        Dim oES As New EntradaSalida.datos
        oES.id = 0
        oES.idUsuario = 28850
        oES.inicio_fecha = Now
        oES

        horaEntrada.Visible = True
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