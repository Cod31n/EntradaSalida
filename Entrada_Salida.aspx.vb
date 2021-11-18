Imports System.Drawing
Imports System.Windows.Forms
Imports System.Data.SqlClient

Public Class Entrada_Salida
    Inherits System.Web.UI.Page

    Private Sub Entrada_Salida_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            Saludo.Visible = True
        End If

    End Sub
    Public Function RegistrarEntrada() As Integer
        Dim Errores As String = ""
        'Dim B_CadenaConexionBBDD As String = "Server=PC-TECNICO;Database=mibd;User Id=DALKOM2\tecnico;Password=seKom2019@"
        Dim B_CadenaConexionBBDD As String = "Persist Security Info=False;Integrated Security=true;  
                                                    Initial Catalog=mibd;Server=MSSQL1" &
                                                "Persist Security Info=False;Integrated Security=SSPI;  
                                                    database=mibd;server=(local)" &
                                                "Persist Security Info=False;Trusted_Connection=True;  
                                                    database=mibd;server=(local)"

        'Validación parametros de entradas
        'If idProvincia <= 0 Then
        '    Errores = "Debes especificar idProvincia"
        '    Return Errores
        'End If



        'Conexion SQL2000
        Dim ConexionAbierta As Boolean = False
        Dim cSelect As New System.Data.SqlClient.SqlCommand
        Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
        Dim cadenaSQL As String =
        $"
       
        insert into Registro_Entrada_Salida(id_usuario,dni,entrada)values('28850','54010084N', GETDATE());SELECT SCOPE_IDENTITY()
        

        "



        Try
            'Dim par As System.Data.SqlClient.SqlParameter
            'par = New System.Data.SqlClient.SqlParameter
            'par.ParameterName = "idProvincia"
            'par.Value = idProvincia
            'cSelect.Parameters.Add(par)


            'par = New System.Data.SqlClient.SqlParameter
            'par.ParameterName = "idProvincia"
            'par.Value = idProvincia
            'cSelect.Parameters.Add(par)


            cSelect.Connection = miCon
            cSelect.CommandText = cadenaSQL 'nombre del S.P.
            'cSelect.CommandType = CommandType.Text 'cSelect.CommandType = CommandType.StoredProcedure
            miCon.Open()
            ConexionAbierta = True
            'Dim r As String = cSelect.ExecuteNonQuery 'ExecuteScalar,ExecuteNonQuery, ExecuteReader (datareader)
            Dim r = cSelect.ExecuteScalar
            Debug.WriteLine(r.ToString)

            'miCon.Close()
            ''Prueba a devolor un valor nulo.
            'Request.QueryString("")

            'If r.HasRows Then
            '    r.Read()
            '    Console.WriteLine(r.GetSqlString(1) + " " + r.GetSqlString(2))
            '    Debug.WriteLine(r.GetSqlString(1) + " " + r.GetSqlString(2))
            'End If
            Errores = r.ToString
            Return Convert.ToInt32(r)
        Catch ex As Exception


            Throw ex
        Finally
            If ConexionAbierta Then miCon.Close()
            cSelect = Nothing
            miCon = Nothing
        End Try
    End Function


    Public Function RegistrarSalida(id As Integer) As String
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
       declare @entrada datetime
        SET @entrada =  (select entrada from Registro_Entrada_Salida where id = {id})
        declare @salida datetime
        set @salida = GETDATE()
        update Registro_Entrada_Salida set salida = @salida ,tiempo_trabajado = DATEDIFF(SECOND,@entrada,@salida) where id ={id}
        

        "

        Try
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

    Public Function ObtenerTiempoTrabajado(id As Integer) As String
        Dim Errores As String = ""
        Dim B_CadenaConexionBBDD As String = "Persist Security Info=False;Integrated Security=true; Initial Catalog=mibd;Server=MSSQL1;Persist Security Info=False;Integrated Security=SSPI; database=mibd;server=(local); Persist Security Info=False;Trusted_Connection=True; database=mibd;server=(local)"

        Dim ConexionAbierta As Boolean = False
        Dim cSelect As New System.Data.SqlClient.SqlCommand
        Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
        Dim cadenaSQL As String =
        $"
            select tiempo_trabajado from Registro_Entrada_Salida where id = {id}
        "
        Try
            cSelect.Connection = miCon
            cSelect.CommandText = cadenaSQL
            miCon.Open()
            ConexionAbierta = True

            Dim r As SqlDataReader = cSelect.ExecuteReader

            If r.HasRows Then

                r.Read()
                Dim tiempoT As String = r.GetString(0)
                Debug.WriteLine("Tiempo trabajado: " & tiempoT)
                Return tiempoT
            End If





        Catch ex As Exception
            Throw ex
        Finally
            If ConexionAbierta Then miCon.Close()
            cSelect = Nothing
            miCon = Nothing
        End Try

    End Function
    Private Sub Entrada_Click(sender As Object, e As ImageClickEventArgs) Handles Entrada.Click
        Hidden.Value = Me.RegistrarEntrada()
        HoraEntrada.Visible = True
        Dim entry As Date = DateTime.Now
        Entrada.Visible = False
        lblEntrada.Visible = False
        btnSalida.Visible = True
        lblSalida.Visible = True

        lblHoraEntrada.Text = "Has entrado a las " & entry.Hour.ToString & " horas y " & entry.Minute.ToString & " minutos"

    End Sub


    Private Sub Salida_Click(sender As Object, e As ImageClickEventArgs) Handles btnSalida.Click
        Me.RegistrarSalida(Hidden.Value)
        Dim tiempo As String = Me.ObtenerTiempoTrabajado(Hidden.Value)
        tiempoTrabajado.Visible = True

        lblTiempoTrabajado.Text = "Llevas " & tiempo & " segundos trabajando ¿Seguro que quieres salir?"



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
       
        update Registro_Entrada_Salida set tiempo_trabajado = null where id ={Hidden.Value}
        

        "

        Try
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
End Class