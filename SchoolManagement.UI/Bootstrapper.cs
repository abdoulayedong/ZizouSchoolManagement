using Caliburn.Micro;
using SchoolManagement.Data;
using SchoolManagement.UI.Library.API;
using SchoolManagement.UI.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SchoolManagement.UI
{
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();

            //ConventionManager.AddElementConvention<PasswordBox>(
            //PasswordBoxHelper.BoundPasswordProperty,
            //"Password",
            //"PasswordChanged");
        }

        protected override void Configure()
        {
            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<ILoggedInUser, LoggedInUser>()
                .Singleton<IAPIHelper, APIHelper>();

            _container.PerRequest<SchoolManagmentDBContext>();

            GetType().Assembly.GetTypes()
               .Where(type => type.IsClass)
               .Where(type => type.Name.EndsWith("ViewModel"))
               .ToList()
               .ForEach(
               viewModelType => _container.RegisterPerRequest(viewModelType, viewModelType.ToString(), viewModelType)
               );
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

    }
}
