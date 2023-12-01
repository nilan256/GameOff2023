using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Game
{

    public class CoroutineHandler : CustomYieldInstruction
    {

        private readonly MonoBehaviour owner;
        private readonly Func<IEnumerator> getEnumerator;
        private IEnumerator enumerator;
        private Coroutine coroutine;

        public bool IsRunning => coroutine != null;
        public bool IsDone { get; private set; }

        public CoroutineHandler(MonoBehaviour owner, IEnumerator enumerator)
        {
            this.owner = owner;
            this.enumerator = enumerator;
        }

        public CoroutineHandler(MonoBehaviour owner, IEnumerable enumerable) : this(owner, enumerable.GetEnumerator) { }

        public CoroutineHandler(MonoBehaviour owner, Func<IEnumerator> getEnumerator)
        {
            this.owner = owner;
            this.getEnumerator = getEnumerator;
        }

        public void Start()
        {
            if (!owner) throw new InvalidOperationException("owner is null or destroyed");
            if (IsRunning) throw new InvalidOperationException("already running");
            if (IsDone) throw new InvalidOperationException("already done");
            if (enumerator == null)
            {
                enumerator = getEnumerator();
            }
            coroutine = owner.StartCoroutine(Wrap());
        }

        public void Stop()
        {
            if (!IsRunning) throw new InvalidOperationException("not running");
            owner.StopCoroutine(coroutine);
            coroutine = null;
            enumerator = null;
        }

        private IEnumerator Wrap()
        {
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }

            IsDone = true;
            coroutine = null;
        }

        private static CoroutineHandler empty;

        public static CoroutineHandler Empty
        {
            get
            {
                if (empty == null)
                {
                    empty = new CoroutineHandler(null, Enumerable.Empty<object>());
                    empty.IsDone = true;
                }

                return empty;
            }
        }

        public static CoroutineHandler Start(MonoBehaviour owner, Func<IEnumerator> func)
        {
            var handler = new CoroutineHandler(owner, func);
            handler.Start();
            return handler;
        }

        public static CoroutineHandler Start(MonoBehaviour owner, IEnumerator enumerator)
        {
            var handler = new CoroutineHandler(owner, enumerator);
            handler.Start();
            return handler;
        }

        public static CoroutineHandler Start(MonoBehaviour owner, IEnumerable enumerable)
        {
            var handler = new CoroutineHandler(owner, enumerable);
            handler.Start();
            return handler;
        }

        public override bool keepWaiting => !IsDone;

    }

}