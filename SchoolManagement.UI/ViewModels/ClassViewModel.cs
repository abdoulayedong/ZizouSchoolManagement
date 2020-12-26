using Caliburn.Micro;
using SchoolManagement.Data;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using SchoolManagement.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.UI.ViewModels
{
    public class ClassViewModel : Screen
    {
        #region Private Fields
        private IWindowManager _manager;
        private SimpleContainer _container;
        private IEventAggregator _aggregator;
        private IClassRepository _classRepository;
        private IDepartmentRepository _departmentRepository;
        private ConfirmationDialogHelper _confirmationDialogHelper;
        private string _name;
        private string _code;
        private int _numberOfStudents;
        private int _numberOfCourses;
        private int _numberOfProfessors;
        private Department _department;
        private BindableCollection<Department> _departments;
        #endregion

        #region Constructor
        public ClassViewModel(IWindowManager manager, SimpleContainer container, IEventAggregator aggregator, 
                              IClassRepository classRepository, IDepartmentRepository departmentRepository)
        {
            _manager = manager;
            _container = container;
            _aggregator = aggregator;
            _classRepository = classRepository;
            _departmentRepository = departmentRepository;
            _confirmationDialogHelper = new ConfirmationDialogHelper(_manager,_aggregator, _container);
        }
        #endregion

        #region Override Screen Method

        #endregion

        #region Public Fields
        public string Name
        {
            get { return _name; }
            set { _name = value; NotifyOfPropertyChange(() => Name); }
        }

        public string Code
        {
            get { return _code; }
            set { _code = value; NotifyOfPropertyChange(() => Code); }
        }

        public int NumberOfStudents
        {
            get { return _numberOfStudents; }
            set { _numberOfStudents = value; NotifyOfPropertyChange(() => NumberOfStudents); }
        }

        public int NumberOfCourses
        {
            get { return _numberOfCourses; }
            set { _numberOfCourses = value; NotifyOfPropertyChange(() => NumberOfCourses); }
        }

        public int NumberOfProfessors
        {
            get { return _numberOfProfessors; }
            set { _numberOfProfessors = value; NotifyOfPropertyChange(() => NumberOfProfessors); }
        }

        public BindableCollection<Department> Departments
        {
            get { return _departments; }
            set { _departments = value; NotifyOfPropertyChange(() => Departments); }
        }

        public Department Department
        {
            get { return _department; }
            set { _department = value; NotifyOfPropertyChange(() => Department); }
        }
        #endregion
    }
}
