VERSION 5.00
Begin {C62A69F0-16DC-11CE-9E98-00AA00574A4F} ТестЦикла 
   Caption         =   "Тест"
   ClientHeight    =   2412
   ClientLeft      =   30
   ClientTop       =   480
   ClientWidth     =   4635
   OleObjectBlob   =   "ТестЦикла.frx":0000
   ShowModal       =   0   'False
   StartUpPosition =   1  'CenterOwner
End
Attribute VB_Name = "ТестЦикла"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

'' задержка с помощью API

Dim Ждём As Boolean

Private Sub UserForm_Terminate()
    Ждём = False                ' Останавливаем таймер
End Sub



Private Sub КнопкаСтарт_Click()
    Ждём = True
    Do                      ' Ждём завершения приёма
        DoEvents
        Sleep 20
        Me![Время] = Time()
      Loop Until Ждём = False
End Sub


Private Sub КнопкаСтоп_Click()
    Ждём = False
    
End Sub

Private Sub КнопкаПозвать_Click()
    Me![ПолеОК] = "Начато !!!"
    Подключиться ("P412-55-45")
    Me![ПолеОК] = "Закончено !!!"
End Sub

