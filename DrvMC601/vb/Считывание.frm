VERSION 5.00
Begin {C62A69F0-16DC-11CE-9E98-00AA00574A4F} Считывание 
   Caption         =   "Считывание"
   ClientHeight    =   7476
   ClientLeft      =   30
   ClientTop       =   480
   ClientWidth     =   7110
   OleObjectBlob   =   "Считывание.frx":0000
   ShowModal       =   0   'False
   StartUpPosition =   1  'CenterOwner
End
Attribute VB_Name = "Считывание"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit


Dim Режим As Integer        ' Режим подключения: кабель, модем, тест
Dim КаналОткрыт As Boolean  ' Можно работать (кнопка "Считать" разблокирована)


' *****************************
' **** Открытие и закрытие ****
' *****************************
Private Sub UserForm_Initialize()
    ВыборЛинии
    СброситьПараметры_Все
    СчётчикМодель = "MC601"
    Отрисовать
End Sub

Private Sub UserForm_Terminate()
    ' Разрываем соединение
    If OnLine Then
        Mdm_РазорватьСоединение
      End If
    StopPortMaster
    StopPoll
End Sub

' **********************
' **** Режим работы ****
' **********************
Private Function ВыборЛинии()
' Определяет номер линии (канала) связи
Dim i As Integer
    If Me![Кабель] Then
        i = 1
      ElseIf Me![Модем] Then
        i = 2
      ElseIf Me![Файл] Then
        i = 3
      ElseIf Me![Тест] Then
        i = 4
      Else
        i = 0
      End If
    УстановитьРежим i
End Function

Private Sub Кабель_Change()
    ВыборЛинии
End Sub

Private Sub Модем_Change()
    ВыборЛинии
End Sub

Private Sub Файл_Change()
    ВыборЛинии
End Sub

Private Sub Тест_Change()
    ВыборЛинии
End Sub

Private Sub УстановитьРежим(ByVal i As Integer)
        If Режим = i Then Exit Sub
    Режим = i
    Me![СостояниеПорта] = Null
    Me![НомерПорта] = Null
    Me![НомерСчётчика] = Null
    
    Select Case Режим
      Case 1        ' Кабель
        Me![НомерПорта].BackColor = &H80000016
        Me![НомерПорта].Locked = False
        Me![Телефон].BackColor = &H8000000F
        Me![Телефон].Locked = True
      Case 2        ' Модем
        Me![НомерПорта].BackColor = &H80000016
        Me![НомерПорта].Locked = False
        Me![Телефон].BackColor = &H80000016
        Me![Телефон].Locked = False
      Case 4        ' Тест
        Me![НомерПорта].BackColor = &H8000000F
        Me![НомерПорта].Locked = True
        Me![Телефон].BackColor = &H8000000F
        Me![Телефон].Locked = True
      Case Else        ' Тест
        Me![НомерПорта].BackColor = &H8000000F
        Me![НомерПорта].Locked = True
        Me![Телефон].BackColor = &H8000000F
        Me![Телефон].Locked = True
      End Select
End Sub

Private Sub СостояниеПорт(ByVal Состояние As Boolean)
'
    OnPort = Состояние
    If OnPort = True Then
        Me![НомерПорта].Locked = True
        Me![КнопкаВключить].Caption = "Закрыть"
        Me![НомерПорта].BackColor = &HC0FFC0
        Me![Линия].Enabled = False
      Else
        Me![НомерПорта].Locked = False
        Me![КнопкаВключить].Caption = "Открыть"
        Me![КнопкаСоединить].Enabled = False
        Me![НомерПорта].BackColor = &H80000016
        Me![Линия].Enabled = True
      End If
End Sub

Private Sub СостояниеЛинии(ByVal Состояние As Boolean)
    '
    OnLine = Состояние
    If OnLine = True Then
        Me![Телефон].Locked = True
        Me![Телефон].BackColor = &HC0FFC0
        Me![КнопкаСоединить].Caption = "Разорвать"
      Else
        Me![Телефон].Locked = False
        Me![Телефон].BackColor = &H80000016
        Me![КнопкаСоединить].Caption = "Соединить"
      End If
End Sub

Private Sub СостояниеКанала(ByVal Состояние As Boolean)
'
    КаналОткрыт = Состояние
    If КаналОткрыт = True Then
        Me![КнопкаЧитать].Enabled = True
        ПоправитьДниХиП
        УстановитьТаймер (3)
      Else
        Me![КнопкаЧитать].Enabled = False
      End If
