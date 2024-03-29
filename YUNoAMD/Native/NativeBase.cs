using Jurassic;
using Jurassic.Library;

namespace YUNoAMD.Native
{
    public class NativeBase : ObjectInstance
    {
        protected ScriptEngine _engine;

        protected NativeBase(ScriptEngine engine) : base(engine.Object)
        {
            _engine = engine;
            this.PopulateFunctions();
        }
    }
}