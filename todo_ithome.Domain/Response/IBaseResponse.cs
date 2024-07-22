using System;
using System.Collections.Generic;
using System.Text;
using todo_ithome.Domain.Enum;

namespace todo_ithome.Domain.Response
{
    public interface IBaseResponse<T>
    {
        string Description { get; set; }

        StatusCode StatusCode { get; set; }

        T Data { get; set; }
    }
}