End Sub



' *************************
' **** ОТКРЫТИЕ КАНАЛА ****
' *************************
Private Sub КнопкаВключить_Click()
Dim i As Integer
  ' Если канал открыт, закрываем
    If OnPort Then
      ' Разрываем соединение
        If OnLine Then
            Mdm_РазорватьСоединение
            СостояниеЛинии False
          End If
      ' Закрываем порт
        StopPortMaster
        СостояниеПорт False
        СостояниеКанала False
        Me![Линия].Enabled = True
        Me![СостояниеПорта] = Null
        Me![НомерСчётчика] = Null
        Me![КнопкаЧитать].Enabled = False
        Exit Sub
      End If
      
  ' Открываем порт
    Select Case Режим
      Case 1    ' Связь через кабель
        StartPortMaster
        УстановитьТаймер (2)
      ' Открываем порт
        If NoData(Me![НомерПорта]) Or (Me![НомерПорта] = 0) Then
            ' Авто-номер
            i = MC601_НайтиСчётчик(0)
            If i = 0 Then
                Me![СостояниеПорта] = ОшибкаСоединения(CE_NoC)
                StopPortMaster
                Exit Sub
              End If
            Me![НомерПорта] = i
          Else
            ' Явный номер. Пробуем открыть порт
            i = MC601ОткрытьПорт(Me![НомерПорта])
            If i <> 0 Then
                Me![СостояниеПорта] = ОшибкаСоединения(CE_Opn)
                StopPortMaster
                Exit Sub
              End If
          End If
      ' Проверяем связь
        
        Me![СостояниеПорта] = "Порт открыт"
        Me![НомерСчётчика] = MC601_СчитатьНомер
        СостояниеПорт True
        If NoData(Me![НомерСчётчика]) Then
            Me![НомерСчётчика] = ОшибкаСоединения(CE_Cal)
            Exit Sub
          End If
        ДеньХ = MC601_СчитатьТекущий(98)
        СостояниеКанала True
        Exit Sub
        
      Case 2    ' Связь через модем
        StartPortMaster
        УстановитьТаймер (2)
      ' Открываем порт
        If NoData(Me![НомерПорта]) Or (Me![НомерПорта] = 0) Then
          ' Авто-номер
            i = Mdm_НайтиМодем(0)
            If i = 0 Then
                Me![СостояниеПорта] = ОшибкаСоединения(CE_NoM)
                StopPortMaster
                Exit Sub
              End If
            Me![НомерПорта] = i
          Else
          ' Явный номер. Пробуем открыть порт
            i = Mdm_ОткрытьПорт(Me![НомерПорта])
            If i <> 0 Then
                Me![СостояниеПорта] = ОшибкаСоединения(CE_Opn)
                StopPortMaster
                Exit Sub
              End If
          End If
      ' Проверяем связь
        i = Mdm_МодемЕсть()
        СостояниеПорт True
        If i = 0 Then ' Проверяем ответ модема
            Me![СостояниеПорта] = ОшибкаСоединения(CE_Mdm)
          Else
            Me![СостояниеПорта] = "Модем обнаружен"
            Me![КнопкаСоединить].Enabled = True
          End If
        Exit Sub
        
      Case 4    ' Тестовое подключение
        Me![СостояниеПорта] = " Тест (пустое считывание)"
        ДеньХ = 101
        СостояниеКанала True
        Exit Sub
        
      Case Else
        Exit Sub
      End Select
      
End Sub

Private Sub КнопкаСоединить_Click()
    If OnLine Then
        Mdm_РазорватьСоединение
        СостояниеЛинии False
        Exit Sub
      End If
      
  ' Устанавливаем и проверяем соединение
    If NoData(Me![Телефон]) Then
        MsgBox "А номер?", vbOKOnly, "Внимание!"
         Exit Sub
      End If
    If Mdm_УстановитьСоединение(Me![Телефон]) = False Then
        Exit Sub
      End If
  '
    ChannalMode = CM_Line
    СостояниеЛинии True
    Me![НомерСчётчика] = Null
    УстановитьТаймер (3)
    
    Me![НомерСчётчика] = MC601_СчитатьНомер
    If NoData(Me![НомерСчётчика]) Then
        Me![НомерСчётчика] = ОшибкаСоединения(CE_Cal)
        Exit Sub
      End If
    ДеньХ = MC601_СчитатьТекущий(98)
    СостояниеКанала True
