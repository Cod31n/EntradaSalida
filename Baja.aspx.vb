Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Public Class Baja1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Public Sub ElegirVacaciones()
        Dim fechaInicio As Date
        Dim fechaFin As Date

        If contadorCal.Value = 0 Then
            fechaInicio = calVacaciones.SelectedDate
            hdnfechaIni.Value = fechaInicio
            lblInicioVacaciones.Text = lblInicioVacaciones.Text & fechaInicio.ToString
            btnSetVacaciones.Text = "Elegir Fecha Fin"
        ElseIf contadorCal.Value = 1 Then
            fechaFin = calVacaciones.SelectedDate
            hdnfechaF.Value = fechaFin
            lblFinVacaciones.Text = lblFinVacaciones.Text & fechaFin.ToString
            btnSetVacaciones.Text = "Solicitar Vacaciones"
        ElseIf contadorCal.Value = 2 Then
            SolicitarVacaciones(hdnfechaIni.Value, hdnfechaF.Value)
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
        Dim cSelect As New System.Data.SqlClient.SqlCommand
        Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
        Dim cadenaSQL As String =
        $"
       
        insert into Registro_Entrada_Salida(idUsuario,idUsuario_Creado,fecha_InicioVacaciones,fecha_FinVacaciones,tipoRegistro,estado_SolicitudVacaciones)values(@idUsuario,@idUsuario,@fechaInicio,@fechaFin,@registro,@estadoVacas);SELECT SCOPE_IDENTITY()
        
        "

        Try
            Dim idUSuario As System.Data.SqlClient.SqlParameter
            idUSuario = New System.Data.SqlClient.SqlParameter
            idUSuario.ParameterName = "idUsuario"
            idUSuario.Value = 28850
            cSelect.Parameters.Add(idUSuario)


            Dim fechaInicio = New System.Data.SqlClient.SqlParameter
            fechaInicio.ParameterName = "fechaInicio"
            fechaInicio.Value = fIni
            cSelect.Parameters.Add(fechaInicio)

            Dim fechaFin = New System.Data.SqlClient.SqlParameter
            fechaFin.ParameterName = "fechaFin"
            fechaFin.Value = fFin
            cSelect.Parameters.Add(fechaFin)

            Dim registro = New System.Data.SqlClient.SqlParameter
            registro.ParameterName = "registro"
            registro.Value = "vacaciones"
            cSelect.Parameters.Add(registro)

            Dim estadoVacas = New System.Data.SqlClient.SqlParameter
            estadoVacas.ParameterName = "estadoVacas"
            estadoVacas.Value = "solicitado"
            cSelect.Parameters.Add(estadoVacas)

            cSelect.Connection = miCon
            cSelect.CommandText = cadenaSQL 'nombre del S.P.

            miCon.Open()
            ConexionAbierta = True

            Dim r = cSelect.ExecuteScalar


            Errores = r.ToString

        Catch ex As Exception


            Throw ex
        Finally
            If ConexionAbierta Then miCon.Close()
            cSelect = Nothing
            miCon = Nothing
        End Try

    End Sub


    Private Sub comboSolicitud_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboSolicitud.SelectedIndexChanged
        If comboSolicitud.SelectedValue = "baja" Then
            divFormBaja.Visible = True
            divFormVacaciones.Visible = False
        ElseIf comboSolicitud.SelectedValue = "vacaciones" Then
            divFormVacaciones.Visible = True
            divTipoBaja.Visible = False
            divFormBaja.Visible = False
        End If
    End Sub


    Private Sub btnSetVacaciones_Click(sender As Object, e As EventArgs) Handles btnSetVacaciones.Click
        ElegirVacaciones()
    End Sub

    Private Sub btnCancelarVacaciones_Click(sender As Object, e As EventArgs) Handles btnCancelarVacaciones.Click
        lblInicioVacaciones.Text = "Inicio vacaciones: "
        lblFinVacaciones.Text = "Fin vacaciones: "
        btnSetVacaciones.Text = "Elegir Fecha Inicio"
        contadorCal.Value = 0
    End Sub

    Private Sub btnConfirmarBaja_Click(sender As Object, e As EventArgs) Handles btnConfirmarBaja.Click

        Dim Errores As String = ""
        Dim B_CadenaConexionBBDD As String = "Persist Security Info=False;Integrated Security=true;  
                                                    Initial Catalog=mibd;Server=MSSQL1; Persist Security Info=False;Integrated Security=SSPI;  
                                                    database=mibd;server=(local); Persist Security Info=False;Trusted_Connection=True;  
                                                    database=mibd;server=(local)"

        Dim ConexionAbierta As Boolean = False
        Dim cSelect As New System.Data.SqlClient.SqlCommand
        Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
        Dim cadenaSQL As String =
        $"       
        insert into Registro_Entrada_Salida(idUsuario,idUsuario_Creado,fecha_InicioBaja,parteBaja,tipoRegistro)values(@idUsuario,@idUsuario,@fechaInicio,@parteBaja,@registro);SELECT SCOPE_IDENTITY()
        
        "

        Try
            Dim idUSuario As System.Data.SqlClient.SqlParameter
            idUSuario = New System.Data.SqlClient.SqlParameter
            idUSuario.ParameterName = "idUsuario"
            idUSuario.Value = 28850
            cSelect.Parameters.Add(idUSuario)


            Dim fechaInicio = New System.Data.SqlClient.SqlParameter
            fechaInicio.ParameterName = "fechaInicio"
            fechaInicio.Value = Date.Now
            cSelect.Parameters.Add(fechaInicio)

            Dim parte = New System.Data.SqlClient.SqlParameter
            parte.ParameterName = "parteBaja"
            parte.Value = flParte
            cSelect.Parameters.Add(parte)

            Dim registro = New System.Data.SqlClient.SqlParameter
            registro.ParameterName = "registro"
            registro.Value = "baja"
            cSelect.Parameters.Add(registro)

            cSelect.Connection = miCon
            cSelect.CommandText = cadenaSQL 'nombre del S.P.

            miCon.Open()
            ConexionAbierta = True

            Dim r = cSelect.ExecuteScalar


            Errores = r.ToString


        Catch ex As Exception
            Throw ex
        Finally
            If ConexionAbierta Then miCon.Close()
            cSelect = Nothing
            miCon = Nothing
        End Try

    End Sub
End Class