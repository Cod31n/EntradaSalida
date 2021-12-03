Imports System.ComponentModel
Imports System.Drawing

Public Class ListaRegistros
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub

    Private Sub Tabla()
        Dim oEs As New EntradaSalida.Filtro
        'If Not txtIdRegistro.Text = "" Then
        '    oEs.id = CInt(txtIdRegistro.Text)
        'End If
        If Not txtIdUsuario.Text = "" Then
            oEs.idUsuario = CInt(txtIdUsuario.Text)
        End If
        oEs.inicio_fecha = IIf(calInicio.SelectedDate = Date.MinValue, Nothing, calInicio.SelectedDate)
        oEs.fin_fecha = IIf(calFin.SelectedDate = Date.MinValue, Nothing, calFin.SelectedDate)
        oEs.tipo_solicitud = IIf(comboTipoSolicitud.SelectedValue = "Tipo Solicitud", Nothing, comboTipoSolicitud.SelectedValue)
        Dim filtro As String = oEs.generarfiltro()

        Dim Obj As New EntradaSalida()
        Dim tab As New DataTable
        tab = Obj.obtener_tabla(filtro)

        If tab IsNot Nothing Then

            divSelect.Visible = True


            For i As Integer = 0 To tab.Rows.Count - 1
                'lboxRegistros.Items.Add(tab(i)(6).ToString)
                'Dim itemLista As String = ""
                Dim nuevaRow As New TableRow
                For x As Integer = 1 To tab.Columns.Count - 1

                    'If tab(i)(x).ToString IsNot "" Then
                    'itemLista = $"{itemLista} - {tab(i)(x).ToString}"
                    Dim nuevaCol As New TableCell
                    nuevaCol.Text = tab(i)(x).ToString
                    If x = 0 Then
                        nuevaCol.ForeColor = Color.White
                        nuevaCol.BackColor = Color.Black
                    End If
                    nuevaRow.Cells.Add(nuevaCol)

                    'End If
                Next
                tblRegistros.Rows.Add(nuevaRow)
                'lboxRegistros.Items.Add(itemLista)
            Next
        Else
            divError.Visible = True
        End If
    End Sub

    Private Sub Lista()
        Dim oEs As New EntradaSalida.Filtro
        'If Not txtIdRegistro.Text = "" Then
        '    oEs.id = CInt(txtIdRegistro.Text)
        'End If
        If Not txtIdUsuario.Text = "" Then
            oEs.idUsuario = CInt(txtIdUsuario.Text)
        End If
        oEs.inicio_fecha = IIf(calInicio.SelectedDate = Date.MinValue, Nothing, calInicio.SelectedDate)
        oEs.fin_fecha = IIf(calFin.SelectedDate = Date.MinValue, Nothing, calFin.SelectedDate)
        oEs.tipo_solicitud = IIf(comboTipoSolicitud.SelectedValue = "Tipo Solicitud", Nothing, comboTipoSolicitud.SelectedValue)
        Dim filtro As String = oEs.generarfiltro()
        Dim listaReg As List(Of EntradaSalida.Dato)
        Dim obj As New EntradaSalida

        listaReg = obj.obtener_lista(filtro)
        If listaReg IsNot Nothing Then
            listRegistros.Items.Add("ID USUARIO     |       FECHA INICIO        |       FECHA FIN")
            divSelectList.Visible = True
            For Each reg As EntradaSalida.Dato In listaReg
                listRegistros.Items.Add(reg.idUsuario & " | " & reg.inicio_fecha & " | " & reg.tipo_solicitud)
            Next
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        divFiltros.Visible = False
        divBusquedaId.Visible = False
        divTipoResultado.Visible = False

        If drpTipoResultado.SelectedValue = "tabla" Then
            Tabla()
        ElseIf drpTipoResultado.SelectedValue = "lista" Then
            Lista()
        End If

    End Sub

    Private Sub btnResetCalendarios_Click(sender As Object, e As EventArgs) Handles btnResetCalendarios.Click
        calInicio.SelectedDate = Nothing
        calFin.SelectedDate = Nothing
    End Sub

    Private Sub btnCerrarError_Click(sender As Object, e As ImageClickEventArgs) Handles btnCerrarError.Click
        divError.Visible = False
        divFiltros.Visible = True
        divBusquedaId.Visible = True
        divTipoResultado.Visible = True
        divSelect.Visible = False
    End Sub

    Private Sub btnVolver_Click(sender As Object, e As ImageClickEventArgs) Handles btnVolver.Click
        Response.Redirect("Entrada_Salida")
    End Sub



    Private Sub btnBuscarDetalle_Click(sender As Object, e As EventArgs) Handles btnBuscarDetalle.Click
        divFiltros.Visible = False
        divBusquedaId.Visible = False
        divTipoResultado.Visible = False
        Dim id As Integer
        If Not txtIdRegistro.Text = "" Then
            id = CInt(txtIdRegistro.Text)
        End If


        Dim obj As New EntradaSalida
        obj.dato_ = New EntradaSalida.Dato


        If obj.obtener_detalle(id) IsNot Nothing Then
            divSelectId.Visible = True
            lblIdUsu.Text = lblIdUsu.Text & obj.dato_.idUsuario.ToString
            lblTipoSol.Text = lblTipoSol.Text & obj.dato_.tipo_solicitud
            lblEstadoSol.Text = lblEstadoSol.Text & obj.dato_.estado_solicitud
            lblFechaIni.Text = lblFechaIni.Text & obj.dato_.inicio_fecha.ToString
            lblFechaFin.Text = lblFechaFin.Text & obj.dato_.fin_fecha.ToString
        Else
            divError.Visible = True
            lblError.Text = "El registro no existe o ha sido borrado"
        End If

    End Sub

    Private Sub btnEliminarRegistro_Click(sender As Object, e As EventArgs) Handles btnEliminarRegistro.Click
        Dim id As Integer
        If Not txtIdRegistro.Text = "" Then
            id = CInt(txtIdRegistro.Text)
        End If
        Dim resultDel As Integer
        Dim obj As New EntradaSalida

        resultDel = obj.EliminarRegistro(id)

        If resultDel = 1 Then
            divFiltros.Visible = False
            divBusquedaId.Visible = False
            divTipoResultado.Visible = False

        Else
            divError.Visible = True
            lblError.Text = "Ha habido un problema al eliminar el registro"

        End If
    End Sub

    Private Sub drpTipoResultado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles drpTipoResultado.SelectedIndexChanged
        If drpTipoResultado.SelectedValue = "detalle" Then
            txtIdRegistro.Enabled = True
            btnBuscarDetalle.Enabled = True
            btnEliminarRegistro.Enabled = True
            txtIdUsuario.Enabled = False
            calInicio.Enabled = False
            calFin.Enabled = False
            comboTipoSolicitud.Enabled = False
            btnBuscar.Enabled = False
            btnResetCalendarios.Enabled = False
        ElseIf drpTipoResultado.SelectedValue = "Tipo de resultado" Then
            txtIdRegistro.Enabled = False
            btnBuscarDetalle.Enabled = False
            btnEliminarRegistro.Enabled = False
            txtIdUsuario.Enabled = False
            calInicio.Enabled = False
            calFin.Enabled = False
            comboTipoSolicitud.Enabled = False
            btnBuscar.Enabled = False
            btnResetCalendarios.Enabled = False
        Else
            txtIdRegistro.Enabled = False
            btnBuscarDetalle.Enabled = False
            btnEliminarRegistro.Enabled = False
            txtIdUsuario.Enabled = True
            calInicio.Enabled = True
            calFin.Enabled = True
            comboTipoSolicitud.Enabled = True
            btnBuscar.Enabled = True
            btnResetCalendarios.Enabled = True
        End If
    End Sub

    'Private Sub listaReg_Load(sender As Object, e As EventArgs) Handles tablareg.Load
    '    Dim oES As EntradaSalida = New EntradaSalida
    '    Dim tab As DataTable '= oES.obtener_lista
    'End Sub



End Class