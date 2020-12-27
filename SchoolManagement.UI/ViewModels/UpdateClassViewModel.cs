using Caliburn.Micro;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using SchoolManagement.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class UpdateClassViewModel : Screen, IHandle<Class>
    {
        #region Private Fields
        private string _name;
        private string _code;
        private Class _class;
        private DepartmentProfessor _department;
        private BindableCollection<DepartmentProfessor> _departments = new BindableCollection<DepartmentProfessor>();
        private readonly ConfirmationDialogHelper _confirmationDialogHelper;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IClassRepository _classRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _manager;
        private readonly SimpleContainer _container;
        #endregion

        #region Constructor
        public UpdateClassViewModel(IDepartmentRepository departmentRepository, IClassRepository classRepository, IEventAggregator eventAggregator, IWindowManager manager, SimpleContainer container)
        {
            _departmentRepository = departmentRepository;
            _classRepository = classRepository;
            _eventAggregator = eventAggregator;
            _manager = manager;
            _container = container;
            _confirmationDialogHelper = new ConfirmationDialogHelper(_manager, _eventAggregator, _container);
            _eventAggregator.SubscribeOnPublishedThread(this);
        }
        #endregion

        #region Override Screen Method
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            Departments.AddRange(_departmentRepository.GetDepartments().Where(dep => dep.IsHead == true));
            return base.OnInitializeAsync(cancellationToken);
        }
        #endregion

        #region Event Handler
        public Task HandleAsync(Class message, CancellationToken cancellationToken)
        {
            Class = message;
            GetClass = message;
            Name = message.Name;
            Code = message.Code;
            Department = Departments.Where(dep => dep.DepartmentId == message.DepartmentId).FirstOrDefault();
            return Task.CompletedTask;
        }
        #endregion

        #region Method
        public async Task OnUpdateClass()
        {
            Class.Code = Code;
            Class.Name = Name;
            Class.DepartmentId = Department.DepartmentId;
            try
            {
                GetClass = await _classRepository.UpdateClass(Class);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            await OnClose();
        }

        public async Task OnClose()
        {
            if(Name != GetClass.Name || Code!= GetClass.Code || Department.DepartmentId != GetClass.DepartmentId)
            {
                var result = await _confirmationDialogHelper.ConfirmationWindowCall(ElementType.UpdateClass);
                if (result)
                {
                    await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.Class);
                    await TryCloseAsync();
                }
            }else
            {
                await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.Class);
                await TryCloseAsync();
            }
        }
        #endregion

        #region Public Fields
        public Class GetClass { get; set; }
        public Class Class
        {
            get { return _class; }
            set { _class = value; NotifyOfPropertyChange(() => Class); }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                var space = value.Split(" ");
                if (value.Length > 3 && space.Length < 2)
                {
                    Code = value.Substring(0, 5).ToUpper();
                }
                if (space.Length >= 2)
                {
                    Code = new string("");
                    for (int i = 0; i < space.Length; i++)
                    {
                        Code += space[i].Substring(0, 1);
                    }
                    NotifyOfPropertyChange(() => Code);
                }
                NotifyOfPropertyChange(() => Name);

            }
        }

        public DepartmentProfessor Department
        {
            get { return _department; }
            set { _department = value; NotifyOfPropertyChange(() => Department); }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; NotifyOfPropertyChange(() => Code); }
        }

        public BindableCollection<DepartmentProfessor> Departments
        {
            get { return _departments; }
            set
            {
                _departments = value;
                NotifyOfPropertyChange(() => Departments);
            }
        }
        #endregion
    }
}
