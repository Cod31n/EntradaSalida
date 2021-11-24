Imports System.ComponentModel.DataAnnotations
Imports System.Data.SqlClient

Public Class EntradaSalida

    Public Property ID As Integer = 0
    Public Property idUsuario As Integer = 28850
    <Required(ErrorMessage:="La fecha de inicio es obligtoria")>
    Public Property inicio_fecha As DateTime = Nothing
    Public Property fin_fecha As Nullable(Of DateTime) = Nothing
    Public Property descripcion_baja As String = Nothing
    Public Property parte_baja As String = Nothing
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

    Private modificadoIdUsuario As Integer = Nothing
    Public ReadOnly Property modificado_idUsuario() As Integer
        Get
            Return modificadoIdUsuario
        End Get
    End Property

    Private modificadoFecha As Integer = Nothing
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

    Private eliminadoIdUsuario As Integer = Nothing
    Public ReadOnly Property eliminado_idUsuario() As Integer
        Get
            Return eliminadoIdUsuario
        End Get
    End Property

    Private eliminadoFecha As DateTime = Nothing
    Public ReadOnly Property eliminado_fecha As DateTime
        Get
            Return eliminadoFecha
        End Get
    End Property

    Public Sub New(iD As Integer, idUsuario As Integer, inicio_fecha As Date, fin_fecha As Date?,
                   descripcion_baja As String, parte_baja As String, tipo_solicitud As String,
                   estado_solicitud As String, creadoIdUsuario As Integer, creadoFecha As Date,
                   modificadoIdUsuario As Integer, modificadoFecha As Integer, eliminadoB As Boolean, eliminadoIdUsuario As Integer, eliminadoFecha As Date)
        Me.ID = iD
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
        Me.ID = iD
    End Sub


    Public Function obtener_lista() As DataTable
        Dim Errores = ""
        Dim B_CadenaConexionBBDD As String = "Persist Security Info=False;Integrated Security=true;  
                                                    Initial Catalog=mibd;Server=MSSQL1; Persist Security Info=False;Integrated Security=SSPI;  
                                                    database=mibd;server=(local); Persist Security Info=False;Trusted_Connection=True"
        Dim ConexionAbierta As Boolean = False
        Dim comando As New System.Data.SqlClient.SqlCommand
        Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
        Dim cadenaSQL As String =
        $"       
        select * from Registro_Entrada_Salida

        "

        Try
            comando.Connection = miCon
            comando.CommandText = cadenaSQL
            miCon.Open()
            ConexionAbierta = True

            Dim reader As SqlDataReader = comando.ExecuteReader
            Dim tablaRegistros As DataTable
            Dim i As Integer
            If reader.HasRows Then

                reader.Read()
                'tablaRegistros.Rows.Add(
                '                    reader.GetSqlInt32(0),
                '                    reader.GetSqlInt32(1),
                '                    reader.GetSqlDateTime(2),
                '                    reader.GetSqlDateTime(3),
                '                    reader.GetSqlString(4),
                '                    reader.GetSqlString(5),
                '                    reader.GetSqlString(6),
                '                    reader.GetSqlString(7),
                '                    reader.GetSqlInt32(8),
                '                    reader.GetSqlDateTime(9),
                '                    reader.GetSqlInt32(10),
                '                    reader.GetSqlDateTime(11),
                '                    reader.GetSqlBoolean(12),
                '                    reader.GetSqlInt32(13),
                '                    reader.GetSqlDateTime(14))

            End If
        Catch ex As Exception

        Finally

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
