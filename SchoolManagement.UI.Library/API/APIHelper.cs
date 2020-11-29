using Newtonsoft.Json;
using SchoolManagement.UI.Library.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SchoolManagement.UI.Library.API
{
    public class APIHelper : IAPIHelper
    {
        private readonly ILoggedInUser _loggedInUser;
        private HttpClient apiClient;

        public APIHelper(ILoggedInUser loggedInUser)
        {
            InitializeClient();
            _loggedInUser = loggedInUser;
        }

        private void InitializeClient()
        {
            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri("https://localhost:44304");
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /**
         * Authenticate user with Web Api Endpoint
         * * @param string username
         * * @param string password
         */
        public async Task<AuthenticatedUser> Authenticate(string email, string password)
        {

            // Defines a new string to pass as the request's body
            // And converts it to JSON Object
            string stringData = JsonConvert.SerializeObject(new
            {
                email = email,
                password = password
            });

            // enconding the string
            var contentData = new StringContent(stringData, System.Text.Encoding.UTF8, "application/json");

            // Sending the request using the HttpClient's set of default functions 
            using (HttpResponseMessage response = await apiClient.PostAsync("/api/Auth/Login", contentData))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }

        }

        /**
       * Get an authenticated user Information 
       * Based on its Token 
       */
        public async Task GetLoggedInUserInfo(string token)
        {
            apiClient.DefaultRequestHeaders.Clear();
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            apiClient.DefaultRequestHeaders.Add("Authorization", $"Bearer { token }");

            using (HttpResponseMessage response = await apiClient.GetAsync("/api/Auth/GetUser"))
            {
                if (response.IsSuccessStatusCode)
                {
                    // Maps the result to LoggedInUser Properties
                    var result = await response.Content.ReadAsAsync<LoggedInUser>();

                    // Setting the properties of this class
                    // to be used by other classes
                    _loggedInUser.Id = result.Id;
                    _loggedInUser.FirstName = result.FirstName;
                    _loggedInUser.LastName = result.LastName;
                    _loggedInUser.Email = result.Email;
                    _loggedInUser.Token = token;

                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }


        }


    }
}
