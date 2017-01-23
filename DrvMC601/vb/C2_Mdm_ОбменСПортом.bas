Attribute VB_Name = "C2_Mdm_ОбменСПортом"
Option Explicit

Global Mdm_TimeOut As Integer        ' Тайм-аут для ожидания

Public Sub Mdm_ПослатьКоманду(Cmd As String)
' Посылает команду на модем. И только!!!
    ReDim Rqs(0 To 127)             ' Готовим массив запроса
    nRqs = Mdm_Cmd2Rqs(Cmd, Rqs)    ' Формируем запрос
    ReDim Preserve Rqs(0 To nRqs - 1) ' Отсекаем хвосты
    Mdm_НачатьПриём                 ' Активируем автомат приёма
    SendDataToPort Rqs              ' Посылаем запрос
End Sub

Public Function Mdm_ПринятьОтвет(ByVal Delay As Integer) As Integer
' Дожидается исполнения команды.
    УстановитьТаймер Delay
    ЗапуститьТаймер
    Do  ' Ждём завершения приёма
        DoEvents
        DoPoll
        If Mdm_ПриёмОкончен() Then Exit Do
        ПроверитьТаймер
        If ПриёмПрерван Then Exit Do
        Sleep 20
      Loop
  ' Завершаем ожидание
    ОстановитьТаймер        ' Отключаем слежение за тайм-аутом
    If Mdm_ОшибкаПриёма() <> 0 Then
        ReceiverState = RS_BadData
      End If
    Mdm_ПринятьОтвет = ReceiverState
End Function

Public Function Mdm_ВыполнитьКоманду(Cmd As String) As Integer
    Mdm_ПослатьКоманду Cmd
    Mdm_ВыполнитьКоманду = Mdm_ПринятьОтвет(3)
End Function

Public Sub Mdm_ОбработатьПосылку(Q As Variant)
' Направляет очередную порцию полученных байтов в автомат приёма
' Вызывается из коммуникационной программы
Dim i As Integer
    For i = 0 To LenB(Q) - 1
        Mdm_ОбработатьБайт Q(i)
      Next i
End Sub

