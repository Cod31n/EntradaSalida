Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Public Class Baja1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    Public Sub ElegirBaja()
        Dim fechaInicio As Date
        Dim fechaFin As Date

        If contadorBaja.Value = 0 Then
            fechaInicio = calBaja.SelectedDate
            If fechaInicio.DayOfWeek = DayOfWeek.Saturday Or fechaInicio.DayOfWeek = DayOfWeek.Sunday Then
                divErrores.Visible = True
                lblError.Text = "No se pueden seleccionar días no laborables"
                btnSetBaja.Enabled = False
            Else
                hdnFechaIniBaja.Value = fechaInicio
                lblIniBaja.Text = lblIniBaja.Text & fechaInicio.ToString
                btnSetBaja.Text = "Establecer Fecha Fin"
            End If

        ElseIf contadorBaja.Value = 1 Then
            fechaFin = calBaja.SelectedDate

            If fechaInicio.DayOfWeek = DayOfWeek.Saturday Or fechaInicio.DayOfWeek = DayOfWeek.Sunday Then
                divErrores.Visible = True
                lblError.Text = "No se pueden seleccionar días no laborables"
                btnSetBaja.Enabled = False
            Else
                hdnFechaFinBaja.Value = fechaFin
                If Date.Compare(hdnFechaIniBaja.Value, hdnFechaFinBaja.Value) > 0 Then
                    divErrores.Visible = True
                    lblError.Text = "La fecha de fin de baja no puede ser anterior a la fecha de inicio"
                    btnSetBaja.Enabled = False
                Else
                    lblFinBaja.Text = lblFinBaja.Text & fechaFin.ToString
                    btnSetBaja.Enabled = False

                End If
                'ElseIf contadorBaja.Value = 2 Then
                '    If Date.Compare(hdnFechaIniBaja.Value, hdnFechaFinBaja.Value) > 0 Then
                '        divErrores.Visible = True
                '        lblError.Text = "La fecha de fin de baja no puede ser anterior a la fecha de inicio"
                '    Else
                '        SolicitarVacaciones(hdnFechaIniBaja.Value, hdnFechaFinBaja.Value)
                '    End If
            End If
        End If

        contadorBaja.Value += 1
    End Sub

    Public Sub ElegirVacaciones()
        Dim fechaInicio As Date
        Dim fechaFin As Date

        If contadorCal.Value = 0 Then
            fechaInicio = calVacaciones.SelectedDate
            If fechaInicio.DayOfWeek = DayOfWeek.Saturday Or fechaInicio.DayOfWeek = DayOfWeek.Sunday Then
                divErrores.Visible = True
                lblError.Text = "No se pueden seleccionar días no laborables"
                btnSetVacaciones.Enabled = False
            Else
                hdnfechaIni.Value = fechaInicio
                lblInicioVacaciones.Text = lblInicioVacaciones.Text & fechaInicio.ToString
                btnSetVacaciones.Text = "Establecer Fecha Fin"
            End If

        ElseIf contadorCal.Value = 1 Then
            fechaFin = calVacaciones.SelectedDate
            If fechaFin.DayOfWeek = DayOfWeek.Saturday Or fechaInicio.DayOfWeek = DayOfWeek.Sunday Then
                divErrores.Visible = True
                lblError.Text = "No se pueden seleccionar días no laborables"
                btnSetBaja.Enabled = False
            Else
                hdnfechaF.Value = fechaFin
                lblFinVacaciones.Text = lblFinVacaciones.Text & fechaFin.ToString
                btnSetVacaciones.Text = "Solicitar Vacaciones"
            End If

        ElseIf contadorCal.Value = 2 Then
            If Date.Compare(hdnfechaIni.Value, hdnfechaF.Value) < 0 Then
                SolicitarVacaciones(hdnfechaIni.Value, hdnfechaF.Value)
            ElseIf Date.Compare(hdnfechaIni.Value, hdnfechaF.Value) > 0 Then
                divErrores.Visible = True
                lblError.Text = "La fecha de fin de vacaciones no puede ser anterior a la fecha de inicio"
            ElseIf Date.Compare(hdnfechaIni.Value, hdnfechaF.Value) = 0 Then
                divErrores.Visible = True
                lblError.Text = "La fecha de fin de vacaciones no puede ser igual a la fecha de inicio "
            End If
        End If
        contadorCal.Value += 1

    End Sub

    Public Sub SolicitarVacaciones(fIni As Date, fFin As Date)
        Dim Errores As String = ""
        Dim B_CadenaConexionBBDD As String = "Persist Security Info=False;Integrated Security=true;  
                                                    Initial Catalog=mibd;Server=MSSQL1; Persist Security Info=False;Integrated Security=SSPI;  
                                                    database=mibd;server=(local); Persist Security Info=False;Trusted_Connection=True;  
                                                    database=mibd;server=(local)"

        Dim ConexionAbierta As Boolean = False
        Dim comando As New System.Data.SqlClient.SqlCommand
        Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
        Dim cadenaSQL As String =
        $"
       
        insert into Registro_Entrada_Salida(idUsuario,inicio_fecha,fin_fecha,tipo_solicitud,estado_solicitud,creado_idUsuario,creado_fecha,eliminado)values(@idUsuario,@fechaInicio,@fechaFin,@registro,@estadoVacas,@idUsuario,@fechaCreado,@eliminado);SELECT SCOPE_IDENTITY()
        
        "

        Try
            Dim Parametro As System.Data.SqlClient.SqlParameter

            Parametro = New System.Data.SqlClient.SqlParameter
            Parametro.ParameterName = "idUsuario"
            Parametro.Value = 28850
            comando.Parameters.Add(Parametro)


            Parametro = New System.Data.SqlClient.SqlParameter
            Parametro.ParameterName = "fechaInicio"
            Parametro.Value = fIni
            comando.Parameters.Add(Parametro)

            Dim fechaFin = New System.Data.SqlClient.SqlParameter
            fechaFin.ParameterName = "fechaFin"
            fechaFin.Value = fFin
            comando.Parameters.Add(fechaFin)

            Dim registro = New System.Data.SqlClient.SqlParameter
            registro.ParameterName = "registro"
            registro.Value = "vacaciones"
            comando.Parameters.Add(registro)

            Dim estadoVacas = New System.Data.SqlClient.SqlParameter
            estadoVacas.ParameterName = "estadoVacas"
            estadoVacas.Value = "solicitado"
            comando.Parameters.Add(estadoVacas)

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


            Errores = r.ToString

        Catch ex As Exception


            Throw ex
        Finally
            If ConexionAbierta Then miCon.Close()
            comando = Nothing
            miCon = Nothing
            Response.Redirect("Entrada_Salida")
        End Try

    End Sub

    'Public Sub SubirParteMedico(parteMed As FileUpload)
    '    Dim carpetaArchivos As String = Server.MapPath("~\PartesMedicos\")
    '    parteMed.SaveAs(carpetaArchivos & Path.GetFileName(parteMed.FileName))
    'End Sub

    Private Sub comboSolicitud_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboSolicitud.SelectedIndexChanged
        If comboSolicitud.SelectedValue = "baja" Then
            divFormBaja.Visible = True
            divFormVacaciones.Visible = False
        ElseIf comboSolicitud.SelectedValue = "vacaciones" Then
            divFormVacaciones.Visible = True
            divFormBaja.Visible = False
        End If
    End Sub

