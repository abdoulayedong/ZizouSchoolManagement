using Caliburn.Micro;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using SchoolManagement.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class CourseViewModel : Screen
    {
        #region Private Fields       
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _manager;
        private readonly SimpleContainer _container;
        private readonly ICourseRepository _courseRepository;
        private ConfirmationDialogHelper _confirmationDialogHelper;
        private BindableCollection<Course> _courses = new BindableCollection<Course>();
        private Course _course;
        List<Course> GetCourses = new List<Course>();
        private string _courseSearch;
        Regex fullNameRx = new Regex(@"^[a-z]*(\s[a-z]*)+?$", RegexOptions.IgnoreCase);
        Regex firstNameOrLastName = new Regex(@"^[a-zÀ-ÿ]*$", RegexOptions.IgnoreCase);
        #endregion

        #region Constructor
        public CourseViewModel(IEventAggregator eventAggregator, IWindowManager manager, SimpleContainer container, ICourseRepository courseRepository)
        {
            _eventAggregator = eventAggregator;
            _manager = manager;
            _container = container;
            _courseRepository = courseRepository;
            _confirmationDialogHelper = new ConfirmationDialogHelper(_manager, _eventAggregator, _container);
        }

        #endregion

        #region Method
        public async Task AddCourse()
        {
            await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.AddCourse);
        }

        public async Task OnSearchCourse()
        {
            if (String.IsNullOrEmpty(CourseSearch) || String.IsNullOrWhiteSpace(CourseSearch))
            {
                await _confirmationDialogHelper.ErrorWindowShow("Veuillez remplir le champ de recherche.");
            }
            else
            {
                Courses.Clear();
                if (firstNameOrLastName.IsMatch(CourseSearch))
                {
                    for (int i = 0; i < GetCourses.Count; i++)
                    {
                        if (GetCourses[i].CodeCourse.ToLower().Contains(CourseSearch.ToLower()))
                        {
                            Courses.Add(GetCourses[i]);
                        }
                        else if (GetCourses[i].NameCourse.ToLower().Contains(CourseSearch.ToLower()))
                        {
                            Courses.Add(GetCourses[i]);
                        }
                    }
                }
                else if (fullNameRx.IsMatch(CourseSearch))
                {
                    var word = CourseSearch.Split(' ');
                    var countWord = word.Length;

                    foreach (var course in GetCourses)
                    {
                        int i = 0;
                        var fullname = course.NameCourse;
                        do
                        {
                            if (fullname.ToLower().Contains((word[i]).ToLower()))
                            {
                                if (i == countWord - 1)
                                {
                                    Courses.Add(course);
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

        public async Task OnViewCourse(Course course)
        {
            await _eventAggregator.PublishOnBackgroundThreadAsync(ViewType.ShowCourse);
            await _eventAggregator.PublishOnBackgroundThreadAsync(course);
        }

        public async Task OnUpdateCourse(Course course)
        {
            await _eventAggregator.PublishOnBackgroundThreadAsync(ViewType.UpdateCourse);
            await _eventAggregator.PublishOnBackgroundThreadAsync(course);
        }

        public async Task OnDeleteCourse(Course course)
        {
            var result = await _confirmationDialogHelper.ConfirmationWindowCall(ElementType.Course);
            if (result)
            {
                Courses.Remove(course);
                _courseRepository.DeleteCourse(course);
            }
        }
        #endregion

        #region Override Screen Method
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var courses = _courseRepository.GetCourses();
            if (courses.Count != 0)
            {
                Courses.AddRange(courses);
                GetCourses.AddRange(courses);
            }
            return base.OnInitializeAsync(cancellationToken);
        }
        #endregion

        #region Public Fields       
        public BindableCollection<Course> Courses
        {
            get { return _courses; }
            set { _courses = value; NotifyOfPropertyChange(() => Courses); }
        }

        public Course Course
        {
            get { return _course; }
            set { _course = value; NotifyOfPropertyChange(() => Course); }
        }

        public string CourseSearch
        {
            get { return _courseSearch; }
            set { _courseSearch = value; NotifyOfPropertyChange(() => CourseSearch); }
        }
        #endregion
    }
}