End Sub


' **************************
' **** РАБОТА С ОТЧЁТОМ ****
' **************************

' *** Отрисовка ***
' Эти процедуры месняют отображение полей периода и границ
' Они нужны при смене параметров отчёта

Private Sub ВключитьПериод(ByVal Разрешение As Boolean)
    Me![ЭтоПериод].Enabled = Разрешение
    Me![Плюс1].Enabled = Разрешение
    Me![Минус1].Enabled = Разрешение
End Sub

Private Sub ВключитьВремена(ByVal Разрешение As Boolean)
' Используется для включения/выключения временнных параметров
Dim i As Integer
    Me![ЭтоДо].Enabled = Разрешение
    Me![ЭтоОт].Enabled = Разрешение
    For i = 2 To 3
'        Me("Сенсор" & CStr(i)).Enabled = Разрешение
        Me("Плюс" & CStr(i)).Enabled = Разрешение
        Me("Минус" & CStr(i)).Enabled = Разрешение
      Next i
End Sub

Private Sub Отрисовать()
    If NoData(ТипПериода) Or (ТипПериода = 0) Then
        ВключитьПериод False
      Else
        ВключитьПериод True
      End If
    If NoData(ТипАрхива) Or (ТипАрхива = 0) Then
        ВключитьВремена False
        Me![Всего].Visible = False
        Me![МеткаВсего].Visible = False
      Else
        ВключитьВремена True
        Me![Всего].Visible = True
        Me![МеткаВсего].Visible = True
      End If
End Sub

' *** Обновления ***
Private Sub ОбновитьФайл()
Dim s As String
    If NoData(ОтчётФайл) Then
        s = ОтчётМаска
        ЗаменитьПараметры s, Subs_Q
        Me![ФайлОтчёта] = s
      End If
End Sub

Private Sub ОбновитьПоля()
'    Me![ЭтоДо] = ВывестиГраницу(ОтчётДо)
'    Me![ЭтоОт] = ВывестиГраницу(ОтчётОт)
'    Me![ЭтоПериод] = ВывестиПериод(ОтчётЗа)
    Me![ЭтоТипАрхива] = ВывестиТипАрхива(ТипАрхива)
    Me![ЭтоТипПериода] = ВывестиТипПериода(ТипПериода)
    Me![ЭтоДеньП] = ВывестиДень(ДеньП, ТипПериода)
    ОбновитьФайл
End Sub

Private Sub ГраницыАвто()
' Вызывается при смене типа архива
' Предполагает назначение значений по умолчанию
    ОтчётДо = ВерхняяАвто(ОтчётЗа)
    Me![ЭтоДо] = ВывестиГраницу(ОтчётДо)
    ОтчётОт = НижняяАвто(ОтчётЗа)
    Me![ЭтоОт] = ВывестиГраницу(ОтчётОт)
    Отрисовать
    ПересчитатьРазность
End Sub

Private Sub ВсёАвто()
' Вызывается при смене типов периода и архива
' Предполагает назначение значений по умолчанию
    ОтчётЗа = ПериодАвто()
    Me![ЭтоПериод] = ВывестиПериод(ОтчётЗа)
    ГраницыАвто
End Sub

Private Sub ПересчитатьРазность()
    Me![Всего] = РазностьМеток(ОтчётОт, ОтчётДо) + 1
End Sub


' *** Профиль и шаблон ***
Private Sub Сенсор1_Click()
    Me![Профиль] = ВыбратьФайл(Путь_Профили)
    ВзятьПараметры_P Me![Профиль]
'    ПересчитатьИмяПоМаске
'    Me![ФайлОтчёта] = СчётчикУзел
    ОбновитьФайл
    
    If NoData(Me![НомерСчётчика]) Then Exit Sub
    If NoData(СчётчикНомер) Then Exit Sub
    If CStr(СчётчикНомер) = CStr(Me![НомерСчётчика]) Then Exit Sub
    MsgBox "Номер счётчика не соответствует номеру," & _
        vbCrLf & "указанному в профиле узла!", vbOKOnly, "Внимание!"
End Sub

Private Sub Сенсор2_Click()
    Me![Шаблон] = ВыбратьФайл(Путь_Шаблоны)
    ВзятьПараметры_I Me![Шаблон]
    ПоправитьДниХиП
    ВсёАвто
    ОбновитьПоля
