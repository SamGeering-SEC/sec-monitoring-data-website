@ModelType ViewProjectsViewModel

@Code
    ViewData("Title") = "My Projects"
End Code
<div>
    <h2>My Projects</h2>
</div>

<br />

@Html.Partial("Index_Map", Model.Projects)

<br />

@Html.Partial("SearchableIndexHeader")
<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.Projects)
</div>

<br />

@Html.Partial("NavigationButtons", Model.NavigationButtons)

