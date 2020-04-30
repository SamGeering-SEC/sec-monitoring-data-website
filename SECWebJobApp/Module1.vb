Imports SEC_Monitoring_Data_Website

Module Module1

    Sub Main()

        While True

            Dim dal = New EFMeasurementsDAL

            Dim locations = dal.GetMonitorLocations

            Console.WriteLine("TESTING LIST OF MONITOR LOCATIONS")
            Console.WriteLine("---------------------------------")
            For Each location In locations
                Console.WriteLine(String.Format("{0}: {1}", location.MonitorLocationName, location.Project.FullName))
            Next
            Console.WriteLine()

            System.Threading.Thread.Sleep(60000)

        End While

    End Sub

End Module
