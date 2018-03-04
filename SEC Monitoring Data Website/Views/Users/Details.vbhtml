@ModelType SEC_Monitoring_Data_Website.UserDetailsViewModel

@Code
    ViewData("Title") = "User Details"
End Code

<h2>User Details</h2>

@Html.Partial("TabsView", Model.Tabs)

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "User")

<script type="text/javascript">
    function resetPasswordSuccess(data, textStatus, xhr) {
        window.location.href = data.redirectToUrl;
    }
</script>

@Using (Html.JQueryUI().Begin(New Dialog().Title("Reset User Password").AutoOpen(False).Modal(True).ConfirmAjax(".ResetUserPasswordLink",
                                                                                                                      "Yes",
                                                                                                                      "No",
                                                                                                                      New AjaxSettings With {.Method = HttpVerbs.Post,
                                                                                                                                             .Success = "resetPasswordSuccess"})))
    @<p>
        @Html.Raw("Would you like to Reset the Password for this User?")
    </p>

End Using