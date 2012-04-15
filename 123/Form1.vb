Imports System.String
Imports System.IO
Public Class Form1
    Dim page As Integer = 0
    Private Sub datagr()
        DataGridView1.DataSource = r.FillingDataView("Select * from dd")
    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Button2.Enabled = False
        TxtFind.Text = page
        Cn.Open()
        r.Start(Me)
        datagr()
    End Sub

    Private Sub DataGridView1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        TxtFind.Text = DataGridView1.Rows(e.RowIndex).Cells(0).Value
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        page = page + 1
        If (page = 21) Then
            Button1.Enabled = False
        End If
        If (page = 2) Then
            Button2.Enabled = True
        End If
        TxtFind.Text = page
        Dim v As Boolean = r.FindInPk(TxtFind.Text)
        Sname.Text = "سورة " + Sname.Text
    End Sub

    Private Sub Button2_Click_1(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        page = page - 1
        If (page = 1) Then
            Button2.Enabled = False
        End If
        TxtFind.Text = page
        Dim v As Boolean = r.FindInPk(TxtFind.Text)
        Sname.Text = "سورة " + Sname.Text
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        page = 1
        Button1.Enabled = True
        Button2.Enabled = False
        TxtFind.Text = page
        Dim v As Boolean = r.FindInPk(TxtFind.Text)
        Sname.Text = "سورة " + Sname.Text
    End Sub

    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles Button4.Click
        page = 21
            Button1.Enabled = False
            Button2.Enabled = True
        TxtFind.Text = page
        Dim v As Boolean = r.FindInPk(TxtFind.Text)
        Sname.Text = "سورة " + Sname.Text
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        Try
            If (TextBox1.Text > 21 Or TextBox1.Text < 1) Then
                MessageBox.Show("رقم صفحة خاطئ")
            Else
                page = TextBox1.Text
                If (page = 21) Then
                    Button1.Enabled = False
                    Button2.Enabled = True
                ElseIf (page = 1) Then
                    Button2.Enabled = False
                    Button1.Enabled = True
                ElseIf (page < 21 Or page > 1) Then
                    Button1.Enabled = True
                    Button2.Enabled = True
                End If
                TxtFind.Text = page
                Dim v As Boolean = r.FindInPk(TxtFind.Text)
                Sname.Text = "سورة " + Sname.Text
            End If
        Catch ex As Exception
            MsgBox("أدخل رقم صحيح")
        End Try
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        FontDialog1.ShowDialog()
        Address.Font = FontDialog1.Font
    End Sub

    Private Sub Button8_Click(sender As System.Object, e As System.EventArgs) Handles Button8.Click
        ColorDialog1.ShowDialog()
        Address.ForeColor = ColorDialog1.Color
    End Sub

    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        Dim str As String = TextBox2.Text
        Dim f As Integer
        f = InStr(Address.SelectionStart + 2, Address.Text, str) - 1
        Address.SelectionStart = f
        Address.SelectionLength = str.Length
        If f = -1 Then
            MsgBox("Pas de résultats..", MsgBoxStyle.Exclamation, "Résultats de recherche")
        Else
            Address.SelectionStart = f
            Address.SelectionLength = str.Length
        End If
    End Sub

    Private Sub Button3_Mouseleave(sender As Object, e As System.EventArgs) Handles Button3.MouseEnter
        ToolStripStatusLabel1.Text = "العودة الى أول صفحة"
    End Sub
    Private Sub Button2_MouseEnter(sender As Object, e As System.EventArgs) Handles Button2.MouseEnter
        ToolStripStatusLabel1.Text = "الصفحة السابقة"
    End Sub
    Private Sub Button1_MouseEnter(sender As Object, e As System.EventArgs) Handles Button1.MouseEnter
        ToolStripStatusLabel1.Text = "الصفحة التالية"
    End Sub
    Private Sub Button4_MouseEnter(sender As Object, e As System.EventArgs) Handles Button4.MouseEnter
        ToolStripStatusLabel1.Text = "الذهاب الى الصفحة الأخيرة"
    End Sub
    Private Sub Button5_MouseEnter(sender As Object, e As System.EventArgs) Handles Button5.MouseEnter
        ToolStripStatusLabel1.Text = "اذهب الى الصفحة"
    End Sub
    Private Sub Button7_MouseEnter(sender As Object, e As System.EventArgs) Handles Button7.MouseEnter
        ToolStripStatusLabel1.Text = "تغيير الخط"
    End Sub
    Private Sub Button8_MouseEnter(sender As Object, e As System.EventArgs) Handles Button8.MouseEnter
        ToolStripStatusLabel1.Text = "تغيير لون الخط"
    End Sub
    Private Sub Button6_MouseEnter(sender As Object, e As System.EventArgs) Handles Button6.MouseEnter
        ToolStripStatusLabel1.Text = "ابحث داخل الصفحة الحالية"
    End Sub

    Private Sub Button3_MouseEnter(sender As Object, e As System.EventArgs) Handles Button3.MouseLeave
        ToolStripStatusLabel1.Text = "جاهز"
    End Sub
    Private Sub Button2_MouseLeave(sender As Object, e As System.EventArgs) Handles Button2.MouseLeave
        ToolStripStatusLabel1.Text = "جاهز"
    End Sub
    Private Sub Button1_MouseLeave(sender As Object, e As System.EventArgs) Handles Button1.MouseLeave
        ToolStripStatusLabel1.Text = "جاهز"
    End Sub
    Private Sub Button4_MouseLeave(sender As Object, e As System.EventArgs) Handles Button4.MouseLeave
        ToolStripStatusLabel1.Text = "جاهز"
    End Sub
    Private Sub Button5_MouseLeave(sender As Object, e As System.EventArgs) Handles Button5.MouseLeave
        ToolStripStatusLabel1.Text = "جاهز"
    End Sub
    Private Sub Button7_MouseLeave(sender As Object, e As System.EventArgs) Handles Button7.MouseLeave
        ToolStripStatusLabel1.Text = "جاهز"
    End Sub
    Private Sub Button8_MouseLeave(sender As Object, e As System.EventArgs) Handles Button8.MouseLeave
        ToolStripStatusLabel1.Text = "جاهز"
    End Sub
    Private Sub Button6_MouseLeave(sender As Object, e As System.EventArgs) Handles Button6.MouseLeave
        ToolStripStatusLabel1.Text = "جاهز"
    End Sub

    Private Sub Button9_Click(sender As System.Object, e As System.EventArgs) Handles Button9.Click
        Dim f As Integer
        f = InStr(Address.SelectionStart + 2, Address.Text, TextBox2.Text) - 1
        MsgBox(f)
        If f = -1 Then
            MsgBox("Pas de résultats..", MsgBoxStyle.Exclamation, "Résultats de recherche")
        Else
            Address.SelectionStart = f
            Address.SelectionLength = TextBox2.Text.Length
        End If
    End Sub
End Class
