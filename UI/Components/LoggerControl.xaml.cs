using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataLayer.Database;
using DataLayer.Model;

namespace UI.Components
{
    /// <summary>
    /// Interaction logic for LoggerControl.xaml
    /// </summary>
    public partial class LoggerControl : UserControl
    {
        public LoggerControl()
        {
            InitializeComponent();
            LoadLogs();
        }

        private void LoadLogs()
        {
            using (var context = new DatabaseContext())
            {
                var logs = context.LogEntries.ToList();
                logDataGrid.DataContext = logs;
            }
        }
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is DataGrid dataGrid && dataGrid.SelectedItem is LogEntry selectedLog)
            {
                string formattedMessage = $"Id: {selectedLog.Id}\nMessage: {selectedLog.Message}";
                MessageBox.Show(formattedMessage, "Log Details");
            }
        }
    }
}