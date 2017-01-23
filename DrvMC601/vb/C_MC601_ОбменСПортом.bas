Attribute VB_Name = "C_MC601_ОбменСПортом"
Option Explicit
'               ****************************
'               *  M U L T I C A L  6 0 1  *
'               ****************************
' Обмен с портом
'
'Dim MC601_TimeOut As Integer        ' Тайм-аут для ожидания

'' задержка с помощью API

Public Function MC601ВыполнитьКоманду(Cmd() As Byte, ByVal n As Integer) As Integer
' Готовит из команды запрос, посылает его, ждёт ответа
  ' Готовим запрос
    ReDim Rqs(0 To 127)                 ' Готовим массив запроса
    nRqs = MC601Cmd2Rqs(Cmd, n, Rqs)    ' Формируем в нём запрос
    ReDim Preserve Rqs(0 To nRqs - 1)   ' Отсекаем хвосты
    MC601НачатьПриём        ' Активируем автомат приёма
    SendDataToPort Rqs      ' Посылаем запрос
  ' Посылаем и ждём ответа
    ЗапуститьТаймер         ' Включаем счётчик тайм-аута
    Do                      ' Ждём завершения приёма
        DoEvents
        DoPoll
        If MC601ПриёмОкончен() Then Exit Do
        ПроверитьТаймер
        If ПриёмПрерван Then Exit Do
        Sleep 20
      Loop
  ' Завершаем получение
    ОстановитьТаймер        ' Отключаем слежение за тайм-аутом
    If ReceiverState = 0 Then
        MC601ПроверитьCRC       ' Проверям и удаляем CRC
        If MC601ОшибкаПриёма() <> 0 Then
            ReceiverState = RS_BadData
          End If
      End If
    MC601ВыполнитьКоманду = ReceiverState
End Function

Public Sub MC601ОбработатьПосылку(Q As Variant)
' Направляет очередную порцию полученных байтов в автомат приёма
' Вызывается из коммуникационной программы
Dim i As Integer
    For i = 0 To LenB(Q) - 1
        MC601ОбработатьБайт Q(i)
      Next i
End Sub

