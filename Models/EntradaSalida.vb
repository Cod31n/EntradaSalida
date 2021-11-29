Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.Mvc


Public Class EntradaSalida
    Dim Errores As String = ""
    Dim B_CadenaConexionBBDD As String = "Persist Security Info=False;Integrated Security=true;  
                                                        Initial Catalog=mibd;Server=MSSQL1; Persist Security Info=False;Integrated Security=SSPI;  
                                                        database=mibd;server=(local); Persist Security Info=False;Trusted_Connection=True"
    Dim ConexionAbierta As Boolean = False
    Dim comando As New System.Data.SqlClient.SqlCommand
    Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)


    Public Class datos
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
            Dim fitroTexto As String = ""
            If id > 0 Then fitroTexto &= $"and id={id}"
            If inicio_fecha IsNot Nothing Then fitroTexto &= $"and inicio_fecha={inicio_fecha}"
            If tipo_solicitud IsNot Nothing Then fitroTexto &= $"and tipo_solicitud='{tipo_solicitud}'"
            Return fitroTexto
        End Function
    End Class

    Public Property id As Integer = 0
    Public Property idUsuario As Integer = 0
    '<Required(ErrorMessage:="La fecha de inicio es obligtoria")>
    Public Property inicio_fecha As Nullable(Of DateTime) = Nothing
    Public Property fin_fecha As Nullable(Of DateTime) = Nothing
    Public Property descripcion_baja As String = Nothing
    Public Property parte_baja As FileUpload = Nothing
    Public Property tipo_solicitud As String = Nothing
    Public Property estado_solicitud As String = Nothing




    'solo lectura
    Private creadoIdUsuario As Integer = idUsuario
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

    Private modificadoIdUsuario As Nullable(Of Integer) = Nothing
    Public ReadOnly Property modificado_idUsuario() As Integer
        Get
            Return modificadoIdUsuario
        End Get
    End Property

    Private modificadoFecha As Nullable(Of Integer) = Nothing
    Public ReadOnly Property modificado_fecha() As Integer
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


    Public Sub New(iD As Integer, idUsuario As Integer, inicio_fecha As Date?, fin_fecha As Date?, descripcion_baja As String,
                   parte_baja As FileUpload, tipo_solicitud As String, estado_solicitud As String, creadoIdUsuario As Integer, creadoFecha As Date,
                   modificadoIdUsuario As Integer?, modificadoFecha As Integer?, eliminadoB As Boolean, eliminadoIdUsuario As Integer?, eliminadoFecha As Date?,
                   v1 As Integer, now1 As Date, v2 As String, v3 As Integer, now2 As Date, v4 As Integer)
        Me.New(iD)
        Me.idUsuario = idUsuario
        Me.inicio_fecha = inicio_fecha
        Me.fin_fecha = fin_fecha
        Me.descripcion_baja = descripcion_baja
        Me.parte_baja = parte_baja
        Me.tipo_solicitud = tipo_solicitud
        Me.estado_solicitud = estado_solicitud
        Me.creadoIdUsuario = creadoIdUsuario
        Me.creadoFecha = creadoFecha
        Me.modificadoIdUsuario = modificadoIdUsuario
        Me.modificadoFecha = modificadoFecha
        Me.eliminadoB = eliminadoB
        Me.eliminadoIdUsuario = eliminadoIdUsuario
        Me.eliminadoFecha = eliminadoFecha

    End Sub

    Public Sub New(iD As Integer)
        Me.id = iD
        'obtener_detalle()
    End Sub

    Public Sub New()
    End Sub



    Public Function validar() As Boolean
        Dim valido As Boolean = True
        Dim digito As String = "[0-9]"

        If Not IsNumeric(idUsuario) Then
            valido = False
            Debug.WriteLine("ERROR IDUSUARIO")
            Return False
        End If
        If IsDate(inicio_fecha) Then
            Debug.WriteLine("ERROR INICIO_FECHA")
            Return False
        End If
        If Not IsNumeric(idUsuario) Then
            valido = False
            Debug.WriteLine("ERROR IDUSUARIO")
            Return valido
        ElseIf IsDate(inicio_fecha) Then
            Debug.WriteLine("ERROR INICIO_FECHA")
            Return valido
        ElseIf Not IsDate(fin_fecha) Then
            Debug.WriteLine("ERROR FIN_FECHA")
            Return valido
        ElseIf descripcion_baja.Length > 120 Then
            Debug.WriteLine("ERROR DESCRIPCION_BAJA")
            Return valido

        Else
            Return valido
        End If


    End Function

    Public Function guardar() As Integer
        Dim RegistroNueo As Boolean = IIf(id <= 0, True, False)

        Dim cadenaSQL As String = ""
        Dim param As SqlParameter = New SqlParameter
        If RegistroNueo Then
            cadenaSQL = $"insert into Registro_Entrada_Salida(idUsuario,inicio_fecha,fin_fecha,descripcion_baja,parte_baja,tipo_solicitud,creado_idUsuario,creado_fecha,eliminado) values(@idUsuario,@entrada,@salida,@descripcion_baja,@parte_baja,@tipo,@idUsuario,getdate(),@eliminado);SELECT SCOPE_IDENTITY()"

            param = New SqlParameter
            param.ParameterName = "id"
            param.Value = id
            comando.Parameters.Add(param)

            param = New SqlParameter
            param.ParameterName = "creado_idUsuario"
            param.Value = creado_idUsuario
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
            param.Value = id
            comando.Parameters.Add(param)

            param = New SqlParameter
            param.ParameterName = "modificado_idUsuario"
            param.Value = id
            comando.Parameters.Add(param)

            param = New SqlParameter
            param.ParameterName = "modificado_fecha"
            param.Value = Now
            comando.Parameters.Add(param)

        End If




        If id <= 0 Then
            'insertar
            Try

                param = New SqlParameter
                param.ParameterName = "idUsuario"
                param.Value = idUsuario
                comando.Parameters.Add(param)

                param = New SqlParameter
                param.ParameterName = "entrada"
                param.Value = inicio_fecha
                comando.Parameters.Add(param)

                param = New SqlParameter
                param.ParameterName = "salida"
                param.Value = IIf(fin_fecha.HasValue = True, fin_fecha.Value, Nothing)
                comando.Parameters.Add(param)

                param = New SqlParameter
                param.ParameterName = "descripcion_baja"
                param.Value = descripcion_baja
                comando.Parameters.Add(param)

                param = New SqlParameter
                param.ParameterName = "parte_baja"
                If parte_baja IsNot Nothing Then
                    param.Value = parte_baja.FileName 'PREGUNTAR A LEE 
                Else
                    param.Value = DBNull.Value
                End If

                comando.Parameters.Add(param)

                param = New SqlParameter
                param.ParameterName = "tipo"
                param.Value = tipo_solicitud
                comando.Parameters.Add(param)



                param = New SqlParameter
                param.ParameterName = "estado"
                param.Value = IIf(estado_solicitud IsNot Nothing, estado_solicitud, DBNull.Value)
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
                Errores = r.ToString
                Return Convert.ToInt32(r)

            Catch ex As Exception
                Debug.WriteLine(ex)
            Finally
                If ConexionAbierta Then miCon.Close()
                comando = Nothing
                miCon = Nothing
            End Try

        Else

            Try
                param = New SqlParameter
                param.ParameterName = "salida"
                param.Value = IIf(fin_fecha Is Nothing, DBNull.Value, fin_fecha)
                comando.Parameters.Add(param)



                comando.Connection = miCon
                comando.CommandText = cadenaSQL
                miCon.Open()
                ConexionAbierta = True

                Dim r As Integer = comando.ExecuteScalar

                Errores = r.ToString
                Return Errores

            Catch ex As Exception

            Finally
                If ConexionAbierta Then miCon.Close()
                comando = Nothing
                miCon = Nothing
            End Try

        End If
    End Function

    Public Function BajaTemporal_Alta(idUsuario As Integer, FechaDesde As Date, FechaHasta As Date, Descripcion As String, ParteMedico As FileUpload) As Integer
        Dim carpetaArchivos As String = HttpContext.Current.Server.MapPath("~\PartesMedicos\")
        Dim datos As New EntradaSalida
        datos.idUsuario = idUsuario
        datos.inicio_fecha = FechaDesde
        datos.fin_fecha = FechaHasta
        datos.descripcion_baja = Descripcion
        ParteMedico.SaveAs(carpetaArchivos & Path.GetFileName(ParteMedico.FileName))
        datos.parte_baja = ParteMedico
        datos.tipo_solicitud = "Baja Temporal"
        datos.estado_solicitud = "solicitado"
        datos.guardar()

    End Function

    Public Function obtener_lista(oFiltro As Filtro) As DataTable
        Dim FiltroTxt As String = oFiltro.generarfiltro

        Return obtener_lista(FiltroTxt)
    End Function


    Public Function obtener_lista(filtroTXT As String) As DataTable
        Dim cadenaSQL As String = $"select * from Registro_Entrada_Salida where 1=1 {filtroTXT}"

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
        Finally
            If ConexionAbierta Then miCon.Close()
            comando = Nothing
            miCon = Nothing
        End Try

    End Function

    Public Function obtener_detalle() As DataRow
        Dim dR As DataRow = obtener_lista($"And p.id={id}").Rows(0)

        id = dR("ID")
        idUsuario = dR("idUsuario")

        Return dR
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
