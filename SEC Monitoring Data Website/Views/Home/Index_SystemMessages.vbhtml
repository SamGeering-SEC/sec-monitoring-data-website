@ModelType IEnumerable(Of SystemMessage)

@Code
    Dim showMessages = DirectCast(ViewData("ShowMessages"), Boolean)
    Dim showCreateMessageButton = DirectCast(ViewData("ShowCreateMessageButton"), Boolean)
    Dim showDeleteMessageButtons = DirectCast(ViewData("ShowDeleteMessageButtons"), Boolean)
    Dim showEditMessageButtons = DirectCast(ViewData("ShowEditMessageButtons"), Boolean)
End Code


@If (showMessages And Model.Count > 0) Or showCreateMessageButton Then
    @<div id="notifications-div">
        <h3>Notifications</h3>
        @If showMessages And Model.Count > 0 Then
            @<table id="messages-table">
                @For Each message In Model
                        Dim messageId = message.Id.ToString
                    @<tr id="message-row-@messageId" , class="message-row">
                        <td class="system-message">
                            <ul>
                                <li id="message-@messageId">
                                    @Html.Raw(message.MessageText)
                                </li>
                            </ul>
                            @If showEditMessageButtons Then
                                @<textarea id="message-input-@messageId" class="message-input">@message.MessageText</textarea>
                            End If

                        </td>
                        @If showEditMessageButtons Then
                            @<td>
                                @Html.JQueryUI.Button("Save", New With {.id = "save-message-button-" + messageId,
                                                                        .class = "save-existing-message-button"})
                            </td>
                            @<td>
                                @Html.JQueryUI.Button("Edit", New With {.id = "edit-message-button-" + messageId,
                                                                        .class = "edit-message-button"})
                            </td>
                        End If
                        @If showDeleteMessageButtons Then
                            @<td>
                                <small>
                                    @Html.JQueryUI.ActionButton(
                                        "Delete", "DeleteSystemMessage", "Home", New With {.SystemMessageId = messageId},
                                        New With {.id = "delete-message-button-" + messageId.ToString,
                                                    .class = "delete-message-button"}
                                    )
                                </small>
                            </td>
                        End If
                    </tr>
                Next
            </table>
        End If
        @If showCreateMessageButton Then
            @<table>
                <tr>
                    <td>
                        @Html.JQueryUI.Button("New Message", New With {.id = "new-message-button"})
                        <textarea id="new-message-input"></textarea>
                        <br />
                        @Html.JQueryUI.Button("Save", New With {.id = "save-new-message-button"})
                        @Html.JQueryUI.Button("Cancel", New With {.id = "cancel-new-message-button"})
                    </td>
                </tr>
            </table>
        End If
    </div>
End If