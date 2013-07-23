Imports System.Drawing

Public Class Fonts
    Dim BrushesNames() As String = {"AliceBlue", "AntiqueWhite", "Aqua", "Aquamarine", "Azure", "Beige", "Bisque", "Black", "BlanchedAlmond", "Blue", "BlueViolet", "Brown", "BurlyWood", "CadetBlue", "Chartreuse", "Chocolate", "Coral", "CornflowerBlue", "Cornsilk", "Crimson", "Cyan", "DarkBlue", "DarkCyan", "DarkGoldenrod", "DarkGray", "DarkGreen", "DarkKhaki", "DarkMagenta", "DarkOliveGreen", "DarkOrange", "DarkOrchid", "DarkRed", "DarkSalmon", "DarkSeaGreen", "DarkSlateBlue", "DarkSlateGray", "DarkTurquoise", "DarkViolet", "DeepPink", "DeepSkyBlue", "DimGray", "DodgerBlue", "Firebrick", "FloralWhite", "ForestGreen", "Fuchsia", "Gainsboro", "GhostWhite", "Gold", "Goldenrod", "Gray", "Green", "GreenYellow", "Honeydew", "HotPink", "IndianRed", "Indigo", "Ivory", "Khaki", "Lavender", "LavenderBlush", "LawnGreen", "LemonChiffon", "LightBlue", "LightCoral", "LightCyan", "LightGoldenrodYellow", "LightGray", "LightGreen", "LightPink", "LightSalmon", "LightSeaGreen", "LightSkyBlue", "LightSlateGray", "LightSteelBlue", "LightYellow", "Lime", "LimeGreen", "Linen", "Magenta", "Maroon", "MediumAquamarine", "MediumBlue", "MediumOrchid", "MediumPurple", "MediumSeaGreen", "MediumSlateBlue", "MediumSpringGreen", "MediumTurquoise", "MediumVioletRed", "MidnightBlue", "MintCream", "MistyRose", "Moccasin", "NavajoWhite", "Navy", "OldLace", "Olive", "OliveDrab", "Orange", "OrangeRed", "Orchid", "PaleGoldenrod", "PaleGreen", "PaleTurquoise", "PaleVioletRed", "PapayaWhip", "PeachPuff", "Peru", "Pink", "Plum", "PowderBlue", "Purple", "Red", "RosyBrown", "RoyalBlue", "SaddleBrown", "Salmon", "SandyBrown", "SeaGreen", "SeaShell", "Sienna", "Silver", "SkyBlue", "SlateBlue", "SlateGray", "Snow", "SpringGreen", "SteelBlue", "Tan", "Teal", "Thistle", "Tomato", "Turquoise", "Violet", "Wheat", "White", "WhiteSmoke", "Yellow", "YellowGreen"}
    Private Sub Window_Loaded_1(sender As Object, e As RoutedEventArgs)
        For Each i In System.Drawing.FontFamily.Families
            Dim tmp As ComboBoxItem = New ComboBoxItem()
            tmp.Content = i.Name + "  (أبجد هوز حطي كلمن سعفص قرشت ثخذ ضظغ 0123456789)"
            tmp.FontFamily = New Windows.Media.FontFamily(i.Name)
            tmp.FontSize = 14
            FontsNames.Items.Add(tmp)
        Next
        For i As Integer = 6 To 72 Step 2
            FontSizes.Items.Add(i)
        Next
        For i As Integer = 0 To BrushesNames.Length - 1
            Dim tmp As ComboBoxItem = New ComboBoxItem()
            tmp.Foreground = MainWindow.WMBrushes(i)
            tmp.Content = BrushesNames(i)
            FontColors.Items.Add(tmp)
        Next
        FontsNames.SelectedItem = MainWindow.Font + "  (أبجد هوز حطي كلمن سعفص قرشت ثخذ ضظغ 0123456789)"
        FontSizes.SelectedItem = MainWindow.FSize
        FontColors.SelectedIndex = MainWindow.FColor
        If (FontsNames.SelectedIndex = -1) Then
            FontsNames.SelectedIndex = 0
        End If
    End Sub

    Private Sub Save_Setting(sender As Object, e As RoutedEventArgs)
        'MainWindow.Font = Strings.Left( DirectCast(FontsNames.Items(FontsNames.SelectedIndex), ComboBoxItem).Content,Strings.
        Integer.TryParse(FontSizes.SelectedItem, MainWindow.FSize)
        MainWindow.FColor = FontColors.SelectedIndex
        Me.Close()
    End Sub

    Private Sub Cancel(sender As Object, e As RoutedEventArgs)
        Close()
    End Sub
End Class
