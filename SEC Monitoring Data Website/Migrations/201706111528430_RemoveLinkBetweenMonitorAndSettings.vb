Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class RemoveLinkBetweenMonitorAndSettings
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropForeignKey("dbo.MonitorSettings", "Id", "dbo.Monitors")
            DropForeignKey("dbo.MonitorDeploymentRecords", "Id", "dbo.MonitorSettings")
            DropForeignKey("dbo.NoiseSettings", "Id", "dbo.MonitorSettings")
            DropForeignKey("dbo.VibrationSettings", "Id", "dbo.MonitorSettings")
            DropForeignKey("dbo.AirQualitySettings", "Id", "dbo.MonitorSettings")
            DropIndex("dbo.MonitorSettings", New String() { "Id" })
            DropPrimaryKey("dbo.MonitorSettings")
            AlterColumn("dbo.MonitorSettings", "Id", Function(c) c.Int(nullable := False, identity := True))
            AddPrimaryKey("dbo.MonitorSettings", "Id")
            AddForeignKey("dbo.MonitorDeploymentRecords", "Id", "dbo.MonitorSettings", "Id")
            AddForeignKey("dbo.NoiseSettings", "Id", "dbo.MonitorSettings", "Id")
            AddForeignKey("dbo.VibrationSettings", "Id", "dbo.MonitorSettings", "Id")
            AddForeignKey("dbo.AirQualitySettings", "Id", "dbo.MonitorSettings", "Id")
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.AirQualitySettings", "Id", "dbo.MonitorSettings")
            DropForeignKey("dbo.VibrationSettings", "Id", "dbo.MonitorSettings")
            DropForeignKey("dbo.NoiseSettings", "Id", "dbo.MonitorSettings")
            DropForeignKey("dbo.MonitorDeploymentRecords", "Id", "dbo.MonitorSettings")
            DropPrimaryKey("dbo.MonitorSettings")
            AlterColumn("dbo.MonitorSettings", "Id", Function(c) c.Int(nullable := False))
            AddPrimaryKey("dbo.MonitorSettings", "Id")
            CreateIndex("dbo.MonitorSettings", "Id")
            AddForeignKey("dbo.AirQualitySettings", "Id", "dbo.MonitorSettings", "Id")
            AddForeignKey("dbo.VibrationSettings", "Id", "dbo.MonitorSettings", "Id")
            AddForeignKey("dbo.NoiseSettings", "Id", "dbo.MonitorSettings", "Id")
            AddForeignKey("dbo.MonitorDeploymentRecords", "Id", "dbo.MonitorSettings", "Id")
            AddForeignKey("dbo.MonitorSettings", "Id", "dbo.Monitors", "Id")
        End Sub
    End Class
End Namespace
