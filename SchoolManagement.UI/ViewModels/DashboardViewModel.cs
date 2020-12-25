using Caliburn.Micro;
using SchoolManagement.Data;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class DashboardViewModel : Screen
    {
        #region Private fields
        private IWindowManager _manager;
        private SimpleContainer _container;
        private IEventAggregator _aggregator;
        private IStudentRepository _studentRepository;
        private IClassRepository _classRepository;
        private BindableCollection<Student> _students = new BindableCollection<Student>();
        private Student _student;


        private int _totalClass;
        private int _totalStudent;
        private int _totalFille;
        private int _totalGarcon;
        #endregion

        #region Constructeur
        public DashboardViewModel(IWindowManager manager, IEventAggregator aggregator, SimpleContainer container,
            IStudentRepository studentRepository, IClassRepository classRepository)
        {
            _manager = manager;
            _container = container;
            _aggregator = aggregator;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
        }
        #endregion

        #region Override Screen Methods
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => TotalClass = _classRepository.GetClasses().Count);
            Task.Run(() =>
            {
                var students = _studentRepository.GetStudents();
                TotalStudent = students.Count;
                foreach(var student in students)
                {
                    if(student.Gender == Gender.Femme)
                    {
                        TotalFille ++;
                    }else if (student.Gender == Gender.Homme)
                    {
                        TotalGarcon ++;
                    }
                }
                if(students.Count != 0)
                {
                    Students.AddRange(students);

                }
            });
            return base.OnInitializeAsync(cancellationToken);
        }
        #endregion

        #region Public fields
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

        public int TotalStudent
        {
            get { return _totalStudent; }
            set { _totalStudent = value; NotifyOfPropertyChange(() => TotalStudent); }
        }

        public int TotalFille
        {
            get { return _totalFille; }
            set { _totalFille = value; NotifyOfPropertyChange(() => TotalFille); }
        }

        public int TotalGarcon
        {
            get { return _totalGarcon; }
            set { _totalGarcon = value; NotifyOfPropertyChange(() => TotalGarcon); }
        }

        public int TotalClass
        {
            get { return _totalClass; }
            set { _totalClass = value; NotifyOfPropertyChange(() => TotalClass); }
        }
        #endregion
    }
}
