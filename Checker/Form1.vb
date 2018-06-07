Public Class Form1
    Dim state As String
    Dim states As Integer

    ' 0 Device Not connected
    ' 1 Device Connected
    ' 2 Device Connected (fastboot)
    ' 3 Device Connected (recovery)
    ' 4 Device Connected (sideload)

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim check As New Process
        check.StartInfo.FileName = "adb.exe"
        check.StartInfo.Arguments = "get-state"
        check.StartInfo.UseShellExecute = False
        check.StartInfo.CreateNoWindow = True
        check.StartInfo.RedirectStandardOutput = True
        check.Start()
        state = check.StandardOutput.ReadToEnd
        check.WaitForExit()

        states = "0"

        If String.Compare(state, "device", StringComparison.InvariantCultureIgnoreCase) = 1 Then
            states = "1"
        End If

        If String.Compare(state, "recovery", StringComparison.InvariantCultureIgnoreCase) = 1 Then
            states = "3"
        End If

        If String.Compare(state, "sideload", StringComparison.InvariantCultureIgnoreCase) = 1 Then
            states = "4"
        End If

        If states = "0" Then
            Dim fcheck As New Process
            fcheck.StartInfo.FileName = "fastboot.exe"
            fcheck.StartInfo.Arguments = "devices"
            fcheck.StartInfo.UseShellExecute = False
            fcheck.StartInfo.CreateNoWindow = True
            fcheck.StartInfo.RedirectStandardOutput = True
            fcheck.Start()
            state = fcheck.StandardOutput.ReadToEnd
            fcheck.WaitForExit()

            If String.Compare(state, "fastboot", StringComparison.InvariantCultureIgnoreCase) = 1 Then
                states = "2"
            End If

        End If


        If states = "0" Then
            Label2.Text = "Device Not Connected"
        End If

        If states = "1" Then
            Label2.Text = "Device Connected"
        End If

        If states = "2" Then
            Label2.Text = "Device COnnected (Fastboot)"
        End If

        If states = "3" Then
            Label2.Text = "Device Connected (Recovery)"
        End If

        If states = "4" Then
            Label2.Text = "Device Connected (Sideload)"
        End If

    End Sub
End Class
