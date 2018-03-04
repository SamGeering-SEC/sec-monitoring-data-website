@ModelType HomePageViewModel

@Code
    ViewData("Title") = "Southdowns Monitoring Site HomePage"
    Dim showMessages = DirectCast(ViewData("ShowMessages"), Boolean)
    Dim showCreateMessageButton = DirectCast(ViewData("ShowCreateMessageButton"), Boolean)
    Dim showDeleteMessageButtons = DirectCast(ViewData("ShowDeleteMessageButtons"), Boolean)
    Dim showEditMessageButtons = DirectCast(ViewData("ShowEditMessageButtons"), Boolean)
End Code

@section featured
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1>@Model.Message</h1>
            </hgroup>
        </div>
    </section>
End Section

<div id="notifications">
    @Html.Partial("Index_SystemMessages", Model.SystemMessages)
</div>

<h3>
    Please click on an icon below to go to the related area of the site. Hover over the icon for help.
</h3>

<div style="height:200px">

    @For Each button In Model.Buttons
        @Html.Partial("Index_HomePageButton", button)
    Next

</div>

@Using (Html.JQueryUI().Begin(New Dialog().Title("Delete System Message").AutoOpen(False).Modal(True).ConfirmAjax(
                             ".delete-message-button", "Yes", "No",
                             New AjaxSettings With {
                                 .Method = HttpVerbs.Post,
                                 .Success = "deleteMessageSuccess",
                                 .Error = "deleteMessageError"
                             })))
    @<p>
        @Html.Raw("Would you like to delete this System Message?")
    </p>

End Using

<script type="text/javascript">

    // define variables and initial state
    var editing = false;
    var enteringNew = false;

    function saveMessage(messageId) {
        var messageText = $('#message-input-' + messageId).val();
        $.ajax({
            type: "POST",
            url: '@Url.Action("UpdateSystemMessage", "Home")',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                messageId: messageId,
                messageText: messageText
            }),
            dataType: "json",
            success: function (result) {
                alert('The message was successfully updated.');
            },
            error: function (result) {
                alert('Failed to update System Message :(');
                console.log(result);
            }
        });
    }

    function createMessage(messageText) {
        var messageText = $('#new-message-input').val();
        $.ajax({
            type: "POST",
            url: '@Url.Action("CreateSystemMessage", "Home")',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({
                messageText: messageText
            }),
            dataType: "json",
            success: function (result) {
                alert('The message was successfully added.');
            },
            error: function (result) {
                alert('Failed to add System Message :(');
                console.log(result);
            }
        });
    }

    // helper function to get the messageId associated with a clicked button event
    function getEventMessageId(event) {
        var elementId = event.currentTarget.id;
        var messageId = elementId.split('-').pop(-1);
        return messageId;
    }

    function addSaveHandlers() {
        $(".save-existing-message-button").click(function (e) {
            var messageId = getEventMessageId(e);
            var newText = $('#message-input-' + messageId).val();
            $('#message-' + messageId).show();
            $('#message-' + messageId).html(newText);
            $('#new-message-button').hide();
            $('#save-message-button-' + messageId).hide();
            $('#message-input-' + messageId).hide();
            saveMessage(messageId);
            editing = false;
        })
        $(".save-existing-message-button").hide();
    }

    function addEditHandlers() {
        $('.edit-message-button').click(function (e) {
            var messageId = getEventMessageId(e);
            $('#message-' + messageId).hide();
            $('#new-message-button').hide();
            $('#edit-message-button-' + messageId).hide();
            $('#delete-message-button-' + messageId).hide();
            $('#save-message-button-' + messageId).show();
            $('#message-input-' + messageId).show();
            editing = true;
        });
        $('.message-row').mouseover(function (e) {
            if (!editing && !enteringNew) {
                var messageId = getEventMessageId(e);
                $('#edit-message-button-' + messageId).show();
            }
        });
        $('.message-row').mouseout(function (e) {
            if (!editing && !enteringNew) {
                var messageId = getEventMessageId(e);
                $('#edit-message-button-' + messageId).hide();
            }
        });
        $('.edit-message-button').hide();
        $('.message-input').hide();
    }

    function addDeleteHandlers() {
        $('.delete-message-button').click(function (e) {

        });
        $('.message-row').mouseover(function (e) {
            if (!editing && !enteringNew) {
                var messageId = getEventMessageId(e);
                $('#delete-message-button-' + messageId).show();
            }
        });
        $('.message-row').mouseout(function (e) {
            if (!editing && !enteringNew) {
                var messageId = getEventMessageId(e);
                $('#delete-message-button-' + messageId).hide();
            }
        });
        $('.delete-message-button').hide();
    }

    function hideEnteringNewControls() {
        $('#new-message-button').show();
        $('#new-message-input').val('');
        $('#new-message-input').hide();
        $('#save-new-message-button').hide();
        $('#cancel-new-message-button').hide();
    }

    function addNewMessageHandlers() {

        // add table mouse events
        $('#notifications-div').mouseover(function () {
            if (!editing && !enteringNew) {
                $('#new-message-button').show();
            }
        });
        $('#notifications-div').mouseout(function () {
            if (!editing && !enteringNew) {
                $('#new-message-button').hide();
            }
        });
        // new message button click
        $('#new-message-button').click(function () {
            $('#new-message-button').hide();
            $('#new-message-input').show();
            $('#save-new-message-button').show();
            $('#cancel-new-message-button').show();
            enteringNew = true;
        })
        // cancel new message button click
        $('#cancel-new-message-button').click(function () {
            hideEnteringNewControls();
            enteringNew = false;
        })

        // hide controls
        hideEnteringNewControls();
    }


    function setUpEvents() {

        @If showEditMessageButtons Then
        @: addSaveHandlers();
                @: addEditHandlers();
                End If
        @If showDeleteMessageButtons Then
        @: addDeleteHandlers();
                End If
        @If showCreateMessageButton Then
        @: addNewMessageHandlers();
                End If

    }

    function renderMessages() {
        $.ajax({
            type: "GET",
            url: '@Url.RouteUrl("SystemMessageIndexRoute")',
            success: function (partialView) {
                $('#notifications-div').html(partialView);
                $('#notifications-div').jQueryUIHelpers();
                setUpEvents();
            },
            error: function (errorData) {
                alert("failed to update table :(");
                console.log(errorData);
            }
        });
    }

    function deleteMessageSuccess() {
        renderMessages();
    }
    function deleteMessageError() {
        alert('The Message was unable to be deleted :(');
    }

    // save new message button click
    $('#save-new-message-button').click(function () {
        var newMessageText = $('#new-message-input').val();
        createMessage(newMessageText);
        renderMessages();
        hideEnteringNewControls();
        enteringNew = false;
    })

    $(document).ready(function () {

        setUpEvents();

    });

</script>
