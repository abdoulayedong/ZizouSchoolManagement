using Caliburn.Micro;
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
    public class ProfessorViewModel : Screen
    {
        #region Private fields
        private readonly IProfessorRepository _professorRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _manager;
        private readonly SimpleContainer _container;
        private readonly ConfirmationDialogHelper _confirmationDialogHelper;
        private string _professorSearch;
        private Professor professor;   
        private BindableCollection<Professor> professors = new BindableCollection<Professor>();
        List<Professor> GetProfessors = new List<Professor>();
        Regex fullNameRx = new Regex(@"^[a-z]*(\s[a-z]*)+?$", RegexOptions.IgnoreCase);
        Regex firstNameOrLastName = new Regex(@"^[a-z]*$", RegexOptions.IgnoreCase);
        #endregion

        #region Constructor
        public ProfessorViewModel(IProfessorRepository professorRepository, IEventAggregator eventAggregator,
                                   IWindowManager manager, SimpleContainer container, IDepartmentRepository departmentRepository)
        {
            _professorRepository = professorRepository;
            _departmentRepository = departmentRepository;
            _eventAggregator = eventAggregator;
            _manager = manager;
            _container = container;
            _confirmationDialogHelper = new ConfirmationDialogHelper(_manager, _eventAggregator, container);
            _eventAggregator.SubscribeOnPublishedThread(this);
        }
        #endregion

        #region Override Screen Method
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var professor = _professorRepository.GetProfessors();
            Task.Run(() =>
            {
                Professors.AddRange(professor);
                GetProfessors.AddRange(professor);
            });
            return base.OnInitializeAsync(cancellationToken);
        }
        #endregion

        #region Method
        public async Task OnSearchProfessor()
        {
            if(String.IsNullOrEmpty(ProfessorSearch) || String.IsNullOrWhiteSpace(ProfessorSearch))
            {
                await _confirmationDialogHelper.ErrorWindowShow("Veuillez remplir le champ de recherche.");
            }
            else 
            {
                var profdep = _departmentRepository.GetDepartments();
                Professors.Clear();
                if (firstNameOrLastName.IsMatch(ProfessorSearch))
                {
                    for(int i = 0; i < GetProfessors.Count; i++)
                    {
                        if(GetProfessors[i].FirstName.ToLower().Contains(ProfessorSearch.ToLower()) || GetProfessors[i].LastName.ToLower().Contains(ProfessorSearch.ToLower()))
                        {
                            Professors.Add(GetProfessors[i]);
                        }
                        else if(GetProfessors[i].Diplome.ToString() == ProfessorSearch)
                        {
                            Professors.Add(GetProfessors[i]);
                        }
                    }
                }else if (fullNameRx.IsMatch(ProfessorSearch))
                {
                    var word = ProfessorSearch.Split(' ');
                    var countWord = word.Length;

                    foreach (var prof in GetProfessors)
                    {
                        int i = 0;
                        var fullname = prof.FirstName + " " + prof.LastName;
                        do
                        {
                            if (fullname.ToLower().Contains((word[i]).ToLower()))
                            {
                                if (i == countWord - 1)
                                {
                                    Professors.Add(prof);
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

        public async Task AddProfessor()
        {
            await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.AddProfessor);
        }

        public async Task OnUpdateProfessor(Professor professor)
        {
            await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.UpdateProfessor);
            await _eventAggregator.PublishOnCurrentThreadAsync(professor);
        }

        public async Task OnDeleteProfessor(Professor professor)
        {
            var result = await _confirmationDialogHelper.ConfirmationWindowCall(ElementType.Professor);
            if (result)
            {
                Professors.Remove(professor);
                _professorRepository.DeleteProfessor(professor);
            }
        }
        #endregion

        #region Public Fields
        public string ProfessorSearch
        {
            get { return _professorSearch; }
            set { _professorSearch = value; NotifyOfPropertyChange(() => ProfessorSearch); }
        }
        public Professor Professor
        {
            get { return professor; }
            set { professor = value; NotifyOfPropertyChange(() => Professor); }
        }

        public BindableCollection<Professor> Professors
        {
            get { return professors; }
            set { professors = value; NotifyOfPropertyChange(() => Professors); }
        }
        #endregion
    }
}
