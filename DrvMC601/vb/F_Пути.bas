Attribute VB_Name = "F_Пути"
Option Explicit

    Global Const Путь_База = "База"
    Global Const Путь_Модули = "Модули"
    Global Const Путь_Профили = "Профили"
    Global Const Путь_Шаблоны = "Шаблоны"
    Global Const Путь_Отчёты = "Отчёты"
    Global Const Путь_IE = "C:\Program Files\Internet Explorer\IEXPLORE.EXE"

'   *****************
'   ***  П У Т И  ***
'   *****************
Public Function ПутьКПапке(ByVal Куда As String) As String
' Возвращает путь к стандартной папке.
' Аргумент -- путь к папке относительно базы (кроме самой базы).
Dim База As String, Папка As String
    База = Application.ActiveWorkbook.Path
    Select Case Куда
      Case Путь_База
        ПутьКПапке = База
        Exit Function
      Case Путь_Модули
        Папка = Путь_Модули
      Case Путь_Профили
        Папка = Путь_Профили
      Case Путь_Шаблоны
        Папка = Путь_Шаблоны
      Case Else
        Папка = Куда
      End Select
    ПутьКПапке = ДополнитьПуть(Папка, База)
End Function

Public Function ДополнитьПуть(ByVal Путь As String, ByVal База As String) As String
' Формирует полный путь. Если Путь -- относительный, добавляет базу
    If Путь = "" Then Exit Function
    If (InStr(1, Путь, ":") = 0) And (InStr(1, Путь, "\\") = 0) Then
        If База <> "" Then
            ДополнитьПуть = База & "\" & Путь
          Else
            ДополнитьПуть = Путь
          End If
      End If
End Function

