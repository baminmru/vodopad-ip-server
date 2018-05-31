Imports System.ComponentModel
Imports System.Drawing.Design
Imports System.Globalization
Imports System.Reflection
Imports System.Xml.Serialization

Namespace PropertyGridEx

    <Serializable(), XmlRootAttribute("CustomProperty")> _
    Public Class CustomProperty

#Region "Protected variables"

        ' Common properties
        Protected sName As String = ""
        Protected oValue As Object = Nothing
        Protected bIsReadOnly As Boolean = False
        Protected bVisible As Boolean = True
        Protected sDescription As String = ""
        Protected sCategory As String = ""
        Protected bIsPassword As Boolean = Nothing
        Protected bIsPercentage As Boolean = Nothing

        ' Filename editor properties
        Protected sFilter As String = Nothing
        Protected eDialogType As UIFilenameEditor.FileDialogType = UIFilenameEditor.FileDialogType.LoadFileDialog
        Protected bUseFileNameEditor As Boolean = False

        ' Custom choices properties
        Protected oChoices As CustomChoices = Nothing

        ' Browsable properties
        Protected bIsBrowsable As Boolean = False
        Protected eBrowsablePropertyLabel As BrowsableTypeConverter.LabelStyle = BrowsableTypeConverter.LabelStyle.lsEllipsis

        ' Dynamic properties
        Protected bRef As Boolean = False
        Protected oRef As Object = Nothing
        Protected sProp As String = ""

        ' Databinding abstraction
        Protected oDatasource As Object = Nothing
        Protected sDisplayMember As String = Nothing
        Protected sValueMember As String = Nothing
        Protected oSelectedValue As Object = Nothing
        Protected oSelectedItem As Object = Nothing
        Protected bIsDropdownResizable As Boolean = Nothing

        ' Extended Attributes
        <NonSerialized()> Protected oCustomAttributes As AttributeCollection = Nothing

        ' Custom Editor and Custom Type Converter
        <NonSerialized()> Protected oCustomEditor As UITypeEditor = Nothing
        <NonSerialized()> Protected oCustomTypeConverter As TypeConverter = Nothing

#End Region

#Region "Public methods"

        Public Sub New()
            sName = "New Property"
            oValue = New String("")
        End Sub

        Public Sub New(ByVal strName As String, ByVal objValue As Object, Optional ByVal boolIsReadOnly As Boolean = True, Optional ByVal strCategory As String = "", Optional ByVal strDescription As String = "", Optional ByVal boolVisible As Boolean = True)
            sName = strName
            oValue = objValue
            bIsReadOnly = boolIsReadOnly
            sDescription = strDescription
            sCategory = strCategory
            bVisible = boolVisible
        End Sub

        Public Sub New(ByVal strName As String, ByRef objRef As Object, ByVal strProp As String, Optional ByVal boolIsReadOnly As Boolean = True, Optional ByVal strCategory As String = "", Optional ByVal strDescription As String = "", Optional ByVal boolVisible As Boolean = True)
            sName = strName
            bIsReadOnly = boolIsReadOnly
            sDescription = strDescription
            sCategory = strCategory
            bVisible = boolVisible

            bRef = True
            oRef = objRef
            sProp = strProp
        End Sub

        Public Sub RebuildAttributes()
            If bUseFileNameEditor Then
                BuildAttributes_FilenameEditor()
            ElseIf Not oChoices Is Nothing Then
                BuildAttributes_CustomChoices()
            ElseIf Not oDatasource Is Nothing Then
                BuildAttributes_ListboxEditor()
            ElseIf bIsBrowsable Then
                BuildAttributes_BrowsableProperty()
            End If
        End Sub

#End Region

