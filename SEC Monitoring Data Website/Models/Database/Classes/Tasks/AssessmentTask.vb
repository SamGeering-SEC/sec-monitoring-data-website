Public Class AssessmentTask

    Public Property Id As Integer
    Public Property StartDate As Date
    Public Property EndDate As Date

    Public Property MonitorLocationId As Integer
    Public Property AssessmentCriterionId As Integer
    Public Property AssessmentTaskTypeId As Integer
    Public Property TaskStatusId As Integer

    Public Overridable Property MonitorLocation As MonitorLocation
    Public Overridable Property AssessmentCriterion As AssessmentCriterion
    Public Overridable Property AssessmentTaskType As AssessmentTaskType
    Public Overridable Property TaskStatus As TaskStatus

End Class
