# Quraan Book

An Arabic Windows app for reading the Quran (المصحف الالكتروني). It shows one sura at a time in the KACST Quran font, searches the whole Quran, and copies or saves ayas. Written in C# with WPF on .NET Framework 4.0.

## Features

- **Reading:** one sura at a time, with previous and next buttons and a sura list (**اختيار سورة**, F2).
- **Quick search:** the search box above the text highlights every match in the current sura.
- **Search** (**بحث**, Ctrl+F): finds one or more words in the whole Quran and lists the matching ayas with the sura name, aya number and number of results. Options: ignore the differences between hamza forms, match whole words only, and ignore diacritics.
- **Copy** (**نسخ**, F4): pick a sura and its ayas, then copy them to the clipboard or save them as HTML, Word (.docx), XPS, OpenDocument (.odt), RTF or plain text, with or without diacritics.
- **Font** (**الخط**, F5): font, size, color, bold and italic of the text.
- **RTF viewer** (F6): opens `.rtf` files, such as the ayas saved from the Copy window.

The Quran text ships as `Quraan_File.ctxt`, compressed to 6 bits per letter with the method of [Text-Compression](https://github.com/Mohammad-Diab/Text-Compression). After the first run the app keeps a decoded copy in `%APPDATA%\Quraan Book`.

## Requirements

Windows with .NET Framework 4.0 or later (4.5 for v1.4.0). Windows 10 and 11 already include it.

## Usage

Download a version from [Releases](https://github.com/Mohammad-Diab/Quraan-Book/releases) and extract it:

- **v2.0.0-beta.1**: this C# app (pre-release).
- **v1.4.0**: the earlier VB.NET version with the same reading, search, copy and font windows.

Keep `Quraan_File.ctxt`, `mry_KacstQurn.ttf` and `7za.exe` next to `Quraan Book.exe`, then run it. For the intended look, install `KacstOne.ttf` (right-click, **Install**). There is no installer.

The exe is not signed, so Windows SmartScreen may warn the first time you run it: click **More info**, then **Run anyway**.

## Building

```
git clone https://github.com/Mohammad-Diab/Quraan-Book.git
```

Open `Quraan Book.sln` in Visual Studio and build, or run:

```
MSBuild "Quraan Book.sln" /p:Configuration=Release
```

The exe is written to `Quraan Book\bin\Release`. Copy `Quraan_File.ctxt`, `mry_KacstQurn.ttf` and `7za.exe` from a release download next to it.

The build needs the .NET Framework 4.0 reference assemblies. If Visual Studio doesn't have them, extract the [Microsoft.NETFramework.ReferenceAssemblies.net40](https://www.nuget.org/packages/Microsoft.NETFramework.ReferenceAssemblies.net40) NuGet package and add `/p:TargetFrameworkRootPath=<package folder>\build` to the command.

## Author

Mohammad Diab

## License

The code is under [Apache 2.0](LICENSE). The KACST fonts and `7za.exe` in the downloads keep their own licenses.
