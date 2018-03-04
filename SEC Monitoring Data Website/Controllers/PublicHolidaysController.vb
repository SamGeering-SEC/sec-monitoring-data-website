Imports System.Data.Entity.Core
Imports libSEC

Public Class PublicHolidaysController
        Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub

#Region "Create"

    Public Function Create(CountryRouteName As String) As ActionResult

        If Not UAL.CanCreatePublicHolidays Then Return New HttpUnauthorizedResult()

        Dim country = MeasurementsDAL.GetCountry(CountryRouteName.FromRouteName)

        Dim vm As New CreatePublicHolidayViewModel With {
            .CountryId = country.Id,
            .CountryName = country.CountryName,
            .PublicHoliday = New PublicHoliday With {.CountryId = country.Id}
        }
        Return View(vm)

    End Function
    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Create(ByVal ViewModel As CreatePublicHolidayViewModel) As ActionResult

        If Not UAL.CanCreatePublicHolidays Then Return New HttpUnauthorizedResult()

        ModelState.Remove("PublicHoliday.Country")

        If ModelState.IsValid Then
            Dim ph = New PublicHoliday With {.HolidayDate = ViewModel.PublicHoliday.HolidayDate,
                                             .HolidayName = ViewModel.PublicHoliday.HolidayName,
                                             .Country = MeasurementsDAL.GetCountry(ViewModel.CountryId)}
            MeasurementsDAL.AddPublicHoliday(ph)
            Dim country = MeasurementsDAL.GetCountry(ViewModel.CountryId)
            Return RedirectToRoute("CountryEditRoute", New With {.CountryRouteName = country.getRouteName})
        End If

        Return View(ViewModel)

    End Function

#End Region



End Class