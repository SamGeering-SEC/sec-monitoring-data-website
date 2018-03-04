@modeltype IAlphabeticalListViewModel

<script type="text/javascript">

    function filterRows() {

        var filterVal = document.querySelector('input[name="FilterViewButton"]:checked').value;
        var tbldiv = document.getElementById('@Model.TableId');
        var tbl = tbldiv.getElementsByTagName('table')[0];
        var rows = tbl.getElementsByTagName('tr');

        for (r = 1; r < rows.length; r++) {

            var nametd = rows[r].getElementsByTagName('td')[0];
            var labelFirstLetter = nametd.getElementsByTagName('a')[0].textContent.toUpperCase()[0];
            if (labelFirstLetter != filterVal && filterVal != 'All') {
                rows[r].style.display = 'none';
            }
            else {
                rows[r].style.display = 'inline';
            }
        }
    }

    $(document).ready(function () {

        filterRows();

    });
    

</script>

<table class="index-table">
    <tr>
        <th>
            Filter
        </th>
        <td>
            @Using (Html.JQueryUI().BeginButtonSet(New With {.class = "editor-field"}))

                @Code
                Dim isFirst = True
                End Code

                @For c = 65 To 90
                    @If Model.Names.Any(Function(n) UCase(Left(n, 1)) = Chr(c)) Then
                        @<label for='@Html.Raw("FilterView"+chr(c))'>@Html.Raw(Chr(c))</label>
                        @If isFirst Then
                            @<input type='radio' id='@Html.Raw("FilterView" + Chr(c))' name='FilterViewButton' class='FilterViewButton' value='@Html.Raw(chr(c))' onclick='filterRows();' checked='checked' />
                            @Code
                            isFirst = False
                            End Code
                        Else
                            @<input type='radio' id='@Html.Raw("FilterView" + Chr(c))' name='FilterViewButton' class='FilterViewButton' value='@Html.Raw(chr(c))' onclick='filterRows();' />
                        End If
                    End If
                Next

                @<label for='@Html.Raw("FilterViewAll")'>All</label>
                @<input type='radio' id='@Html.Raw("FilterViewAll")' name='FilterViewButton' class='FilterViewButton' value='All' onclick='filterRows();' />

            End Using
        </td>
    </tr>
</table>

