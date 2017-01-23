VERSION 5.00
Begin {C62A69F0-16DC-11CE-9E98-00AA00574A4F} Подключение 
   Caption         =   "Подключение"
   ClientHeight    =   1935
   ClientLeft      =   30
   ClientTop       =   480
   ClientWidth     =   5175
   OleObjectBlob   =   "Подключение.frx":0000
   ShowModal       =   0   'False
   StartUpPosition =   1  'CenterOwner
End
Attribute VB_Name = "Подключение"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Const cBase = &HFFC0C0
Const cFore = &HFFFF80
Dim Ждём As Boolean
Dim Номер As String

Private Sub UserForm_Terminate()
    Ждём = False                ' Останавливаем индикатор
    ЖдёмПодключения = False     ' Сообщаем клиенту
'    DoEvents
End Sub


' *** Управление индикатором ***
Sub ИндикаторСбросить()
Dim i As Integer
    For i = 0 To 7
        Me("L" & CInt(i)).BackColor = cBase
      Next i
End Sub

Sub ИндикаторПродвинуть(ByVal i As Integer)
Static m As Integer
    Me("L" & CInt(m)).BackColor = cBase
    m = i Mod 8
    Me("L" & CInt(m)).BackColor = cFore
End Sub

Public Sub Для(ЭтоНомер As String)
    Номер = ЭтоНомер
End Sub

Public Sub ЗапуститьПодключение()
    Me![КнопкаСтоп].Visible = True
    Me![КнопкаСтоп].SetFocus
    Me![КнопкаСтарт].Visible = False
    Me![МеткаСостояния] = "Попытка соединения"
    
    If Соединиться() Then
        Выждать
      End If
        
'    ЖдёмПодключения = False     ' Соединились
  
  On Error Resume Next
    Me![КнопкаСтарт].Visible = True
    Me![КнопкаСтарт].SetFocus
    Me![КнопкаСтоп].Visible = False
End Sub


Private Function Соединиться() As Boolean
Static T As Double
Dim i As Long

    Соединиться = False
    T = Now()
    i = 0
    Ждём = True
    Mdm_ПослатьКоманду "ATD" & Номер
    УстановитьТаймер 60
    ЗапуститьТаймер

    Do                      ' Ждём резальтата вызова
        DoEvents
        DoPoll
        If Mdm_ПриёмОкончен() Then Exit Do
        ПроверитьТаймер
'        If Ждём = False Then
'            ReceiverState = RS_UserEsc
'          End If
        If ПриёмПрерван Then Exit Do
        If T <> Now() Then
            T = Now()
            ИндикаторПродвинуть i
            i = i + 1
          End If
        Sleep 20
     Loop
    ОстановитьТаймер        ' Отключаем слежение за тайм-аутом
    
  On Error Resume Next      ' Защита от закрытия формы
    ИндикаторСбросить
    If Mdm_ОшибкаПриёма() <> 0 Then
        ReceiverState = RS_BadData
      End If
      
' Модем нормально отработал
    If ReceiverState = 0 Then
        Me![МеткаСостояния] = СообщениеМодема(MdmKWord)
        If MdmKWord = AC_CT Then
            Соединиться = True      ' Соединились!
          End If
        Exit Function
      End If
          
' Подключение было прервано
    If ReceiverState = RS_UserEsc Then
        Me![МеткаСостояния] = "Прервано пользователем"
        ЖдёмПодключения = False
      ElseIf ReceiverState = RS_TimeOut Then
        Me![МеткаСостояния] = "Тайм-аут"
      Else
        Me![МеткаСостояния] = "Ошибка связи"
       End If
  On Error GoTo 0
End Function

Public Function Выждать() As Boolean
' Задержка после получения сообщения от модема о подключении.
' В этоот момент могут придти "левые" сообщения.
' Возвращает True, если всё ОК

    Выждать = True          ' По умолчанию всё OK
    Mdm_НачатьПриём
    УстановитьТаймер 3
    ЗапуститьТаймер
    Do                      ' Ждём
        DoEvents
        DoPoll
'        If Mdm_ПриёмОкончен() Then Exit Do
        ПроверитьТаймер
        If ПриёмПрерван Then Exit Do
        Sleep 20
     Loop
    ОстановитьТаймер        ' Отключаем слежение за тайм-аутом
    
  On Error Resume Next      ' Защита от закрытия формы
' Было ли какое-то сообщение на модем?
'    If ReceiverState = 0 Then
'        If MdmKWord = AC_NC Then
'            Me![МеткаСостояния] = СообщениеМодема(MdmKWord)
'            Выждать = False     ' Было сообщение "NO CARRIER"
'          Else
'            MdmKWord = AC_CT
'            ЖдёмПодключения = False
'          End If
'        On Error GoTo 0
'        Exit Function
'      End If
          
' Подключение было прервано
    If ReceiverState = RS_UserEsc Then
        Me![МеткаСостояния] = "Прервано пользователем"
        ЖдёмПодключения = False
        Выждать = False
        On Error GoTo 0
        Exit Function
      End If
      
' Было сообщение "NO CARRIER"
    If ReceiverState = RS_TimeOut Then
        If MdmKWord = AC_NC Then
            Me![МеткаСостояния] = СообщениеМодема(MdmKWord)
            Выждать = False     ' Было сообщение "NO CARRIER"
            On Error GoTo 0
            Exit Function
          End If
      End If
    '
    MdmKWord = AC_CT
    ЖдёмПодключения = False
End Function


' *** Запуск ожидания ***
Sub КнопкаСтарт_Click()
    ЗапуститьПодключение
End Sub

Private Sub КнопкаСтоп_Click()
    ReceiverState = RS_UserEsc
'    Ждём = False
    ClosePort
    Mdm_ОткрытьПорт WrkPortNumber
End Sub

