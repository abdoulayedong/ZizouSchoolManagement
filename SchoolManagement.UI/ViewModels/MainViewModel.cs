using Caliburn.Micro;
using SchoolManagement.UI.EventModel;
using SchoolManagement.UI.Library.API;
using SchoolManagement.UI.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
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

            // Initiate the View 
            ActivateItemAsync(_simpleContainer.GetInstance<LoginViewModel>());
        }

        #region IHandle Implementation
        public Task HandleAsync(LogInEvent logInEvent, CancellationToken cancellationToken)
        {
            ActivateItemAsync(_homeViewModel);
            return Task.CompletedTask;
        }

        #endregion
    }
}
