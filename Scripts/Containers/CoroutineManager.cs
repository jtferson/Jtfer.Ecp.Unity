using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jtfer.Ecp.Unity
{
    public class CoroutineManager : UnityScript
    {
        Queue<Func<IEnumerator>> _coroutines = new Queue<Func<IEnumerator>>();

        public Coroutine RunCoroutine(Func<IEnumerator> action)
        {
            var a = action;
            return StartCoroutine(a());
        }

        public Coroutine RunCoroutine(Action action)
        {
            var a = Func(action);
            return StartCoroutine(Func(action));
        }

        public void AbortCoroutine(Coroutine coroutine)
        {
            StopCoroutine(coroutine);
        }

        private IEnumerator Func(Action action)
        {
            action();
            yield return null;
        }

        private IEnumerator Func(Func<IEnumerator> action)
        {
            yield return action();
            yield return null;
        }
    }
}
