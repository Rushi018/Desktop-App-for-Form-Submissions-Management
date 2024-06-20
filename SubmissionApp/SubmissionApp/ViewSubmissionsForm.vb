Imports Newtonsoft.Json
Imports System.Collections.Generic
Imports System.Net.Http
Imports System.Windows.Forms

Public Class ViewSubmissionsForm
    Private submissions As New List(Of SubmissionData)
    Private currentIndex As Integer = 0

    ' Define your backend server URL
    Private Const BaseUrl As String = "http://localhost:3000/"

    Private Sub ViewSubmissionsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load submissions from backend
        LoadSubmissionsFromBackend()
    End Sub

    Private Sub LoadSubmissionsFromBackend()
        Try
            ' Example URL for your backend server endpoint
            Dim readUrl As String = BaseUrl & "read?index=" & currentIndex

            ' Create HttpClient instance
            Using client As New HttpClient()
                ' Send GET request asynchronously
                Dim response As HttpResponseMessage = client.GetAsync(readUrl).Result

                ' Check response status
                If response.IsSuccessStatusCode Then
                    ' Deserialize JSON response to SubmissionData object
                    Dim responseBody As String = response.Content.ReadAsStringAsync().Result
                    Dim submission As SubmissionData = JsonConvert.DeserializeObject(Of SubmissionData)(responseBody)

                    ' Display submission data
                    DisplaySubmission(submission)
                Else
                    MessageBox.Show($"Failed to load submission: {response.StatusCode}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error connecting to server: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DisplaySubmission(submission As SubmissionData)
        If submission IsNot Nothing Then
            txtName.Text = submission.Name
            txtEmail.Text = submission.Email
            txtPhone.Text = submission.Phone
            txtGitHub.Text = submission.GitHubLink
            txtStopwatchTime.Text = submission.StopwatchTime
        Else
            MessageBox.Show("No submission found", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If currentIndex > 0 Then
            currentIndex -= 1
            LoadSubmissionsFromBackend()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If currentIndex < submissions.Count - 1 Then
            currentIndex += 1
            LoadSubmissionsFromBackend()
        End If
    End Sub


    ' Class representing each submission data
    Public Class SubmissionData
        Public Property Name As String
        Public Property Email As String
        Public Property Phone As String
        Public Property GitHubLink As String
        Public Property StopwatchTime As String
    End Class
End Class
