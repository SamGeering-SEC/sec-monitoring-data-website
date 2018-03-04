@ModelType SEC_Monitoring_Data_Website.MeasurementViewDetailsViewModel

@Code
    ViewData("Title") = "Measurement View Details"
End Code

<h2>Measurement View Details</h2>

<fieldset>
    <legend>MeasurementView</legend>

    @Using t = Html.JQueryUI().BeginTabs()

        t.Tab("Basic Details", "basic_details")

        @If Model.MeasurementView.ExcludingMeasurementCommentTypes.Count > 0 Then
            t.Tab("Comment Types", "comment_types")
        End If

        @If Model.MeasurementView.Projects.Count > 0 Then
            t.Tab("Projects", "projects")
        End If


        ' Basic Details Tab
        @Using t.BeginPanel
            @Html.Partial("Details_BasicDetails", Model.MeasurementView)
        End Using

        ' Comment Types Tab
        @If Model.MeasurementView.ExcludingMeasurementCommentTypes.Count > 0 Then
            @Using t.BeginPanel
                @Html.Partial("Details_CommentTypes", Model.MeasurementView)
            End Using
        End If

        ' Projects Tab
        @If Model.MeasurementView.Projects.Count > 0 Then
            @Using t.BeginPanel
                @Html.Partial("Details_Projects", Model.MeasurementView)
            End Using
        End If

    End Using


</fieldset>

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "MeasurementView")