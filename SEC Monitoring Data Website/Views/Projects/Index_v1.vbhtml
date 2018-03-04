@ModelType IEnumerable(Of SEC_Monitoring_Data_Website.Project)

@Code
    ViewData("Title") = "My Projects"
End Code

<h2>My Projects</h2>

@Using t = Html.JQueryUI().BeginTabs()

    t.Tab("Map", "map")
    t.Tab("Project List", "project_list")

    ' Map Tab
    @Using t.BeginPanel
        @Html.Partial("Index_Map", Model)
    End Using

    ' Project List Tab
    @Using t.BeginPanel
        @Html.Partial("Index_Table", Model)
    End Using



End Using

<p>
    @Html.RouteLink("Add a new Project",
                     "ProjectCreateRoute",
                     Nothing,
                     New With {.class = "sitewide-button-64 create-button-64"})
</p>

