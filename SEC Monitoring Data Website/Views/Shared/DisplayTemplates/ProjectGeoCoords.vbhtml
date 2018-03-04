@ModelType SEC_Monitoring_Data_Website.ProjectGeoCoords

@*Synchronous Version*@
@*===================*@

@Code
    Dim name As String = ViewData.TemplateInfo.HtmlFieldPrefix
    Dim id As String = name.Replace(".", "_")
    Dim pId As String = Model.Projects.First.Id.ToString
    Dim lat As String = id + "_Latitude" + pId
    Dim lon As String = id + "_Longitude" + pId
    Dim canvasName As String = "canvas" + pId
    Dim initName As String = "init" + pId
    Dim initNameFull As String = "init" + pId + "()"
End Code

<script type="text/javascript">
    $( document ).ready(function() {
        // set up shared objects
        var targetIcon = L.icon({
            iconUrl: '/Images/target.png',
            shadowSize: [0, 0]
        });
        // create map
        var mymap = L.map('mapid').setView([@Model.Latitude, @Model.Longitude], 13);
        var OpenStreetMap_Mapnik = L.tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
            maxZoom: 19,
            attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
        });
        OpenStreetMap_Mapnik.addTo(mymap);
        // add project marker
        @Html.Raw(String.Format(
                    "var marker = L.marker([{0}, {1}]",
                    Model.Latitude, Model.Longitude))
        @Html.Raw(", {icon: targetIcon}).addTo(mymap);")
    });
</script>

<div id="mapid" style="height:300px; width: 300px; border:solid; border-width:2px; border-color:lightgray;"></div>
