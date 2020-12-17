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
    public class StudentViewModel : Screen
    {
        #region Private fields
        private IWindowManager _manager;
        private SimpleContainer _container;
        private IEventAggregator _aggregator;
        private IStudentRepository _studentRepository;
        private BindableCollection<Student> _students = new BindableCollection<Student>();
        #endregion

        public StudentViewModel(IStudentRepository studentRepository, IEventAggregator aggregator,
            IWindowManager manager, SimpleContainer container)
        {
            _studentRepository = studentRepository;
            _manager = manager;
            _container = container;
            _aggregator = aggregator;
        }

        #region Override Screen Methods
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => Students.AddRange(_studentRepository.GetStudents()));
            return base.OnInitializeAsync(cancellationToken);
        }
        #endregion
        #region Public fields
        public BindableCollection<Student> Students
        {
            get { return _students; }
            set { _students = value; NotifyOfPropertyChange(() => Students); }
        }
        #endregion
    }
}
