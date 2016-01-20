Imports System.Net.Sockets

Public Class ROBUSTELSocket
    Inherits GRPSSocket


    Public Overrides ReadOnly Property SocketType() As String
        Get
            Return "ROBUSTELSOCKET"
        End Get
    End Property


    Public Sub New(ByRef aSocket As Socket)
        MyBase.New(aSocket)
    End Sub






End Class
