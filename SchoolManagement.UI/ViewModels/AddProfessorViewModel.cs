using Caliburn.Micro;
using Microsoft.Win32;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class AddProfessorViewModel : Screen
    {
        #region Private Fields
        private string firstName;
        private string lastName;
        private string email;
        private string photo;
        private string cin;
        private UniversityDiploma _diploma;
        private BindableCollection<DepartmentProfessor> departments = new BindableCollection<DepartmentProfessor>();
        private DepartmentProfessor department;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IWindowManager _manager;
        private readonly IEventAggregator _eventAggregator;
        private readonly SimpleContainer _container;
        #endregion

        #region Constructor
        public AddProfessorViewModel(IProfessorRepository professorRepository, IDepartmentRepository departmentRepository,
            IWindowManager manager, IEventAggregator eventAggregator, SimpleContainer container)
        {
            _professorRepository = professorRepository;
            _departmentRepository = departmentRepository;
            _manager = manager;
            _eventAggregator = eventAggregator;
            _container = container;
        }
        #endregion

        #region Methods
        public async Task OnSave()
        {
            Professor.FirstName = FirstName;
            Professor.LastName = LastName;
            Professor.Cin = Cin;
            Professor.Email = Email;
            Professor.HiringDate = DateTime.Now;
            Professor.Diplome = Diploma;
            Professor.MainPhotoUrl = Photo;

            await _professorRepository.AddProfessor(Professor);
            await OnCancel();
        }
        public async Task OnCancel()
        {
            await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.Professor);
        }

        public async Task OnLoadPhoto()
        {
            using(System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.BMP, *.JPG, *.PNG, *.GIF)|*.BMP;*.JPG;*.PNG;*.GIF";
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //var test = new Uri(openFileDialog.FileName)
                    //    .ToString().Split('/');
                    //Photo = (test[test.Length - 1].Split('.'))[0];
                    Photo = new Uri(openFileDialog.FileName).ToString();
                }
            }            
        }
        #endregion

        #region Override Screen Class
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var deps = _departmentRepository.GetDepartments();
            if(deps.Count != 0)
            {
                Departments.AddRange(deps);
            }
            return base.OnInitializeAsync(cancellationToken);   
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

        public Professor Professor { get; set; } = new Professor();
        #endregion
    }
}
