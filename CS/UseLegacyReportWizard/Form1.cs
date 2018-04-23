using DevExpress.DataAccess.UI.Native.Sql;
using DevExpress.DataAccess.Wizard;
using System;

namespace UseLegacyReportWizard {
    public partial class Form1 : System.Windows.Forms.Form {
        public Form1() {
            InitializeComponent();

            // Replace the default ISqlWizardOptionsProvider service with a custom one.
            reportDesigner1.RemoveService(typeof(ISqlWizardOptionsProvider));
            reportDesigner1.AddService(typeof(ISqlWizardOptionsProvider), new MySqlWizardOptionsProvider(() =>
                reportDesigner1.DataSourceWizardSettings.SqlWizardSettings.ToSqlWizardOptions()));
        }

        // Implement a custom SqlWizardOptionsProvider and specify the SqlWizardOptions.
        public class MySqlWizardOptionsProvider : ISqlWizardOptionsProvider {
            readonly Func<SqlWizardOptions> getOptions;

            public MySqlWizardOptionsProvider(Func<SqlWizardOptions> getOptions) {
                this.getOptions = getOptions;
            }
            SqlWizardOptions ISqlWizardOptionsProvider.SqlWizardOptions {
                get { return getOptions() & SqlWizardOptions.MultiQueryWizard; }
            }
        }
    }
}
