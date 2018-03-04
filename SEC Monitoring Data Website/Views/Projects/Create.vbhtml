@ModelType SEC_Monitoring_Data_Website.CreateProjectViewModel

@Code
    ViewData("Title") = "Create New Project"
End Code

<h2>Create New Project</h2>

@Using Html.BeginForm()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Project</legend>
         
         <div>
             <table class="create-table">
                 <tr>
                     <th>
                         Full Name
                     </th>
                     <td>
                         @Html.EditorFor(Function(model) model.Project.FullName)
                     </td>
                     <td>
                         @Html.ValidationMessageFor(Function(model) model.Project.FullName)
                     </td>
                 </tr>
                 <tr>
                     <th>
                         Short Name
                     </th>
                     <td>
                         @Html.EditorFor(Function(model) model.Project.ShortName)
                     </td>
                     <td>
                         @Html.ValidationMessageFor(Function(model) model.Project.ShortName)
                     </td>
                 </tr>
                 <tr>
                     <th>
                         Project Number
                     </th>
                     <td>
                         @Html.EditorFor(Function(model) model.Project.ProjectNumber)
                     </td>
                     <td>
                         @Html.ValidationMessageFor(Function(model) model.Project.ProjectNumber)
                     </td>
                 </tr>
                 <tr>
                     <th>
                         Location
                     </th>
                     <td>
                         @Html.EditorFor(Function(model) model.Project.ProjectGeoCoords)
                     </td>
                     <td>
                         @Html.ValidationMessageFor(Function(model) model.Project.ProjectGeoCoords)
                     </td>
                 </tr>
                 @Html.HiddenFor(Function(model) model.Project.MapLink)
                 @*<tr>
                     <th>
                         Map Link
                     </th>
                     <td>
                         @Html.EditorFor(Function(model) model.Project.MapLink)
                     </td>
                     <td>
                         @Html.ValidationMessageFor(Function(model) model.Project.MapLink)
                     </td>
                 </tr>*@
                 <tr>
                     <th>
                         Client Organisation
                     </th>
                     <td>
                         @Html.DropDownListFor(Function(model) model.ClientOrganisationId,
                                               Model.ClientOrganisationList,
                                               "Please select a Client Organisation...")
                     </td>
                     <td>
                         @Html.ValidationMessageFor(Function(model) model.ClientOrganisationId)
                     </td>
                 </tr>
                 <tr>
                     <th>
                         Country
                     </th>
                     <td>
                         @Html.DropDownListFor(Function(model) model.CountryId,
                                               Model.CountryList,
                                               "Please select a Country...")
                     </td>
                     <td>
                         @Html.ValidationMessageFor(Function(model) model.CountryId)
                     </td>
                 </tr>
                 <tr>
                     <th>
                         Working Hours
                     </th>
                     <td>
                         @Html.EditorFor(Function(model) model.WorkingWeekViewModel)
                     </td>
                 </tr>
             </table>
         </div>
             <p>
                 @Html.JQueryUI.Button("Create")
             </p>
</fieldset>

End Using


