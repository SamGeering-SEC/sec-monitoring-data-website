Imports System.Security.Cryptography
Imports System.Text
Imports System.Net.Mail
Imports System.Net.Mime


Public Module Security

    Public Function CreateRandomSalt() As String

        ' The following is the string that will hold the salt characters
        Dim mix As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+=][}{<>"
        Dim salt As String = ""
        Dim rnd As New Random
        Dim sb As New StringBuilder
        For i As Integer = 1 To 100 ' Length of the salt
            Dim x As Integer = rnd.Next(0, mix.Length - 1)
            salt &= (mix.Substring(x, 1))
        Next

        Return salt

    End Function

    Public Function Hash512(password As String, salt As String) As String

        Dim convertedToBytes As Byte() = Encoding.UTF8.GetBytes(password & salt)
        Dim hashType As HashAlgorithm = New SHA512Managed()
        Dim hashBytes As Byte() = hashType.ComputeHash(convertedToBytes)
        Dim hashedResult As String = Convert.ToBase64String(hashBytes)

        Return hashedResult

    End Function

    Public Function GetPasswordResetKey(user As User) As String

        Dim passwordResetKey As String = Hash512(
            user.UserName + user.Contact.ContactName + user.Contact.EmailAddress + DateTime.Today.ToString,
            user.Salt
        )

        Dim badCharacters = {"!", "@", "#", "$", "%", "^", "&", "*", "(", ")",
                               "_", "+", "=", "]", "[", "}", "{", "<", ">"}.ToList()

        For Each character In badCharacters
            passwordResetKey = passwordResetKey.Replace(character, badCharacters.IndexOf(character).ToString)
        Next

        Return passwordResetKey

    End Function

    Public Sub InitiatePasswordReset(user As User)

        Dim passwordResetKey = GetPasswordResetKey(user)
        Dim passwordResetUrl = BaseUrl + "/Login?PasswordResetKey=" + passwordResetKey

        Dim htmlBody = (
            "<p>We have received your password change request. This E-mail contains the information that you need to change your password.</p>" +
            "<p>Login Name: " + user.UserName + "</p>" +
            "<p>Click this link to reset your password: " + "<a href='" + passwordResetUrl + "'>Reset Password</a></p>" +
            "<p>Please reset your password today - the link will expire at midnight tonight.</p>" +
            "<p>Regards,</p>" +
            "<p>Southdowns EU</p>" +
            "<p>===================================================================================================</p>" +
            "<p>Please do not reply to this email - we are unable to review and respond to messages at this address</p>"
        )
        Dim textBody = (
            "We have received your password change request. This E-mail contains the information that you need to change your password.\n\n" +
            "Login Name: " + user.UserName + "\n\n" +
            "Copy and paste the following link into your browser to reset your password: " + passwordResetUrl + "\n\n" +
            "Please reset your password today - the link will expire at midnight tonight.\n\n" +
            "Regards,\n\n" +
            "Southdowns EU\n\n" +
            "===================================================================================================\n" +
            "Please do not reply to this email - we are unable to review and respond to messages at this address"
        )
        SendEmail(
            userEmailAddress:=user.Contact.EmailAddress,
            userContactName:=user.Contact.ContactName,
            subject:="Password Reset Request for Southdowns Monitoring Site",
            textBody:=textBody,
            htmlBody:=htmlBody
        )

    End Sub
    Sub NotifyAboutLockedUser(NotificationUser As User, LockedUser As User)

        Dim htmlBody = (
            "<p>The following User has entered 3 incorrect passwords and has been locked out of the system.</p>" +
            "<p>Login Name: " + LockedUser.UserName + "</p>" +
            "<p>Contact Name: " + LockedUser.Contact.ContactName + "</p>" +
            "<p>Email Address: <a href='mailto:" + LockedUser.Contact.EmailAddress + "'>" + LockedUser.Contact.EmailAddress + "</a></p>" +
            "<p>Organisation: " + LockedUser.Contact.Organisation.FullName + "</p>" +
            "<p>===================================================================================================</p>"
        )
        Dim textBody = (
            "The following User has entered 3 incorrect passwords and has been locked out of the system.\n\n" +
            "Login Name: " + LockedUser.UserName + "\n\n" +
            "Contact Name: " + LockedUser.Contact.ContactName + "\n\n" +
            "Email Address: " + LockedUser.Contact.EmailAddress + "\n\n" +
            "Organisation: " + LockedUser.Contact.Organisation.FullName + "\n\n" +
            "===================================================================================================\n"
        )
        SendEmail(
            userEmailAddress:=NotificationUser.Contact.EmailAddress,
            userContactName:=NotificationUser.Contact.ContactName,
            subject:="Locked User Notification",
            textBody:=textBody,
            htmlBody:=htmlBody
        )

    End Sub

    Sub SendEmail(userEmailAddress As String, userContactName As String, subject As String, textBody As String, htmlBody As String)

        Try
            Dim mailMsg As New MailMessage()

            mailMsg.To.Add(New MailAddress(userEmailAddress, userContactName))

            ' From
            mailMsg.From = New MailAddress("donotreply@southdowns.eu.com", "DoNotReply@SouthDowns.eu.com")

            ' Subject and multipart/alternative Body
            mailMsg.Subject = subject
            Dim text As String = textBody
            Dim html As String = htmlBody
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, Nothing, MediaTypeNames.Text.Plain))
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, Nothing, MediaTypeNames.Text.Html))

            ' Init SmtpClient and send
            Dim smtpClient As New SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587))
            Dim credentials As New System.Net.NetworkCredential("azure_357110ca349da51e20478aee04067d10@azure.com", SecretPassword)
            smtpClient.Credentials = credentials

            smtpClient.Send(mailMsg)

        Catch ex As Exception

            Console.WriteLine(ex.Message)

        End Try

    End Sub

End Module

