using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG211FinalProject.Classes
{
    public class CustomerManager
    {
        // Retrieves the list of all customers from the database
        List<Customer> customers = DatabaseManager.GetAllCustomers();

        /// <summary>
        /// Formats a 10-digit phone number into the format XXX-XXX-XXXX.
        /// </summary>
        /// <param name="num">A 10-digit phone number as a string</param>
        /// <returns>Formatted phone number</returns>
        public static string FormatPhoneNumber(string num)
        {
            return num.Substring(0,3) + "-" + num.Substring(3,3) +"-"+num.Substring(6,4);
        }

        /// <summary>
        /// Removes dashes from a formatted phone number.
        /// </summary>
        /// <param name="phone">Phone number in format XXX-XXX-XXXX</param>
        /// <returns>Phone number with only digits</returns>
        public static string RemoveDashes(string phone)
        {
            return phone.Replace("-", "");
        }

        /// <summary>
        /// Generates a unique customer ID in the format C# (e.g., C1, C2, ...).
        /// </summary>
        /// <returns>New unique customer ID</returns>
        public static string MakeId()
        {
            return DatabaseManager.GetAllCustomers().Count == 0 ? // id = "C1" if there is no customer in database
                    "C1" : 
                    "C" + ((DatabaseManager.GetAllCustomers()
                                                              .Select(c => Int32.Parse(c.CustomerID.Substring(1))) //get the INT digits after the first character ('C')
                                                              .ToList())
                                                              .Max() + 1).ToString(); //Get the max value of the list, then plus 1 to make a new ID
        }

        /// <summary>
        /// Validates the format of an email address.
        /// </summary>
        /// <param name="email">Email address to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if a phone number is valid (10 digits, all numeric).
        /// </summary>
        /// <param name="phone">Phone number to validate</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool IsValidPhone(string phone)
        {
            return phone != null && phone.All(char.IsDigit) && phone.Length == 10;
        }

        /// <summary>
        /// Checks if any required customer fields are missing or empty.
        /// </summary>
        /// <param name="c">Customer object to validate</param>
        /// <returns>True if any field is empty or null, false otherwise</returns>
        public static bool CheckCustomer(Customer c)
        {
            return String.IsNullOrEmpty(c.FirstName) || String.IsNullOrEmpty(c.LastName) || String.IsNullOrEmpty(c.Email) || String.IsNullOrEmpty(c.Phone);
        }
    }
}