End Sub


' *** День "П" ***
Private Sub ЭтоДеньП_BeforeUpdate(ByVal Cancel As MSForms.ReturnBoolean)
    If NoData(Me![ЭтоДеньП]) Then Exit Sub
    If СчитатьДень(Me![ЭтоДеньП], ТипПериода) = 0 Then
        MsgBox "Неправильный формат", vbOKOnly, "Ошибка!"
        Cancel = True
      End If
End Sub

Private Sub ЭтоДеньП_AfterUpdate()
    ДеньП = СчитатьДень(Me![ЭтоДеньП], ТипПериода)
    ПоправитьДниХиП
    Me![ЭтоДеньП] = ВывестиДень(ДеньП, ТипПериода)
    ВсёАвто
    ОбновитьФайл
End Sub


' *** ПЕРИОД ***
Public Sub ОбновитьПериод()
    Me![ЭтоПериод] = ВывестиПериод(ОтчётЗа)
    ГраницыАвто
    ОбновитьФайл
End Sub

Private Sub ЭтоПериод_BeforeUpdate(ByVal Cancel As MSForms.ReturnBoolean)
    If NoData(Me![ЭтоПериод]) Then Exit Sub
    If СчитатьПериод(Me![ЭтоПериод], ТипПериода) = 0 Then
        MsgBox "Неправильный формат", vbOKOnly, "Ошибка!"
        Cancel = True
      End If
End Sub

Private Sub ЭтоПериод_AfterUpdate()
    ОтчётЗа = СчитатьПериод(Me![ЭтоПериод], ТипПериода)
    ОбновитьПериод
End Sub

Private Sub Минус1_Click()
    If NoData(ОтчётЗа) Then
        ОтчётЗа = ПериодАвто
      Else
        ОтчётЗа = ПредыдущийПериод(ОтчётЗа)
      End If
    ОбновитьПериод
End Sub

Private Sub Плюс1_Click()
    If NoData(ОтчётЗа) Then Exit Sub
    ОтчётЗа = СледующийПериод(ОтчётЗа)
    If ОтчётЗа > ПериодАвто Then
        ОтчётЗа = Null
      End If
    ОбновитьПериод
End Sub


' *** Верхняя Граница ***
Public Sub ОбновитьВерхнюю()
    Me.ЭтоДо = ВывестиГраницу(ОтчётДо)
    If NoData(ОтчётДо) Then
        ОтчётОт = Null
        ОбновитьНижнюю
      End If
    If ДатаМеткиЗаписи(ОтчётДо, ДеньХ) > Date Then
        ОтчётДо = МеткаЗаписиПоДате(Date, ДеньХ, ТипАрхива)
      End If
    ОбновитьНижнюю
End Sub

Private Sub ЭтоДо_BeforeUpdate(ByVal Cancel As MSForms.ReturnBoolean)
        If NoData(Me.ЭтоДо) Then Exit Sub
        If Str2YYMMDD(Me.ЭтоДо, ТипАрхива) <> 0 Then Exit Sub
    MsgBox "Неправильное значение!", vbOKOnly, "Внимание!"
    Cancel = True
End Sub

Private Sub ЭтоДо_AfterUpdate()
    If NoData(Me.ЭтоДо) Or NoData(ТипАрхива) Then
        ОтчётДо = Null
      Else
        ОтчётДо = СчитатьГраницу(Me.ЭтоДо, ТипАрхива)
      End If
    ОбновитьВерхнюю
End Sub

Private Sub Плюс3_Click()
Dim d As Date, l As Long
    If NoData(ОтчётДо) Then Exit Sub
    l = СледующаяМетка(ОтчётДо, ТипАрхива)
    d = ДатаМеткиЗаписи(l, ДеньХ)
    If d < Date Then ОтчётДо = l
    ОбновитьВерхнюю
End Sub

Private Sub Минус3_Click()
Dim d As Integer, m As Integer, Y As Integer
    If NoData(ОтчётДо) Then Exit Sub
    ОтчётДо = ПредыдущаяМетка(ОтчётДо, ТипАрхива)
    ОбновитьВерхнюю
End Sub


