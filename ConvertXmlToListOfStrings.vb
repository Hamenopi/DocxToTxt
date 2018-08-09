Private Shared Function ConvertXmlToList(fileName) As IEnumerable(Of String)
        Dim list As New List(Of String)
        Dim recordValue As Boolean = False
        Dim value As String = ""

        Dim settings As New XmlReaderSettings With {.DtdProcessing = DtdProcessing.Parse}
        Using reader As XmlReader = XmlReader.Create(fileName, settings)
            reader.MoveToContent()

            While reader.Read()
                Select Case reader.NodeType
                    Case XmlNodeType.Element

                        Select Case reader.Name
                            Case "w:p"
                                recordValue = True
                        End Select

                    Case XmlNodeType.Text
                        If recordValue Then
                            value &= reader.Value
                        End If

                    Case XmlNodeType.EndElement
                        If reader.Name = "w:p" Then
                            recordValue = False
                            list.Add(value)
                            value = ""
                        End If

                End Select
            End While
        End Using
        Return list
    End Function
