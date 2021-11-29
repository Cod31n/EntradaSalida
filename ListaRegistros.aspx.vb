Imports System.ComponentModel
Public Class ListaRegistros
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub


    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click


        divFiltros.Visible = False
        divSelect.Visible = True

        Dim oEs As New EntradaSalida.Filtro
        If Not txtIdRegistro.Text = "" Then
            oEs.id = CInt(txtIdRegistro.Text)
        End If
        If Not txtIdUsuario.Text = "" Then
            oEs.idUsuario = CInt(txtIdUsuario.Text)
        End If
        oEs.inicio_fecha = IIf(calInicio.SelectedDate = Date.MinValue, Nothing, calInicio.SelectedDate)
        oEs.fin_fecha = IIf(calFin.SelectedDate = Date.MinValue, Nothing, calFin.SelectedDate)
        oEs.tipo_solicitud = IIf(comboTipoSolicitud.SelectedValue = "Tipo Solicitud", Nothing, comboTipoSolicitud.SelectedValue)
        Dim filtro As String = oEs.generarfiltro()

        Dim Obj As New EntradaSalida
        Dim tab As New DataTable
        tab = Obj.obtener_lista(filtro)

        For i As Integer = 0 To tab.Rows.Count - 1
            'lboxRegistros.Items.Add(tab(i)(6).ToString)
            Dim itemLista As String = ""
            For x As Integer = 0 To tab.Columns.Count - 1
                If tab(i)(x).ToString IsNot "" Then
                    itemLista = $"{itemLista} - {tab(i)(x).ToString}"
                End If
            Next
            lboxRegistros.Items.Add(itemLista)
        Next

    End Sub

    'Private Sub listaReg_Load(sender As Object, e As EventArgs) Handles tablareg.Load
    '    Dim oES As EntradaSalida = New EntradaSalida

    '    Dim tab As DataTable '= oES.obtener_lista

    '    
    'End Sub



End Class