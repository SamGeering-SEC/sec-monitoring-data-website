@ModelType SEC_Monitoring_Data_Website.CreateOrganisationViewModel

@Code
    ViewData("Title") = "Create New Organisation"
End Code

<h2>Create New Organisation</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Organisation</legend>

         <table class="create-table">
             <tr>
                 <th>
                     Full Name
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.Organisation.FullName)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.Organisation.FullName)
                 </td>
             </tr>
             <tr>
                 <th>
                     Short Name
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.Organisation.ShortName)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.Organisation.ShortName)
                 </td>
             </tr>
             <tr>
                 <th>
                     Address
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.Organisation.Address)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.Organisation.Address)
                 </td>
             </tr>
             <tr>
                 <th>
                     Organisation Type
                 </th>
                 <td>
                     @Html.DropDownListFor(Function(model) model.OrganisationTypeId, Model.OrganisationTypeList, String.Empty)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.OrganisationTypeId)
                 </td>
             </tr>
         </table>

        <p>
            @Html.JQueryUI.Button("Create")
        </p>
    </fieldset>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
