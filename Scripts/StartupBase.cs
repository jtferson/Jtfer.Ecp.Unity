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
        Domain _domain;
        UnityScriptContext _scriptContext;
        Pipeline _awake;
        Pipeline _start;
        Pipeline _update;
        Pipeline _fixedUpdate;
        Pipeline _lateUpdate;


        private void OnEnable()
        {
            _domain = new Domain();
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
        }

        private void Update()
        {
            _scriptContext.Update(_update);
        }

        private void FixedUpdate()
        {
            _scriptContext.Update(_fixedUpdate);
        }

        private void LateUpdate()
        {
            _scriptContext.Update(_lateUpdate);
        }
    }
}