#Region "Private methods"

        Private Sub BuildAttributes_FilenameEditor()
            Dim attrs As ArrayList = New ArrayList()
            Dim FilterAttribute As New UIFilenameEditor.FileDialogFilterAttribute(sFilter)
            Dim SaveDialogAttribute As New UIFilenameEditor.SaveFileAttribute
            Dim attrArray As Attribute()
            attrs.Add(FilterAttribute)
            If eDialogType = UIFilenameEditor.FileDialogType.SaveFileDialog Then attrs.Add(SaveDialogAttribute)
            attrArray = attrs.ToArray(GetType(Attribute))
            oCustomAttributes = New AttributeCollection(attrArray)
        End Sub

        Private Sub BuildAttributes_CustomChoices()
            If Not oChoices Is Nothing Then
                Dim list As New CustomChoices.CustomChoicesAttributeList(oChoices.Items)
                Dim attrs As ArrayList = New ArrayList
                Dim attrArray As Attribute()
                attrs.Add(list)
                attrArray = attrs.ToArray(GetType(Attribute))
                oCustomAttributes = New AttributeCollection(attrArray)
            End If
        End Sub

        Private Sub BuildAttributes_ListboxEditor()
            If Not oDatasource Is Nothing Then
                Dim ds As New UIListboxEditor.UIListboxDatasource(oDatasource)
                Dim vm As New UIListboxEditor.UIListboxValueMember(sValueMember)
                Dim dm As New UIListboxEditor.UIListboxDisplayMember(sDisplayMember)
                Dim ddr As UIListboxEditor.UIListboxIsDropDownResizable = Nothing
                Dim attrs As ArrayList = New ArrayList
                attrs.Add(ds)
                attrs.Add(vm)
                attrs.Add(dm)
                If bIsDropdownResizable Then
                    ddr = New UIListboxEditor.UIListboxIsDropDownResizable
                    attrs.Add(ddr)
                End If
                Dim attrArray As Attribute()
                attrArray = attrs.ToArray(GetType(Attribute))
                oCustomAttributes = New AttributeCollection(attrArray)
            End If
        End Sub

        Private Sub BuildAttributes_BrowsableProperty()
            Dim style As New BrowsableTypeConverter.BrowsableLabelStyleAttribute(eBrowsablePropertyLabel)
            oCustomAttributes = New AttributeCollection(New Attribute() {style})
        End Sub

        Private Property DataColumn() As Object
            Get
                Dim oRow As DataRow = CType(oRef, System.Data.DataRow)
                If oDatasource Is Nothing Then
                    Return oRow(sProp)
                Else
                    Dim oLookupTable As DataTable = CType(oDatasource, DataTable)
                    If Not oLookupTable Is Nothing Then
                        Return oLookupTable.Select(sValueMember & "=" & oRow(sProp))(0).Item(sDisplayMember)
                    Else
                        Err.Raise(vbObjectError + 513, , "Bind of DataRow with a DataSource that is not a DataTable is impossible")
                        Return Nothing
                    End If
                End If
            End Get
            Set(ByVal value As Object)
                Dim oRow As DataRow = CType(oRef, System.Data.DataRow)
                If oDatasource Is Nothing Then
                    oRow(sProp) = value
                Else
                    Dim oLookupTable As DataTable = CType(oDatasource, DataTable)
                    If Not oLookupTable Is Nothing Then
                        If oLookupTable.Columns(sDisplayMember).DataType.Equals(System.Type.GetType("System.String")) Then

                            oRow(sProp) = oLookupTable.Select(oLookupTable.Columns(sDisplayMember).ColumnName & " = '" & value & "'")(0).Item(sValueMember)
                        Else
                            oRow(sProp) = oLookupTable.Select(oLookupTable.Columns(sDisplayMember).ColumnName & " = " & value)(0).Item(sValueMember)
                        End If
                    Else
                        Err.Raise(vbObjectError + 514, , "Bind of DataRow with a DataSource that is not a DataTable is impossible")
                    End If
                End If
            End Set
        End Property

#End Region

