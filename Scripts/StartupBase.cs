using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Jtfer.Ecp.Unity
{
    public struct CustomPipeline
    {
        public PipelineContext Context { get; private set; }
        public Pipeline Pipeline { get; private set; }

        public CustomPipeline(PipelineContext context, Pipeline pipeline)
        {
            Context = context;
            Pipeline = pipeline;
        }
    }
    public abstract class StartupBase : MonoBehaviour
    {
        private Domain _domainInstance;
        protected Domain _domain => _domainInstance = _domainInstance ?? new Domain();

        private List<PipelineContext> _contexts = new List<PipelineContext>();
        private List<CustomPipeline> _defaultPipelines = new List<CustomPipeline>();
        private List<CustomPipeline> _fixedUpdatePipelines = new List<CustomPipeline>();
        private List<CustomPipeline> _lateUpdatePipelines = new List<CustomPipeline>();



        public virtual void OnEnable()
        {
            var scriptContext = new UnityScriptContext(_domain, true, "ScriptContext");

#if UNITY_EDITOR
            SupervisorObserver.Create(_domain.GetSupervisor());
            //TODO
            //PipelineObserver.Create(_systems);
#endif
            AddContext(scriptContext);
            DefineContexts();

            for (var i = 0; i < _contexts.Count; i++)
            {
                _contexts[i].Prepare();
            }

            for (var i = 0; i < _contexts.Count; i++)
            {
                _contexts[i].Initialize();
            }
        }

        protected abstract void DefineContexts();

        protected void AddContext(PipelineContext context)
        {
            context.CreateOperations();
            var defaultPipeline = context.GetDefaultPipeline();
            var operationList = new IUpdateOperation[0];
            defaultPipeline.GetRunSystems(ref operationList);
            var fixedUpdateOperations = operationList.Where(q => q is IFixedUpdateOperation).ToArray();
            if(fixedUpdateOperations.Length > 0)
                _fixedUpdatePipelines.Add(new CustomPipeline(context, context.CreateOperations("FixedUpdate", fixedUpdateOperations)));
           
            var lateUpdateOperations = operationList.Where(q => q is ILateUpdateOperation).ToArray();
            if (lateUpdateOperations.Length > 0)
                _lateUpdatePipelines.Add(new CustomPipeline(context, context.CreateOperations("LateUpdate", lateUpdateOperations)));

            defaultPipeline.RemoveRunSystems(fixedUpdateOperations.Concat(lateUpdateOperations).ToArray());
            _defaultPipelines.Add(new CustomPipeline(context, defaultPipeline));

            _contexts.Add(context);
        }

        public virtual void Update()
        {
            for(var i = 0; i < _defaultPipelines.Count; i++)
            {
                _defaultPipelines[i].Context.Update(_defaultPipelines[i].Pipeline);
            }
            _domain.RemoveOneFrameComponents();
        }

        public virtual void FixedUpdate()
        {
            for (var i = 0; i < _fixedUpdatePipelines.Count; i++)
            {
                _fixedUpdatePipelines[i].Context.Update(_fixedUpdatePipelines[i].Pipeline);
            }
        }

        public virtual void LateUpdate()
        {
            for (var i = 0; i < _lateUpdatePipelines.Count; i++)
            {
                _lateUpdatePipelines[i].Context.Update(_lateUpdatePipelines[i].Pipeline);
            }
        }
    }
}
