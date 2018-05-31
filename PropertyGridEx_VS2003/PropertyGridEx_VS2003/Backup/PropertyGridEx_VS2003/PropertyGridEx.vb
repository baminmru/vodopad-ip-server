Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Reflection

Namespace PropertyGridEx
    Public Class PropertyGridEx
        Inherits PropertyGrid

#Region "Protected variables and objects"
        ' CustomPropertyCollection & CustomPropertyCollectionSet
        Protected oCustomPropertyCollection As CustomPropertyCollection
        Protected oCustomPropertyCollectionSet As CustomPropertyCollectionSet
        Protected bShowCustomProperties As Boolean
        Protected bShowCustomPropertiesSet As Boolean

        ' Internal PropertyGrid Objects
        Protected oPropertyGridView As Object
        Protected oPropertyGridEntries As FieldInfo
        Protected oLabelWidth As FieldInfo

        ' Properties variables
        Protected bAutoSizeProperties As Boolean
        Protected bDrawFlatToolbar As Boolean
#End Region

#Region "Public Functions"

        Public Sub MoveSplitterTo(ByVal x As Integer)
            oPropertyGridView.GetType().InvokeMember("MoveSplitterTo", BindingFlags.InvokeMethod Or BindingFlags.NonPublic Or BindingFlags.Instance, Nothing, oPropertyGridView, New Object() {x})
        End Sub

        Public Overrides Sub Refresh()
            If bShowCustomPropertiesSet Then
                MyBase.SelectedObjects = oCustomPropertyCollectionSet.ToArray
            End If
            MyBase.Refresh()
            If bAutoSizeProperties Then AutoSizeSplitter()
        End Sub
#End Region

#Region "Protected Functions"
        Protected Sub AutoSizeSplitter(Optional ByVal RightMargin As Integer = 32)

            Dim oItemCollection As GridItemCollection = oPropertyGridEntries.GetValue(oPropertyGridView)
            If oItemCollection Is Nothing Then Exit Sub
            Dim oGraphics As Drawing.Graphics = Drawing.Graphics.FromHwnd(Me.Handle)
            Dim CurWidth As Integer = 0
            Dim MaxWidth As Integer = 0

            For Each oItem As GridItem In oItemCollection
                If oItem.GridItemType = GridItemType.Property Then
                    CurWidth = oGraphics.MeasureString(oItem.Label, Me.Font).Width + RightMargin
                    If CurWidth > MaxWidth Then
                        MaxWidth = CurWidth
                    End If
                End If
            Next

            MoveSplitterTo(MaxWidth)
        End Sub
#End Region

#Region "Properties"

        <Category("Behavior"), _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Content), _
         DescriptionAttribute("Set the collection of the CustomProperty. Set ShowCustomProperties to True to enable it."), _
         RefreshProperties(RefreshProperties.Repaint)> _
        Public ReadOnly Property Item() As CustomPropertyCollection
            Get
                Return oCustomPropertyCollection
            End Get
        End Property

        <Category("Behavior"), _
         DesignerSerializationVisibility(DesignerSerializationVisibility.Content), _
         DescriptionAttribute("Set the CustomPropertyCollectionSet. Set ShowCustomPropertiesSet to True to enable it.")> _
        Public ReadOnly Property ItemSet() As CustomPropertyCollectionSet
            Get
                Return oCustomPropertyCollectionSet
            End Get
        End Property

        <Category("Behavior"), _
         DefaultValue(False), _
         DescriptionAttribute("Move automatically the splitter to better fit all the properties shown.")> _
        Public Property AutoSizeProperties() As Boolean
            Get
                Return bAutoSizeProperties
            End Get
            Set(ByVal Value As Boolean)
                bAutoSizeProperties = Value
                If Value Then AutoSizeSplitter()
            End Set
        End Property

        <Category("Behavior"), _
         DefaultValue(False), _
         DescriptionAttribute("Use the custom properties collection as SelectedObject."), _
         RefreshProperties(RefreshProperties.All)> _
        Public Property ShowCustomProperties() As Boolean
            Get
                Return bShowCustomProperties
            End Get
            Set(ByVal value As Boolean)
                If value = True Then
                    bShowCustomPropertiesSet = False
                    MyBase.SelectedObject = oCustomPropertyCollection
                End If
                bShowCustomProperties = value
            End Set
        End Property

        <Category("Behavior"), _
         DefaultValue(False), _
         DescriptionAttribute("Use the custom properties collections as SelectedObjects."), _
         RefreshProperties(RefreshProperties.All)> _
        Public Property ShowCustomPropertiesSet() As Boolean
            Get
                Return bShowCustomPropertiesSet
            End Get
            Set(ByVal value As Boolean)
                If value = True Then
                    bShowCustomProperties = False
                    MyBase.SelectedObjects = oCustomPropertyCollectionSet.ToArray
                End If
                bShowCustomPropertiesSet = value
            End Set
        End Property

#End Region

#Region " Windows Form Designer generated code "

        Public Sub New()
            MyBase.New()

            'This call is required by the Windows Form Designer.
            InitializeComponent()

            'Add any initialization after the InitializeComponent() call
            oCustomPropertyCollection = New CustomPropertyCollection
            oCustomPropertyCollectionSet = New CustomPropertyCollectionSet

            oPropertyGridView = MyBase.GetType.BaseType.InvokeMember("gridView", BindingFlags.NonPublic Or BindingFlags.GetField Or BindingFlags.Instance, Nothing, Me, Nothing)
            oPropertyGridEntries = oPropertyGridView.GetType.GetField("allGridEntries", BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.DeclaredOnly)
            oLabelWidth = oPropertyGridView.GetType.GetField("labelWidth", BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.DeclaredOnly)

        End Sub

        'UserControl overrides dispose to clean up the component list.
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not (components Is Nothing) Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        'Required by the Windows Form Designer
        Private components As System.ComponentModel.IContainer

        'NOTE: The following procedure is required by the Windows Form Designer
        'It can be modified using the Windows Form Designer.  
        'Do not modify it using the code editor.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
            components = New System.ComponentModel.Container
        End Sub

#End Region

    End Class

End Namespace

