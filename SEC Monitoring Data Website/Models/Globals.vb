Module Globals

    Public DefaultLatitude As Double = 51.5286416
    Public DefaultLongitude As Double = -0.1015987

    ' LOCAL TEST CONFIG
    'Public MeasurementsConnectionString As String = (
    '    "Data Source=VAHNDI-ASUS\sqlexpress;" +
    '    "Initial Catalog=SECMonitoringDatabase;" +
    '    "Integrated Security=True;" +
    '    "MultipleActiveResultSets=True;" +
    '    "Application Name=EntityFramework"
    ')
    'Public BaseUrl = "http://localhost:50377"

    ' AZURE DEPLOYMENT CONFIG
    Public MeasurementsConnectionString As String = (
        "Server=tcp:xql28hijwa.database.windows.net,1433;" +
        "Database=SECMonitoringDatabase;" +
        "User ID=vahndi@xql28hijwa;" +
        "Password=" + SecretPassword + ";" +
        "Trusted_Connection=False;" +
        "MultipleActiveResultSets=True;" +
        "Encrypt=True;" +
        "Connection Timeout=60;"
    )
    Public BaseUrl = "http://secsite.azurewebsites.net"

End Module
