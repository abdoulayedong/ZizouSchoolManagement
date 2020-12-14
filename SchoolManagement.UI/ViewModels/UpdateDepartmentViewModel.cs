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
    public class UpdateDepartmentViewModel : Screen, IHandle<Department>
    {
        #region Properties
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                var space = value.Split(" ");
                if (value.Length > 3 && space.Length < 2)
                {
                    Code = value.Substring(0, 5).ToUpper();
                }
                if (space.Length >= 2)
                {
                    Code = new string("");
                    for (int i = 0; i < space.Length; i++)
                    {
                        Code += space[i].Substring(0, 1);
                    }
                    NotifyOfPropertyChange(() => Code);
                }
                NotifyOfPropertyChange(() => Name);

            }
        }

        private string _code;

        public string Code
        {
            get { return _code; }
            set { _code = value; NotifyOfPropertyChange(() => Code); }
        }

        private BindableCollection<Professor> _professors = new BindableCollection<Professor>();
        public BindableCollection<Professor> Professors
        {
            get { return _professors; }
            set { _professors = value; NotifyOfPropertyChange("Professors"); }
        }
        public Department Department { get; set; } = new Department();
        public Professor Professor { get; set; } = new Professor();

        private readonly IDepartmentRepository _departmentRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IEventAggregator _eventAggregator;
        private readonly IWindowManager _manager;
        private readonly SimpleContainer _container;
        #endregion
        public UpdateDepartmentViewModel(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public Task HandleAsync(Department message, CancellationToken cancellationToken)
        {
            Name = message.Name;
            Code = message.Code;

            return Task.CompletedTask;
        }
    }
}
