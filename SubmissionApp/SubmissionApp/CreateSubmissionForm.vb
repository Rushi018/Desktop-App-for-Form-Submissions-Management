Imports Newtonsoft.Json
Imports System.Net.Http
Imports System.Text
Imports System.IO
Imports System.Windows.Forms

Public Class CreateSubmissionForm
    Private stopwatch As New Stopwatch()
    Private WithEvents timer As New Timer()

    ' Define your backend server URL
    Private Const BaseUrl As String = "http://localhost:3000/"

    Private Sub CreateSubmissionForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Timer setup
        timer.Interval = 1000 ' 1 second interval
        timer.Start()

        ' Start the stopwatch
        stopwatch.Start()

        ' Set initial button text
        btnStartPause.Text = "Pause"
    End Sub

    Private Sub timer_Tick(sender As Object, e As EventArgs) Handles timer.Tick
        ' Update stopwatch display
        txtStopwatch.Text = stopwatch.Elapsed.ToString("hh\:mm\:ss")
    End Sub

    Private Sub btnStartPause_Click(sender As Object, e As EventArgs) Handles btnStartPause.Click
        If stopwatch.IsRunning Then
            ' Pause the stopwatch
            stopwatch.Stop()
            btnStartPause.Text = "Resume"
        Else
            ' Resume the stopwatch
            stopwatch.Start()
            btnStartPause.Text = "Pause"
        End If
    End Sub

    Private Sub CreateSubmissionForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        ' Handle keyboard shortcuts
        If e.Control AndAlso e.KeyCode = Keys.S Then ' Ctrl + S for Submit
            btnSubmit.PerformClick()
        End If
    End Sub

    Private Async Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        ' Validate inputs
        Dim name As String = txtName.Text.Trim()
        Dim email As String = txtEmail.Text.Trim()
        Dim phone As String = txtPhone.Text.Trim()
        Dim githubLink As String = txtGitHub.Text.Trim()
        Dim stopwatchTime As String = stopwatch.Elapsed.ToString("hh\:mm\:ss")

        If String.IsNullOrEmpty(name) OrElse String.IsNullOrEmpty(email) OrElse
           String.IsNullOrEmpty(phone) OrElse String.IsNullOrEmpty(githubLink) Then
            MessageBox.Show("Please fill in all fields", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Example URL for your backend server endpoint
        Dim submitUrl As String = BaseUrl & "submit"

        ' Prepare POST data
        Dim postData As New Dictionary(Of String, String) From {
            {"name", name},
            {"email", email},
            {"phone", phone},
            {"github_link", githubLink},
            {"stopwatch_time", stopwatchTime}
        }

        ' Send data to backend server
        Try
            Using client As New HttpClient()
                Dim json = JsonConvert.SerializeObject(postData)
                Dim content = New StringContent(json, Encoding.UTF8, "application/json")

                ' Send POST request asynchronously
                Dim response As HttpResponseMessage = Await client.PostAsync(submitUrl, content)

                ' Check response status
                If response.IsSuccessStatusCode Then
                    MessageBox.Show("Submission saved successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Clear form fields after successful submission
                    txtName.Clear()
                    txtEmail.Clear()
                    txtPhone.Clear()
                    txtGitHub.Clear()

                    ' Restart stopwatch
                    stopwatch.Reset()
                    txtStopwatch.Text = "00:00:00"
                Else
                    MessageBox.Show("Failed to save submission", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error connecting to server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CreateSubmissionForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Stop the stopwatch and timer when the form is closing
        stopwatch.Stop()
        timer.Stop()
    End Sub
End Class