using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 namespace BLAPI
{
    /// <summary>
    /// this class is used by UI layer
    /// </summary>
    public static class BLFactory
    {
        public static IBL GetBL()
        {
            return new BL.BLImp();
        }
    }
}
