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
        List<Customer> customers = DatabaseManager.GetAllCustomers();

        public static string FormatPhoneNumber(string num)
        {
            return num.Substring(0,3) + "-" + num.Substring(3,3) +"-"+num.Substring(6,4);
        }

        public static string RemoveDashes(string phone)
        {
            return phone.Replace("-", "");
        }

        public static string MakeId()
        {
            return "C" + ((DatabaseManager.GetAllCustomers().Select(c => Int32.Parse(c.CustomerID.Substring(1))).ToList()).Max() + 1).ToString();
        }

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

        public static bool IsValidPhone(string phone)
        {
            return phone != null && phone.All(char.IsDigit) && phone.Length == 10;
        }

        public static bool CheckCustomer(Customer c)
        {
            return String.IsNullOrEmpty(c.FirstName) || String.IsNullOrEmpty(c.LastName) || String.IsNullOrEmpty(c.Email) || String.IsNullOrEmpty(c.Phone);
        }
    }
}
