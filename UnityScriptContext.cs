using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtfer.Ecp.Unity
{
    public class UnityScriptContext : PipelineContext
    {
        public UnityScriptContext(Domain domain, bool isActive = true, string name = null) : base(domain, isActive, name)
        {
        }

        protected override void AddContainers()
        {
            var scriptContainer = AddContainer<ScriptContainer>();
            var sceneScripts = UnityEngine.Object.FindObjectsOfType<UnityScript>().OrderBy(q => q.Priority).ToArray();
            foreach (var s in sceneScripts)
                scriptContainer.AddScript(s);
        }

        protected override void AddOperations(Pipeline pipeline)
        {
        }
    }
}
