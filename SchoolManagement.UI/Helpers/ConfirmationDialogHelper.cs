using Caliburn.Micro;
using SchoolManagement.UI.Models;
using SchoolManagement.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace SchoolManagement.UI.Helpers
{
    public class ConfirmationDialogHelper
    {
        #region Private fields
        private readonly IWindowManager _manager;
        private readonly IEventAggregator _eventAggregator;
        private readonly SimpleContainer _simpleContainer;
        #endregion

        #region Constructor
        public ConfirmationDialogHelper(IWindowManager manager, IEventAggregator eventAggregator, SimpleContainer simpleContainer)
        {
            _manager = manager;
            _eventAggregator = eventAggregator;
            _simpleContainer = simpleContainer;
        }
        #endregion
        public async Task<bool> ConfirmationWindowCall(ElementType type)
        {

            var confirmationView = _simpleContainer.GetInstance<ConfirmationViewModel>();
            dynamic setting = Setting();
            switch (type)
            {
                case ElementType.Department:
                    await _eventAggregator.PublishOnUIThreadAsync(new ConfirmationDialogModel("Are you sure of delete this department ?", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CA5100"))));
                    break;
                case ElementType.Professor:
                    await _eventAggregator.PublishOnUIThreadAsync(new ConfirmationDialogModel("Are you sure of delete this professor ?", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CA5100"))));
                    break;
                case ElementType.UpdateDepartment:
                    await _eventAggregator.PublishOnUIThreadAsync(new ConfirmationDialogModel("Are you sure of stop update this department ?", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00B294"))));
                    break;
                case ElementType.Student:
                    await _eventAggregator.PublishOnUIThreadAsync(new ConfirmationDialogModel("Are you sure of delete this student ?", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CA5100"))));
                    break;
                case ElementType.Course:
                    await _eventAggregator.PublishOnUIThreadAsync(new ConfirmationDialogModel("Are you sure of delete this course ?", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CA5100"))));
                    break;
                case ElementType.Class:
                    await _eventAggregator.PublishOnUIThreadAsync(new ConfirmationDialogModel("Are you sure of delete this class ?", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CA5100"))));
                    break;
                case ElementType.UpdateClass:
                    await _eventAggregator.PublishOnUIThreadAsync(new ConfirmationDialogModel("Are you sure of stop update this class ?", new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00B294"))));
                    break;
                default:
                    await _eventAggregator.PublishOnUIThreadAsync(new ConfirmationDialogModel("Êtes vous certain de vouloir supprimer cet élement ?"));
                    break;
            }
            return await _manager.ShowDialogAsync(confirmationView, null, setting);
        }
        public async Task SuccessWindowShow(string message)
        {
            var successView = _simpleContainer.GetInstance<SuccessMessageViewModel>();
            dynamic setting = Setting();
            await _eventAggregator.PublishOnUIThreadAsync(message);
            await _manager.ShowDialogAsync(successView, null, setting);
        }


        public async Task ErrorWindowShow(string message)
        {
            var errorView = _simpleContainer.GetInstance<ErrorMessageViewModel>();
            dynamic setting = Setting();
            await _eventAggregator.PublishOnUIThreadAsync(message);
            await _manager.ShowDialogAsync(errorView, null, setting);
        }

        private static dynamic Setting()
        {
            dynamic setting = new ExpandoObject();
            setting.WindowStyle = WindowStyle.None;
            setting.AllowsTransparency = true;
            setting.ResizeMode = ResizeMode.NoResize;
            return setting;
        }
    }


    public enum ElementType
    {
        Department,
        UpdateDepartment,
        Professor,
        Course,
        Student,
        Class,
        UpdateClass
    }
}
