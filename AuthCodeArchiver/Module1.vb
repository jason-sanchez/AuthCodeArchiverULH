Imports System.IO
Imports System.Security.AccessControl
Imports System.Threading
Module Module1

    Sub Main()
        Dim errorfilepath As String = System.Configuration.ConfigurationManager.AppSettings("Errorpath")

        Dim todaysDate As String = System.DateTime.Today.Date
        Dim DateArray() = todaysDate.Split("/")
        Dim theYear = DateArray(2)
        Dim theMonth = DateArray(0)
        If theMonth.Length < 2 Then
            theMonth = 0 & theMonth
        End If
        Dim theday = DateArray(1)
        If theday.Length < 2 Then
            theday = 0 & theday
        End If
        Dim filedate = theYear & theMonth & theday


        Try
            'transfer authcode files from Authcode History to Archive daily
            'For Each AuthFile As String In System.Configuration.ConfigurationManager.AppSettings
            'Dim authfileDirectorypath As String = System.Configuration.ConfigurationManager.AppSettings("ULH")
            Dim source As String = System.Configuration.ConfigurationManager.AppSettings("Sourcepath")
            Dim dest As String = System.Configuration.ConfigurationManager.AppSettings("Destinationpath")
            For Each sourcefile As String In Directory.GetFiles(source)
                    If File.GetLastWriteTime(sourcefile) < Today Then
                        Dim destfile As String = Path.GetFileName(sourcefile).Trim()
                        Dim destpath As String = dest & destfile
                        FileCopy(sourcefile, destpath)
                        File.Delete(sourcefile)
                    End If
                Next
            'Next

            'transfer authcode log to authcode log archive
            'Not needed for ULH
            'Dim logsource As String = System.Configuration.ConfigurationManager.AppSettings("logsource")
            'Dim logtarget As String = System.Configuration.ConfigurationManager.AppSettings("logtarget")
            'Dim logfile As String = Path.GetFileName(logtarget).Trim()
            'FileCopy(logsource, logtarget)
            'My.Computer.FileSystem.RenameFile(logtarget, logfile & filedate)
            'File.Delete(logsource)


        Catch ex As Exception
            Dim errorfilename As String = String.Format("AuthcodeError-'" & "'_{0:yyyyMMdd_HH-mm-ss}.txt", Date.Now)
            Dim errorfile = New StreamWriter(errorfilepath & errorfilename, True)
            errorfile.Write(ex)
            errorfile.Close()
        End Try

    End Sub

End Module
