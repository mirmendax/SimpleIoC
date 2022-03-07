using System;

namespace SimpleIoC.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class SimpleOverridedAttribute : Attribute
    {
        public SimpleOverridedAttribute() { }
    }
}
