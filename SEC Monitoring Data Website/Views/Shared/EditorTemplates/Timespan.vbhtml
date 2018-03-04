@Modeltype TimeSpan
 
@Html.DropDownList("", Enumerable.Range(0, 24).Select(Function(i) New SelectListItem With {.Value = i.ToString(), .Text = i.ToString(), .Selected = If(Model.Hours = i, True, False)}))h&nbsp:
@Html.DropDownList("", Enumerable.Range(0, 60).Select(Function(i) New SelectListItem With {.Value = i.ToString(), .Text = i.ToString(), .Selected = If(Model.Minutes = i, True, False)}))m&nbsp:
@Html.DropDownList("", Enumerable.Range(0, 60).Select(Function(i) New SelectListItem With {.Value = i.ToString(), .Text = i.ToString(), .Selected = If(Model.Seconds = i, True, False)}))s