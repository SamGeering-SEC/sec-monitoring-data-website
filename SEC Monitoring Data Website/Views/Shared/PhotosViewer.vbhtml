@ModelType IEnumerable(Of SEC_Monitoring_Data_Website.Document)

@code
    Dim photos = Model
End Code

<script type="text/javascript">

    function hideAllPhotos() {
        @For i = 0 To photos.Count - 1
            @<text>$('#photo</text>@i.ToString@<text>').hide();</text>
        Next

    };
    function showPhoto(photoNum) {
        hideAllPhotos();
        $('#photo' + photoNum).show()
    };
    $(document).ready(function () {
        showPhoto(0);
    });

</script>


<table style="photo-table">
    @For i = 0 To photos.Count - 1
        @<tr>
            <td align="left" valign="top">
                <img class="photo-preview" src='~/Content/Documents/@photos(i).FilePath?w=100&h=100&mode=crop' onclick='showPhoto(@i.ToString)' />
            </td>
            @If i = 0 Then
                @<td rowspan='@photos.Count' id='bigPhoto' align="left" valign="top" height="110">
                    @For j = 0 To photos.Count - 1
                        @<a href='~/Content/Documents/@photos(j).FilePath' target='_blank'>
                            <img id='@Html.Raw("photo"+j.tostring)' src='~/Content/Documents/@photos(j).FilePath?h=@Html.Raw((photos.Count + 1) * 100.ToString)&w=800&mode=max&anchor=topleft' />
                        </a>
                    Next
                </td>
            End If
        </tr>
    Next
</table>
