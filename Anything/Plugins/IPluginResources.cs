using System;

namespace Anything.Plugins
{
    public interface IPluginResources
    {
        Type ResultType { get; }

        Type TemplateType { get; }
    }
}
