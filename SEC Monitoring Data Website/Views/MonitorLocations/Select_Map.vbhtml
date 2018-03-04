@ModelType IEnumerable(Of SEC_Monitoring_Data_Website.MonitorLocation)

@Code
    ' Calculate geographical centre of projects
    Dim latMin As Double = Model.Select(Function(ml) ml.MonitorLocationGeoCoords.Latitude).ToList.Min
    Dim latMax As Double = Model.Select(Function(ml) ml.MonitorLocationGeoCoords.Latitude).ToList.Max
    Dim longMin As Double = Model.Select(Function(ml) ml.MonitorLocationGeoCoords.Longitude).ToList.Min
    Dim longMax As Double = Model.Select(Function(ml) ml.MonitorLocationGeoCoords.Longitude).ToList.Max
    

    Dim latAv = (latMin + latMax) / 2
    Dim longAv = (longMin + longMax) / 2
    
    Dim latMinView As Double = latAv - (latAv - latMin) * 1.1
    Dim latMaxView As Double = latAv + (latMax - latAv) * 1.1
    Dim longMinView As Double = longAv - (longAv - longMin) * 1.1
    Dim longMaxView As Double = longAv + (longMax - longAv) * 1.1
    
    
    Dim name As String = ViewData.TemplateInfo.HtmlFieldPrefix
    Dim id As String = name.Replace(".", "_")
    Dim canvasName As String = "canvas"

End Code

<script type="text/javascript">
    $(document).ready(function () {
        // set up shared objects
        var circleIcon = L.icon({
            iconUrl: '/Images/placemark_circle.png',
            shadowUrl: '/Images/placemark_circle_shadow.png',
            shadowSize: [0, 0]
        });
        // create map
        var mymap = L.map('mapid').setView([@latAv, @longAv], 19);
        var OpenStreetMap_Mapnik = L.tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        });
        OpenStreetMap_Mapnik.addTo(mymap);
        // add project markers
        @For Each monitorLocation In Model
            @Html.Raw(String.Format(
                      "var marker_{0} = L.marker([{1}, {2}]",
                      monitorLocation.Id, monitorLocation.MonitorLocationGeoCoords.Latitude, monitorLocation.MonitorLocationGeoCoords.Longitude))
            @Html.Raw(", {icon: circleIcon}).addTo(mymap);")
            If monitorLocation.AssessmentCriteria.Count > 0 AndAlso monitorLocation.hasMeasurements Then
                Dim monitorLocation_url = Url.RouteUrl(
                    "AssessmentViewRoute",
                    New With {.ProjectRouteName = monitorLocation.Project.getRouteName,
                    .MonitorLocationRouteName = monitorLocation.getRouteName,
                    .strAssessmentDate = Format(Date.Today, "yyyy-MM-dd")}
                )
                @Html.Raw(String.Format(
                      "marker_{0}.bindPopup('<b>{1}</b><br><a href={2}>Click to view Assessments</a>.').openPopup();",
                      monitorLocation.Id, monitorLocation.MonitorLocationName, monitorLocation_url)
                )
            Else
                @Html.Raw(String.Format(
                      "marker_{0}.bindPopup('<b>{1}</b>').openPopup();",
                      monitorLocation.Id, monitorLocation.MonitorLocationName
                ))
            End If
        Next
        mymap.closePopup();
        // set map bounds to markers
        var group = new L.featureGroup([
            @Html.Raw(String.Join(", ", Model.Select(Function(ml) "marker_" + ml.Id.ToString)))
        ]);

        mymap.fitBounds(group.getBounds());
    });
</script>

<div id="mapid"
     style="height:450px; width:800px; border:solid; border-width:2px; border-color:lightgray;">
</div>
