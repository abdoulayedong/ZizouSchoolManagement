using Caliburn.Micro;
using SchoolManagement.Data;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using SchoolManagement.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class StudentViewModel : Screen
    {
        #region Private fields
        private IWindowManager _manager;
        private SimpleContainer _container;
        private IEventAggregator _eventAggregator;
        private IStudentRepository _studentRepository;
        private readonly ConfirmationDialogHelper _confirmationDialogHelper;
        private string _studentSearch;
        private Student _student;
        private BindableCollection<Student> _students = new BindableCollection<Student>();
        List<Student> GetStudents = new List<Student>();
        Regex fullNameRx = new Regex(@"^[a-z]*(\s[a-z]*)+?$", RegexOptions.IgnoreCase);
        Regex firstNameOrLastName = new Regex(@"^[a-z]*$", RegexOptions.IgnoreCase);
        #endregion

        public StudentViewModel(IStudentRepository studentRepository, IEventAggregator aggregator,
            IWindowManager manager, SimpleContainer container)
        {
            _studentRepository = studentRepository;
            _manager = manager;
            _container = container;
            _eventAggregator = aggregator;
            _confirmationDialogHelper = new ConfirmationDialogHelper(_manager, _eventAggregator, _container);
        }

        #region Override Screen Methods
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var student = _studentRepository.GetStudents();
            if(student.Count != 0)
            {
                Task.Run(() =>
                {
                    Students.AddRange(student);
                    GetStudents.AddRange(student);
                });
            }
            return base.OnInitializeAsync(cancellationToken);
        }
        #endregion

        #region Method 
        public async Task AddStudent()
        {
            await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.AddStudent);
        }
        public async Task OnUpdateStudent(Student student)
        {
            await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.UpdateStudent);
            await _eventAggregator.PublishOnCurrentThreadAsync(student);
        }

        public async Task OnDeleteStudent(Student student)
        {
            var result = await _confirmationDialogHelper.ConfirmationWindowCall(ElementType.Student);
            if (result)
            {
                Students.Remove(student);
                _studentRepository.DeleteStudent(student);
            }
        }
        public async Task OnSearchStudent()
        {
            if (String.IsNullOrEmpty(StudentSearch) || String.IsNullOrWhiteSpace(StudentSearch))
            {
                await _confirmationDialogHelper.ErrorWindowShow("Veuillez remplir le champ de recherche.");
            }
            else
            {
                Students.Clear();
                if (firstNameOrLastName.IsMatch(StudentSearch))
                {
                    for (int i = 0; i < GetStudents.Count; i++)
                    {
                        if (GetStudents[i].FirstName.ToLower().Contains(StudentSearch.ToLower()))
                        {
                            Students.Add(GetStudents[i]);
                        }
                        else if (GetStudents[i].LastName.ToLower().Contains(StudentSearch.ToLower()))
                        {
                            Students.Add(GetStudents[i]);
                        }
                    }
                }
                else if (fullNameRx.IsMatch(StudentSearch))
                {
                    var word = StudentSearch.Split(' ');
                    var countWord = word.Length;

                    foreach (var student in GetStudents)
                    {
                        int i = 0;
                        var fullname = student.FirstName + " " + student.LastName;
                        do
                        {
                            if (fullname.ToLower().Contains((word[i]).ToLower()))
                            {
                                if (i == countWord - 1)
                                {
                                    Students.Add(student);
                                }
                                i++;
                            }
                            else
                            {
                                break;
                            }
                        } while (i < countWord);
                    }
                }
            }
        }
        #endregion

        #region Public fields
        public string StudentSearch
        {
            get { return _studentSearch; }
            set { _studentSearch = value; NotifyOfPropertyChange(() => StudentSearch); }
        }
        public Student Student
        {
            get { return _student; }
            set { _student = value; NotifyOfPropertyChange(() => Student); }
        }
        public BindableCollection<Student> Students
        {
            get { return _students; }
            set { _students = value; NotifyOfPropertyChange(() => Students); }
        }
        #endregion
    }
}
