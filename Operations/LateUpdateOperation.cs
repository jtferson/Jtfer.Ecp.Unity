using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtfer.Ecp.Unity
{
    class LateUpdateOperation : IUpdateOperation
    {
        readonly ScriptContainer _scripts;
        public void Update()
        {
            _scripts.LateUpdate();
        }
    }
}
