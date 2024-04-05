using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA.Shared.Interfaces
{
    public interface IResult<T>
    {
        List<string> Messages { get; set; }

        bool Success { get; set; }

        T Data { get; set; }
        Exception Exception { get; set; }

        int Code { get; set; }
    }
}
