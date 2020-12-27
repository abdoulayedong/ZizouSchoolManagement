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
    public class ClassViewModel : Screen
    {
        #region Private Fields
        private IWindowManager _manager;
        private SimpleContainer _container;
        private IEventAggregator _aggregator;
        private IClassRepository _classRepository;
        private ConfirmationDialogHelper _confirmationDialogHelper;       
        List<Class> GetClasses = new List<Class>();
        private BindableCollection<Class> _classes = new BindableCollection<Class>();
        private Class _class;
        private string _classSearch;
        Regex fullNameRx = new Regex(@"^[a-z]*(\s[a-z]*)+?$", RegexOptions.IgnoreCase);
        Regex firstNameOrLastName = new Regex(@"^[a-zÀ-ÿ]*$", RegexOptions.IgnoreCase);
        #endregion

        #region Constructor
        public ClassViewModel(IWindowManager manager, SimpleContainer container, IEventAggregator aggregator, 
                              IClassRepository classRepository)
        {
            _manager = manager;
            _container = container;
            _aggregator = aggregator;
            _classRepository = classRepository;
            _confirmationDialogHelper = new ConfirmationDialogHelper(_manager,_aggregator, _container);
        }
        #endregion

        #region Override Screen Method
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var classes = _classRepository.GetClasses();
            if(classes.Count != 0)
            {
                Classes.AddRange(classes);
                GetClasses.AddRange(classes);
            }
            return base.OnInitializeAsync(cancellationToken);
        }
        #endregion

        #region Method
        public async Task OnSearchClass()
        {
            if (String.IsNullOrEmpty(ClassSearch) || String.IsNullOrWhiteSpace(ClassSearch))
            {
                await _confirmationDialogHelper.ErrorWindowShow("Veuillez remplir le champ de recherche.");
            }
            else
            {
                Classes.Clear();
                if (firstNameOrLastName.IsMatch(ClassSearch))
                {
                    for (int i = 0; i < GetClasses.Count; i++)
                    {
                        if (GetClasses[i].Code.ToLower().Contains(ClassSearch.ToLower()))
                        {
                            Classes.Add(GetClasses[i]);
                        }
                        else if (GetClasses[i].Name.ToLower().Contains(ClassSearch.ToLower()))
                        {
                            Classes.Add(GetClasses[i]);
                        }
                    }
                }
                else if (fullNameRx.IsMatch(ClassSearch))
                {
                    var word = ClassSearch.Split(' ');
                    var countWord = word.Length;

                    foreach (var @class in GetClasses)
                    {
                        int i = 0;
                        var fullname = @class.Name;
                        do
                        {
                            if (fullname.ToLower().Contains((word[i]).ToLower()))
                            {
                                if (i == countWord - 1)
                                {
                                    Classes.Add(@class);
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

        public async Task AddClass()
        {
            await _aggregator.PublishOnBackgroundThreadAsync(ViewType.AddClass);
        }

        public async Task OnViewClass(Class _class)
        {
            await _aggregator.PublishOnBackgroundThreadAsync(ViewType.ShowClass);
            await _aggregator.PublishOnBackgroundThreadAsync(_class);
        }

        public async Task OnUpdateClass(Class _class)
        {
            await _aggregator.PublishOnBackgroundThreadAsync(ViewType.UpdateClass);
            await _aggregator.PublishOnBackgroundThreadAsync(_class);
        }

        public async Task OnDeleteClass(Class _class)
        {
            var result = await _confirmationDialogHelper.ConfirmationWindowCall(ElementType.Class);
            if (result)
            {
                Classes.Remove(_class);
                _classRepository.DeleteClass(_class);
            }
        }
        #endregion

        #region Public Fields
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

        public string ClassSearch
        {
            get { return _classSearch; }
            set { _classSearch = value; NotifyOfPropertyChange(() => ClassSearch); }
        }

        #endregion
    }
}
