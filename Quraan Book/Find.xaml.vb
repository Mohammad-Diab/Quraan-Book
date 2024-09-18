Public Class Find
    Dim Search_In As Integer = 0
    Dim Search_In1 As Integer = 0
    Dim Resulti(9999) As Integer
    Dim Resultj(9999) As Integer
    Dim Result_Index As Integer = 0
    Dim Hamza As Boolean = False
    Dim Word As Boolean = False
    Dim Accent As Boolean = True

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        ComboBox1.Items.Add("كامل المصحف")
        ComboBox1.SelectedIndex = 0
        For i As Integer = 0 To 113
            ComboBox1.Items.Add(MainWindow.S_Name_With_Index(i))
        Next
    End Sub

    Private Sub ComboBox1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles ComboBox1.SelectionChanged
        Search_In = 200
        ListBox1.Items.Clear()
        Search_In = ComboBox1.SelectedIndex
        Label4.Content = "اسم السورة: "
        Label5.Content = "رقم الآية: "
        Label6.Content = "عدد النتائج: "
    End Sub

    Private Sub ListBox1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles ListBox1.SelectionChanged
        If (Search_In = 0) Then
            Label4.Content = String.Concat("اسم السورة: ", MainWindow.S_Name(Resulti(ListBox1.SelectedIndex)))
            Label5.Content = String.Concat("رقم الآية: ", Resultj(ListBox1.SelectedIndex) + 1)
        ElseIf (Search_In < 150) Then
            Label4.Content = String.Concat("اسم السورة: ", MainWindow.S_Name(Search_In - 1))
            Label5.Content = String.Concat("رقم الآية: ", Resultj(ListBox1.SelectedIndex) + 1)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        If (TextBox1.Text = "") Then
            MessageBox.Show("أدخل كلمة للبحث", "البحث", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.RtlReading)
        ElseIf (TextBox1.Text.Length < 2) Then
            MessageBox.Show("كلمة البحث قصيرة جدا", "البحث", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, MessageBoxOptions.RtlReading)
        Else
            Label4.Content = "اسم السورة: "
            Label5.Content = "رقم الآية: "
            Search_In1 = Search_In
            Search_In = 200
            ListBox1.Items.Clear()
            Search_In = Search_In1
            For i As Integer = 0 To Resulti.Length - 1
                Resulti(i) = 0
            Next
            For i As Integer = 0 To Resultj.Length - 1
                Resultj(i) = 0
            Next
            Result_Index = 0
            Dim Search_Word As String = TextBox1.Text
            If (Search_In > 0) Then
                If (Word = True) Then
                    For j As Integer = 0 To MainWindow.Ayaat(Search_In - 1).Length - 1
                        If (Search_By_Word(MainWindow.Ayaat(Search_In - 1)(j), Search_Word) = True) Then
                            Resultj(Result_Index) = j
                            Result_Index = Result_Index + 1
                            Label6.Content = String.Concat("عدد النتائج: ", Result_Index)
                        End If
                    Next
                Else
                    For j As Integer = 0 To MainWindow.Ayaat(Search_In - 1).Length - 1
                        If (Search_By_Text(MainWindow.Ayaat(Search_In - 1)(j), Search_Word) = True) Then
                            Resultj(Result_Index) = j
                            Result_Index = Result_Index + 1
                            Label6.Content = String.Concat("عدد النتائج: ", Result_Index)
                        End If
                    Next
                End If
                If (Result_Index <> 0) Then
                    For i As Integer = 0 To Result_Index - 1
                        ListBox1.Items.Add(String.Concat(i + 1, "-", MainWindow.Ayaat(Search_In - 1)(Resultj(i)), "(", Resultj(i) + 1, ")"))
                    Next
                Else
                    MessageBox.Show("لا يوجد نتائج", "البحث", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading)
                    Label6.Content = "عدد النتائج: 0"
                End If
            Else
                If (Word = True) Then
                    For i As Integer = 0 To MainWindow.Ayaat.Length - 2
                        For j As Integer = 0 To MainWindow.Ayaat(i).Length - 1
                            If (Search_By_Word(MainWindow.Ayaat(i)(j), Search_Word) = True) Then
                                Resulti(Result_Index) = i
                                Resultj(Result_Index) = j
                                Result_Index = Result_Index + 1
                                Label6.Content = String.Concat("عدد النتائج: ", Result_Index)
                            End If
                        Next
                    Next
                Else
                    For i As Integer = 0 To MainWindow.Ayaat.Length - 2
                        For j As Integer = 0 To MainWindow.Ayaat(i).Length - 1
                            If (Search_By_Text(MainWindow.Ayaat(i)(j), Search_Word) = True) Then
                                Resulti(Result_Index) = i
                                Resultj(Result_Index) = j
                                Result_Index = Result_Index + 1
                                Label6.Content = String.Concat("عدد النتائج: ", Result_Index)
                            End If
                        Next
                    Next
                End If
                If (Result_Index <> 0) Then
                    For i As Integer = 0 To Result_Index - 1
                        ListBox1.Items.Add(String.Concat(i + 1, "-", MainWindow.Ayaat(Resulti(i))(Resultj(i)), "(", Resultj(i) + 1, ")"))
                    Next
                Else
                    MessageBox.Show("لا يوجد نتائج", "البحث", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.RtlReading)
                    Label6.Content = "عدد النتائج: 0"
                End If
            End If
        End If
    End Sub

    Function Search_By_Word(ByVal In_Text As String, ByVal Find_Text As String) As Boolean
        Dim Tmp1 As String = Replace_Hamza(Replace_Accent(In_Text))
        Dim Tmp2 As String = Replace_Hamza(Replace_Accent(Find_Text))
        Dim Words() As String
        Words = Tmp1.Split(" ")
        For i As Integer = 0 To Words.Length - 1
            If (String.Compare(Words(i), Tmp2) = 0) Then
                Return True
            End If
        Next
        Return False
    End Function

    Function Search_By_Text(ByVal In_Text As String, ByVal Find_Text As String) As Boolean
        Dim Tmp1 As String = Replace_Hamza(Replace_Accent(In_Text))
        Dim Tmp2 As String = Replace_Hamza(Replace_Accent(Find_Text))
        If (Tmp1.Contains(Tmp2) = True) Then
            Return True
        Else
            Return False
        End If
    End Function

    Function Replace_Hamza(ByVal str As String) As String
        If (Hamza = True) Then
            Return str.Replace("أ", "ا").Replace("إ", "ا").Replace("آ", "ا")
        Else
            Return str
        End If
    End Function

    Function Replace_Accent(ByVal str As String) As String
        If (Accent = True) Then
            Return str.Replace("َ", "").Replace("اً", "").Replace("ً", "").Replace("ُ", "").Replace("ٌ", "").Replace("ِ", "").Replace("ٍ", "").Replace("ّ", "").Replace("ْ", "")
        Else
            Return str
        End If
    End Function

    Private Sub CheckBox1_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles CheckBox1.Click
        If (CheckBox1.IsChecked = True) Then
            Hamza = True
        Else
            Hamza = False
        End If
    End Sub

    Private Sub CheckBox2_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles CheckBox2.Click
        If (CheckBox2.IsChecked = True) Then
            Word = True
        Else
            Word = False
        End If
    End Sub

    Private Sub CheckBox3_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles CheckBox3.Click
        If (CheckBox3.IsChecked = True) Then
            Accent = True
        Else
            Accent = False
        End If
    End Sub

End Class
