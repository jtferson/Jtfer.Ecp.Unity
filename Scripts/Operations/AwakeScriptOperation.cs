using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtfer.Ecp.Unity
{
    class AwakeScriptOperation : IPreInitOperation
    {
        readonly ScriptContainer _scripts = null;

        public void PreInitialize()
        {
            _scripts.Awake();
        }

        public void PreDestroy()
        {

        }
    }
}
