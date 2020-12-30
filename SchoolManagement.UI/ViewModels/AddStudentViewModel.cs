using Caliburn.Micro;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using SchoolManagement.UI.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class AddStudentViewModel : Screen
    {
        #region Private Fields
        private string firstName;
        private string lastName;
        private string email;
        private string photo;
        private string adresse;
        private DateTime birthDate = DateTime.Now;
        private string phoneNumber;
        private Gender _studentGender;
        private UniversityDiploma _studiesGrade;
        private BindableCollection<Class> _classes 
            = new BindableCollection<Class>();
        private Class _class;
        private readonly IWindowManager _manager;
        private readonly IEventAggregator _eventAggregator;
        private readonly SimpleContainer _container;
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly ConfirmationDialogHelper _confirmationDialogHelper;
        #endregion

        #region Constructor
        public AddStudentViewModel(IWindowManager manager, IEventAggregator eventAggregator, SimpleContainer container,
            IStudentRepository studentRepository, IClassRepository classRepository)
        {
            _manager = manager;
            _eventAggregator = eventAggregator;
            _container = container;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _confirmationDialogHelper = new ConfirmationDialogHelper(_manager, _eventAggregator, _container);
        }
        #endregion

        #region Override Screen Method
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var classes = _classRepository.GetClasses();
            if (classes.Count != 0)
            {
                Classes.AddRange(classes);
            }
            return base.OnInitializeAsync(cancellationToken);
        }
        #endregion

        #region Method
        public async Task OnSave()
        {
            if(Class == null)
            {
                await _confirmationDialogHelper.ErrorWindowShow("Can you selected class please?");
            }
            else
            {
                Student.FirstName = FirstName;
                Student.LastName = LastName;
                Student.Address = Adresse;
                Student.Email = Email;
                Student.BirthDate = BirthDate;
                Student.PhoneNumber = PhoneNumber;
                Student.Gender = Gender;
                Student.InscriptionDate = DateTime.Now;
                Student.StudiesGrade = StudiesGrade;
                Student.MainPhotoUrl = Photo;
                Student.ClassId = Class.Id;
                try
                {
                    await _studentRepository.AddStudent(Student);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception de type " + ex.Message);
                }
                await OnCancel();
            }
        }
        public async Task OnCancel()
        {
            await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.Student);
        }
        public Task OnLoadPhoto()
        {
            using (System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files(*.BMP, *.JPG, *.PNG, *.GIF)|*.BMP;*.JPG;*.PNG;*.GIF";
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Photo = new Uri(openFileDialog.FileName).ToString();
                }
            }
            return Task.CompletedTask;
        }
        #endregion

        #region Public Fields
        public Gender Gender
        {
            get { return _studentGender; }
            set { _studentGender = value; NotifyOfPropertyChange(() => Gender); }
        }
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

        public string Adresse
        {
            get { return adresse; }
            set { adresse = value; NotifyOfPropertyChange(() => Adresse); }
        }

        public UniversityDiploma StudiesGrade
        {
            get { return _studiesGrade; }
            set { _studiesGrade = value; NotifyOfPropertyChange(() => StudiesGrade); }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; NotifyOfPropertyChange(() => BirthDate); }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; NotifyOfPropertyChange(() => PhoneNumber); }
        }
        
        public BindableCollection<Class> Classes
        {
            get { return _classes; }
            set { _classes = value; NotifyOfPropertyChange(() => Classes); }
        }
        public Class Class
        {
            get { return _class; }
            set { _class = value; NotifyOfPropertyChange(() => Class); }
        }
        public Student Student { get; set; } = new Student();
        #endregion
    }
}
