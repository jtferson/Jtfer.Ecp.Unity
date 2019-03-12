using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtfer.Ecp.Unity
{
    public class ScriptContainer : IContainer
    {
        readonly UnityScriptContext _context = null;

        UnityScript[] _containers = new UnityScript[8];
        int _containersCount;

        public void AddScript(UnityScript script)
        {
            _context.AddContainer(script);
            if (_containersCount == _containers.Length)
            {
                Array.Resize(ref _containers, _containersCount << 1);
            }
            _containers[_containersCount++] = script;
        }

        public void Update()
        {
            foreach(var container in _containers)
                container.CustomUpdate();
        }

        public void FixedUpdate()
        {
            foreach (var container in _containers)
                container.CustomFixedUpdate();
        }

        public void LateUpdate()
        {
            foreach (var container in _containers)
                container.CustomLateUpdate();
        }

        public void Awake()
        {
            foreach (var container in _containers)
                container.CustomAwake();
        }

        public void Start()
        {
            foreach (var container in _containers)
                container.CustomStart();
        }
    }
}
