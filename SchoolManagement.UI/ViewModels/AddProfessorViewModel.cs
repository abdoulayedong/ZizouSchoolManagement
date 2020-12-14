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
        #region Fields
        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; NotifyOfPropertyChange(() => FirstName); }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; NotifyOfPropertyChange(() => LastName); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; NotifyOfPropertyChange(() => Email); }
        }

        private string photo;

        public string Photo
        {
            get { return photo; }
            set { photo = value; NotifyOfPropertyChange(() => Photo); }
        }

        private string cin;

        public string Cin
        {
            get { return cin; }
            set { cin = value; NotifyOfPropertyChange(() => Cin); }
        }

        private UniversityDiploma _diploma;

        public UniversityDiploma Diploma
        {
            get { return _diploma; }
            set { _diploma = value; NotifyOfPropertyChange(()=>Diploma); }
        }

        private BindableCollection<Department> departments = new BindableCollection<Department>();

        public BindableCollection<Department> Departments
        {
            get { return departments; }
            set { departments = value; NotifyOfPropertyChange(() => Departments );  }
        }

        private Department department;

        public Department Department
        {
            get { return department; }
            set { department = value; NotifyOfPropertyChange(() => Department); }
        }

        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IWindowManager _manager;
        private readonly IEventAggregator _eventAggregator;
        private readonly SimpleContainer _container;
        public Professor Professor { get; set; } = new Professor();
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
                    Photo = new Uri(openFileDialog.FileName).ToString();
                }
            }            
        }
        #endregion

        #region Override Screen Class
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            Departments.AddRange(_departmentRepository.GetDepartments());
            return base.OnInitializeAsync(cancellationToken);   
        }

       
        #endregion
    }
}
