@ModelType SEC_Monitoring_Data_Website.EditOrganisationViewModel

@Html.HiddenFor(Function(model) model.Organisation.Id)

<table class="edit-table">
	<tr>
		<th>
			Full Name
		</th>
		<td>
			@Html.EditorFor(Function(model) model.Organisation.FullName)
		</td>
		<td>
			@Html.ValidationMessageFor(Function(model) model.Organisation.FullName)
		</td>
	</tr>
	<tr>
		<th>
			Short Name
		</th>
		<td>
			@Html.EditorFor(Function(model) model.Organisation.ShortName)
		</td>
		<td>
			@Html.ValidationMessageFor(Function(model) model.Organisation.ShortName)
		</td>
	</tr>
	<tr>
		<th>
			Address
		</th>
		<td>
			@Html.EditorFor(Function(model) model.Organisation.Address)
		</td>
		<td>
			@Html.ValidationMessageFor(Function(model) model.Organisation.Address)
		</td>
	</tr>
	<tr>
		<th>
			Organisation Type 
		</th>
		<td>
			@Html.DropDownListFor(Function(model) model.OrganisationTypeId, model.OrganisationTypeList, String.Empty)
		</td>
		<td>
			@Html.ValidationMessageFor(Function(model) model.OrganisationTypeId)
		</td>
	</tr>
</table>
