using Caliburn.Micro;
using SchoolManagement.Data;
using SchoolManagement.Data.DTOs;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using SchoolManagement.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class DepartmentViewModel : Screen
    {
        #region Private fields
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _manager;
        private readonly SimpleContainer _container;
        private readonly ConfirmationDialogHelper _confirmationDialogHelper;
        private DepartmentProfessor department;
        private BindableCollection<DepartmentProfessor> departments = new BindableCollection<DepartmentProfessor>();
        List<DepartmentProfessor> GetDepartments = new List<DepartmentProfessor>();
        private string _departmentSearch;
        Regex fullNameRx = new Regex(@"^[a-z]*(\s[a-z]*)+?$", RegexOptions.IgnoreCase);
        Regex firstNameOrLastName = new Regex(@"^[a-zÀ-ÿ]*$", RegexOptions.IgnoreCase);
        #endregion

        #region Constructor
        public DepartmentViewModel(IDepartmentRepository departmentRepository, IEventAggregator eventAggregator,
                                   IWindowManager manager, SimpleContainer container)
        {
            _departmentRepository = departmentRepository;
            _eventAggregator = eventAggregator;
            _manager = manager;
            _container = container;
            _confirmationDialogHelper = new ConfirmationDialogHelper(_manager, _eventAggregator, container);
            _eventAggregator.SubscribeOnPublishedThread(this);
        }
        #endregion

        #region Override Screen Methods
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var departments = _departmentRepository.GetDepartments();
            if(departments.Count != 0)
            {               
                Departments.AddRange(departments.Where(dep => dep.IsHead == true));
                GetDepartments.AddRange(departments);
            }
            return base.OnInitializeAsync(cancellationToken);
        }
        #endregion

        #region Methods
        public async Task AddDepartment()
        {
            await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.AddDepartment);
        }

        public async Task OnSearchDepartment()
        {
            if (String.IsNullOrEmpty(DepartmentSearch) || String.IsNullOrWhiteSpace(DepartmentSearch))
            {
                await _confirmationDialogHelper.ErrorWindowShow("Veuillez remplir le champ de recherche.");
            }
            else
            {
                Departments.Clear();
                if (firstNameOrLastName.IsMatch(DepartmentSearch))
                {
                    for (int i = 0; i < GetDepartments.Count; i++)
                    {
                        if (GetDepartments[i].Code.ToLower().Contains(DepartmentSearch.ToLower()))
                        {
                            Departments.Add(GetDepartments[i]);
                        }
                        else if (GetDepartments[i].Name.ToLower().Contains(DepartmentSearch.ToLower()))
                        {
                            Departments.Add(GetDepartments[i]);
                        }
                    }
                }
                else if (fullNameRx.IsMatch(DepartmentSearch))
                {
                    var word = DepartmentSearch.Split(' ');
                    var countWord = word.Length;

                    foreach (var depart in GetDepartments)
                    {
                        int i = 0;
                        var fullname = depart.Name;
                        do
                        {
                            if (fullname.ToLower().Contains((word[i]).ToLower()))
                            {
                                if (i == countWord - 1)
                                {
                                    Departments.Add(depart);
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

        public async Task OnViewDepartment(DepartmentProfessor department)
        {
            await _eventAggregator.PublishOnBackgroundThreadAsync(ViewType.DepartmentProfessor);
            await _eventAggregator.PublishOnBackgroundThreadAsync(department);
        }

        public async Task OnUpdateDepartment(DepartmentProfessor department)
        {
            await _eventAggregator.PublishOnBackgroundThreadAsync(ViewType.UpdateDepartment);
            await _eventAggregator.PublishOnBackgroundThreadAsync(department);
        }

        public async Task OnDeleteDepartment(DepartmentProfessor department)
        {
            var result = await _confirmationDialogHelper.ConfirmationWindowCall(ElementType.Department);
            if (result)
            {
                var dep = new Domain.Department()
                {
                    Id = department.DepartmentId,
                    Code = department.Code,
                    Name = department.Name
                };
                Departments.Remove(department);
                _departmentRepository.DeleteDepartment(dep);
            }
        }

        #endregion

        #region Public fields
        public BindableCollection<DepartmentProfessor> Departments
        {
            get { return departments; }
            set { departments = value; NotifyOfPropertyChange(() => Departments); }
        }

        public DepartmentProfessor Department
        {
            get { return department; }
            set 
            {
                department = value;               
                NotifyOfPropertyChange(() => Department);
            }
        }

        public string DepartmentSearch
        {
            get { return _departmentSearch; }
            set { _departmentSearch = value; NotifyOfPropertyChange(() => DepartmentSearch); }
        }
    }
        #endregion
}

