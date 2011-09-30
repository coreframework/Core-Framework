using System;
using Omu.ValueInjecter;

namespace Framework.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class CloneEntityInjection : LoopValueInjection
    {
        protected override bool UseSourceProp(String sourcePropName)
        {
            if (sourcePropName == "Id")
                return false;
            return base.UseSourceProp(sourcePropName);
        }
    }
}
