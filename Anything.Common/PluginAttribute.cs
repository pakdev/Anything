using System;
using System.ComponentModel.Composition;

namespace Anything.Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class PluginAttribute : ExportAttribute
    {
        public PluginAttribute(Type contractType, Type resultType)
            : base(contractType)
        {
            this.ResultType = resultType;
        }

        public Type ResultType { get; private set; }
    }
}
