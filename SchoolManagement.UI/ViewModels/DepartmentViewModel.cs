using Caliburn.Micro;
using SchoolManagement.Data;
using SchoolManagement.Data.DTOs;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class DepartmentViewModel : Screen
    {
        #region Private fields
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _manager;
        private readonly SimpleContainer _container;
        private DepartmentProfessor department;
        private BindableCollection<DepartmentProfessor> departments = new BindableCollection<DepartmentProfessor>();
        private bool _isUpdateDepartment;

        public bool IsUpdateDepartment
        {
            get { return _isUpdateDepartment; }
            set 
            {
                if(value != _isUpdateDepartment)
                {
                    _isUpdateDepartment = value; 
                    NotifyOfPropertyChange(() => IsUpdateDepartment); 
                }
            }
        }

        private bool _isDeleteDepartment;

        public bool IsDeleteDepartment
        {
            get { return _isDeleteDepartment; }
            set 
            {
                if(value != _isDeleteDepartment)
                {
                    _isDeleteDepartment = value;
                    NotifyOfPropertyChange(() => IsDeleteDepartment);
                }
            }
        }

        #endregion

        #region Constructor
        public DepartmentViewModel(IDepartmentRepository departmentRepository, IEventAggregator eventAggregator,
                                   IWindowManager manager, SimpleContainer container)
        {
            IsUpdateDepartment = false;
            IsDeleteDepartment = false;
            _departmentRepository = departmentRepository;
            _eventAggregator = eventAggregator;
            _manager = manager;
            _container = container;
            _eventAggregator.SubscribeOnPublishedThread(this);
        }
        #endregion

        #region Override Screen Methods
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var departments = _departmentRepository.GetDepartments();
            if(departments.Count != 0)
            {
                foreach(var department in departments)
                {
                    Departments.Add(department);
                }
            }
            return base.OnInitializeAsync(cancellationToken);
        }
        #endregion

        #region Methods
        public async Task AddDepartment()
        {
            await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.AddDepartment);
        }
        
        public async Task UpdateDepartment()
        {
            IsDeleteDepartment = false;
            IsUpdateDepartment = true;
        }

        public async Task DeleteDepartment()
        {
            IsUpdateDepartment = false;
            IsDeleteDepartment = true;
        }

        public async Task OnDesactivate()
        {
            IsUpdateDepartment = false;
            IsDeleteDepartment = false;
        }

        public async Task OnUpdateDepartment(DepartmentProfessor department)
          {
            await _eventAggregator.PublishOnBackgroundThreadAsync(ViewType.UpdateDepartment);
            await _eventAggregator.PublishOnBackgroundThreadAsync(department);
        }

        public async Task OnDeleteDepartment(DepartmentProfessor department)
        {
            
        }

        #endregion

        #region Public fields
        public BindableCollection<DepartmentProfessor> Departments
        {
            get { return departments; }
            set { departments = value; NotifyOfPropertyChange(() => Departments); }
        }

        public DepartmentProfessor Department
        {
            get { return department; }
            set 
            {
                department = value;
                if (department != null)
                {
                    IsUpdateDepartment = true;
                }
                else
                {
                IsUpdateDepartment = false;
                NotifyOfPropertyChange(() => IsUpdateDepartment);
            }
            /// Shows and hides the detailed section
        }
    }
        #endregion
    }
}
