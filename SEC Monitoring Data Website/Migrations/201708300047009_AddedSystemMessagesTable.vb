Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedSystemMessagesTable
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.SystemMessages",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .MessageText = c.String(),
                        .DateTimeCreated = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropTable("dbo.SystemMessages")
        End Sub
    End Class
End Namespace
