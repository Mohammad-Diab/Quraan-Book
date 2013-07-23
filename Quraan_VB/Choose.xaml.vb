Public Class Choose

    Private Sub Window_Loaded_1(sender As Object, e As RoutedEventArgs)
        For i As Integer = 0 To 113
            ComboBox1.Items.Add(MainWindow.S_Name_With_Index(i))
        Next
        ComboBox1.SelectedIndex = MainWindow.Sura_Index
    End Sub

    Private Sub Cancel(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

    Private Sub Save_Setting(sender As Object, e As RoutedEventArgs)
        MainWindow.Sura_Index = ComboBox1.SelectedIndex
        Me.Close()
    End Sub
End Class
