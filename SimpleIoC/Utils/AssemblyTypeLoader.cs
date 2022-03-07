using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SimpleIoC.Utils
{
    public class AssemblyTypeLoader
    {
        //private static readonly ILog _log = LogManager.GetLogger(typeof(AssemblyTypeLoader));

        public IEnumerable<Type> GetTypes(Assembly assembly)
        {
            IEnumerable<Type> types1 = null;
            try
            {
                types1 = (IEnumerable<Type>)assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {

            }
            return types1 ?? (IEnumerable<Type>)new Type[0];
        }
        public IEnumerable<Type> GetTypes(
            IEnumerable<Assembly> assemblies) => 
                assemblies.SelectMany<Assembly, Type>(new Func<Assembly, IEnumerable<Type>>(GetTypes));
    }
}
