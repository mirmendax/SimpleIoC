using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Interface
{
    public interface IRuleConvert<T, K>
    {
        void Convert(T source, K dest);
        
    }
}
