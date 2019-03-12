using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtfer.Ecp.Unity
{
    class StartScriptOperation : IInitOperation
    {
        readonly ScriptContainer _scripts = null;

        public void Initialize()
        {
            _scripts.Start();
        }

        public void Destroy()
        {
            
        }
    }
}
