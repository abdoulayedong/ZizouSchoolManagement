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
            set 
            {
                _name = value; 
                var space = value.Split(" ");
                if(value.Length > 3 && space.Length < 2)
                {
                    Code = value.Substring(0, 5).ToUpper();
                }
                if (space.Length >= 2)
                {
                    Code = new string("");
                    for(int i = 0; i < space.Length; i++)
                    {
                        Code += space[i].Substring(0, 1);
                    }
                    NotifyOfPropertyChange(() => Code);
                }
                NotifyOfPropertyChange(() => Name); 
            
            }
        }

        private string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; NotifyOfPropertyChange(() => Code); }
        }

        private BindableCollection<Professor> _professors = new BindableCollection<Professor>(); 
        public BindableCollection<Professor> Professors
        {
            get { return _professors; }
            set { _professors = value; NotifyOfPropertyChange("Professors"); }
        }
        public Department Department { get; set; } = new Department();
        public Professor Professor { get; set; } = new Professor();

        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _manager;
        private readonly SimpleContainer _container;
        #endregion
        public AddDepartmentViewModel(IDepartmentRepository departmentRepository, IProfessorRepository professorRepository, 
            IEventAggregator eventAggregator, IWindowManager manager, SimpleContainer container)
        {
            _departmentRepository = departmentRepository;
            _professorRepository = professorRepository;
            _eventAggregator = eventAggregator;
            _manager = manager;
            _container = container;
            _eventAggregator.SubscribeOnPublishedThread(this);
        }

        #region Override Methode Caliburn
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            Professors.AddRange(_professorRepository.GetProfessors());
            return Task.CompletedTask;
        }
        #endregion
        #region Method
        public async Task OnSaveDepartment()
        {
            Department.Name = Name;
            Department.Code = Code;
            //HeadDepartment.IsHead = true;            
            await _departmentRepository.AddDepartment(Department);
            await OnCancel();
        }

        public async Task OnCancel()
        {
            await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.Department);
        }
        #endregion
    }
}
