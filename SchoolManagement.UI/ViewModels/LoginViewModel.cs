using Caliburn.Micro;
using SchoolManagement.UI.EventModel;
using SchoolManagement.UI.Library.API;
using SchoolManagement.UI.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchoolManagement.UI.ViewModels
{
    public class LoginViewModel : Screen
    {
		private string _userEmail;
		private string _password;

		private string _errorMessage;

		private bool _isSpinnerVisible;

		private IAPIHelper _apiHelper;
		private IEventAggregator _events;


		public LoginViewModel(IAPIHelper apiHelper,
			                  IEventAggregator events)
		{
			_apiHelper = apiHelper;
			_events = events;
		}
		

		public async Task LogIn(KeyEventArgs keyArgs)
		{
			if (keyArgs.Key == Key.Enter)
			{
				try
				{
					ErrorMessage = "";
					IsSpinnerVisible = true;
					await Task.Run(() => AuthenticateUser());
					IsSpinnerVisible = false;
				}
				catch (Exception ex)
				{
					switch (ex.Message)
					{
						case "Bad Request":
							ErrorMessage = "Incorrect login or password";
							break;
						case "Unauthorized":
							ErrorMessage = "Accès denied";
							break;
						case "Internal Server Error":
							ErrorMessage = "Network error!";
							break;
						default:
							ErrorMessage = "Une erreur est survenue. Veuillez essayer plus tard!";
							break;
					}

					IsSpinnerVisible = false;
				}
			}
		}

		public async Task OnLogIn()
		{
			try
			{
				ErrorMessage = "";
				IsSpinnerVisible = true;
				await Task.Run(() => AuthenticateUser());
				IsSpinnerVisible = false;
			}
			catch (Exception ex)
			{
				switch (ex.Message)
				{
					case "Bad Request":
						ErrorMessage = "Pseudo ou mot de passe incorrect";
						break;
					case "Unauthorized":
						ErrorMessage = "Mot de passe incorrect";
						break;
					case "Internal Server Error":
						ErrorMessage = "Une erreur est survenue. Veuillez essayer plus tard!";
						break;
					default:
						ErrorMessage = "Une erreur est survenue. Veuillez essayer plus tard!";
						break;
				}

				IsSpinnerVisible = false;
			}
		}

		public async Task Close()
        {
			await TryCloseAsync();
        }

		#region public properties

		public string UserEmail
		{
			get { return _userEmail; }
			set
			{
				_userEmail = value;
				NotifyOfPropertyChange(() => UserEmail);
				NotifyOfPropertyChange(() => CanLogIn);
				NotifyOfPropertyChange(() => CanOnLogIn);
			}
		}

		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				NotifyOfPropertyChange(() => Password);
				NotifyOfPropertyChange(() => CanLogIn);
				NotifyOfPropertyChange(() => CanOnLogIn);
			}
		}

		public bool IsErrorVisible
		{
			get
			{
				bool output = false;
				if (ErrorMessage?.Length > 0)
				{
					output = true;
				}
				return output;
			}

		}

		public string ErrorMessage
		{
			get { return _errorMessage; }
			set
			{
				_errorMessage = value;
				NotifyOfPropertyChange(() => ErrorMessage);
				NotifyOfPropertyChange(() => IsErrorVisible);
			}
		}

		public bool CanLogIn
		{
			get
			{
				if (UserEmail?.Length >= 4)
				{
					return true;
				}

				return false;
			}
		}

		public bool CanOnLogIn
		{
			get
			{
				if (UserEmail?.Length >= 4 && Password?.Length >= 8)
				{
					return true;
				}

				return false;
			}
		}

		public bool IsSpinnerVisible

		{
			get { return _isSpinnerVisible; }
			set
			{
				_isSpinnerVisible = value;
				NotifyOfPropertyChange(() => IsSpinnerVisible);
			}
		}


		#endregion

		#region private Properties

		private async Task AuthenticateUser()
		{
			AuthenticatedUser result = await _apiHelper.Authenticate(UserEmail, Password);

			await _apiHelper.GetLoggedInUserInfo(result.Token);

			await _events.PublishOnUIThreadAsync(new LogInEvent());

		}

		#endregion
	}
}
