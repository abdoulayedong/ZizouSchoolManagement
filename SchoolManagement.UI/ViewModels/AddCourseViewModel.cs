using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.UI.ViewModels
{
    public class AddCourseViewModel : Screen
    {
        #region Private Fields
        private int _nameCourse;
        private string _codeCourse;
        private int _hoursAmountCourse;
        private int _totalWeekAmountCourse;
        private int _coefficientCourse;
        #endregion

        #region Constructor

        #endregion

        #region Method

        #endregion

        #region Override Screen

        #endregion

        #region Public Fields
        public int NameCourse
        {
            get { return _nameCourse; }
            set { _nameCourse = value; NotifyOfPropertyChange(() => NameCourse); }
        }

        public string CodeCourse
        {
            get { return _codeCourse; }
            set { _codeCourse = value; NotifyOfPropertyChange(() => CodeCourse); }
        }

        public int HoursAmountCourse
        {
            get { return _hoursAmountCourse; }
            set { _hoursAmountCourse = value; NotifyOfPropertyChange(() => HoursAmountCourse); }
        }

        public int TotalWeekAmountCourse
        {
            get { return _totalWeekAmountCourse; }
            set { _totalWeekAmountCourse = value; NotifyOfPropertyChange(() => TotalWeekAmountCourse); }
        }

        public int CoefficientCourse
        {
            get { return _coefficientCourse; }
            set { _coefficientCourse = value; NotifyOfPropertyChange(() => CoefficientCourse); }
        }
        #endregion
    }
}
