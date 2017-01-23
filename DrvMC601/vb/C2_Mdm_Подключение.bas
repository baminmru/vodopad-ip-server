Attribute VB_Name = "C2_Mdm_Подключение"
Option Explicit


Global ЖдёмПодключения As Boolean

Function Подключиться(Телефон As String) As Boolean
    ЖдёмПодключения = True
    Подключение.Show
    Подключение.Для Телефон
    Подключение.ЗапуститьПодключение
    Do                      ' Ждём завершения приёма
        DoEvents
        DoPoll
        Sleep 50
      Loop Until ЖдёмПодключения = False
    Unload Подключение
    Подключиться = (MdmKWord = AC_CT)
End Function

