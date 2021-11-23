Public Class Baja
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles btnSolicitudes.Click
        Response.Redirect("Solicitudes")
    End Sub

    Private Sub btnVolver_Click(sender As Object, e As ImageClickEventArgs) Handles btnVolver.Click
        Response.Redirect("Entrada_Salida")
    End Sub
End Class