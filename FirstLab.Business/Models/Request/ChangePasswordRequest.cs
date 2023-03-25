using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstLab.Business.Models.Request
{
    public class ChangePasswordRequest
    {
        public string PreviousPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
