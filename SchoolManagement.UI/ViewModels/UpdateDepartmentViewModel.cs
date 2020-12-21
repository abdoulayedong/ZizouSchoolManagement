using Caliburn.Micro;
using SchoolManagement.Data.DTOs;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using SchoolManagement.UI.Helpers;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class UpdateDepartmentViewModel : Screen, IHandle<DepartmentProfessor>
    {
        #region Private fields
        private string _name;
        private string _code;
        private Professor _professor;
        private BindableCollection<Professor> _professors = new BindableCollection<Professor>();
        private readonly ConfirmationDialogHelper _confirmationDialogHelper;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _manager;
        private readonly SimpleContainer _container;
        private Department _department = new Department(); 

        #endregion

        #region Constructor
        public UpdateDepartmentViewModel(IDepartmentRepository departmentRepository, IProfessorRepository professorRepository,
            IEventAggregator eventAggregator, IWindowManager manager, SimpleContainer container)
        {
            _departmentRepository = departmentRepository;
            _professorRepository = professorRepository;
            _eventAggregator = eventAggregator;
            _manager = manager;
            _container = container;
            _confirmationDialogHelper = new ConfirmationDialogHelper(_manager, _eventAggregator, _container);
            _eventAggregator.SubscribeOnPublishedThread(this);
        }
        #endregion

        #region Event Handler
        public Task HandleAsync(DepartmentProfessor message, CancellationToken cancellationToken)
        {
            Department.Id = message.DepartmentId;
            Department.Name = message.Name;
            Department.Code = message.Code;
            Name = message.Name;
            Code = message.Code;
            var prof = _professorRepository.GetProfessors();
            Professors.AddRange(prof);
            Id = message.ProfessorId;
            if(Professors.Count != 0)
            {
                Professor = Professors.Where(p => p.Id == message.ProfessorId).First();
            }
            return Task.CompletedTask;
        }
        #endregion

        #region Override Screen Method
        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            Professors.Clear();
            return base.OnDeactivateAsync(close, cancellationToken);
        }
        #endregion

        #region Methods
        public async Task OnUpdateDepartment()
        {
            var depart = new Department()
            {
                Id = Department.Id,
                Code = Code,
                Name = Name
            };
            try
            {
                await _departmentRepository.UpdateDepartment(depart);
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
            }
            if(Professor.Id != Id)
            {
                var ancienHeadDep = new ProfessorDepartment() { DepartmentId = Department.Id, ProfessorId = Id, IsHead = false };
                try
                {
                    await _departmentRepository.UpdateProfessorDepartment(ancienHeadDep);
                }
                catch (DbException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                var newHeadDep = new ProfessorDepartment() { DepartmentId = Department.Id, ProfessorId = Id, IsHead = true };
                try
                {
                    await _departmentRepository.UpdateProfessorDepartment(newHeadDep);
                }
                catch (DbException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            await OnClose();
        }

        public async Task OnClose()
        {
            if(Name != Department.Name || Code != Department.Code)
            {
                var result = await _confirmationDialogHelper.ConfirmationWindowCall(ElementType.UpdateDepartment);
                if (result)
                {
                    await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.Department);
                    await TryCloseAsync();
                }
            } else
            {
                await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.Department);
                await TryCloseAsync();
            }
        }
        #endregion

        #region Public fields
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

        public Department Department
        {
            get { return _department; }
            set { _department = value; NotifyOfPropertyChange(() => Department); }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; NotifyOfPropertyChange(() => Code); }
        }

        public BindableCollection<Professor> Professors
        {
            get { return _professors; }
            set 
            {
                _professors = value; 
                NotifyOfPropertyChange(() => Professors); }
        }
        

        public Professor Professor
        {
            get { return _professor; }
            set { _professor = value; NotifyOfPropertyChange(() => Professor); }
        }

        public int Id { get; set; }
        #endregion
    }
}
