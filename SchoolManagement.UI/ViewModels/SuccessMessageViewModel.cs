using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class SuccessMessageViewModel : Screen, IHandle<string>
    {
        private string _successMessage;
        private readonly IEventAggregator _eventAggregator;
        public string SuccessMessage
        {
            get { return _successMessage; }
            set
            {
                _successMessage = value;
                NotifyOfPropertyChange(() => SuccessMessage);
            }
        }

        public SuccessMessageViewModel(IEventAggregator eventAggregator)
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
            SuccessMessage = message;
            return Task.CompletedTask;
        }

        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            _eventAggregator.Unsubscribe(this);
            return base.OnDeactivateAsync(close, cancellationToken);
        }
    }
}

