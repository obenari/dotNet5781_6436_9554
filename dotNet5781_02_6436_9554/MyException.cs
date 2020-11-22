using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6436_9554
{
    /// <summary>
    /// this exception class  for the case :cannot delete the required value 
    /// </summary>
    [Serializable]
    
   public class CannotDeletedException :Exception
    {
        public CannotDeletedException() : base(){ }
        public CannotDeletedException(string message) : base(message) { }
        public CannotDeletedException(string message,Exception inner) : base(message,inner) { }
        protected CannotDeletedException(SerializationInfo info,StreamingContext context):base(info, context) { }

    }
}
