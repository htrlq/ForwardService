using System;

namespace Forward.Core
{
    public class ParamTypeAttribute:Attribute
    {
        public Type ParamType { get; set; }

        public ParamTypeAttribute(Type paramType)
        {
            ParamType = paramType;
        }
    }
}
