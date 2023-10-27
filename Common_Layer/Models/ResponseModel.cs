using System;
using System.Collections.Generic;
using System.Text;

namespace Common_Layer.Models
{
    public class ResponseModel<T>
    {
        public bool status { get; set; }
        public string message { get; set; } 
        public T data { get; set; }
    }
}