#Region "Public properties"

        <Category("Appearance"), _
         DescriptionAttribute("Display Name of the CustomProperty."), _
         ParenthesizePropertyName(True), _
         XmlElementAttribute("Name")> _
        Public Property Name() As String
            Get
                Return sName
            End Get
            Set(ByVal value As String)
                sName = value
            End Set
        End Property

        <Category("Appearance"), _         
         DescriptionAttribute("Set read only attribute of the CustomProperty."), _
         XmlElementAttribute("ReadOnly")> _
        Public Property IsReadOnly() As Boolean
            Get
                Return bIsReadOnly
            End Get
            Set(ByVal value As Boolean)
                bIsReadOnly = value
            End Set
        End Property

        <Category("Appearance"), _
         DescriptionAttribute("Set visibility attribute of the CustomProperty.")> _
        Public Property Visible() As Boolean
            Get
                Return bVisible
            End Get
            Set(ByVal value As Boolean)
                bVisible = value
            End Set
        End Property

        <Category("Appearance"), _
         DescriptionAttribute("Represent the Value of the CustomProperty.")> _
        Public Property Value() As Object
            Get
                If bRef Then
                    Select Case oRef.GetType.FullName
                        Case "System.Data.DataRow"
                            Return Me.DataColumn
                        Case Else
                            Return CallByName(oRef, sProp, CallType.Get)
                    End Select
                Else
                    Return oValue
                End If
            End Get
            Set(ByVal value As Object)
                If bRef Then
                    Select Case oRef.GetType.FullName
                        Case "System.Data.DataRow"
                            Me.DataColumn = value
                        Case Else
                            CallByName(oRef, sProp, CallType.Set, value)
                    End Select
                Else
                    oValue = value
                End If
            End Set
        End Property

        <Category("Appearance"), _
         DescriptionAttribute("Set description associated with the CustomProperty.")> _
        Public Property Description() As String
            Get
                Return sDescription
            End Get
            Set(ByVal value As String)
                sDescription = value
            End Set
        End Property

        <Category("Appearance"), _
         DescriptionAttribute("Set category associated with the CustomProperty.")> _
        Public Property Category() As String
            Get
                Return sCategory
            End Get
            Set(ByVal value As String)
                sCategory = value
            End Set
        End Property

        <XmlIgnore()> _
        Public ReadOnly Property Type() As System.Type
            Get
                If bRef Then
                    Return Value.GetType
                Else
                    Return oValue.GetType
                End If
            End Get
        End Property

        <XmlIgnore()> _
        Public Property Attributes() As AttributeCollection
            Get
                Return oCustomAttributes
            End Get
            Set(ByVal value As AttributeCollection)
                oCustomAttributes = value
            End Set
        End Property

        <Category("Behavior"), _
         DescriptionAttribute("Indicates if the property is browsable or not."), _
         XmlElementAttribute(IsNullable:=False)> _
        Public Property IsBrowsable() As Boolean
            Get
                Return bIsBrowsable
            End Get
            Set(ByVal value As Boolean)
                bIsBrowsable = value
                If value = True Then
                    BuildAttributes_BrowsableProperty()
                End If
            End Set
        End Property

        <Category("Behavior"), _
         DescriptionAttribute("Indicates the style of the label when a property is browsable."), _
         XmlElementAttribute(IsNullable:=False)> _
        Public Property BrowsableLabelStyle() As BrowsableTypeConverter.LabelStyle
            Get
                Return eBrowsablePropertyLabel
            End Get
            Set(ByVal value As BrowsableTypeConverter.LabelStyle)
                Dim Update As Boolean = False
                If value <> eBrowsablePropertyLabel Then Update = True
                eBrowsablePropertyLabel = value
                If Update Then
                    Dim style As New BrowsableTypeConverter.BrowsableLabelStyleAttribute(value)
                    oCustomAttributes = New AttributeCollection(New Attribute() {style})
                End If
            End Set
        End Property

        <Category("Behavior"), _
         DescriptionAttribute("Indicates if the property is masked or not."), _
         XmlElementAttribute(IsNullable:=False)> _
        Public Property IsPassword() As Boolean
            Get
                Return bIsPassword
            End Get
            Set(ByVal value As Boolean)
                bIsPassword = value
            End Set
        End Property

        <Category("Behavior"), _
         DescriptionAttribute("Indicates if the property represents a value in percentage."), _
         XmlElementAttribute(IsNullable:=False)> _
        Public Property IsPercentage() As Boolean
            Get
                Return bIsPercentage
            End Get
            Set(ByVal value As Boolean)
                bIsPercentage = value
            End Set
        End Property

        <Category("Behavior"), _
         DescriptionAttribute("Indicates if the property uses a FileNameEditor converter."), _
         XmlElementAttribute(IsNullable:=False)> _
        Public Property UseFileNameEditor() As Boolean
            Get
                Return bUseFileNameEditor
            End Get
            Set(ByVal value As Boolean)
                bUseFileNameEditor = value
            End Set
        End Property

        <Category("Behavior"), _
         DescriptionAttribute("Apply a filter to FileNameEditor converter."), _
         XmlElementAttribute(IsNullable:=False)> _
        Public Property FileNameFilter() As String
            Get
                Return sFilter
            End Get
            Set(ByVal value As String)
                Dim UpdateAttributes As Boolean = False
                If value <> sFilter Then UpdateAttributes = True
                sFilter = value
                If UpdateAttributes Then BuildAttributes_FilenameEditor()
            End Set
        End Property

        <Category("Behavior"), _
         DescriptionAttribute("DialogType of the FileNameEditor."), _
         XmlElementAttribute(IsNullable:=False)> _
        Public Property FileNameDialogType() As UIFilenameEditor.FileDialogType
            Get
                Return eDialogType
            End Get
            Set(ByVal value As UIFilenameEditor.FileDialogType)
                Dim UpdateAttributes As Boolean = False
                If value <> eDialogType Then UpdateAttributes = True
                eDialogType = value
                If UpdateAttributes Then BuildAttributes_FilenameEditor()
            End Set
        End Property

        <Category("Behavior"), _
         DescriptionAttribute("Custom Choices list."), _
         XmlIgnore()> _
        Public Property Choices() As CustomChoices
            Get
                Return oChoices
            End Get
            Set(ByVal value As CustomChoices)
                oChoices = value
                BuildAttributes_CustomChoices()
            End Set
        End Property

        <Category("Databinding"), _
         XmlIgnore()> _
        Public Property Datasource() As Object
            Get
                Return oDatasource
            End Get
            Set(ByVal value As Object)
                oDatasource = value
                BuildAttributes_ListboxEditor()
            End Set
        End Property

        <Category("Databinding"), _
         XmlElementAttribute(IsNullable:=False)> _
        Public Property ValueMember() As String
            Get
                Return sValueMember
            End Get
            Set(ByVal value As String)
                sValueMember = value
                BuildAttributes_ListboxEditor()
            End Set
        End Property

        <Category("Databinding"), _
         XmlElementAttribute(IsNullable:=False)> _
        Public Property DisplayMember() As String
            Get
                Return sDisplayMember
            End Get
            Set(ByVal value As String)
                sDisplayMember = value
                BuildAttributes_ListboxEditor()
            End Set
        End Property

        <Category("Databinding"), _
         XmlElementAttribute(IsNullable:=False)> _
        Public Property SelectedValue() As Object
            Get
                Return oSelectedValue
            End Get
            Set(ByVal value As Object)
                oSelectedValue = value
            End Set
        End Property

        <Category("Databinding"), _
         XmlElementAttribute(IsNullable:=False)> _
        Public Property SelectedItem() As Object
            Get
                Return oSelectedItem
            End Get
            Set(ByVal value As Object)
                oSelectedItem = value
            End Set
        End Property

        <Category("Databinding"), _
         XmlElementAttribute(IsNullable:=False)> _
        Public Property IsDropdownResizable() As Boolean
            Get
                Return bIsDropdownResizable
            End Get
            Set(ByVal value As Boolean)
                bIsDropdownResizable = value
                BuildAttributes_ListboxEditor()
            End Set
        End Property

        <XmlIgnore()> _
        Public Property CustomEditor() As UITypeEditor
            Get
                Return oCustomEditor
            End Get
            Set(ByVal value As UITypeEditor)
                oCustomEditor = value
            End Set
        End Property

        <XmlIgnore()> _
        Public Property CustomTypeConverter() As TypeConverter
            Get
                Return oCustomTypeConverter
            End Get
            Set(ByVal value As TypeConverter)
                oCustomTypeConverter = value
            End Set
        End Property

