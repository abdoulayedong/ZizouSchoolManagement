using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class ErrorMessageViewModel : Screen, IHandle<string>
    {
        private string _errorMessage;
        private readonly IEventAggregator _eventAggregator;
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                NotifyOfPropertyChange(() => ErrorMessage);
            }
        }

        public ErrorMessageViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.SubscribeOnPublishedThread(this);
        }

        public void OnClosed()
        {
            TryCloseAsync();
        }

        public Task HandleAsync(string message, CancellationToken cancellationToken)
        {
            ErrorMessage = message;
            return Task.CompletedTask;
        }

        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            _eventAggregator.Unsubscribe(this);
            return base.OnDeactivateAsync(close, cancellationToken);
        }
    }
}

