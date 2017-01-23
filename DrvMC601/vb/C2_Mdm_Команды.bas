Attribute VB_Name = "C2_Mdm_Команды"
Option Explicit

Const InitPar = "1200,N,8,1"    ' Строка параметров обмена порта

' ************************
' *** Работа с каналом ***
' ************************
Public Function Mdm_ОткрытьПорт(ByVal Номер As Integer) As Integer
    ChannalMode = CM_Modem
    Mdm_ОткрытьПорт = OpenPort(Номер, InitPar)
End Function

Public Function Mdm_МодемЕсть() As Boolean
' В случае неудачи возвращает 0
    Mdm_ВыполнитьКоманду "AT"
    Mdm_МодемЕсть = (MdmKWord = AC_OK)
End Function

Public Function Mdm_НайтиМодем(Номер As Integer) As Integer
' Ищет модем. Аргумент задаёт предпочтительный номер порта
' Возвращает номер порта, который ответил, 0 -- в случае неудачи
Dim i As Integer, k As Integer
    Mdm_НайтиМодем = 0
  ' Сначала пробуем переданный номер порта
    If Номер <> 0 Then
        i = Номер
        If Mdm_ОткрытьПорт(i) = 0 Then
            If Mdm_МодемЕсть Then
                Mdm_НайтиМодем = i
                Exit Function
              End If
            ClosePort
          End If
      End If
    ' Атака не удалась, ищем перебором
    For i = 1 To 8
    If i <> Номер Then
        If Mdm_ОткрытьПорт(i) = 0 Then
            If Mdm_МодемЕсть Then
                Mdm_НайтиМодем = i
                Exit Function
              End If
            ClosePort
          End If
      End If
      Next i
End Function

Public Function Mdm_УстановитьСоединение(Телефон As String) As Integer
' Подаёт команду на установку соединения.
' Возвращает 0, если соединение установлено, или код результата.
    Mdm_УстановитьСоединение = Подключиться(Телефон)
End Function

Public Sub Mdm_РазорватьСоединение()
' Разрывает соединение
'
    ClosePort
    Mdm_ОткрытьПорт WrkPortNumber
End Sub


