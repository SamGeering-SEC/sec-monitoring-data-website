@ModelType SEC_Monitoring_Data_Website.ProjectGeoCoords

@Code
    Dim name As String = ViewData.TemplateInfo.HtmlFieldPrefix
    Dim id As String = name.Replace(".", "_")
    Dim lat As String = id + "_Latitude"
    Dim lon As String = id + "_Longitude"
End Code

@Html.HiddenFor(Function(model) model.Id)
@Html.HiddenFor(Function(model) model.Latitude)
@Html.HiddenFor(Function(model) model.Longitude)

<script type="text/javascript">
    $( document ).ready(function() {
        // set up shared objects
        var targetIcon = L.icon({
            iconUrl: '/Images/target.png',
            shadowSize: [0, 0]
        });
        // create map
        var mymap = L.map('mapid').setView([@Model.Latitude, @Model.Longitude], 16);
        var OpenStreetMap_Mapnik = L.tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        });
        OpenStreetMap_Mapnik.addTo(mymap);
        // add project marker
        @Html.Raw(String.Format(
                    "var marker = L.marker([{0}, {1}]",
                    Model.Latitude, Model.Longitude))
        @Html.Raw(", {icon: targetIcon, draggable: true}).addTo(mymap);")
        marker.on('drag', function(e) {
            var pos = marker.getLatLng();
            $("#@lat").val(pos.lat);
            $("#@lon").val(pos.lng);
        });
        marker.on('click', L.DomEvent.stopPropagation);
        marker.addTo(mymap);
    });
</script>

<div id="mapid" style="height:300px; width: 300px; border:solid; border-width:2px; border-color:lightgray;"></div>
