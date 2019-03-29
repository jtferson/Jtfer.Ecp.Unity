using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtfer.Ecp.Unity
{
    public class UnityScriptContext : PipelineContext
    {
        private ScriptContainer scriptContainer;
        public UnityScriptContext(Domain domain, bool isActive = true, string name = null) : base(domain, isActive, name)
        {
        }

        public void AddAndPrepareScript(UnityScript unityScript)
        {
            AddAndPrepare(unityScript);
            scriptContainer.AddScript(unityScript);
        }

        protected override void AddContainers()
        {
            scriptContainer = AddContainer<ScriptContainer>();
            var sceneScripts = UnityEngine.Object.FindObjectsOfType<UnityScript>().OrderBy(q => q.Priority).ToArray();
            foreach (var s in sceneScripts)
            {
                AddContainer(s);
                scriptContainer.AddScript(s);
            }  
        }



        protected override void AddOperations(Pipeline pipeline)
        {
            pipeline
                .Add(new AwakeScriptOperation())
                .Add(new StartScriptOperation())
                .Add(new UpdateScriptOperation())
                .Add(new FixedUpdateOperation())
                .Add(new LateUpdateOperation());
        }
    }
}