#Region "Eventos"
    Private Sub btnSetVacaciones_Click(sender As Object, e As EventArgs) Handles btnSetVacaciones.Click
        ElegirVacaciones()
    End Sub

    Private Sub btnSetBaja_Click(sender As Object, e As EventArgs) Handles btnSetBaja.Click
        ElegirBaja()
    End Sub

    Private Sub btnCancelarVacaciones_Click(sender As Object, e As EventArgs) Handles btnCancelarVacaciones.Click
        lblInicioVacaciones.Text = "Inicio vacaciones: "
        lblFinVacaciones.Text = "Fin vacaciones: "
        btnSetVacaciones.Text = "Establecer días de vacaciones:"
        divErrores.Visible = False
        lblError.Text = ""
        contadorCal.Value = 0
    End Sub

    Private Sub btnCancelarBaja_Click(sender As Object, e As EventArgs) Handles btnCancelarBaja.Click
        lblIniBaja.Text = "Inicio baja: "
        lblFinBaja.Text = "Fin baja: "
        btnSetBaja.Text = "Establecer días de baja:"
        divErrores.Visible = False
        lblError.Text = ""
        contadorBaja.Value = 0
        btnSetBaja.Enabled = True
    End Sub

    Private Sub btnConfirmarBaja_Click(sender As Object, e As EventArgs) Handles btnConfirmarBaja.Click

        Dim oES As New EntradaSalida
        oES.BajaTemporal_Alta(100, CDate(hdnFechaIniBaja.Value), CDate(hdnFechaFinBaja.Value), txtDescripcion.Text, flParte)

        'Dim Errores As String = ""
        'Dim B_CadenaConexionBBDD As String = "Persist Security Info=False;Integrated Security=true;  
        '                                            Initial Catalog=mibd;Server=MSSQL1; Persist Security Info=False;Integrated Security=SSPI;  
        '                                            database=mibd;server=(local); Persist Security Info=False;Trusted_Connection=True"


        'Dim ConexionAbierta As Boolean = False
        'Dim comando As New System.Data.SqlClient.SqlCommand
        'Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
        'Dim cadenaSQL As String =
        '$"       
        'insert into Registro_Entrada_Salida(idUsuario,inicio_fecha,fin_fecha, descripcion_baja, parte_baja, tipo_solicitud, estado_solicitud,creado_idUsuario,creado_fecha,eliminado)values(@idUsuario,@fechaInicio, @fechaFin, @descripcion,@parteBaja, @registro,@estado,@idUsuario,@fechaCreado,@eliminado);SELECT SCOPE_IDENTITY()        
        '"

        'Try
        '    Dim idUSuario As System.Data.SqlClient.SqlParameter
        '    idUSuario = New System.Data.SqlClient.SqlParameter
        '    idUSuario.ParameterName = "idUsuario"
        '    idUSuario.Value = 28850
        '    comando.Parameters.Add(idUSuario)


        '    Dim fechaInicio = New System.Data.SqlClient.SqlParameter
        '    fechaInicio.ParameterName = "fechaInicio"
        '    fechaInicio.Value = hdnFechaIniBaja.Value
        '    comando.Parameters.Add(fechaInicio)

        '    Dim fechaFin = New System.Data.SqlClient.SqlParameter
        '    fechaFin.ParameterName = "fechaFin"
        '    fechaFin.Value = hdnFechaFinBaja.Value
        '    comando.Parameters.Add(fechaFin)

        '    Dim descripcion = New SqlParameter
        '    descripcion.ParameterName = "descripcion"
        '    descripcion.Value = txtDescripcion.Text
        '    comando.Parameters.Add(descripcion)

        '    Dim parte = New System.Data.SqlClient.SqlParameter
        '    parte.ParameterName = "parteBaja"
        '    parte.Value = flParte.FileName
        '    comando.Parameters.Add(parte)

        '    Dim registro = New System.Data.SqlClient.SqlParameter
        '    registro.ParameterName = "registro"
        '    registro.Value = radioTipoBaja.SelectedItem.Value
        '    comando.Parameters.Add(registro)

        '    Dim estado = New SqlParameter
        '    estado.ParameterName = "estado"
        '    estado.Value = "solicitado"
        '    comando.Parameters.Add(estado)

        '    Dim creadoFecha = New SqlParameter
        '    creadoFecha.ParameterName = "fechaCreado"
        '    creadoFecha.Value = DateTime.Now
        '    comando.Parameters.Add(creadoFecha)

        '    Dim eliminado = New SqlParameter
        '    eliminado.ParameterName = "eliminado"
        '    eliminado.Value = 0
        '    comando.Parameters.Add(eliminado)


        '    comando.Connection = miCon
        '    comando.CommandText = cadenaSQL 'nombre del S.P.

        '    miCon.Open()
        '    ConexionAbierta = True

        '    Dim r As Integer = comando.ExecuteScalar

        '    SubirParteMedico(flParte)

        '    Errores = r.ToString


        'Catch ex As Exception
        '    Throw ex
        'Finally
        '    If ConexionAbierta Then miCon.Close()
        '    comando = Nothing
        '    miCon = Nothing
        '    Response.Redirect("Entrada_Salida")
        'End Try

    End Sub

    Private Sub btnVolver_Click(sender As Object, e As ImageClickEventArgs) Handles btnVolver.Click
        Response.Redirect("Entrada_Salida")
    End Sub
#End Region

End Class