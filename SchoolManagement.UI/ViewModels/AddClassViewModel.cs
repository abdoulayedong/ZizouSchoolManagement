using Caliburn.Micro;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using SchoolManagement.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class AddClassViewModel : Screen
    {
        #region Private Fields
        private IWindowManager _manager;
        private SimpleContainer _container;
        private IEventAggregator _aggregator;
        private IClassRepository _classRepository;
        private IDepartmentRepository _departmentRepository;
        private ConfirmationDialogHelper _confirmationDialogHelper;
        private string _name;
        private string _code;
        private DepartmentProfessor _department;
        private BindableCollection<DepartmentProfessor> _departments = new BindableCollection<DepartmentProfessor>();
        private Class _class = new Class();
        #endregion

        #region Constructor
        public AddClassViewModel(IWindowManager manager, SimpleContainer container, IEventAggregator aggregator,
                              IClassRepository classRepository, IDepartmentRepository departmentRepository)
        {
            _manager = manager;
            _container = container;
            _aggregator = aggregator;
            _classRepository = classRepository;
            _departmentRepository = departmentRepository;
            _confirmationDialogHelper = new ConfirmationDialogHelper(_manager, _aggregator, _container);
        }
        #endregion

        #region Override Screen Method
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var department = _departmentRepository.GetDepartments();
            if (department.Count != 0)
            {
                Departments.AddRange(department.Where(dep=> dep.IsHead == true));
            }
            return base.OnInitializeAsync(cancellationToken);
        }
        #endregion

        #region Method       
        public async Task OnSaveClass()
        {
            if(Department == null)
            {
                await _confirmationDialogHelper.ErrorWindowShow("Can you selected a department to validate creation class!");
            }else if(String.IsNullOrEmpty(Name) || String.IsNullOrWhiteSpace(Name))
            {
                await _confirmationDialogHelper.ErrorWindowShow("Can you completed class name to validate creation class!");
            }else if (String.IsNullOrWhiteSpace(Code) || String.IsNullOrEmpty(Code))
            {
                await _confirmationDialogHelper.ErrorWindowShow("Can you completed class code to validate creation class!");
            }
            else
            {
                Class.Name = Name;
                Class.Code = Code;
                Class.DepartmentId = Department.DepartmentId;
                try
                {
                    Class = await _classRepository.AddClass(Class);
                }
                catch (DbException ex)
                {
                    Console.WriteLine(ex.Message);
                }            
                await OnCancel();
            }
        }

        public async Task OnCancel()
        {
            await _aggregator.PublishOnCurrentThreadAsync(ViewType.Class);
        }
        #endregion

        #region Public Fields
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

        public string Code
        {
            get { return _code; }
            set { _code = value; NotifyOfPropertyChange(() => Code); }
        }

        public BindableCollection<DepartmentProfessor> Departments
        {
            get { return _departments; }
            set { _departments = value; NotifyOfPropertyChange(() => Departments); }
        }

        public DepartmentProfessor Department
        {
            get { return _department; }
            set { _department = value; NotifyOfPropertyChange(() => Department); }
        }

        public Class Class
        {
            get { return _class; }
            set { _class = value; NotifyOfPropertyChange(() => Class); }
        }
        #endregion
    }
}
