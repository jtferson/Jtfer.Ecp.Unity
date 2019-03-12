using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Jtfer.Ecp.Unity
{
    public abstract class UnityScript : MonoBehaviour, IContainer
    {
        public bool EnsureIsActive => gameObject.activeSelf && enabled;
        public virtual int Priority => 0;

        private bool AwakeWasSuccessfullyCalled;
        private bool StartWasSuccessfullyCalled;

        void Awake() { }
        void Start() { }
        void Update() { }
        void FixedUpdate() { }
        void LateUpdate() { }

        internal void CustomAwake()
        {
            if (!AwakeWasSuccessfullyCalled)
            {
                AwakeWasSuccessfullyCalled = true;
                AwakeScript();
            }
        }

        internal void CustomStart()
        {
            if (EnsureIsActive && AwakeWasSuccessfullyCalled && !StartWasSuccessfullyCalled)
            {
                StartWasSuccessfullyCalled = true;
                StartScript();
            }
        }

        internal void CustomUpdate()
        {
            if (EnsureIsActive && AwakeWasSuccessfullyCalled && StartWasSuccessfullyCalled)
                UpdateScript();
        }

        internal void CustomFixedUpdate()
        {
            if (EnsureIsActive && AwakeWasSuccessfullyCalled && StartWasSuccessfullyCalled)
                FixedUpdateScript();
        }

        internal void CustomLateUpdate()
        {
            if (EnsureIsActive && AwakeWasSuccessfullyCalled && StartWasSuccessfullyCalled)
                LateUpdateScript();
        }


        protected virtual void AwakeScript() { }
        protected virtual void StartScript() { }
        protected virtual void UpdateScript() { }
        protected virtual void LateUpdateScript() { }
        protected virtual void FixedUpdateScript() { }
    }
}
