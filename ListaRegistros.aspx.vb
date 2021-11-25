Public Class ListaRegistros
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


    End Sub

    Private Sub listaReg_Load(sender As Object, e As EventArgs) Handles tablareg.Load
        Dim oES As EntradaSalida = New EntradaSalida

        Dim tab As DataTable = oES.obtener_lista

        For i As Integer = 0 To tab.Rows.Count - 1
            For x As Integer = 0 To tab.Columns.Count - 1
                'tablareg. (tab(i)(x))


            Next
        Next
    End Sub
End Class