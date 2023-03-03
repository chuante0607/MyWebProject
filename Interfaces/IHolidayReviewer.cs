using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCOMProject.Models;

namespace UCOMProject.Interfaces
{
    interface IHolidayReviewer
    {
       Task<bool> ReviewHolidayApply(List<HolidayDetailViewModel> data , ReviewType state);
    }
}