' *** Нижняя граница ***
Public Sub ОбновитьНижнюю()
'    If NoData(ОтчётДо) Then ОтчётОт = Null
    If Not (NoData(ОтчётДо) Or NoData(ОтчётДо)) Then
        If (ОтчётОт > ОтчётДо) Then ОтчётОт = ОтчётДо
      End If
    Me![ЭтоОт] = ВывестиГраницу(ОтчётОт)
    ПересчитатьРазность
End Sub

Private Sub ЭтоОт_BeforeUpdate(ByVal Cancel As MSForms.ReturnBoolean)
        If NoData(Me![ЭтоОт]) Then Exit Sub
        If Str2YYMMDD(Me![ЭтоОт], ТипАрхива) <> 0 Then Exit Sub
    MsgBox "Неправильное значение!", vbOKOnly, "Внимание!"
    Cancel = 1
End Sub

Private Sub ЭтоОт_AfterUpdate()
    If NoData(Me![ЭтоОт]) Or NoData(ТипАрхива) Then
        ОтчётОт = Null
      Else
        ОтчётОт = СчитатьГраницу(Me![ЭтоОт], ТипАрхива)
    End If
    ОбновитьНижнюю
End Sub

Private Sub Плюс2_Click()
Dim d As Date, l As Long
    If NoData(ОтчётОт) Then Exit Sub
    ОтчётОт = СледующаяМетка(ОтчётОт, ТипАрхива)
    ОбновитьНижнюю
End Sub

Private Sub Минус2_Click()
Dim d As Integer, m As Integer, Y As Integer
    If NoData(ОтчётОт) Then Exit Sub
    ОтчётОт = ПредыдущаяМетка(ОтчётОт, ТипАрхива)
    ОбновитьНижнюю
End Sub

' ***********************
' ****   Ф А Й Л Ы   ****
' ***********************
Private Sub КнопкаПрофиль_Click()
    ОткрытьФайл Me![Профиль], Путь_Профили, True
End Sub

Private Sub КнопкаШаблон_Click()
    ОткрытьФайл Me![Шаблон], Путь_Шаблоны, True
End Sub

Private Sub КнопкаОтчёты_Click()
'    ОткрытьПапку Путь_Отчёты
    ВыбратьИОткрытьФайл Путь_Отчёты, True
End Sub



' *************************
' *** ОБРАБОТКА  ОТЧЁТА ***
' *************************
Private Sub КнопкаЧитать_Click()
Dim Отступ, Всего
' Dim ЭтоЯ As Object

    If NoData(Me![Профиль]) Then
        MsgBox "Не указан профиль!", vbOKOnly, "Внимание!"
        Exit Sub
      End If
    If NoData(Me![Шаблон]) Then
        MsgBox "Не указан шаблон!", vbOKOnly, "Внимание!"
        Exit Sub
      End If
    If NoData(ТипАрхива) Or (ТипАрхива = 0) Then
      Else
        If NoData(ОтчётОт) Or NoData(ОтчётДо) Then
            MsgBox "Не определены границы считывания!", vbOKOnly, "Внимание!"
            Exit Sub
          End If
      End If
    
    If Not ОткрытьПрофиль(Me![Профиль]) Then
        MsgBox "Не удалось открыть профиль!", vbOKOnly, "Ошибка!"
        Exit Sub
      End If
    
    Плановый = Me![ОтчётПлановый]
    ОтчётФайл = Me![ФайлОтчёта]
    ОтчётДата = Date
    ОтчётВремя = Time()
    Всего = РазностьМеток(ОтчётОт, ОтчётДо) + 1
    If Всего <= 0 Then
        MsgBox "Неправильные границы!", vbOKOnly, "Внимание!"
        Exit Sub
      End If
    Отступ = РазностьМеток(ОтчётДо, Date2YYMMDD(Date, 2))
    
  ' ***********************
    ПрерватьЧтение = False
    Select Case ШаблонТип
      Case 101
        ОтчётФорма101 Me![Шаблон], Me![ФайлОтчёта], Отступ, Всего
      Case 201
        ОтчётФорма201 Me![Шаблон], Me![ФайлОтчёта], Отступ, Всего
      Case 301
        ОтчётФорма301 Me![Шаблон], Me![ФайлОтчёта], Отступ, Всего
      Case Else
        ОтчётНикакой Me![Шаблон], Me![ФайлОтчёта], Отступ, Всего
      End Select
  ' ***********************
  
    Me![ОтчётПлановый] = False
    ЗакрытьПрофиль
    ОтчётФайл = ""
End Sub


