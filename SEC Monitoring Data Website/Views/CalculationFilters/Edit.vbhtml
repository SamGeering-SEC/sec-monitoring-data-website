@ModelType SEC_Monitoring_Data_Website.EditCalculationFilterViewModel

@Code
    ViewData("Title") = "Edit Calculation Filter"
End Code

<h2>Edit Calculation Filter</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>CalculationFilter</legend>

        @Html.HiddenFor(Function(model) model.CalculationFilter.Id)

         @Using t = Html.JQueryUI().BeginTabs()

                 t.Tab("Basic Details", "basic_details")
                 t.Tab("Days Of Week", "days_of_week")


                 ' Basic Details Tab
             @Using t.BeginPanel
                 @Html.Partial("Edit_BasicDetails", Model)
             End Using

             ' Days Of Week Tab
             @Using t.BeginPanel
                 @Html.Partial("Edit_DaysOfWeek", Model)
             End Using

         End Using
       
        <p>
            @Html.JQueryUI.Button("Save")
        </p>
    </fieldset>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
