using System.Windows.Media;

namespace SchoolManagement.UI.Models
{
    public class ConfirmationDialogModel
    {
        public string DialogMessage { get; set; }
        public Brush DialogBackground { get; set; }


        public ConfirmationDialogModel() : this(string.Empty, null) { }

        public ConfirmationDialogModel(string dialogMessage) : this(dialogMessage, null) { }

        public ConfirmationDialogModel(string dialogMessage, Brush dialogBackground)
        {
            DialogMessage = dialogMessage;
            DialogBackground = dialogBackground;
        }

    }
}
