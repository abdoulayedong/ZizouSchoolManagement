using Caliburn.Micro;
using SchoolManagement.Data;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class DepartmentViewModel : Screen, IHandle<Department>
    {
        //private WindowManager manager;
        //private SimpleContainer container;
        //private EventAggregator aggregator;
        //private SchoolManagmentDBContext context;
        //private DepartmentRepository DepartmentRepository;
        //public DepartmentViewModel()
        //{
        //    manager = new WindowManager();
        //    container = new SimpleContainer();
        //    aggregator = new EventAggregator();
        //    context = new SchoolManagmentDBContext();
        //    DepartmentRepository = new DepartmentRepository(context);
        //    var dep = DepartmentRepository.GetDepartments();
        //    Departments.AddRange(dep);
        //}
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _manager;
        private readonly SimpleContainer _container;

        private Department department;
        private BindableCollection<Department> departments = new BindableCollection<Department>();
        public DepartmentViewModel(IDepartmentRepository departmentRepository, IEventAggregator eventAggregator,
            IWindowManager manager, SimpleContainer container)
        {
            _departmentRepository = departmentRepository;
            _eventAggregator = eventAggregator;
            _manager = manager;
            _container = container;
            _eventAggregator.SubscribeOnPublishedThread(this);
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var department = _departmentRepository.GetDepartments();
            Departments.AddRange(department);
            return base.OnInitializeAsync(cancellationToken);
        }

        public async Task AddDepartment()
        {
            await _manager.ShowDialogAsync(_container.GetInstance<AddDepartmentViewModel>());
        }

        public Task HandleAsync(Department message, CancellationToken cancellationToken)
        {
            if (message != null)
            {
                Departments.Add(message);
            }
            return Task.CompletedTask;
        }

        public BindableCollection<Department> Departments
        {
            get { return departments; }
            set { departments = value; NotifyOfPropertyChange(() => Departments); }
        }

        public Department Department
        {
            get { return department; }
            set { department = value; }
        }

    }
}
