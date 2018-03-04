@ModelType SEC_Monitoring_Data_Website.EditMeasurementViewViewModel

@Code
    ViewData("Title") = "Edit Measurement View"
End Code

<script type="text/javascript">

    function deleteGroupSuccess(data, textStatus, jqXHR) {
        window.location.href = data.redirectToUrl;
    }

    function deleteGroupError(jqXHR, textStatus, errorThrown) {
        alert(textStatus);
    }

    function deleteSequenceSuccess(data, textStatus, jqXHR) {
        window.location.href = data.redirectToUrl;
    }

    function deleteSequenceError(jqXHR, textStatus, errorThrown) {
        alert(textStatus);
    }

</script>

<h2>Edit Measurement View</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>MeasurementView</legend>

        @Html.HiddenFor(Function(model) model.MeasurementView.Id)

        @Using t = Html.JQueryUI().BeginTabs()

                t.Tab("Basic Details", "basic_details")
                t.Tab("Comment Types", "comment_types")
                t.Tab("Projects", "projects")


                ' Basic Details Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_BasicDetails", Model)
            End Using

            ' Comment Types Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_CommentTypes", Model)
            End Using

            ' Projects
            @Using t.BeginPanel
                @Html.Partial("Edit_Projects", Model)
            End Using

        End Using

        <p>
            @Html.JQueryUI.Button("Save")
        </p>
    </fieldset>
End Using

@Using (Html.JQueryUI().Begin(New Dialog().Title("Delete Sequence").AutoOpen(False).Modal(True).ConfirmAjax(".DeleteSequenceLink",
                                                                                                            "Yes",
                                                                                                            "No",
                                                                                                            New AjaxSettings With {.Method = HttpVerbs.Post,
                                                                                                                                   .Success = "deleteSequenceSuccess",
                                                                                                                                   .Error = "deleteSequenceError"})))
    @<p>
        @Html.Raw("Would you like to delete this Sequence?")
    </p>
End Using

@Using (Html.JQueryUI().Begin(New Dialog().Title("Delete Group").AutoOpen(False).Modal(True).ConfirmAjax(".DeleteGroupLink",
                                                                                                            "Yes",
                                                                                                            "No",
                                                                                                            New AjaxSettings With {.Method = HttpVerbs.Post,
                                                                                                                                   .Success = "deleteGroupSuccess",
                                                                                                                                   .Error = "deleteGroupError"})))
    @<p>
        @Html.Raw("Would you like to delete this Group?")
    </p>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