#End Region

#Region "CustomPropertyDescriptor"
        Public Class CustomPropertyDescriptor
            Inherits PropertyDescriptor
            Protected oCustomProperty As CustomProperty

            Public Sub New(ByVal myProperty As CustomProperty, ByVal attrs() As Attribute)
                MyBase.New(myProperty.Name, attrs)
                If myProperty Is Nothing Then
                    oCustomProperty = Nothing
                Else : oCustomProperty = myProperty
                End If
            End Sub

            Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
                Return False
            End Function

            Public Overrides ReadOnly Property ComponentType() As System.Type
                Get
                    Return Me.GetType
                End Get
            End Property

            Public Overrides Function GetValue(ByVal component As Object) As Object
                Return oCustomProperty.Value
            End Function

            Public Overrides ReadOnly Property IsReadOnly() As Boolean
                Get
                    Return oCustomProperty.IsReadOnly
                End Get
            End Property

            Public Overrides ReadOnly Property PropertyType() As System.Type
                Get
                    Return oCustomProperty.Type
                End Get
            End Property

            Public Overrides Sub ResetValue(ByVal component As Object)
                oCustomProperty.Value = Nothing
            End Sub

            Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
                oCustomProperty.Value = value
            End Sub

            Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
                Return False
            End Function

            Public Overrides ReadOnly Property Description() As String
                Get
                    Return oCustomProperty.Description
                End Get
            End Property

            Public Overrides ReadOnly Property Category() As String
                Get
                    Return oCustomProperty.Category
                End Get
            End Property

            Public Overrides ReadOnly Property DisplayName() As String
                Get
                    Return oCustomProperty.Name
                End Get
            End Property

            Public Overrides ReadOnly Property IsBrowsable() As Boolean
                Get
                    Return oCustomProperty.IsBrowsable
                End Get
            End Property

            Public ReadOnly Property CustomProperty() As CustomProperty
                Get
                    Return oCustomProperty
                End Get
            End Property
        End Class
#End Region

    End Class

End Namespace









