@ModelType SEC_Monitoring_Data_Website.SelectableMeasurementView

@Html.HiddenFor(Function(model) model.MeasurementView.Id)
@Html.CheckBoxFor(Function(model) model.IsSelected)
@Html.DisplayFor(Function(model) model.MeasurementView.ViewName)
