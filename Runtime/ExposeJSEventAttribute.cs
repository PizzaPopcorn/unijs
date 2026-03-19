using System;

namespace UniJS
{
    [AttributeUsage(AttributeTargets.Class,  Inherited = false)]
    public class ExposeJSEventAttribute : Attribute
    {
        public string Name { get; }
        public ExposeJSEventAttribute(string name) => Name = name;
    }
}