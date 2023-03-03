using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCOMProject.Models;

namespace UCOMProject.Interfaces
{
    interface IEmployeeList
    {
        Task<List<EmployeeViewModel>> GetEmployeesByBranch(List<int> branchId);
    }
}
