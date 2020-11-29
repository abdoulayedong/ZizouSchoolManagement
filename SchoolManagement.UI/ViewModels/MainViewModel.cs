using Caliburn.Micro;
using SchoolManagement.UI.EventModel;
using SchoolManagement.UI.Library.API;
using SchoolManagement.UI.Library.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class MainViewModel : Conductor<object>, IHandle<LogInEvent>
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly SimpleContainer _simpleContainer;
        private readonly IAPIHelper _apiHelper;
        private readonly IWindowManager _windowManager;
        private readonly ILoggedInUser _loggedInUser;
        private readonly IEventAggregator _events;

        private bool _isSideMenuVisible;
        private bool _isMenuBarVisible;

        public MainViewModel(HomeViewModel homeViewModel,
                             SimpleContainer simpleContainer,
                             IAPIHelper apiHelper,
                             IWindowManager windowManager,
                             ILoggedInUser loggedInUser,
                             IEventAggregator events)
        {
            _homeViewModel = homeViewModel;
            _simpleContainer = simpleContainer;
            _apiHelper = apiHelper;
            _windowManager = windowManager;
            _loggedInUser = loggedInUser;
            _events = events;

            _events.SubscribeOnPublishedThread(this);

            // Initiate the View 
            GetLoginViewModel();
        }

        private void GetLoginViewModel()
        {
            ActivateItemAsync(_simpleContainer.GetInstance<LoginViewModel>());
        }

        #region IHandle Implementation
        public Task HandleAsync(LogInEvent logInEvent, CancellationToken cancellationToken)
        {
            ActivateItemAsync(_homeViewModel);
            IsSideMenuVisible = true;
            IsMenuBarVisible = true;
            return Task.CompletedTask;
        }
        #endregion

        #region LogOut Command
        public bool CanLogOut
        {
            get { return true; }
        }

        public void LogOut()
        {
            _loggedInUser.Clear();
            IsSideMenuVisible = IsMenuBarVisible = false;
            ActivateItemAsync(_simpleContainer.GetInstance<LoginViewModel>());
        }
        #endregion
        #region Public Prop

        public bool IsSideMenuVisible
        {
            get { return _isSideMenuVisible; }
            set 
            { 
                _isSideMenuVisible = value;
                NotifyOfPropertyChange(() => IsSideMenuVisible);
            }
        }

        public bool IsMenuBarVisible
        {
            get { return _isMenuBarVisible; }
            set 
            {
                _isMenuBarVisible = value;
                NotifyOfPropertyChange(() => IsMenuBarVisible);
            }
        }



        #endregion
    }
}
