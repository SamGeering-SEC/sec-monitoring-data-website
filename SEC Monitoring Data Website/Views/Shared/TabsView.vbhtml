@ModelType IEnumerable(Of TabViewModel)

@Using t = Html.JQueryUI().Begin(New Tabs().OnLoad("addButtonAnimations"))

    ' Tabs
    For Each tabViewModel In Model
        If tabViewModel.isAjax = True Then
            t.AjaxTab(tabViewModel.tabLabel, tabViewModel.tabId)
        Else
            t.Tab(tabViewModel.tabLabel, tabViewModel.tabId)
        End If
    Next
    
    ' Panels
    For Each tabViewModel In Model
        If tabViewModel.isAjax = False Then
            If tabViewModel.tabDiv <> "" Then
                Html.Raw("<div id='" + tabViewModel.tabDiv + "'>")
            End If
            Using t.BeginPanel
                Html.RenderPartial("~/Views/" +
                                   tabViewModel.controllerName + "/" +
                                   tabViewModel.viewName + ".vbhtml",
                                   tabViewModel.modelObject)
            End Using
            If tabViewModel.tabDiv <> "" Then
                Html.Raw("</div>")
            End If
        End If
    Next

End Using


