@ModelType SEC_Monitoring_Data_Website.ViewContactsViewModel

@Code
    ViewData("Title") = "Index"
End Code

<h2>My Contacts</h2>

@Html.Partial("SearchableIndexHeader")

<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.Contacts)

</div>

@Html.Partial("NavigationButtons", Model.NavigationButtons)