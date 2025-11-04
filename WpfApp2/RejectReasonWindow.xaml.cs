using System.Windows;

namespace YourNamespace
{
    public partial class RejectionReasonDialog : Window
    {
        public string RejectionReason { get; private set; }

        public RejectionReasonDialog()
        {
            InitializeComponent();
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            // Simple validation to ensure a reason is provided
            if (string.IsNullOrWhiteSpace(ReasonTextBox.Text))
            {
                MessageBox.Show("A rejection reason is required.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            this.RejectionReason = ReasonTextBox.Text;
            this.DialogResult = true; // Signals that the user confirmed
        }
    }
}