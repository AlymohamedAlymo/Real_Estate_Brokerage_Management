using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorERP.Helpers
{
    public class OperationType
    {
        public enum OperationIs
        {
            Add = 1,
            Edit = 2,
            Delete = 3,
            Print = 4
        }
    }
}
