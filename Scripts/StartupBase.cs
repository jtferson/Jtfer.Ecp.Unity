using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Jtfer.Ecp.Unity
{
    public abstract class StartupBase : MonoBehaviour
    {
        private Domain _domainInstance;
        protected Domain _domain => _domainInstance = _domainInstance ?? new Domain();
        UnityScriptContext _scriptContext;
        Pipeline _awake;
        Pipeline _start;
        Pipeline _update;
        Pipeline _fixedUpdate;
        Pipeline _lateUpdate;


        public virtual void OnEnable()
        {
            _scriptContext = new UnityScriptContext(_domain, true, "ScriptContext");

#if UNITY_EDITOR
            SupervisorObserver.Create(_domain.GetSupervisor());
            //TODO
            //PipelineObserver.Create(_systems);
#endif

            _awake = _scriptContext.CreateOperations("AwakeScripts", new AwakeScriptOperation());
            _start = _scriptContext.CreateOperations("StartScripts", new StartScriptOperation());
            _update = _scriptContext.CreateOperations("UpdateScripts", new UpdateScriptOperation());
            _fixedUpdate = _scriptContext.CreateOperations("FixedUpdateScripts", new FixedUpdateOperation());
            _lateUpdate = _scriptContext.CreateOperations("LateUpdateScripts", new LateUpdateOperation());

            _scriptContext.Initialize();
            //_scriptContext.Initialize(_awake);
            //_scriptContext.Initialize(_start);

            DefineContexts();
        }

        protected abstract void DefineContexts();

        public virtual void Update()
        {
            _scriptContext.Update(_update);
        }

        public virtual void FixedUpdate()
        {
            _scriptContext.Update(_fixedUpdate);
        }

        public virtual void LateUpdate()
        {
            _scriptContext.Update(_lateUpdate);
        }
    }
}
