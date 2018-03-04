@ModelType SEC_Monitoring_Data_Website.ViewOrganisationsViewModel

@Code
    ViewData("Title") = "Organisations List"
End Code

<h2>List of Organisations</h2>

@Html.Partial("SearchableIndexHeader")

@Html.Partial("AlphabeticalIndexHeader", Model)


<div id='@Model.TableId'>

    @Html.Partial("Index_Table", Model.Organisations)

</div>


@Html.Partial("NavigationButtons", Model.NavigationButtons)
