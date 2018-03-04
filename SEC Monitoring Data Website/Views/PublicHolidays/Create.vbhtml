@ModelType SEC_Monitoring_Data_Website.CreatePublicHolidayViewModel

@Code
    ViewData("Title") = "Create Public Holiday"
End Code

<h2>Create Public Holiday for @Model.CountryName</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>PublicHoliday</legend>

         <table class="create-table">
             <tr>
                 <th>
                     Public Holiday
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.PublicHoliday.HolidayName)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.PublicHoliday.HolidayName)
                 </td>
             </tr>
             <tr>
                 <th>
                     Public Holiday
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.PublicHoliday.HolidayDate)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.PublicHoliday.HolidayDate)
                 </td>
             </tr>
         </table>

        @Html.HiddenFor(Function(model) model.CountryId)

        <p>
            @Html.JQueryUI.Button("Create")
        </p>
    </fieldset>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
