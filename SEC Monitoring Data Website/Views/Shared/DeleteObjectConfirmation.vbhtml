@ModelType String

<script type="text/javascript">
    function deleteObjectSuccess(data, textStatus, xhr) {
        window.location.href = data.redirectToUrl;
    }
</script>

@Using (Html.JQueryUI().Begin(New Dialog().Title("Delete " + Model.SpaceOutPascalCaseText).AutoOpen(False).Modal(True).ConfirmAjax(".Delete" + Model + "Link",
                                                                                                                      "Yes",
                                                                                                                      "No",
                                                                                                                      New AjaxSettings With {.Method = HttpVerbs.Post,
                                                                                                                                             .Success = "deleteObjectSuccess"})))
    @<p>
        @Html.Raw("Would you like to delete this " + Model.SpaceOutPascalCaseText+"?")
    </p>

End Using
