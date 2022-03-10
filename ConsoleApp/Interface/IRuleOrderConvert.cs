using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Interface
{
    public interface IRuleOrderConvert<T, K> : IRuleConvert<T, K>
    {
        int OrderRule { get; }
    }
}