Public Function СократитьПуть(ByVal Путь As String, ByVal База As String) As String
' Если путь содержит в себе базовую часть, то удаляет её
' и оставляет только "относительную часть"
    СократитьПуть = Путь
    If База <> "" Then
        If (Left(Путь, Len(База)) = База) Then
            СократитьПуть = Mid(Путь, Len(База & "\") + 1)
          End If
      End If
End Function


'   *******************
'   ***  Ф А Й Л Ы  ***
'   *******************
Public Function ВыбратьШаблон() As String
' Выбор xls-файла из стандартной папки
Dim s As String
    s = ПутьКПапке(Путь_Шаблоны)
  With Application.FileDialog(msoFileDialogFilePicker)
    .Title = "Путь к шаблону"
    .AllowMultiSelect = False
    .Filters.Clear
    .Filters.Add "Все файлы", "*.*"
    .Filters.Add "Шаблоны отчётов", "*.xls"
    .FilterIndex = 2
    .InitialFileName = s
        If (.Show = 0) Then Exit Function
    ВыбратьШаблон = СократитьПуть(.SelectedItems.Item(1), s)
  End With
End Function

Public Function ВыбратьПрофиль() As String
' Выбор xls-файла из стандартной папки
Dim s As String
    s = ПутьКПапке(Путь_Профили)
  With Application.FileDialog(msoFileDialogFilePicker)
    .Title = "Путь к файлу профиля"
    .AllowMultiSelect = False
    .Filters.Clear
    .Filters.Add "Все файлы", "*.*"
    .Filters.Add "Файлы профилей", "*.xls"
    .FilterIndex = 2
    .InitialFileName = s
        If (.Show = 0) Then Exit Function
    ВыбратьПрофиль = СократитьПуть(.SelectedItems.Item(1), s)
  End With
End Function

Public Function ВыбратьФайл(ИзПапки As String) As String
' Выбор xls-файла из стандартной папки
Dim s As String
    s = ПутьКПапке(ИзПапки)
  With Application.FileDialog(msoFileDialogFilePicker)
    .Title = "Путь к файлу"
    .AllowMultiSelect = False
    .Filters.Clear
    .Filters.Add "Все файлы", "*.*"
    .Filters.Add "Файлы Excel", "*.xls"
    .FilterIndex = 2
    .InitialFileName = s
        If (.Show = 0) Then Exit Function
    ВыбратьФайл = СократитьПуть(.SelectedItems.Item(1), s)
  End With
End Function


Public Function ПутьКФайлу(Файл As String, ИзПапки As String) As String
' Возвращает полный путь к файлу. Файл -- путь к файлу. Где -- одна из папок.
Dim s As String
    If Файл = "" Then
          MsgBox "Не указан файл!", vbOKOnly, "Ошибка!"
          Exit Function
        End If
    s = ДополнитьПуть(Файл, ПутьКПапке(ИзПапки))
    If Dir(s) = "" Then
        MsgBox "Файл не найден! Проверьте имя файла" & vbCrLf & _
        vbCrLf & "или выберите его заново!", _
        vbOKOnly, "Ошибка!"
        Exit Function
      End If
    ПутьКФайлу = s
End Function

Public Function ОткрытьФайл(Файл As String, ИзПапки As String, Правка As Boolean) As Integer
' Открывает файл указанного типа (тип определяет стандартную папку)
' Имя файла передаётся
Dim MyExl As Excel.Application
Dim MyWbk As Excel.Workbook
Dim s As String
    
    If Файл = "" Then
          MsgBox "Не указан файл!", vbOKOnly, "Ошибка!"
          Exit Function
        End If
    s = ДополнитьПуть(Файл, ПутьКПапке(ИзПапки))
    If Dir(s) = "" Then
        MsgBox "Файл не найден! Проверьте пути" & vbCrLf & _
        vbCrLf & "или попробуйте найти файл самостоятельно.", _
        vbOKOnly, "Ошибка!"
          Exit Function
        End If
        
    On Error Resume Next
    Set MyExl = New Excel.Application
    If Err <> 0 Then
          MsgBox "Невозможно запустить Microsoft Excel!" & vbCrLf & _
            "Попробуйте запустить Excel из главного меню." & vbCrLf & _
            "В случае сохранения ошибке обратитесь к руководству.", _
            vbOKOnly, "Ошибка!"
          Exit Function
        End If
    Set MyWbk = MyExl.Workbooks.Open(s, , Not Правка)
    If Err <> 0 Then
          MsgBox "Не удаётся открыть файл." & vbCrLf & _
            "Проверьте, что файл существуе и" & vbCrLf & _
            "находится по указанному пути.", _
            vbOKOnly, "Ошибка!"
          MyExl.Quit
          Exit Function
        End If
    MyExl.Visible = True
End Function

Public Sub ВыбратьИОткрытьФайл(ИзПапки As String, Правка As Boolean)
' Выбор xls-файла из стандартной папки
Dim MyExl As Excel.Application
Dim MyWbk As Excel.Workbook
Dim s As String
    s = ПутьКПапке(ИзПапки)
  With Application.FileDialog(msoFileDialogFilePicker)
    .Title = "Выбор файла"
    .AllowMultiSelect = False
    .Filters.Clear
    .Filters.Add "Все файлы", "*.*"
    .Filters.Add "Файлы Excel", "*.xls"
    .FilterIndex = 2
    .InitialFileName = s
        If (.Show = 0) Then Exit Sub
    s = .SelectedItems.Item(1)
  End With
  
    On Error Resume Next
    Set MyExl = New Excel.Application
    Set MyWbk = MyExl.Workbooks.Open(s, , Not Правка)
    If Err <> 0 Then
          MsgBox "Не удаётся открыть файл.", vbOKOnly, "Ошибка!"
          MyExl.Quit
          Exit Sub
        End If
    MyExl.Visible = True
End Sub


Public Sub ОткрытьПапку(Папка As String)
Dim s As String
    If Папка = "" Then
        MsgBox "Папка не указана!", vbOKOnly, "Внимание!"
        Exit Sub
      End If
    s = ПутьКПапке(Папка)
    If s = "" Then
        MsgBox "Проверьте правильность задания" & Chr(10) _
        & "пути к папке ""Отчёты""", vbOKOnly, "Внимание!"
        Exit Sub
      End If
    On Error Resume Next
    Shell Путь_IE & " " & s
'    Shell Путь_IE & " " & s, vbNormalFocus
    If Err() <> 0 Then
        MsgBox "Не удалось открыть папку!" & Chr(10) _
        & "Проверьте путь к Internet Explorer'у", vbOKOnly, "Внимание!"
      End If
End Sub

