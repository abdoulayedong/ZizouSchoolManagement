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
    public class UpdateProfessorViewModel : Screen, IHandle<Professor>, IHandle<ProfessorDepartment>
    {
        #region Private Fields
        private string firstName;
        private string lastName;
        private string email;
        private string photo;
        private string cin;
        private Professor _professor = new Professor();
        public Professor GetProfessor { get; set; }
        List<ProfessorDepartment> GetProfessorDepartments = new List<ProfessorDepartment>();
        public ProfessorDepartment GetProfessorDepartment { get; set; }
        private UniversityDiploma _diploma;
        private BindableCollection<DepartmentProfessor> departments = new BindableCollection<DepartmentProfessor>();
        private DepartmentProfessor department;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IWindowManager _manager;
        private readonly IEventAggregator _eventAggregator;
        private readonly SimpleContainer _container;
        private readonly ConfirmationDialogHelper _confirmationDialogHelper;
        #endregion

        #region Constructor
        public UpdateProfessorViewModel(IProfessorRepository professorRepository, IDepartmentRepository departmentRepository,
            IWindowManager manager, IEventAggregator eventAggregator, SimpleContainer container)
        {
            _professorRepository = professorRepository;
            _departmentRepository = departmentRepository;
            _manager = manager;
            _eventAggregator = eventAggregator;
            _container = container;
            _confirmationDialogHelper = new ConfirmationDialogHelper(_manager, _eventAggregator, _container);
            _eventAggregator.SubscribeOnPublishedThread(this);
        }
        #endregion

        #region Methods
        public async Task OnUpdate()
        {
            Professor.Id = GetProfessor.Id;
            Professor.FirstName = FirstName;
            Professor.LastName = LastName;
            Professor.Cin = Cin;
            Professor.Email = Email;
            Professor.HiringDate = DateTime.Now;
            Professor.Diplome = Diploma;
            Professor.MainPhotoUrl = Photo;
            try
            {
                await _professorRepository.UpdateProfessor(Professor);

                await _departmentRepository.UpdateProfessorDepartment(new ProfessorDepartment() 
                {
                    ProfessorId = Professor.Id,
                    DepartmentId = Department.Id,
                    IsHead = GetProfessorDepartments.Where(proDep => proDep.ProfessorId == Professor.Id).FirstOrDefault().IsHead
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception de type " + ex.Message);   
            }
            await OnCancel();
        }
        public async Task OnCancel()
        {
            await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.Professor);
        }

        public async Task OnLoadPhoto()
        {
            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.BMP, *.JPG, *.PNG, *.GIF)|*.BMP;*.JPG;*.PNG;*.GIF";
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Photo = new Uri(openFileDialog.FileName).ToString();
                }
            }
        }
        #endregion

        #region Override Screen Class
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var deps = _departmentRepository.GetDepartments();
            var profsDep = _departmentRepository.GetProfessorDepartments();
            if (deps.Count != 0)
            {
                Departments.AddRange(deps);
                GetProfessorDepartments = profsDep;
            }
            return base.OnInitializeAsync(cancellationToken);
        }
        #endregion

        #region Handle Method
        public Task HandleAsync(Professor message, CancellationToken cancellationToken)
        {
            GetProfessor = message;
            FirstName = message.FirstName;
            LastName = message.LastName;
            Email = message.Email;
            Photo = message.MainPhotoUrl;
            Cin = message.Cin;
            Diploma = message.Diplome;
            foreach (var departm in Departments)
            {
                if (departm.ProfessorId == message.Id)
                {
                    Department = departm;
                    goto end;
                }
            }
        end:return Task.CompletedTask;
        }

        public Task HandleAsync(ProfessorDepartment message, CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                GetProfessor = _professorRepository.GetProfessorById(message.ProfessorId).Result;
                Department = Departments.Where(dep => dep.DepartmentId == message.DepartmentId).FirstOrDefault();               
            });
            FirstName = GetProfessor.FirstName;
            LastName = GetProfessor.LastName;
            Email = GetProfessor.Email;
            Photo = GetProfessor.MainPhotoUrl;
            Cin = GetProfessor.Cin;
            Diploma = GetProfessor.Diplome;
            return Task.CompletedTask;
        }
        #endregion

        #region Public Fields
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; NotifyOfPropertyChange(() => FirstName); }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; NotifyOfPropertyChange(() => LastName); }
        }

        public string Email
        {
            get { return email; }
            set { email = value; NotifyOfPropertyChange(() => Email); }
        }
        public string Photo
        {
            get { return photo; }
            set { photo = value; NotifyOfPropertyChange(() => Photo); }
        }

        public string Cin
        {
            get { return cin; }
            set { cin = value; NotifyOfPropertyChange(() => Cin); }
        }

        public UniversityDiploma Diploma
        {
            get { return _diploma; }
            set { _diploma = value; NotifyOfPropertyChange(() => Diploma); }
        }

        public BindableCollection<DepartmentProfessor> Departments
        {
            get { return departments; }
            set { departments = value; NotifyOfPropertyChange(() => Departments); }
        }

        public DepartmentProfessor Department
        {
            get { return department; }
            set { department = value; NotifyOfPropertyChange(() => Department); }
        }       

        public Professor Professor
        {
            get { return _professor; }
            set { _professor = value; NotifyOfPropertyChange(() => Professor); }
        }

        #endregion
    }
}
