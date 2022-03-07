using System;

namespace SimpleIoC.Models
{
    public class TypeConfig
    {
        public Type Service { get; set; }
        public Type Implementation { get; set; }
        public string Name { get; set; }
        public bool IsSingleton { get; set; } = false;
        public object Instance { get; set; }

        public TypeConfig(Type service, Type implementation)
        {
            Service = service;
            Implementation = implementation;
        }

        public TypeConfig(Type service, Type implementation, string name)
        {
            Service = service;
            Implementation = implementation;
            Name = name;
        }
    }
}
