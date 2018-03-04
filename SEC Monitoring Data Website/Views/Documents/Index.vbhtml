@ModelType SEC_Monitoring_Data_Website.ViewDocumentsViewModel

@Code
    ViewData("Title") = "Document List"
End Code

@Html.AntiForgeryToken()


<h2>List of Documents</h2>

@Html.Partial("SearchableIndexDTHeader")

<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.Documents)
</div>

@Html.Partial("NavigationButtons", Model.NavigationButtons)