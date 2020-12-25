using Caliburn.Micro;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using SchoolManagement.UI.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class DepartmentProfessorViewModel : Screen, IHandle<DepartmentProfessor>
    {
        #region Private Fields
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _manager;
        private readonly SimpleContainer _container;
        private readonly ConfirmationDialogHelper _confirmationDialogHelper;
        private BindableCollection<Professor> _professors = new BindableCollection<Professor>();
        #endregion

        #region Constructor
        public DepartmentProfessorViewModel(IDepartmentRepository departmentRepository, IEventAggregator eventAggregator,
                                            IWindowManager manager, SimpleContainer container)
        {
            _departmentRepository = departmentRepository;
            _eventAggregator = eventAggregator;
            _manager = manager;
            _container = container;
            _confirmationDialogHelper = new ConfirmationDialogHelper(_manager, _eventAggregator, _container);
            _eventAggregator.SubscribeOnPublishedThread(this);
        }
        #endregion

        #region Method

        #endregion

        #region Event Handler
        public Task HandleAsync(DepartmentProfessor message, CancellationToken cancellationToken)
        {
            if(message != null)
            {
                Task.Run(() => Professors.AddRange(_departmentRepository.GetProfessors(message)));
            }
            return Task.CompletedTask;
        }

        #endregion

        #region Pulic Fields
        public BindableCollection<Professor> Professors
        {
            get { return _professors; }
            set 
            {
                _professors = value; 
                NotifyOfPropertyChange(() => Professors); }
        }
        #endregion
    }
}
