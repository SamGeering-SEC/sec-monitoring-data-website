@ModelType SEC_Monitoring_Data_Website.ViewDocumentTypesViewModel

@Code
    ViewData("Title") = "Document Types List"
End Code

<h2>List of Document Types</h2>

@Html.Partial("SearchableIndexHeader")

<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.DocumentTypes)
</div>

@Html.Partial("NavigationButtons", Model.NavigationButtons)