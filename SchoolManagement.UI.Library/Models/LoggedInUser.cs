using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.UI.Library.Models
{
    public class LoggedInUser : ILoggedInUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public string Email { get; set; }
        public string Token { get; set; }

        /// <summary>
        /// Resets the LoggedInUser Properties
        /// </summary>
        public void Clear()
        {
            Id = 0;
            LastName = FirstName = Email = Token = String.Empty;
        }

    }
}
