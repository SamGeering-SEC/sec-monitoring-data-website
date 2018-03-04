@ModelType SEC_Monitoring_Data_Website.ProjectDetailsViewModel

@Code
    ViewData("Title") = "Project Details"
End Code

<h2>@Model.Project.FullName Project Details</h2>

@Html.Partial("TabsView", Model.Tabs)

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "Project")
