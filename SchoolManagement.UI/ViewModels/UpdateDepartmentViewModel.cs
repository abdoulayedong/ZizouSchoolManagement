using Caliburn.Micro;
using SchoolManagement.Data.DTOs;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
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
        private BindableCollection<Professor> _professors = new BindableCollection<Professor>();
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _manager;
        private readonly SimpleContainer _container;
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
            _eventAggregator.SubscribeOnPublishedThread(this);
        }
        #endregion

        #region Event Handler
        public Task HandleAsync(DepartmentProfessor message, CancellationToken cancellationToken)
        {
            Department.Id = message.DepartmentId;
            Name = message.Name;
            Code = message.Code;
            var prof = _professorRepository.GetProfessors();
            Professors.AddRange(prof);
            //Task.Run(() => Professors.AddRange(_professorRepository.GetProfessors()));
            //Professors.AddRange();
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
            Department.Code = Code;
            Department.Name = Name;
            try
            {
                await _departmentRepository.UpdateDepartment(Department);
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
            await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.Department);
            await TryCloseAsync();
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
        public Department Department { get; set; } = new Department();
        private Professor _professor;

        public Professor Professor
        {
            get { return _professor; }
            set { _professor = value; NotifyOfPropertyChange(() => Professor); }
        }

        public int Id { get; set; }
        #endregion
    }
}
