using Caliburn.Micro;
using SchoolManagement.UI.Helpers;
using SchoolManagement.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SchoolManagement.UI.ViewModels
{
    public class ConfirmationViewModel : Screen, IHandle<ConfirmationDialogModel>
    {
        private readonly IEventAggregator _eventAggregator;
        private string _dialogMessage;
        private Brush _dialogBackground;

        public ConfirmationViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);
        }

        public void OnValidated()
        {
            Validated().Await(HandlerError);
        }

        private void HandlerError(Exception obj)
        {
            System.Console.WriteLine("Erreur de validation");
        }

        private async Task Validated()
        {
            await TryCloseAsync(true);
        }

        public void OnDenied()
        {
            Denied().Await(HandlerError);
        }

        private async Task Denied()
        {
            await TryCloseAsync(false);
        }

        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            _eventAggregator.Unsubscribe(this);
            return base.OnDeactivateAsync(close, cancellationToken);
        }

        public Task HandleAsync(ConfirmationDialogModel dialogModel, CancellationToken cancellationToken)
        {
            if (dialogModel != null)
            {
                DialogMessage = dialogModel.DialogMessage;
                DialogBackground = dialogModel.DialogBackground;
            }
            else
            {
                DialogMessage = "Voulez vous supprimer cette élément ?";
            }

            return Task.CompletedTask;
        }

        public bool CanClosePopup
        {
            get
            {
                return true;
            }
        }

        public async Task ClosePopup()
        {
            await TryCloseAsync();
        }

        public string DialogMessage
        {
            get { return _dialogMessage; }
            set
            {
                _dialogMessage = value;
                NotifyOfPropertyChange(() => DialogMessage);
            }
        }

        public Brush DialogBackground
        {
            get { return _dialogBackground; }
            set
            {
                _dialogBackground = value;
                NotifyOfPropertyChange(() => DialogBackground);
            }
        }
    }
}
