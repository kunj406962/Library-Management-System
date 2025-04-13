using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG211FinalProject.Classes
{
    public partial class CustomerManager: ComponentBase
    {
        List<Customer> customers = DatabaseManager.GetAllCustomers();


    }
}
