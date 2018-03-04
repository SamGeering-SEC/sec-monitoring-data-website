@ModelType ViewOrganisationTypesViewModel

@Code
    ViewData("Title") = "Index"
End Code

<h2>List of Organisation Types</h2>

@Html.Partial("SearchableIndexHeader")

<div id='@Model.TableId'>

    @Html.Partial("Index_Table", Model.OrganisationTypes)

</div>

@Html.Partial("NavigationButtons", Model.NavigationButtons)