using Caliburn.Micro;
using SchoolManagement.Data;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.UI.ViewModels
{
    public class ClassViewModel : Screen
    {
        private WindowManager manager;
        private SimpleContainer container;
        private EventAggregator aggregator;
        private SchoolManagmentDBContext context;
        private ClassRepository classRepository;
        public BindableCollection<Class> Classes { get; set; } = new BindableCollection<Class>();
        public ClassViewModel()
        {
            manager = new WindowManager();
            container = new SimpleContainer();
            aggregator = new EventAggregator();
            context = new SchoolManagmentDBContext();
            classRepository = new ClassRepository(context);
            var dep = classRepository.GetClasses();
            Classes.AddRange(dep);
        }
    }
}
