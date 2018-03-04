@ModelType SEC_Monitoring_Data_Website.MonitorLocationGeoCoords

@Code
    Dim name As String = ViewData.TemplateInfo.HtmlFieldPrefix
    Dim id As String = name.Replace(".", "_")
    Dim mlId As String = Model.MonitorLocations.First.Id.ToString
    Dim lat As String = id + "_Latitude" + mlId
    Dim lon As String = id + "_Longitude" + mlId
    Dim canvasName As String = "canvas" + mlId
    Dim initName As String = "init" + mlId
    Dim initNameFull As String = "init" + mlId + "()"
End Code

<script type="text/javascript">
    $( document ).ready(function() {
        // set up shared objects
        var circleIcon = L.icon({
            iconUrl: '/Images/placemark_circle.png',
            shadowSize: [0, 0]
        });
        // create map
        var mymap = L.map('mapid').setView([@Model.Latitude, @Model.Longitude], 16);
        var OpenStreetMap_Mapnik = L.tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        });
        OpenStreetMap_Mapnik.addTo(mymap);
        // add monitor location marker
        @Html.Raw(String.Format(
                  "var marker = L.marker([{0}, {1}]",
                  Model.Latitude, Model.Longitude))
        @Html.Raw(", {icon: circleIcon}).addTo(mymap);")
    });
</script>

<div id="mapid" style="height:300px; width: 300px; border:solid; border-width:2px; border-color:lightgray;"></div>

