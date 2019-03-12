using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jtfer.Ecp.Unity
{
    public class ScriptContainer : IContainer
    {
        UnityScript[] _containers = new UnityScript[8];
        int _containersCount;

        public void AddScript(UnityScript script)
        {
            if (_containersCount == _containers.Length)
            {
                Array.Resize(ref _containers, _containersCount << 1);
            }
            _containers[_containersCount++] = script;
        }

        public void Update()
        {
            for(int i = 0, iMax = _containersCount; i < iMax; i++)
                _containers[i].CustomUpdate();               
        }

        public void FixedUpdate()
        {
            for (int i = 0, iMax = _containersCount; i < iMax; i++)
                _containers[i].CustomFixedUpdate();
        }

        public void LateUpdate()
        {
            for (int i = 0, iMax = _containersCount; i < iMax; i++)
                _containers[i].CustomLateUpdate();
        }

        public void Awake()
        {
            for (int i = 0, iMax = _containersCount; i < iMax; i++)
                _containers[i].CustomAwake();
        }

        public void Start()
        {
            for (int i = 0, iMax = _containersCount; i < iMax; i++)
                _containers[i].CustomStart();
        }
    }
}
