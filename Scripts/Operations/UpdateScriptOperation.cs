﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtfer.Ecp.Unity
{
    [EcpInject]
    class UpdateScriptOperation : IUpdateOperation
    {
        readonly ScriptContainer _scripts = null;
        public void Update()
        {
            _scripts.Update();
        }
    }
}
