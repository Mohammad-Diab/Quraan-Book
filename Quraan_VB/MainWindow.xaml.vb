Imports System.Text
Imports System.IO

Class MainWindow
    Public Shared Ayaat(114)() As String
    Public Shared S_Name(114) As String
    Public Shared S_Name_With_Index(114) As String
    Public Shared Font As String
    Public Shared FSize As Int32
    Public Shared FColor As Int32
    Public Shared Sura_Index As Integer = -1
    Public Shared WMBrushes() As Windows.Media.Brush = {Windows.Media.Brushes.AliceBlue, Windows.Media.Brushes.AntiqueWhite, Windows.Media.Brushes.Aqua, Windows.Media.Brushes.Aquamarine, Windows.Media.Brushes.Azure, Windows.Media.Brushes.Beige, Windows.Media.Brushes.Bisque, Windows.Media.Brushes.Black, Windows.Media.Brushes.BlanchedAlmond, Windows.Media.Brushes.Blue, Windows.Media.Brushes.BlueViolet, Windows.Media.Brushes.Brown, Windows.Media.Brushes.BurlyWood, Windows.Media.Brushes.CadetBlue, Windows.Media.Brushes.Chartreuse, Windows.Media.Brushes.Chocolate, Windows.Media.Brushes.Coral, Windows.Media.Brushes.CornflowerBlue, Windows.Media.Brushes.Cornsilk, Windows.Media.Brushes.Crimson, Windows.Media.Brushes.Cyan, Windows.Media.Brushes.DarkBlue, Windows.Media.Brushes.DarkCyan, Windows.Media.Brushes.DarkGoldenrod, Windows.Media.Brushes.DarkGray, Windows.Media.Brushes.DarkGreen, Windows.Media.Brushes.DarkKhaki, Windows.Media.Brushes.DarkMagenta, Windows.Media.Brushes.DarkOliveGreen, Windows.Media.Brushes.DarkOrange, Windows.Media.Brushes.DarkOrchid, Windows.Media.Brushes.DarkRed, Windows.Media.Brushes.DarkSalmon, Windows.Media.Brushes.DarkSeaGreen, Windows.Media.Brushes.DarkSlateBlue, Windows.Media.Brushes.DarkSlateGray, Windows.Media.Brushes.DarkTurquoise, Windows.Media.Brushes.DarkViolet, Windows.Media.Brushes.DeepPink, Windows.Media.Brushes.DeepSkyBlue, Windows.Media.Brushes.DimGray, Windows.Media.Brushes.DodgerBlue, Windows.Media.Brushes.Firebrick, Windows.Media.Brushes.FloralWhite, Windows.Media.Brushes.ForestGreen, Windows.Media.Brushes.Fuchsia, Windows.Media.Brushes.Gainsboro, Windows.Media.Brushes.GhostWhite, Windows.Media.Brushes.Gold, Windows.Media.Brushes.Goldenrod, Windows.Media.Brushes.Gray, Windows.Media.Brushes.Green, Windows.Media.Brushes.GreenYellow, Windows.Media.Brushes.Honeydew, Windows.Media.Brushes.HotPink, Windows.Media.Brushes.IndianRed, Windows.Media.Brushes.Indigo, Windows.Media.Brushes.Ivory, Windows.Media.Brushes.Khaki, Windows.Media.Brushes.Lavender, Windows.Media.Brushes.LavenderBlush, Windows.Media.Brushes.LawnGreen, Windows.Media.Brushes.LemonChiffon, Windows.Media.Brushes.LightBlue, Windows.Media.Brushes.LightCoral, Windows.Media.Brushes.LightCyan, Windows.Media.Brushes.LightGoldenrodYellow, Windows.Media.Brushes.LightGray, Windows.Media.Brushes.LightGreen, Windows.Media.Brushes.LightPink, Windows.Media.Brushes.LightSalmon, Windows.Media.Brushes.LightSeaGreen, Windows.Media.Brushes.LightSkyBlue, Windows.Media.Brushes.LightSlateGray, Windows.Media.Brushes.LightSteelBlue, Windows.Media.Brushes.LightYellow, Windows.Media.Brushes.Lime, Windows.Media.Brushes.LimeGreen, Windows.Media.Brushes.Linen, Windows.Media.Brushes.Magenta, Windows.Media.Brushes.Maroon, Windows.Media.Brushes.MediumAquamarine, Windows.Media.Brushes.MediumBlue, Windows.Media.Brushes.MediumOrchid, Windows.Media.Brushes.MediumPurple, Windows.Media.Brushes.MediumSeaGreen, Windows.Media.Brushes.MediumSlateBlue, Windows.Media.Brushes.MediumSpringGreen, Windows.Media.Brushes.MediumTurquoise, Windows.Media.Brushes.MediumVioletRed, Windows.Media.Brushes.MidnightBlue, Windows.Media.Brushes.MintCream, Windows.Media.Brushes.MistyRose, Windows.Media.Brushes.Moccasin, Windows.Media.Brushes.NavajoWhite, Windows.Media.Brushes.Navy, Windows.Media.Brushes.OldLace, Windows.Media.Brushes.Olive, Windows.Media.Brushes.OliveDrab, Windows.Media.Brushes.Orange, Windows.Media.Brushes.OrangeRed, Windows.Media.Brushes.Orchid, Windows.Media.Brushes.PaleGoldenrod, Windows.Media.Brushes.PaleGreen, Windows.Media.Brushes.PaleTurquoise, Windows.Media.Brushes.PaleVioletRed, Windows.Media.Brushes.PapayaWhip, Windows.Media.Brushes.PeachPuff, Windows.Media.Brushes.Peru, Windows.Media.Brushes.Pink, Windows.Media.Brushes.Plum, Windows.Media.Brushes.PowderBlue, Windows.Media.Brushes.Purple, Windows.Media.Brushes.Red, Windows.Media.Brushes.RosyBrown, Windows.Media.Brushes.RoyalBlue, Windows.Media.Brushes.SaddleBrown, Windows.Media.Brushes.Salmon, Windows.Media.Brushes.SandyBrown, Windows.Media.Brushes.SeaGreen, Windows.Media.Brushes.SeaShell, Windows.Media.Brushes.Sienna, Windows.Media.Brushes.Silver, Windows.Media.Brushes.SkyBlue, Windows.Media.Brushes.SlateBlue, Windows.Media.Brushes.SlateGray, Windows.Media.Brushes.Snow, Windows.Media.Brushes.SpringGreen, Windows.Media.Brushes.SteelBlue, Windows.Media.Brushes.Tan, Windows.Media.Brushes.Teal, Windows.Media.Brushes.Thistle, Windows.Media.Brushes.Tomato, Windows.Media.Brushes.Turquoise, Windows.Media.Brushes.Violet, Windows.Media.Brushes.Wheat, Windows.Media.Brushes.White, Windows.Media.Brushes.WhiteSmoke, Windows.Media.Brushes.Yellow, Windows.Media.Brushes.YellowGreen}
    Dim DecodedList As List(Of Boolean)

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        Dim Quraan As String = Read_data_strucure()
        Dim Suras As String = My.Resources.Sura
        Dim Suras_With_Index As String = My.Resources.Sura_With_Index
        Dim Sura() As String = Quraan.Split("#")
        S_Name = Suras.Split("#")
        S_Name_With_Index = Suras_With_Index.Split("#")
        For i As Integer = 0 To 113
            Ayaat(i) = Sura(i).Split("@")
        Next
        Quraan = Nothing
        Suras = Nothing
        Suras_With_Index = Nothing
        For i As Integer = 0 To 113
            Sura(i) = Nothing
        Next
    End Sub

    Private Sub F2_PreviewKeyUp(sender As Object, e As KeyEventArgs)
        If (e.Key = Key.F2) Then
            Dim window As Choose = New Choose()
            window.ShowDialog()
            ChangeSura()
        End If
    End Sub

    Private Sub ChangeSura()
        If (Sura_Index > -1) Then
            If (Sura_Index = 8 OrElse Sura_Index = 0) Then
                TextBox1.Text = Nothing
            Else
                TextBox1.Text = String.Concat("بِسْمِ اللَّهِ الرَّحْمَنِ الرَّحِيمِ", vbCrLf)
            End If
            Dim tmp As StringBuilder = New StringBuilder()
            For i As Integer = 0 To Ayaat(Sura_Index).Length - 2
                tmp.Insert(tmp.Length, Ayaat(Sura_Index)(i))
                tmp.Insert(tmp.Length, "(")
                tmp.Insert(tmp.Length, i + 1)
                tmp.Insert(tmp.Length, ")")
            Next
            TextBox1.Text = String.Concat(TextBox1.Text, tmp)
            Label3.Text = String.Concat("سورة: ", S_Name(Sura_Index))
            Label3.Visibility = Visibility.Visible
            Label4.Text = String.Concat("ترتيبها: ", Sura_Index + 1)
            Label4.Visibility = Visibility.Visible
            Label5.Text = String.Concat("عدد آياتها: ", Ayaat(Sura_Index).Length - 1)
            Label5.Visibility = Visibility.Visible
        End If
    End Sub

    Private Sub Choose_Click(sender As Object, e As RoutedEventArgs)
        Dim window As Choose = New Choose()
        window.ShowDialog()
        ChangeSura()
    End Sub

    Private Sub Font_Click(sender As Object, e As RoutedEventArgs)
        If (IsNothing(Font)) Then
            FColor = 100
            FSize = 22
            Font = "KacstOne"
        End If
        Dim window As Fonts = New Fonts()
        window.ShowDialog()
        TextBox1.Foreground = WMBrushes(FColor)
        TextBox1.FontSize = FSize
        TextBox1.FontFamily = New FontFamily(Font)
    End Sub

    Private Function Read_data_strucure() As String
        Dim count As Integer
        DecodedList = New List(Of Boolean)
        Dim DecodedText As List(Of UInt16) = New List(Of UInt16)
        Dim DecompredText As StringBuilder = New StringBuilder(String.Empty)
        Try
            Dim DeSerializer As BinaryReader = New BinaryReader(File.OpenRead("Quraan_File.ctxt"))
            DeSerializer.ReadString()
            DeSerializer.ReadBoolean()
            DeSerializer.ReadString()
            DeSerializer.ReadBoolean()
            DeSerializer.ReadString()
            DeSerializer.ReadBoolean()
            count = DeSerializer.ReadInt32()
            DeSerializer.ReadBoolean()
            For i As Integer = 0 To count - 1
                DecodedText.Add(DeSerializer.ReadUInt16())
            Next
            For Each i In DecodedText
                Int16toBinary(i)
            Next
            For i As Integer = 0 To (DecodedList.Count / 6) - 2
                If (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append(" ")
                ElseIf (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ا")
                ElseIf (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ب")
                ElseIf (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ت")
                ElseIf (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ث")
                ElseIf (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ج")
                ElseIf (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ح")
                ElseIf (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("خ")
                ElseIf (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("د")
                ElseIf (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ذ")
                ElseIf (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ر")
                ElseIf (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ز")
                ElseIf (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("س")
                ElseIf (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ش")
                ElseIf (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ص")
                ElseIf (Not DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ض")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ط")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ظ")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ع")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("غ")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ف")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ق")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ك")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ل")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("م")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ن")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ه")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("و")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ي")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("أ")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("إ")
                ElseIf (Not DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("آ")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ى")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ة")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ء")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ؤ")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ئ")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append(",")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append(".")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append(")")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("(")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("0")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("1")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("2")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("3")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("4")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("5")
                ElseIf (DecodedList(i * 6) AndAlso Not DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("6")
                ElseIf (DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("7")
                ElseIf (DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("8")
                ElseIf (DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("9")
                ElseIf (DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("َ")
                ElseIf (DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ُ")
                ElseIf (DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ِ")
                ElseIf (DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ْ")
                ElseIf (DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso Not DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ً")
                ElseIf (DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ٌ")
                ElseIf (DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ْ")
                ElseIf (DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("ّ")
                ElseIf (DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso Not DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("\n")
                ElseIf (DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("@")
                ElseIf (DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso Not DecodedList(i * 6 + 4) AndAlso DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("#")
                ElseIf (DecodedList(i * 6) AndAlso DecodedList(i * 6 + 1) AndAlso DecodedList(i * 6 + 2) AndAlso DecodedList(i * 6 + 3) AndAlso DecodedList(i * 6 + 4) AndAlso Not DecodedList(i * 6 + 5)) Then
                    DecompredText.Append("\t")
                End If
            Next
            Return DecompredText.ToString()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error)
            End
        End Try
    End Function

    Private Sub Int16toBinary(i As UShort)
        Dim tmp(15) As Boolean
        tmp(15) = (i Mod 2 = 1)
        If (i Mod 2 = 1) Then
            i = (i - 1) / 2
        Else
            i = i / 2
        End If
        tmp(14) = (i Mod 2 = 1)
        If (i Mod 2 = 1) Then
            i = (i - 1) / 2
        Else
            i = i / 2
        End If
        tmp(13) = (i Mod 2 = 1)
        If (i Mod 2 = 1) Then
            i = (i - 1) / 2
        Else
            i = i / 2
        End If
        tmp(12) = (i Mod 2 = 1)
        If (i Mod 2 = 1) Then
            i = (i - 1) / 2
        Else
            i = i / 2
        End If
        tmp(11) = (i Mod 2 = 1)
        If (i Mod 2 = 1) Then
            i = (i - 1) / 2
        Else
            i = i / 2
        End If
        tmp(10) = (i Mod 2 = 1)
        If (i Mod 2 = 1) Then
            i = (i - 1) / 2
        Else
            i = i / 2
        End If
        tmp(9) = (i Mod 2 = 1)
        If (i Mod 2 = 1) Then
            i = (i - 1) / 2
        Else
            i = i / 2
        End If
        tmp(8) = (i Mod 2 = 1)
        If (i Mod 2 = 1) Then
            i = (i - 1) / 2
        Else
            i = i / 2
        End If
        tmp(7) = (i Mod 2 = 1)
        If (i Mod 2 = 1) Then
            i = (i - 1) / 2
        Else
            i = i / 2
        End If
        tmp(6) = (i Mod 2 = 1)
        If (i Mod 2 = 1) Then
            i = (i - 1) / 2
        Else
            i = i / 2
        End If
        tmp(5) = (i Mod 2 = 1)
        If (i Mod 2 = 1) Then
            i = (i - 1) / 2
        Else
            i = i / 2
        End If
        tmp(4) = (i Mod 2 = 1)
        If (i Mod 2 = 1) Then
            i = (i - 1) / 2
        Else
            i = i / 2
        End If
        tmp(3) = (i Mod 2 = 1)
        If (i Mod 2 = 1) Then
            i = (i - 1) / 2
        Else
            i = i / 2
        End If
        tmp(2) = (i Mod 2 = 1)
        If (i Mod 2 = 1) Then
            i = (i - 1) / 2
        Else
            i = i / 2
        End If
        tmp(1) = (i Mod 2 = 1)
        If (i Mod 2 = 1) Then
            i = (i - 1) / 2
        Else
            i = i / 2
        End If
        tmp(0) = (i Mod 2 = 1)
        For n As Integer = 0 To 15
            DecodedList.Add(tmp(n))
        Next
    End Sub

    Private Sub Find_Click(sender As Object, e As RoutedEventArgs)
        Dim window As Find = New Find()
        window.ShowDialog()
    End Sub

    Private Sub Copy_Click(sender As Object, e As RoutedEventArgs)
        Dim window As Copy = New Copy()
        window.ShowDialog()
    End Sub

    Private Sub About_Click(sender As Object, e As RoutedEventArgs)
        Dim window As About = New About()
        window.ShowDialog()
    End Sub

    Private Sub Close_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub
End Class