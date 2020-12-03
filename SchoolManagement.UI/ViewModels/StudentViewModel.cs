using Caliburn.Micro;
using SchoolManagement.Data;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.UI.ViewModels
{
    public class StudentViewModel : Screen
    {
        private WindowManager manager;
        private SimpleContainer container;
        private EventAggregator aggregator;
        private SchoolManagmentDBContext context;
        private StudentRepository studentRepository;
        public BindableCollection<Student> Students { get; set; } = new BindableCollection<Student>();
        public StudentViewModel()
        {
            manager = new WindowManager();
            container = new SimpleContainer();
            aggregator = new EventAggregator();
            context = new SchoolManagmentDBContext();
            studentRepository = new StudentRepository(context);
            var dep = studentRepository.GetStudents();
            Students.AddRange(dep);
        }
    }
}
