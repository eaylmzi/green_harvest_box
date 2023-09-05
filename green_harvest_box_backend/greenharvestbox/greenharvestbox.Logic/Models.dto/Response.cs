using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace greenharvestbox.Logic.Models.dto
{
    public class Response<T>
    {
        public string Message { get; set; } = null!;
        public T? Data { get; set; }
        public bool Progress { get; set; }

    }
}
