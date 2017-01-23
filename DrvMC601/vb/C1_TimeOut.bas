Attribute VB_Name = "C1_TimeOut"
Option Explicit

'   *************************************
'   *  Р А Б О Т А  С  Т А Й М Е Р О М  *
'   *************************************
' Процедуры ниже -- для определения тайм-аута.
' В Excel'е часы тикают 1 раз в секунду!

Dim TOLong As Integer       ' Длительность тайм-аута
Dim TOTime As Date          ' Момент окончания тайм-аута
Dim TOWait As Boolean       ' Тайм-аут запущен

Public Sub УстановитьТаймер(ByVal Sec As Integer)
    TOLong = Sec
End Sub


Public Sub ЗапуститьТаймер()
' Запускает таймер на указанное число секунд.
    TOTime = Now() + TimeSerial(0, 0, TOLong)
    TOWait = True
End Sub

Public Sub ОстановитьТаймер()
    TOWait = False
End Sub

Public Sub ПроверитьТаймер()
' Выставляет флаг тайм-аута (имитирует асинхронную процедуру)
    If TOWait Then
        If Now() > TOTime Then
            ReceiverState = RS_TimeOut
          End If
      End If
End Sub

'Public Sub ПереустановитьТаймер(Sec As Integer)
'    ОстановитьТаймер
'    ЗапуститьТаймер Sec
'End Sub

