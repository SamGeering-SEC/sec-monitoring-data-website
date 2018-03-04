@ModelType SEC_Monitoring_Data_Website.EditContactViewModel

<table class="edit-table">
    <tr>
        <th>
            Contact Name
        </th>
        <td>
            @Html.EditorFor(Function(model) model.Contact.ContactName)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.Contact.ContactName)
        </td>
    </tr>
    <tr>
        <th>
            Email Address
        </th>
        <td>
            @Html.EditorFor(Function(model) model.Contact.EmailAddress)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.Contact.EmailAddress)
        </td>
    </tr>
    <tr>
        <th>
            Primary Telephone Number
        </th>
        <td>
            @Html.EditorFor(Function(model) model.Contact.PrimaryTelephoneNumber)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.Contact.PrimaryTelephoneNumber)
        </td>
    </tr>
    <tr>
        <th>
            Secondary Telephone Number
        </th>
        <td>
            @Html.EditorFor(Function(model) model.Contact.SecondaryTelephoneNumber)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.Contact.SecondaryTelephoneNumber)
        </td>
    </tr>
    <tr>
        <th>
            Organisation
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.OrganisationId, Model.OrganisationList, String.Empty)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.OrganisationId)
        </td>
    </tr>
</table>