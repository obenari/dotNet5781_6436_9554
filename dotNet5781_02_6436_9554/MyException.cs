using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
    [Serializable]
    class MyException :Exception
    {
        public MyException() : base(){ }
        public MyException(string message) : base(message) { }
        public MyException(string message,Exception inner) : base(message,inner) { }
        protected MyException(SerializationInfo info,StreamingContext context):base(info, context) { }

    }
}
