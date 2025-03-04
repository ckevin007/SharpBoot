using System;
using System.Collections.Generic;
using System.Text;

namespace SharpBoot.Common.Services
{
    public interface IValueInjecter
    {
        T Get<T>(string section);

        object Get(Type valueType, string section);

        //int InjectIndex { get; }

        Type ValueAttributeType { get; }


        Type ConfigPropertyType { get; }
    }
}
