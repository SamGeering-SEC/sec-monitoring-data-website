@ModelType SEC_Monitoring_Data_Website.Country

@Code
    ViewData("Title") = "Edit Country"
End Code

<h2>Edit</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Country</legend>

         @Using t = Html.JQueryUI().BeginTabs()

                 t.Tab("Basic Details", "basic_details")
                 t.Tab("Public Holidays", "public_holidays")


                 ' Basic Details Tab
             @Using t.BeginPanel
                 @Html.Partial("Edit_BasicDetails", Model)
             End Using

             ' Public Holidays Tab
             @Using t.BeginPanel
                 @Html.Partial("Edit_PublicHolidays", Model)
             End Using

         End Using

    
    </fieldset>
End Using


@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
