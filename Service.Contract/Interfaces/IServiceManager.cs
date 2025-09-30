using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contract.Interfaces
{
    public interface IServiceManager
    {
        ICompanyService CompanyService { get; set; }
        IEmployeeService EmployeeService { get; set; }
    }
}
