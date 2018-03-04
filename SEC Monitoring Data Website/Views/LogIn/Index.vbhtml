@ModelType SEC_Monitoring_Data_Website.LogInViewModel

@Code
    ViewData("Title") = "Log In"
    Dim activePanelIndex As Integer = 0
    Dim showLogInPanel As Boolean = False
    Dim showForgotPasswordPanel As Boolean = False
    Dim showNewPasswordRequestedPanel As Boolean = False
    Dim showResetPasswordPanel As Boolean = False
    Dim showLinkExpiredPanel As Boolean = False
    Dim showAccountLockedPanel As Boolean = False
    Select Case Model.CurrentAction
        Case "Log In"
            showLogInPanel = True
            showForgotPasswordPanel = True
            activePanelIndex = 0
        Case "Forgot Password"
            showLogInPanel = True
            showForgotPasswordPanel = True
            activePanelIndex = 1
        Case "New Password Requested"
            showNewPasswordRequestedPanel = True
            activePanelIndex = 0
        Case "Reset Password"
            showResetPasswordPanel = True
            activePanelIndex = 0
        Case "Link Expired"
            showForgotPasswordPanel = True
            showLinkExpiredPanel = True
            activePanelIndex = 1
        Case "Account Locked"
            showAccountLockedPanel = True
            activePanelIndex = 0
    End Select
End Code

<h2>Log In</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.HiddenFor(Function(model) model.ReturnUrl)

    @Using accordion = Html.JQueryUI().Begin(New Accordion().Active(activePanelIndex))

            If showLogInPanel Then
                Using accordion.BeginPanel("Log In")


        @<table class="login-table">
            <tr>
                <th>
                    User Name
                </th>
                <td>
                    @Html.TextBoxFor(Function(model) model.UserName)
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.UserName)
                </td>
            </tr>
            <tr>
                <th>
                    Password
                </th>
                <td>
                    @Html.PasswordFor(Function(model) model.Password)
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.Password)
                </td>
            </tr>
        </table>

        @Html.JQueryUI().Button("Log In", New With {.name = "button"})

    End Using
End If

If showForgotPasswordPanel Then

    Using accordion.BeginPanel("Forgot Password?")
        @<table class="details-table">
            <tr>
                <th>
                    Enter your email address here
                </th>
                <td>
                    @Html.EditorFor(Function(model) model.ResetPasswordEmailAddress)
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.ResetPasswordEmailAddress)
                </td>
            </tr>
        </table>
        @Html.JQueryUI().Button("Request Password Reset", New With {.name = "button"})
    End Using

End If

If showNewPasswordRequestedPanel Then

    Using accordion.BeginPanel("New Password Requested")
        @<p>
            A password reset link has been sent to your email address. Please check your email and click the link.
        </p>
        @<p>
            Please note that it may take a few minutes for the email to reach you. Please reset your password today, as the link will expire at midnight tonight.
        </p>

    End Using

End If

        @If showResetPasswordPanel Then

            @Using accordion.BeginPanel("Reset Password")
                    
                @Html.HiddenFor(Function(model) model.PasswordResetUserId)

                @<p>Hello @Model.UserName, please choose a new password and enter it below.</p>
                @<table class="details-table">
                    <tr>
                        <th>
                            Enter New Password
                        </th>
                        <td>
                            @Html.PasswordFor(Function(model) model.EnterNewPassword)
                        </td>
                        <td>
                            @Html.ValidationMessageFor(Function(model) model.EnterNewPassword)
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Re-Enter New Password
                        </th>
                        <td>
                            @Html.PasswordFor(Function(model) model.ReEnterNewPassword)
                        </td>
                        <td>
                            @Html.ValidationMessageFor(Function(model) model.ReEnterNewPassword)
                        </td>
                    </tr>
                    
                </table>
                
                @Html.JQueryUI().Button("Reset Password", New With {.name = "button"})


End Using

End If
        
        @If showLinkExpiredPanel = True Then
                
                @Using accordion.BeginPanel("Reset Link Expired")
                        @<p>
                            Sorry, the link you have been given is not correct or has expired.
                        </p>
                        @<p>
                            Please click on the <em>Forgot Password</em> tab above and enter your email address to request a new Password Reset Link.
                        </p>
                End Using

                End If
        
        @If showAccountLockedPanel Then
                
                @Using accordion.BeginPanel("Account is Locked")
                        
                        @<p>
                            Sorry, your account has been locked due to too many unsuccessful Log In attempts. Please contact Southdowns to unlock your account.
                        </p>
                        
                    End Using
                
                End If

        End Using
    

End Using



