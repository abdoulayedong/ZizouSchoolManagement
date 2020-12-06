using Caliburn.Micro;
using SchoolManagement.Data;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class AddDepartmentViewModel : Screen
    {
        #region Properties
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyOfPropertyChange("Name"); }
        }

        private string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; NotifyOfPropertyChange("Code"); }
        }

        private List<Professor> _professors;

        public List<Professor> Professors
        {
            get { return _professors; }
            set { _professors = value; NotifyOfPropertyChange("Professors"); }
        }

        public Professor Professor { get; set; }

        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _manager;
        private readonly SimpleContainer _container;
        private Department  department;
        private BindableCollection<Department> departments = new BindableCollection<Department>();
        #endregion
        public AddDepartmentViewModel(IDepartmentRepository departmentRepository, IEventAggregator eventAggregator,
                                   IWindowManager manager, SimpleContainer container)
        {
            _departmentRepository = departmentRepository;
            _eventAggregator = eventAggregator;
            _manager = manager;
            _container = container;
            _eventAggregator.SubscribeOnPublishedThread(this);
        }

        #region Override Methode Caliburn
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            departments.AddRange(_departmentRepository.GetDepartments());
            return Task.CompletedTask;
        }
        #endregion
        public async Task OnClose()
        {
            await TryCloseAsync();
        }
    } 
}
