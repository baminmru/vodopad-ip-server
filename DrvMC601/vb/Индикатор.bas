Attribute VB_Name = "Индикатор"
Option Explicit

Private Индикатор As Object
Private ВРаботе As Boolean
Private Xmin As Single, Xmax As Single
Private Всего As Long, Уже As Long

Public Function ИндикаторНачать() As Boolean
    On Error Resume Next
    Set Индикатор = Прогресс
    Индикатор.Show
    ВРаботе = (Err = 0)
    ИндикаторНачать = ВРаботе
    Индикатор.Начать
    Индикатор.Repaint
End Function

Public Sub ИндикаторДоля(ByVal Q As Single)
    If Not ВРаботе Then Exit Sub
    Индикатор.Доля Q
    Индикатор.Repaint
End Sub

Public Sub ИндикаторГотово()
    If Not ВРаботе Then Exit Sub
    Индикатор.Готово
    Индикатор.Repaint
End Sub

Public Sub ИндикаторЗакрыть()
    On Error Resume Next
    Unload Индикатор
    ВРаботе = False
End Sub

Public Sub ИндикаторПодговить(Будет As Long, Zmin As Single, Zmax As Single)
    If Not ВРаботе Then Exit Sub
    Xmin = Zmin
    Xmax = Zmax
    Всего = Будет
    Уже = 0
    ИндикаторДоля Xmin
End Sub

 Public Sub ИндикаторЕщё(ByVal k As Long)
 Dim Q As Single
     If Not ВРаботе Then Exit Sub
    Уже = Уже + k
    Q = Xmin + (Xmax - Xmin) * Уже / Всего
     ИндикаторДоля Q
End Sub


