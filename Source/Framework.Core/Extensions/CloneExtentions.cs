using Omu.ValueInjecter;

namespace Framework.Core.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class CloneEntityInjection : LoopValueInjection
    {
        protected override bool UseSourceProp(string sourcePropName)
        {
            if (sourcePropName == "Id")
                return false;
            return base.UseSourceProp(sourcePropName);
        }
    }
}
