using System;

namespace SimpleIoC.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class SimpleBinderAttribute : Attribute
    {
        public Type Service { get; }
        public string Name { get; set; }
        public SimpleBinderAttribute(Type Service)
        {
            this.Service = Service;
        }
    }
}
