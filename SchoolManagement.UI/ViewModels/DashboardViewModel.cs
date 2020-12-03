using Caliburn.Micro;
using SchoolManagement.Data;
using SchoolManagement.Data.Repositories;
using SchoolManagement.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.UI.ViewModels
{
    public class DashboardViewModel : Screen
    {   
        private WindowManager manager;
        private SimpleContainer container;
        private EventAggregator aggregator;
        public BindableCollection<Student> Students { get; set; } = new BindableCollection<Student>();
        public int TotalStudent { get; set; }
        public int TotatFille { get; set; }
        public int TotalGarcon { get; set; }
        public int TotalClass { get; set; }
        private SchoolManagmentDBContext context;
        private StudentRepository StudentRepository;
        private ClassRepository ClassRepository;
        public DashboardViewModel()
        {
            manager = new WindowManager();
            container = new SimpleContainer();
            aggregator = new EventAggregator();
            context = new SchoolManagmentDBContext();
            StudentRepository = new StudentRepository(context);
            ClassRepository = new ClassRepository(context);
            TotalClass = ClassRepository.GetClasses().Count;
            var dep = StudentRepository.GetStudents();
            TotalStudent = dep.Count;
            foreach(var stu in dep)
            {
                if(stu.Gender == Gender.Femme)
                {
                    TotatFille++;
                }
                else if(stu.Gender == Gender.Homme)
                {
                    TotalGarcon++;
                }
            }
            Students.AddRange(dep);
        }
    }
}
