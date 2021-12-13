using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
    public class Errors
    {
        public async void BadAsyncMethod()
        {
            await Task.Delay(1000);
        }
    }
}
