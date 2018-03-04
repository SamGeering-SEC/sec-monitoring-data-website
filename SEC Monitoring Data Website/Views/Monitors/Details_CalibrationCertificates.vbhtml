@ModelType SEC_Monitoring_Data_Website.Monitor

@code
    Dim fcrs = Model.getFieldCalibrationRecords
    Dim ccs = Model.getCalibrationCertificates
    Dim showCalibrationCertificateLinks = DirectCast(ViewData("ShowCalibrationCertificateLinks"), Boolean)
End Code

@If fcrs.Count > 0 Then

    @<h3>Field Calibration Records</h3>
    @<table class="details-table">
        <tr>
            <th>
                Title
            </th>
        </tr>
        @For Each fcr In fcrs
            @<tr>
                <td>
                    @If showCalibrationCertificateLinks Then
                        @Html.RouteLink(fcr.Title,
                                    "DocumentDetailsRoute",
                                    New With {.DocumentFileName = fcr.getFileName,
                                              .DocumentUploadDate = fcr.getUploadDate,
                                              .DocumentUploadTime = fcr.getUploadTime})
                    Else
                        @fcr.Title
                    End If
                </td>
            </tr>
        Next
    </table>

End If


@If ccs.Count > 0 Then

    @<h3>Full Calibration Certifcates</h3>
    @<table class="details-table">
        <tr>
            <th>
                Title
            </th>
        </tr>
        @For Each cc In ccs
            @<tr>
                <td>
                    @If showCalibrationCertificateLinks Then
                        @Html.RouteLink(cc.Title,
                                        "DocumentDetailsRoute",
                                        New With {.DocumentFileName = cc.getFileName,
                                                  .DocumentUploadDate = cc.getUploadDate,
                                                  .DocumentUploadTime = cc.getUploadTime})
                    Else
                        @cc.Title
                    End If
                </td>
            </tr>
        Next
    </table>

End If

