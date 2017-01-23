VERSION 5.00
Begin {C62A69F0-16DC-11CE-9E98-00AA00574A4F} Прогресс 
   Caption         =   "Ход считывания"
   ClientHeight    =   1572
   ClientLeft      =   30
   ClientTop       =   480
   ClientWidth     =   5790
   OleObjectBlob   =   "Прогресс.frx":0000
   ShowModal       =   0   'False
   StartUpPosition =   1  'CenterOwner
End
Attribute VB_Name = "Прогресс"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Dim Размер As Long  ' Полный размер индикатора

' *** МЕХАНИКА ***
Public Sub Начать()
'    ИндикаторДоля 0
    Доля 0
    Me![МеткаКонец].Visible = False
    Me![МеткаРабота].Visible = True
    Me![КнопкаСтоп].Enabled = True
End Sub

Public Sub Готово()
'    ИндикаторДоля 1
    Доля 1
    Me![МеткаРабота].Visible = False
    Me![МеткаКонец].Visible = True
    Me![КнопкаСтоп].Enabled = False
End Sub

Public Sub Доля(ByVal Q As Single)
' Устанавливает размер индикатора. Q -- доли единицы
    If Q < 0# Then Q = 0#
    If Q > 1# Then Q = 1#
    Me![Стебель].Width = Me![База].Width * Q
End Sub

Private Sub КнопкаСтоп_Click()
    ПрерватьЧтение = True
End Sub
