Imports System.ComponentModel
Imports System.IO
Imports System.Windows.Forms
Imports System.Text

Public Class Copy
    Dim WithEvents worker As BackgroundWorker
    Dim Accent As Boolean = False
    Dim Test As Boolean = True
    Dim Progress_Value As Double = 0
    Dim SFD As New SaveFileDialog
    Dim SW As StreamWriter
    Dim Address As String

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        For i As Integer = 0 To 113
            ComboBox1.Items.Add(MainWindow.S_Name(i))
        Next
        Me.worker = New BackgroundWorker
        worker.WorkerReportsProgress = True
        worker.RunWorkerAsync()
        TaskbarItemInfo.ProgressState = Shell.TaskbarItemProgressState.Normal
        SFD.Filter = "Plain Text(*.Txt) |*.txt"
    End Sub

    Private Sub ComboBox1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles ComboBox1.SelectionChanged
        Test = False
        Progress_Value = 0
        ProgressBar1.Value = 0
        TaskbarItemInfo.ProgressValue = 0
        ComboBox4.Items.Clear()
        ComboBox3.Items.Clear()
        ComboBox2.Items.Clear()
        Test = True
        For i As Integer = 1 To MainWindow.Ayaat(ComboBox1.SelectedIndex).Length - 1
            ComboBox2.Items.Add(i)
        Next
    End Sub

    Private Sub ComboBox2_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles ComboBox2.SelectionChanged
        Progress_Value = 0
        ProgressBar1.Value = 0
        TaskbarItemInfo.ProgressValue = 0
        If (Test = True) Then
            Test = False
            ComboBox3.Items.Clear()
            ComboBox4.Items.Clear()
            Test = True
            For i As Integer = ComboBox1.SelectedIndex To 113
                ComboBox3.Items.Add(MainWindow.S_Name(i))
            Next
        End If
    End Sub

    Private Sub ComboBox2_PreviewMouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles ComboBox2.PreviewMouseUp
        If (ComboBox1.SelectedItem = Nothing) Then
            MessageBox.Show("اختر اسم السورة أولا", "نسخ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.RtlReading)
        End If
    End Sub

    Private Sub ComboBox3_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles ComboBox3.SelectionChanged
        Progress_Value = 0
        ProgressBar1.Value = 0
        TaskbarItemInfo.ProgressValue = 0
        If (Test = True) Then
            Test = False
            ComboBox4.Items.Clear()
            Test = True
            If (ComboBox3.SelectedItem = ComboBox1.SelectedItem) Then
                For i As Integer = ComboBox2.SelectedIndex + 1 To MainWindow.Ayaat(ComboBox1.SelectedIndex).Length - 1
                    ComboBox4.Items.Add(i)
                Next
            Else
                For i As Integer = 1 To MainWindow.Ayaat(ComboBox3.SelectedIndex + ComboBox1.SelectedIndex).Length - 1
                    ComboBox4.Items.Add(i)
                Next
            End If
        End If
    End Sub

    Private Sub ComboBox3_PreviewMouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles ComboBox3.PreviewMouseUp
        If (ComboBox2.SelectedItem = Nothing) Then
            MessageBox.Show("حدد البداية أولا", "نسخ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.RtlReading)
        End If
    End Sub

    Private Sub ComboBox4_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles ComboBox4.SelectionChanged
        Progress_Value = 0
        ProgressBar1.Value = 0
        TaskbarItemInfo.ProgressValue = 0
    End Sub

    Private Sub ComboBox4_PreviewMouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles ComboBox4.PreviewMouseUp
        If (ComboBox3.SelectedItem = Nothing) Then
            MessageBox.Show("اختر اسم السورة أولا", "نسخ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.RtlReading)
        End If
    End Sub

    Private Sub CheckBox1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles CheckBox1.Click
        If (CheckBox1.IsChecked = True) Then
            Accent = True
        Else
            Accent = False
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        If (ComboBox1.SelectedItem = Nothing OrElse ComboBox2.SelectedItem = Nothing OrElse ComboBox3.SelectedItem = Nothing OrElse ComboBox4.SelectedItem = Nothing) Then
            MessageBox.Show("قم بتحديد كل الحقول أولا", "نسخ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.RtlReading)
        Else
            Dim Copy_String As String = Collect()
            Clipboard.SetText(Copy_String)
            ProgressBar1.Value = 100
            TaskbarItemInfo.ProgressValue = 1.0
            CheckBox1.IsEnabled = True
            MessageBox.Show("تم نسخ النص الى الحافظة", "نسخ الى الحافظة", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.RtlReading)
        End If
    End Sub

    Function Replace_Accent(ByVal str As String) As String
        If (Accent = True) Then
            Return str.Replace("َ", "").Replace("اً", "").Replace("ً", "").Replace("ُ", "").Replace("ٌ", "").Replace("ِ", "").Replace("ٍ", "").Replace("ّ", "").Replace("ْ", "")
        Else
            Return str
        End If
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button2.Click
        If (ComboBox1.SelectedItem = Nothing OrElse ComboBox2.SelectedItem = Nothing OrElse ComboBox3.SelectedItem = Nothing OrElse ComboBox4.SelectedItem = Nothing) Then
            MessageBox.Show("قم بتحديد كل الحقول أولا", "نسخ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.RtlReading)
        Else
            If (SFD.ShowDialog = Windows.Forms.DialogResult.OK) Then
                Address = SFD.FileName
                CheckBox1.IsEnabled = False
                Progress_Value = 0
                ProgressBar1.Value = 0
                TaskbarItemInfo.ProgressValue = 0
                Dim x1 As Double = ComboBox1.SelectedIndex
                Dim x2 As Double = ComboBox2.SelectedIndex
                Dim x3 As Double = ComboBox3.SelectedIndex + ComboBox1.SelectedIndex
                Dim x4 As Double
                Try
                    Address = SFD.FileName
                    SW = New StreamWriter(Address)
                    If (x1 = x3) Then
                        x4 = ComboBox4.SelectedIndex + ComboBox2.SelectedIndex
                    Else
                        x4 = ComboBox4.SelectedIndex
                    End If
                    Progress_Value = (x3 - x1 + 1)
                    For i As Integer = x1 To x3
                        If (x1 = x3) Then
                            If (x2 = 0) Then
                                SW.Write(String.Concat("سورة ", MainWindow.S_Name(i), vbCrLf))
                                If (i <> 0 AndAlso i <> 8) Then
                                    SW.Write(String.Concat(Replace_Accent("بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ"), vbCrLf))
                                End If
                            End If
                            For j As Integer = x2 To x4
                                ProgressBar1.Value = ProgressBar1.Value + ((100 / Progress_Value) / (x4 - x2 + 1))
                                TaskbarItemInfo.ProgressValue = ProgressBar1.Value / 100
                                If (j = x2) Then
                                    SW.Write(String.Concat(Replace_Accent(MainWindow.Ayaat(i)(j)), "(", j + 1, ")"))
                                Else
                                    SW.Write(String.Concat(" ", Replace_Accent(MainWindow.Ayaat(i)(j)), "(", j + 1, ")"))
                                End If
                            Next
                        ElseIf (i = x1) Then
                            If (x2 = 0) Then
                                SW.Write(String.Concat("سورة ", MainWindow.S_Name(i), vbCrLf))
                                If (i <> 0 AndAlso i <> 8) Then
                                    SW.Write(String.Concat(Replace_Accent("بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ"), vbCrLf))
                                End If
                            End If
                            For j As Integer = x2 To MainWindow.Ayaat(i).Length - 2
                                ProgressBar1.Value = ProgressBar1.Value + ((100 / Progress_Value) / (MainWindow.Ayaat(i).Length - x2 - 1))
                                TaskbarItemInfo.ProgressValue = ProgressBar1.Value / 100
                                If (j = x2) Then
                                    SW.Write(String.Concat(Replace_Accent(MainWindow.Ayaat(i)(j)), "(", j + 1, ")"))
                                Else
                                    SW.Write(String.Concat(" ", Replace_Accent(MainWindow.Ayaat(i)(j)), "(", j + 1, ")"))
                                End If
                            Next
                        ElseIf (i = x3) Then
                            SW.Write(String.Concat(vbCrLf, "#"))
                            If (i <> 0 AndAlso i <> 8) Then
                                SW.Write(String.Concat("بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ", vbCrLf))
                            End If
                            For j As Integer = 0 To x4
                                ProgressBar1.Value = ProgressBar1.Value + ((100 / Progress_Value) / (x4 + 1))
                                TaskbarItemInfo.ProgressValue = ProgressBar1.Value / 100
                                If (j = 0) Then
                                    SW.Write(String.Concat(Replace_Accent(MainWindow.Ayaat(i)(j)), "(", j + 1, ")"))
                                Else
                                    SW.Write(String.Concat(" ", Replace_Accent(MainWindow.Ayaat(i)(j)), "(", j + 1, ")"))
                                End If
                            Next
                        Else
                            SW.Write(String.Concat(vbCrLf, "سورة ", MainWindow.S_Name(i), vbCrLf))
                            If (i <> 0 AndAlso i <> 8) Then
                                SW.Write(String.Concat(Replace_Accent("بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ"), vbCrLf))
                            End If
                            For j As Integer = 0 To MainWindow.Ayaat(i).Length - 2
                                ProgressBar1.Value = ProgressBar1.Value + ((100 / Progress_Value) / (MainWindow.Ayaat(i).Length - 2))
                                TaskbarItemInfo.ProgressValue = ProgressBar1.Value / 100
                                If (j = 0) Then
                                    SW.Write(String.Concat(Replace_Accent(MainWindow.Ayaat(i)(j)), "(", j + 1, ")"))
                                Else
                                    SW.Write(String.Concat(" ", Replace_Accent(MainWindow.Ayaat(i)(j)), "(", j + 1, ")"))
                                End If
                            Next
                        End If
                    Next
                    ProgressBar1.Value = 100
                    TaskbarItemInfo.ProgressValue = 1.0
                    SW.Close()
                    MessageBox.Show(String.Concat("تم حفظ الملف الى: ", "' ", Address, " '"), "حفظ الملف", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.RtlReading)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "حفظ الملف", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.RtlReading)
                End Try
            End If
        End If
        CheckBox1.IsEnabled = True
    End Sub

    Function Collect() As String
        CheckBox1.IsEnabled = False
        Progress_Value = 0
        ProgressBar1.Value = 0
        TaskbarItemInfo.ProgressValue = 0
        Dim x1 As Double = ComboBox1.SelectedIndex
        Dim x2 As Double = ComboBox2.SelectedIndex
        Dim x3 As Double = ComboBox3.SelectedIndex + ComboBox1.SelectedIndex
        Dim x4 As Double
        If (x1 = x3) Then
            x4 = ComboBox4.SelectedIndex + ComboBox2.SelectedIndex
        Else
            x4 = ComboBox4.SelectedIndex
        End If
        'Dim Copy_String As String = Nothing
        Dim Copy_String As StringBuilder = New StringBuilder()
        Progress_Value = (x3 - x1 + 1)
        For i As Integer = x1 To x3
            If (x1 = x3) Then
                If (x2 = 0) Then
                    Copy_String.Insert(0, "سورة ")
                    Copy_String.Insert(Copy_String.Length, MainWindow.S_Name(i))
                    Copy_String.Insert(Copy_String.Length, vbCrLf)
                    If (i <> 0 AndAlso i <> 8) Then
                        Copy_String.Insert(Copy_String.Length, Replace_Accent("بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ"))
                        Copy_String.Insert(Copy_String.Length, vbCrLf)
                    End If
                End If
                For j As Integer = x2 To x4
                    ProgressBar1.Value = ProgressBar1.Value + ((100 / Progress_Value) / (x4 - x2 + 1))
                    TaskbarItemInfo.ProgressValue = ProgressBar1.Value / 100
                    If (j = x2) Then
                        Copy_String.Insert(Copy_String.Length, Replace_Accent(MainWindow.Ayaat(i)(j)))
                        Copy_String.Insert(Copy_String.Length, "(")
                        Copy_String.Insert(Copy_String.Length, j + 1)
                        Copy_String.Insert(Copy_String.Length, ")")
                    Else
                        Copy_String.Insert(Copy_String.Length, " ")
                        Copy_String.Insert(Copy_String.Length, Replace_Accent(MainWindow.Ayaat(i)(j)))
                        Copy_String.Insert(Copy_String.Length, "(")
                        Copy_String.Insert(Copy_String.Length, j + 1)
                        Copy_String.Insert(Copy_String.Length, ")")
                    End If
                Next
            ElseIf (i = x1) Then
                If (x2 = 0) Then
                    Copy_String.Insert(0, "سورة ")
                    Copy_String.Insert(Copy_String.Length, MainWindow.S_Name(i))
                    Copy_String.Insert(Copy_String.Length, vbCrLf)
                    If (i <> 0 AndAlso i <> 8) Then
                        Copy_String.Insert(Copy_String.Length, Replace_Accent("بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ"))
                        Copy_String.Insert(Copy_String.Length, vbCrLf)
                    End If
                End If
                For j As Integer = x2 To MainWindow.Ayaat(i).Length - 2
                    ProgressBar1.Value = ProgressBar1.Value + ((100 / Progress_Value) / (MainWindow.Ayaat(i).Length - x2 - 1))
                    TaskbarItemInfo.ProgressValue = ProgressBar1.Value / 100
                    If (j = x2) Then
                        Copy_String.Insert(Copy_String.Length, Replace_Accent(MainWindow.Ayaat(i)(j)))
                        Copy_String.Insert(Copy_String.Length, "(")
                        Copy_String.Insert(Copy_String.Length, j + 1)
                        Copy_String.Insert(Copy_String.Length, ")")
                    Else
                        Copy_String.Insert(Copy_String.Length, " ")
                        Copy_String.Insert(Copy_String.Length, Replace_Accent(MainWindow.Ayaat(i)(j)))
                        Copy_String.Insert(Copy_String.Length, "(")
                        Copy_String.Insert(Copy_String.Length, j + 1)
                        Copy_String.Insert(Copy_String.Length, ")")
                    End If
                Next
            ElseIf (i = x3) Then
                Copy_String.Insert(Copy_String.Length, vbCrLf)
                Copy_String.Insert(Copy_String.Length, "سورة ")
                Copy_String.Insert(Copy_String.Length, MainWindow.S_Name(i))
                Copy_String.Insert(Copy_String.Length, vbCrLf)
                If (i <> 0 AndAlso i <> 8) Then
                    Copy_String.Insert(Copy_String.Length, Replace_Accent("بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ"))
                    Copy_String.Insert(Copy_String.Length, vbCrLf)
                End If
                For j As Integer = 0 To x4
                    ProgressBar1.Value = ProgressBar1.Value + ((100 / Progress_Value) / (x4 + 1))
                    TaskbarItemInfo.ProgressValue = ProgressBar1.Value / 100
                    If (j = 0) Then
                        Copy_String.Insert(Copy_String.Length, Replace_Accent(MainWindow.Ayaat(i)(j)))
                        Copy_String.Insert(Copy_String.Length, "(")
                        Copy_String.Insert(Copy_String.Length, j + 1)
                        Copy_String.Insert(Copy_String.Length, ")")
                    Else
                        Copy_String.Insert(Copy_String.Length, " ")
                        Copy_String.Insert(Copy_String.Length, Replace_Accent(MainWindow.Ayaat(i)(j)))
                        Copy_String.Insert(Copy_String.Length, "(")
                        Copy_String.Insert(Copy_String.Length, j + 1)
                        Copy_String.Insert(Copy_String.Length, ")")
                    End If
                Next
            Else
                Copy_String.Insert(Copy_String.Length, vbCrLf)
                Copy_String.Insert(Copy_String.Length, "سورة ")
                Copy_String.Insert(Copy_String.Length, MainWindow.S_Name(i))
                Copy_String.Insert(Copy_String.Length, vbCrLf)
                If (i <> 0 AndAlso i <> 8) Then
                    Copy_String.Insert(Copy_String.Length, Replace_Accent("بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ"))
                    Copy_String.Insert(Copy_String.Length, vbCrLf)
                End If
                For j As Integer = 0 To MainWindow.Ayaat(i).Length - 2
                    ProgressBar1.Value = ProgressBar1.Value + ((100 / Progress_Value) / (MainWindow.Ayaat(i).Length - 2))
                    TaskbarItemInfo.ProgressValue = ProgressBar1.Value / 100
                    If (j = 0) Then
                        Copy_String.Insert(Copy_String.Length, Replace_Accent(MainWindow.Ayaat(i)(j)))
                        Copy_String.Insert(Copy_String.Length, "(")
                        Copy_String.Insert(Copy_String.Length, j + 1)
                        Copy_String.Insert(Copy_String.Length, ")")
                    Else
                        Copy_String.Insert(Copy_String.Length, " ")
                        Copy_String.Insert(Copy_String.Length, Replace_Accent(MainWindow.Ayaat(i)(j)))
                        Copy_String.Insert(Copy_String.Length, "(")
                        Copy_String.Insert(Copy_String.Length, j + 1)
                        Copy_String.Insert(Copy_String.Length, ")")
                    End If
                Next
            End If
        Next
        Return Copy_String.ToString()
    End Function
End Class