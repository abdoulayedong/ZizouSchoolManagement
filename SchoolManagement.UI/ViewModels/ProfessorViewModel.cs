using Caliburn.Micro;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolManagement.UI.ViewModels
{
    public class ProfessorViewModel : Screen
    {
        private readonly IProfessorRepository _professorRepository; 
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _manager;
        private readonly SimpleContainer _container;
        private Professor professor;   
        private BindableCollection<Professor> professors = new BindableCollection<Professor>();
        public ProfessorViewModel(IProfessorRepository professorRepository, IEventAggregator eventAggregator,
                                   IWindowManager manager, SimpleContainer container)
        {
            _professorRepository = professorRepository;
            _eventAggregator = eventAggregator;
            _manager = manager;
            _container = container;
            _eventAggregator.SubscribeOnPublishedThread(this);
        }

        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var professor = _professorRepository.GetProfessors();
            Professors.AddRange(professor);
            return base.OnInitializeAsync(cancellationToken);
        }

        public async Task AddProfessor()
        {
            await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.AddProfessor);
        }

        public async Task UpdateProfessor(Professor professor)
        {
            await _eventAggregator.PublishOnCurrentThreadAsync(ViewType.UpdateProfessor);
            await _eventAggregator.PublishOnCurrentThreadAsync(professor);
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
    }        
}
