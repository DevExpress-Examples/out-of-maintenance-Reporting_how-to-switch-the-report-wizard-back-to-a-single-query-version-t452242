Imports DevExpress.DataAccess.UI.Native.Sql
Imports DevExpress.DataAccess.Wizard
' ... 

Public Class Form1
    Inherits System.Windows.Forms.Form
    Public Sub New()
        InitializeComponent()

        ReportDesigner1.RemoveService(GetType(ISqlWizardOptionsProvider))
        ReportDesigner1.AddService(GetType(ISqlWizardOptionsProvider), New MySqlWizardOptionsProvider(Function() _
                ReportDesigner1.DataSourceWizardSettings.SqlWizardSettings.ToSqlWizardOptions()))
    End Sub
End Class

Public Class MySqlWizardOptionsProvider
    Implements ISqlWizardOptionsProvider
    ReadOnly getOptions As Func(Of SqlWizardOptions)

    Public Sub New(getOptions As Func(Of SqlWizardOptions))
        Me.getOptions = getOptions
    End Sub
    Private ReadOnly Property ISqlWizardOptionsProvider_SqlWizardOptions() As SqlWizardOptions Implements ISqlWizardOptionsProvider.SqlWizardOptions
        Get
            Return getOptions() And SqlWizardOptions.MultiQueryWizard
        End Get
    End Property
End Class