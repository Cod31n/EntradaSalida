Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.Mvc


Public Class EntradaSalida

    Dim B_CadenaConexionBBDD As String = "Persist Security Info=False;Integrated Security=true;  
                                                        Initial Catalog=mibd;Server=MSSQL1; Persist Security Info=False;Integrated Security=SSPI;  
                                                        database=mibd;server=(local); Persist Security Info=False;Trusted_Connection=True"
    Dim ConexionAbierta As Boolean = False
    Dim comando As New System.Data.SqlClient.SqlCommand
    Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
    Dim UsuarioSesion As Integer = 0

    Public Class Dato
        Public Property id As Integer = 0
        Public Property idUsuario As Integer = 0
        '<Required(ErrorMessage:="La fecha de inicio es obligtoria")>
        Public Property inicio_fecha As Nullable(Of DateTime) = Nothing
        Public Property fin_fecha As Nullable(Of DateTime) = Nothing
        Public Property descripcion_baja As String = Nothing
        Public Property parte_baja As FileUpload = Nothing
        Public Property tipo_solicitud As String = Nothing
        Public Property estado_solicitud As String = Nothing


        'datos solo lectura
        Public ReadOnly Property horastrabajadas As Long
            Get
                Return DateDiff(DateInterval.Hour, CDate(inicio_fecha), CDate(fin_fecha))
            End Get
        End Property
    End Class
    Public Class Filtro
        Public Property id As Integer = 0
        Public Property idUsuario As Integer = 0
        '<Required(ErrorMessage:="La fecha de inicio es obligtoria")>
        Public Property inicio_fecha As Nullable(Of DateTime) = Nothing
        Public Property fin_fecha As Nullable(Of DateTime) = Nothing
        Public Property descripcion_baja As String = Nothing
        Public Property parte_baja As FileUpload = Nothing
        Public Property tipo_solicitud As String = Nothing
        Public Property estado_solicitud As String = Nothing

        Public Function generarfiltro() As String
            Dim filtroTexto As String = ""
            If id > 0 Then filtroTexto &= $"and p.id={id}"
            If inicio_fecha IsNot Nothing Then filtroTexto &= $"and p.inicio_fecha=convert(date,'{CDate(inicio_fecha).ToString("yyyy-MM-dd HH:mm:ss")}',120)"
            If fin_fecha IsNot Nothing Then filtroTexto &= $"and p.fin_fecha=convert(date,'{CDate(fin_fecha).ToString("yy-MM-dd HH:mm:ss")}',120"
            If tipo_solicitud IsNot Nothing Then filtroTexto &= $"and p.tipo_solicitud='{CStr(tipo_solicitud)}'"
            If descripcion_baja IsNot Nothing Then filtroTexto &= $"and p.descripcion_baja='{descripcion_baja}'"

            Return filtroTexto
        End Function
    End Class

    Public dato_ As Dato = Nothing

    'solo lectura
    Private creadoIdUsuario As Integer = 0
    Public ReadOnly Property creado_idUsuario() As Integer
        Get
            Return creadoIdUsuario
        End Get
    End Property

    Private creadoFecha As DateTime = DateTime.Now
    Public ReadOnly Property creado_fecha() As DateTime
        Get
            Return creadoFecha
        End Get
    End Property

    Private modificadoIdUsuario As Nullable(Of Integer) = UsuarioSesion
    Public ReadOnly Property modificado_idUsuario() As Integer
        Get
            Return modificadoIdUsuario
        End Get
    End Property

    Private modificadoFecha As Nullable(Of DateTime) = Nothing
    Public ReadOnly Property modificado_fecha() As Nullable(Of DateTime)
        Get
            Return modificadoFecha
        End Get
    End Property
    Private eliminadoB As Boolean = 0
    Public ReadOnly Property eliminado() As Boolean
        Get
            Return eliminadoB
        End Get
    End Property

    Private eliminadoIdUsuario As Nullable(Of Integer) = Nothing
    Public ReadOnly Property eliminado_idUsuario() As Integer
        Get
            Return eliminadoIdUsuario
        End Get
    End Property

    Private eliminadoFecha As Nullable(Of DateTime) = Nothing
    Public ReadOnly Property eliminado_fecha As DateTime
        Get
            Return eliminadoFecha
        End Get
    End Property
    Public Sub New()

    End Sub
    Public Sub New(iD As Integer, UsuarioSesion_ As Integer)
        'Me.id = iD
        dato_ = obtener_detalle(iD)
        UsuarioSesion = UsuarioSesion_
    End Sub

    Public Sub New(UsuarioSesion_ As Integer)
        dato_ = New Dato
        UsuarioSesion = UsuarioSesion_
    End Sub

    Public Function obtener_lista(oFiltro As Filtro) As DataTable
        Dim FiltroTxt As String = oFiltro.generarfiltro

        Return obtener_tabla(FiltroTxt)
    End Function
    Public Function obtener_tabla(filtroTXT As String) As DataTable
        Dim cadenaSQL As String = $"select * from Registro_Entrada_Salida p where 1=1 and p.eliminado=0 {filtroTXT}"
        Try
            comando.Connection = miCon
            comando.CommandText = cadenaSQL
            miCon.Open()
            ConexionAbierta = True

            'Dim reader As SqlDataReader = comando.ExecuteReader
            Dim tablaRegistros As DataTable = New DataTable

            Dim dad As New SqlDataAdapter(cadenaSQL, miCon)

            dad.Fill(tablaRegistros)

            Return tablaRegistros

        Catch ex As Exception
            Debug.WriteLine(ex)
            'logs
            'Throw
            Return Nothing
        Finally
            If ConexionAbierta Then miCon.Close()
            comando = Nothing
            miCon = Nothing
        End Try
    End Function
    'Public Function obtener_detalle(ID_ As Integer) As DataRow
    '    Dim dR As DataRow = obtener_lista($"And p.id={ID_}").Rows(0)

    '    id = dR("ID")
    '    idUsuario = dR("idUsuario")
    '    '...



    '    Return dR
    'End Function
    Public Function obtener_lista(filtroTXT As String) As List(Of Dato)

        Dim datos As New List(Of Dato)
        'datareader --> más optimo

        Dim cadenaSQL As String = $"select * from Registro_Entrada_Salida p where 1=1 {filtroTXT} and eliminado=0"
        Dim path As String = "~\PartesMedicos\"

        Try
            comando.Connection = miCon
            comando.CommandText = cadenaSQL
            miCon.Open()
            ConexionAbierta = True

            Dim reader As SqlDataReader = comando.ExecuteReader

            While reader.Read
                Dim dat As New Dato

                dat.id = reader("id")
                dat.idUsuario = reader("idUsuario")
                dat.inicio_fecha = reader("inicio_fecha")
                If reader("fin_fecha") IsNot DBNull.Value Then
                    dat.fin_fecha = reader("fin_fecha")
                End If

                If reader("descripcion_baja") IsNot DBNull.Value Then
                    dat.descripcion_baja = reader.GetSqlString(4)
                End If

                'dat.parte_baja = File.OpenRead(path & reader.GetSqlString(5).ToString)
                'Dim parte As FileAccess
                dat.parte_baja = Nothing
                dat.tipo_solicitud = reader("tipo_solicitud")
                If reader("estado_solicitud") IsNot DBNull.Value Then
                    dat.estado_solicitud = reader("estado_solicitud")
                End If
                datos.Add(dat)
            End While

            Return datos

        Catch ex As Exception
            Debug.WriteLine(ex)
            'logs
            'Throw
            Return Nothing
        Finally
            If ConexionAbierta Then miCon.Close()
            comando = Nothing
            miCon = Nothing
        End Try

    End Function
    Public Function obtener_detalle(ID_ As Integer) As Dato
        Try
            Dim dR As DataRow = obtener_tabla($"And p.id={ID_}").Rows(0)

            dato_.id = dR("id")
            dato_.idUsuario = ifnullEntero(dR("idUsuario"))
            dato_.inicio_fecha = ifnullDateNothing(dR("inicio_fecha"))
            dato_.fin_fecha = ifnullDateNothing(dR("fin_fecha"))
            dato_.tipo_solicitud = ifnullString(dR("tipo_solicitud"))
            dato_.estado_solicitud = ifnullString(dR("estado_solicitud"))


            Return dato_
        Catch ex As Exception
            dato_ = Nothing
            Return dato_
        End Try



    End Function
    Public Function ifnullString(valor As Object, Optional valorPordefecto As String = "") As String
        If valor Is DBNull.Value Then
            Return valorPordefecto
        Else
            Return CStr(valor)
        End If
    End Function
    Public Function ifnullEntero(valor As Object, Optional valorPordefecto As Integer = 0) As Integer
        If valor Is DBNull.Value Then
            Return valorPordefecto
        Else
            Return CInt(valor)
        End If
    End Function
    Public Function ifnullDateNothing(valor As Object, Optional valorPordefecto As Nullable(Of Date) = Nothing) As Nullable(Of Date)
        If valor Is DBNull.Value Then
            Return valorPordefecto
        Else
            Return CType(valor, Nullable(Of Date))
        End If
    End Function

    Class Errores
        Property Errores_lista As New List(Of String)
        Property Resultado_integer As Integer = 0

        Public Function ExisteError() As Boolean
            If Errores_lista.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function
    End Class

    Public Function validar() As Errores
        Dim Errores As New Errores

        If dato_.tipo_solicitud Is Nothing Then
            Errores.Errores_lista.Add("Debes elegir un tipo de solicitud")
        End If

        Dim rx As New Regex("^\d{1,5}$")
        If rx.IsMatch(dato_.idUsuario.ToString) = False Then
            Errores.Errores_lista.Add("El idUsuario debe contener sólo numeros")
        End If

        Select Case dato_.tipo_solicitud
            Case "Baja temporal"

                If dato_.descripcion_baja Is Nothing Then
                    Errores.Errores_lista.Add("La descripción es obligatoria")
                ElseIf dato_.descripcion_baja.Length > 120 Then

                    Errores.Errores_lista.Add("La descripción debe contener como máximo 120 caracteres")
                End If

                If dato_.parte_baja Is Nothing Then
                    Errores.Errores_lista.Add("Debe adjuntar un parte médico")

                End If

                If dato_.inicio_fecha Is Nothing Or dato_.fin_fecha Is Nothing Then
                    Errores.Errores_lista.Add("Es obligatorio establecer las fechas")
                Else
                    Dim diaSemanaInicio As DayOfWeek = CDate(dato_.inicio_fecha.Value).DayOfWeek
                    Dim diaSemanaFin As DayOfWeek = CDate(dato_.fin_fecha.Value).DayOfWeek
                    If CDate(dato_.inicio_fecha.Value) > CDate(dato_.fin_fecha) Then
                        Errores.Errores_lista.Add("La fecha de inicio no puede ser posterior a la fecha de fin")
                    ElseIf DateDiff(DateInterval.Day, CDate(dato_.inicio_fecha), CDate(dato_.fin_fecha)) > 3 Then
                        Errores.Errores_lista.Add("La duración de una baja temporal tiene un máximo de 3 días")
                    ElseIf diaSemanaInicio = 6 Or diaSemanaInicio = 7 Then
                        Errores.Errores_lista.Add("la fecha de inicio no puede ser sábado ni domingo")
                    ElseIf diaSemanaFin = 6 Or diaSemanaFin = 7 Then
                        Errores.Errores_lista.Add("la fecha de finalización no puede ser sábado ni domingo")
                    End If
                End If


            Case "Baja larga"

                If dato_.descripcion_baja Is Nothing Then
                    Errores.Errores_lista.Add("La descripción es obligatoria")
                ElseIf dato_.descripcion_baja.Length > 120 Then

                    Errores.Errores_lista.Add("La descripción debe contener como máximo 120 caracteres")
                End If

                If dato_.parte_baja Is Nothing Then
                    Errores.Errores_lista.Add("Debe adjuntar un parte médico")

                End If

                If dato_.inicio_fecha Is Nothing Or dato_.fin_fecha Is Nothing Then
                    Errores.Errores_lista.Add("Es obligatorio establecer las fechas")
                Else
                    Dim diferenciaDias As Integer = DateDiff(DateInterval.Day, CDate(dato_.inicio_fecha), CDate(dato_.fin_fecha))
                    Dim diaSemanaInicio As DayOfWeek = CDate(dato_.inicio_fecha.Value).DayOfWeek
                    Dim diaSemanaFin As DayOfWeek = CDate(dato_.fin_fecha.Value).DayOfWeek
                    If CDate(dato_.inicio_fecha.Value) > CDate(dato_.fin_fecha) Then
                        Errores.Errores_lista.Add("La fecha de inicio no puede ser posterior a la fecha de fin")
                    ElseIf diferenciaDias <= 3 Or diferenciaDias > 30 Then
                        Errores.Errores_lista.Add("La duración de una baja de larga duración tiene un mínimo de 3 días y un máximo de 1 mes")
                    ElseIf diaSemanaInicio = 6 Or diaSemanaInicio = 7 Then
                        Errores.Errores_lista.Add("la fecha de inicio no puede ser sábado ni domingo")
                    ElseIf diaSemanaFin = 6 Or diaSemanaFin = 7 Then
                        Errores.Errores_lista.Add("la fecha de finalización no puede ser sábado ni domingo")
                    End If
                End If

            Case "Vacaciones"
                If dato_.inicio_fecha Is Nothing Or dato_.fin_fecha Is Nothing Then
                    Errores.Errores_lista.Add("Es obligatorio establecer las fechas")
                Else
                    Dim diaSemanaInicio As DayOfWeek = CDate(dato_.inicio_fecha.Value).DayOfWeek
                    Dim diaSemanaFin As DayOfWeek = CDate(dato_.fin_fecha.Value).DayOfWeek
                    If DateDiff(DateInterval.Day, CDate(dato_.inicio_fecha), CDate(dato_.fin_fecha)) > 30 Then
                        Errores.Errores_lista.Add("Las vacaciones no pueden durar más de 30 días")
                    ElseIf diaSemanaInicio = 6 Or diaSemanaInicio = 7 Then
                        Errores.Errores_lista.Add("la fecha de inicio no puede ser sábado ni domingo")
                    ElseIf diaSemanaFin = 6 Or diaSemanaFin = 7 Then
                        Errores.Errores_lista.Add("la fecha de finalización no puede ser sábado ni domingo")
                    End If
                End If
            Case Else

        End Select
        Return Errores

    End Function

    Public Function guardar() As Errores
        Dim oErrores As Errores = validar()
        'oErrores.Errores_lista.Count>0 'hay error
        If oErrores.ExisteError Then
            Return oErrores
        End If
        Dim RegistroNueo As Boolean = IIf(dato_.id <= 0, True, False)

        Dim cadenaSQL As String = ""
        Dim param As SqlParameter = New SqlParameter
        If RegistroNueo = True Then
            cadenaSQL = $"insert into Registro_Entrada_Salida(idUsuario,inicio_fecha,fin_fecha,descripcion_baja,parte_baja,tipo_solicitud,estado_solicitud,creado_idUsuario,creado_fecha,eliminado) values(@idUsuario,@entrada,@salida,@descripcion_baja,@parte_baja,@tipo,@estado,@idUsuario,getdate(),@eliminado);SELECT SCOPE_IDENTITY()"

            param = New SqlParameter
            param.ParameterName = "id"
            param.Value = dato_.id
            comando.Parameters.Add(param)

            param = New SqlParameter
            param.ParameterName = "creado_idUsuario"
            param.Value = UsuarioSesion
            comando.Parameters.Add(param)

            'param = New SqlParameter
            'param.ParameterName = "creado_fecha"
            'param.Value = Now
            'comando.Parameters.Add(param)

        Else
            'modificar
            cadenaSQL = $"update Registro_Entrada_Salida set fin_fecha = @salida, modificado_idUsuario = @modificado_idUsuario, modificado_fecha = @modificado_fecha where id =@id"

            param = New SqlParameter
            param.ParameterName = "id"
            param.Value = CInt(dato_.id)
            comando.Parameters.Add(param)

            param = New SqlParameter
            param.ParameterName = "modificado_idUsuario"
            param.Value = UsuarioSesion
            comando.Parameters.Add(param)

            param = New SqlParameter
            param.ParameterName = "modificado_fecha"
            param.Value = Now
            comando.Parameters.Add(param)

        End If




        If dato_.id <= 0 Then
            'insertar
            Try

                param = New SqlParameter
                param.ParameterName = "idUsuario"
                param.Value = dato_.idUsuario
                comando.Parameters.Add(param)

                param = New SqlParameter
                param.ParameterName = "entrada"
                param.Value = dato_.inicio_fecha
                comando.Parameters.Add(param)

                param = New SqlParameter
                param.ParameterName = "salida"
                If dato_.fin_fecha.HasValue Then
                    param.Value = dato_.fin_fecha.Value
                Else
                    param.Value = DBNull.Value
                End If
                comando.Parameters.Add(param)

                param = New SqlParameter
                param.ParameterName = "descripcion_baja"
                If dato_.descripcion_baja IsNot Nothing Then
                    param.Value = dato_.descripcion_baja
                Else
                    param.Value = DBNull.Value
                End If
                comando.Parameters.Add(param)

                param = New SqlParameter
                param.ParameterName = "parte_baja"
                If dato_.parte_baja IsNot Nothing Then
                    param.Value = dato_.parte_baja.FileName 'PREGUNTAR A LEE 
                Else
                    param.Value = DBNull.Value
                End If
                comando.Parameters.Add(param)

                param = New SqlParameter
                param.ParameterName = "tipo"
                param.Value = dato_.tipo_solicitud
                comando.Parameters.Add(param)



                param = New SqlParameter
                param.ParameterName = "estado"
                param.Value = IIf(dato_.estado_solicitud IsNot Nothing, dato_.estado_solicitud, DBNull.Value)
                comando.Parameters.Add(param)

                param = New SqlParameter
                param.ParameterName = "eliminado"
                param.Value = eliminado
                comando.Parameters.Add(param)

                comando.Connection = miCon
                comando.CommandText = cadenaSQL 'nombre del S.P.

                miCon.Open()
                ConexionAbierta = True

                Dim r = comando.ExecuteScalar
                Debug.WriteLine(r.ToString)
                'Err = r.ToString
                oErrores.Resultado_integer = CInt(r)
                Return oErrores

                Dim carpetaArchivos As String = HttpContext.Current.Server.MapPath("~\PartesMedicos\")
                dato_.parte_baja.SaveAs(carpetaArchivos & Path.GetFileName(dato_.parte_baja.FileName))

            Catch ex As Exception
                Debug.WriteLine(ex)
                Return oErrores
            Finally
                If ConexionAbierta Then miCon.Close()
                comando = Nothing
                miCon = Nothing
            End Try

        Else
            'modificar
            Try
                param = New SqlParameter
                param.ParameterName = "salida"
                param.Value = IIf(dato_.fin_fecha Is Nothing, DBNull.Value, CDate(dato_.fin_fecha))
                comando.Parameters.Add(param)



                comando.Connection = miCon
                comando.CommandText = cadenaSQL
                miCon.Open()
                ConexionAbierta = True

                oErrores.Resultado_integer = comando.ExecuteNonQuery

                Return oErrores
            Catch ex As Exception
                Return oErrores
            Finally
                If ConexionAbierta Then miCon.Close()
                comando = Nothing
                miCon = Nothing
            End Try

        End If



    End Function

    Public Function Baja_Alta(idUsuario As Integer, FechaDesde As Date, FechaHasta As Date, Descripcion As String, ParteMedico As FileUpload, tipoSolicitud As String) As Errores
        Dim carpetaArchivos As String = HttpContext.Current.Server.MapPath("~\PartesMedicos\")
        Dim datos As New Dato
        dato_.idUsuario = idUsuario
        dato_.inicio_fecha = FechaDesde
        dato_.fin_fecha = FechaHasta
        dato_.descripcion_baja = Descripcion
        ParteMedico.SaveAs(carpetaArchivos & Path.GetFileName(ParteMedico.FileName))
        dato_.parte_baja = ParteMedico
        dato_.tipo_solicitud = tipoSolicitud
        dato_.estado_solicitud = "solicitado"


        Return guardar()
    End Function

    Public Function Vacaciones_Alta(idUsuario As Integer, FechaDesde As Date, FechaHasta As Date, tipoSolicitud As String) As Errores
        'Dim datos As New Dato
        'datos.idUsuario = idUsuario
        'datos.inicio_fecha = FechaDesde
        'datos.fin_fecha = FechaHasta
        'datos.tipo_solicitud = tipoSolicitud
        'datos.estado_solicitud = "solicitado"
        dato_.idUsuario = idUsuario
        dato_.inicio_fecha = FechaDesde
        dato_.fin_fecha = FechaHasta
        dato_.tipo_solicitud = tipoSolicitud
        dato_.estado_solicitud = "solicitado"

        Return guardar()

    End Function

    Public Function EliminarRegistro(id As Integer) As Integer
            'marcar el campo eliminado. no hacer delete
            Dim cadenaSql As String = "update Registro_Entrada_Salida set eliminado=1 where id = @id "
            Try
                Dim param As SqlParameter = New SqlParameter
                param.ParameterName = "id"
                param.Value = id
                comando.Parameters.Add(param)

                comando.Connection = miCon
                comando.CommandText = cadenaSql
                miCon.Open()
                ConexionAbierta = True
                Dim resultDelete As Integer = comando.ExecuteNonQuery

                If resultDelete = 1 Then
                    Return 1
                Else
                    Return 0
                End If

            Catch ex As Exception
                Throw ex
                Return 0
            Finally
                If ConexionAbierta Then miCon.Close()
                comando = Nothing
                miCon = Nothing
            End Try

        End Function



        'constructor()
        'constructor(id)
        'validación
        'obtener_lista ---> datatable, datareader
        'obtener_detalle --> datatable.datarow
        'obtener_lista_list ---> lista de EntradasSalidas
        'obtener_detalle --> EntradasSalidas
        'guardar <-- validación
        'eliminar


    End Class
