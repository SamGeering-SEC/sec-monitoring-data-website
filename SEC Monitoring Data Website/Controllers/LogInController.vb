Imports System.Data.Entity.Core
Imports libSEC

Public Class LogInController
    Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub

#Region "Index"

    Public Function Index(Optional ReturnUrl As String = Nothing, Optional PasswordResetKey As String = Nothing) As ActionResult

        Dim vm As New LogInViewModel
        If Not ReturnUrl Is Nothing Then
            vm.ReturnUrl = ReturnUrl
        End If
        If PasswordResetKey Is Nothing Then
            vm.CurrentAction = "Log In"
        Else
            Dim users = MeasurementsDAL.GetUsers
            Dim resetUser As User = Nothing
            For Each potentialUser In users
                If GetPasswordResetKey(potentialUser) = PasswordResetKey Then
                    resetUser = potentialUser
                    vm.PasswordResetUserId = potentialUser.Id
                    vm.CurrentAction = "Reset Password"
                    vm.UserName = potentialUser.Contact.ContactName.Split(" ").First()
                End If
            Next
            If resetUser Is Nothing Then
                vm.CurrentAction = "Link Expired"
            End If
        End If

        Return View(vm)

    End Function
    <HttpPost()> _
    Public Function Index(model As LogInViewModel, button As String) As ActionResult

        If button = "Log In" Then

            model.CurrentAction = "Log In"
            Dim theUser = MeasurementsDAL.GetUser(model.UserName)
            ' Incorrect User Name
            If theUser Is Nothing Then
                ModelState.AddModelError(
                    "UserName", "The User Name you entered is not associated with an account on this Website."
                )
                Return View(model)
            End If
            ' Locked User
            If theUser.IsLocked Then
                model.CurrentAction = "Account Locked"
                Return View(model)
            End If
            If Not theUser.Password = Hash512(model.Password, theUser.Salt) Then
                ' Incorrect Password
                theUser.ConsecutiveUnsuccessfulLogins += 1
                MeasurementsDAL.UpdateUser(theUser)
                If theUser.ConsecutiveUnsuccessfulLogins >= 3 Then
                    model.CurrentAction = "Account Locked"
                    ' Lock User
                    theUser.IsLocked = True
                    MeasurementsDAL.UpdateUser(theUser)
                    ' Send Email Notifications
                    Dim notificationUsers = MeasurementsDAL.GetUsers.Where(Function(u) u.ReceivesLockNotifications = True).ToList()
                    For Each notificationUser In notificationUsers
                        NotifyAboutLockedUser(notificationUser, theUser)
                    Next
                    Return View(model)
                Else
                    ModelState.AddModelError(
                        "Password", "Incorrect Password!"
                    )
                    Return View(model)
                End If
            Else
                ' Correct Password
                theUser.ConsecutiveUnsuccessfulLogins = 0
                FormsAuthentication.SetAuthCookie(theUser.UserName, False)
                If Not String.IsNullOrEmpty(model.ReturnUrl) Then
                    Dim decodedUrl = Server.UrlDecode(model.ReturnUrl)
                    Return Redirect(decodedUrl)
                End If
                Return RedirectToAction("Index", "Home")
            End If

        ElseIf button = "Request Password Reset" Then

            model.CurrentAction = "Forgot Password"
            ' Check that the email is associated with a User
            Dim users = MeasurementsDAL.GetUsers()
            Dim associatedUser As User = Nothing
            For Each potentialUser In users
                If potentialUser.Contact.EmailAddress = model.ResetPasswordEmailAddress Then
                    associatedUser = potentialUser
                End If
            Next
            If associatedUser Is Nothing Then
                ModelState.AddModelError(
                    "ResetPasswordEmailAddress",
                    "The Email Address you entered is not associated with an account on this Website."
                )
                Return View(model)
            End If
            If associatedUser.IsLocked Then
                model.CurrentAction = "Account Locked"
                Return View(model)
            End If

            InitiatePasswordReset(associatedUser)
            associatedUser.IsLocked = True
            MeasurementsDAL.UpdateUser(associatedUser)
            model.CurrentAction = "New Password Requested"

            Return View(model)

        ElseIf button = "Reset Password" Then

            model.CurrentAction = "Reset Password"
            ' Check Passwords
            If model.EnterNewPassword.Count < 10 Then
                ModelState.AddModelError("EnterNewPassword", "Passwords must have at least 10 characters")
                Return View(model)
            End If
            If Not model.EnterNewPassword = model.ReEnterNewPassword Then
                ModelState.AddModelError("ReEnterNewPassword", "Passwords do not match")
                Return View(model)
            End If
            ' Reset Password and Unlock User
            Dim resetUser = MeasurementsDAL.GetUser(model.PasswordResetUserId)
            MeasurementsDAL.ChangeUserPassword(resetUser, Hash512(model.EnterNewPassword, resetUser.Salt))
            resetUser.IsLocked = False
            resetUser.ConsecutiveUnsuccessfulLogins = 0
            MeasurementsDAL.UpdateUser(resetUser)
            FormsAuthentication.SetAuthCookie(resetUser.UserName, False)
            If Not String.IsNullOrEmpty(model.ReturnUrl) Then
                Dim decodedUrl = Server.UrlDecode(model.ReturnUrl)
                Return Redirect(decodedUrl)
            End If
            Return RedirectToAction("Index", "Home")

        End If

        Return View()

    End Function


#End Region

#Region "LogOut"

    Public Function LogOut() As ActionResult

        FormsAuthentication.SignOut()
        Return RedirectToAction("Index", "Home")

    End Function

#End Region

#Region "ChangePassword"

    Public Function ChangePassword() As ActionResult

        Return View(getChangePasswordViewModel())

    End Function

    Public Function getChangePasswordViewModel() As ChangePasswordViewModel

        Return New ChangePasswordViewModel With {
            .UserName = User.Identity.Name,
            .OldPassword = "",
            .NewPassword = "",
            .ConfirmNewPassword = ""
        }

    End Function

    <HttpPost()> _
    Public Function ChangePassword(ViewModel As ChangePasswordViewModel) As ActionResult

        Dim currentUserPassword = CurrentUser.Password

        If currentUserPassword <> Hash512(ViewModel.OldPassword, CurrentUser.Salt) Then ModelState.AddModelError(
            "OldPassword", "Password does not match User's Current Password!"
        )
        If ViewModel.NewPassword <> ViewModel.ConfirmNewPassword Then ModelState.AddModelError(
            "ConfirmNewPassword", "New Passwords are Different!"
        )

        If ModelState.IsValid Then
            MeasurementsDAL.ChangeUserPassword(CurrentUser, Hash512(ViewModel.NewPassword, CurrentUser.Salt))
            Return RedirectToAction("Index", "Home")
        End If

        Return View(getChangePasswordViewModel())

    End Function

#End Region



End Class