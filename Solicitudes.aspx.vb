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
            btnSetVacaciones.Text = "Establecer Fecha Fin"
        ElseIf contadorCal.Value = 1 Then
            fechaFin = calVacaciones.SelectedDate
            hdnfechaF.Value = fechaFin
            lblFinVacaciones.Text = lblFinVacaciones.Text & fechaFin.ToString
            btnSetVacaciones.Text = "Solicitar Vacaciones"
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
        Dim cSelect As New System.Data.SqlClient.SqlCommand
        Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
        Dim cadenaSQL As String =
        $"
       
        insert into Registro_Entrada_Salida(idUsuario,creado_idUsuario,fechaInicio,fechaFin,tipoRegistro,estado_SolicitudVacaciones)values(@idUsuario,@idUsuario,@fechaInicio,@fechaFin,@registro,@estadoVacas);SELECT SCOPE_IDENTITY()
        
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
            Response.Redirect("Entrada_Salida")
        End Try

    End Sub

    Public Sub SubirParteMedico(parteMed As FileUpload)
        Dim carpetaArchivos As String = Server.MapPath("~\PartesMedicos\")

        parteMed.SaveAs(carpetaArchivos & Path.GetFileName(parteMed.FileName))



    End Sub

    Private Sub comboSolicitud_SelectedIndexChanged(sender As Object, e As EventArgs) Handles comboSolicitud.SelectedIndexChanged
        If comboSolicitud.SelectedValue = "baja" Then
            divFormBaja.Visible = True
            divFormVacaciones.Visible = False
        ElseIf comboSolicitud.SelectedValue = "vacaciones" Then
            divFormVacaciones.Visible = True
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
        divErrores.Visible = False
        lblError.Text = ""
        contadorCal.Value = 0
    End Sub

    Private Sub btnConfirmarBaja_Click(sender As Object, e As EventArgs) Handles btnConfirmarBaja.Click

        Dim Errores As String = ""
        Dim B_CadenaConexionBBDD As String = "Persist Security Info=False;Integrated Security=true;  
                                                    Initial Catalog=mibd;Server=MSSQL1; Persist Security Info=False;Integrated Security=SSPI;  
                                                    database=mibd;server=(local); Persist Security Info=False;Trusted_Connection=True;  
                                                    database=mibd;server=(local)"

        'Dim filename As String = Path.GetFileName(flParte.PostedFile.FileName)
        'Dim contentType As String = flParte.PostedFile.ContentType
        'Using fs As Stream = flParte.PostedFile.InputStream
        '    Using br As New BinaryReader(fs)
        '        Dim bytes As Byte() = br.ReadBytes(CType(fs.Length, Integer))

        '        Using con As New SqlConnection(B_CadenaConexionBBDD)
        '            Dim query As String = "INSERT INTO Archivos VALUES (@Name, @ContentType, @Data)"
        '            Using cmd As New SqlCommand(query)
        '                cmd.Connection = con
        '                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = filename
        '                cmd.Parameters.Add("@ContentType", SqlDbType.VarChar).Value = contentType
        '                cmd.Parameters.Add("@Data", SqlDbType.Binary).Value = bytes
        '                con.Open()
        '                cmd.ExecuteNonQuery()
        '                con.Close()
        '            End Using
        '        End Using
        '    End Using
        'End Using


        Dim ConexionAbierta As Boolean = False
        Dim comando As New System.Data.SqlClient.SqlCommand
        Dim miCon As New System.Data.SqlClient.SqlConnection(B_CadenaConexionBBDD)
        Dim cadenaSQL As String =
        $"       
        insert into Registro_Entrada_Salida(idUsuario,creado_idUsuario,fechaInicio,fechaFin, descripcionBaja, parteBaja, tipoRegistro, estado_solicitudBaja)values(@idUsuario,@idUsuario,@fechaInicio, @descripcion,@parteBaja,  @registro,@estado);SELECT SCOPE_IDENTITY()        
        "

        Try
            Dim idUSuario As System.Data.SqlClient.SqlParameter
            idUSuario = New System.Data.SqlClient.SqlParameter
            idUSuario.ParameterName = "idUsuario"
            idUSuario.Value = 28850
            comando.Parameters.Add(idUSuario)


            Dim fechaInicio = New System.Data.SqlClient.SqlParameter
            fechaInicio.ParameterName = "fechaInicio"
            fechaInicio.Value = Date.Now
            comando.Parameters.Add(fechaInicio)

            Dim descripcion = New SqlParameter
            descripcion.ParameterName = "descripcion"
            descripcion.Value = txtDescripcion.Text
            comando.Parameters.Add(descripcion)

            Dim parte = New System.Data.SqlClient.SqlParameter
            parte.ParameterName = "parteBaja"
            parte.Value = flParte.FileName
            comando.Parameters.Add(parte)

            Dim registro = New System.Data.SqlClient.SqlParameter
            registro.ParameterName = "registro"
            registro.Value = radioTipoBaja.SelectedItem.Value
            comando.Parameters.Add(registro)

            Dim estado = New SqlParameter
            estado.ParameterName = "estado"
            estado.Value = "solicitado"
            comando.Parameters.Add(estado)

            comando.Connection = miCon
            comando.CommandText = cadenaSQL 'nombre del S.P.

            miCon.Open()
            ConexionAbierta = True

            Dim r = comando.ExecuteScalar

            SubirParteMedico(flParte)

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

    Private Sub btnVolver_Click(sender As Object, e As ImageClickEventArgs) Handles btnVolver.Click
        Response.Redirect("Entrada_Salida")
    End Sub
End Class