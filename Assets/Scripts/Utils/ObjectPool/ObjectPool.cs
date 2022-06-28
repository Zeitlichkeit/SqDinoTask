using System;
using System.Collections.Generic;

namespace Utils.ObjectPool
{
    public class ObjectPool<T> : IDisposable, IObjectPool<T> where T : class
    {
        readonly Func<T> objectCreateFunction;
        readonly Action<T> getAction;
        readonly Action<T> releaseAction;
        readonly Action<T> destroyAction;
        readonly int maximumPooledObjects;
        
        private bool checkIfReleased;
        private Stack<T> objectStack;

        public int CountAll { get; private set; }

        public int CountActive { get { return CountAll - CountInactive; } }

        public int CountInactive { get { return objectStack.Count; } }

        public ObjectPool(Func<T> onCreate, Action<T> onGet = null, Action<T> onRelease = null, Action<T> onDestroy = null, bool checkIfReleased = true, int defaultCapacity = 10, int maxSize = 10000)
        {
            objectStack = new Stack<T>(defaultCapacity);
            objectCreateFunction = onCreate;
            maximumPooledObjects = maxSize;
            getAction = onGet;
            releaseAction = onRelease;
            destroyAction = onDestroy;
            this.checkIfReleased = checkIfReleased;
        }


        public T Get()
        {
            T element;
            if (objectStack.Count == 0)
            {
                element = objectCreateFunction();
                CountAll++;
            }
            else
            {
                element = objectStack.Pop();
            }
            getAction?.Invoke(element);
            return element;
        }

        public void Release(T element)
        {
            if (checkIfReleased && objectStack.Count > 0)
            {
                if (objectStack.Contains(element))
                    throw new InvalidOperationException("Object already released.");
            }

            releaseAction?.Invoke(element);

            if (CountInactive < maximumPooledObjects)
            {
                objectStack.Push(element);
            }
            else
            {
                destroyAction?.Invoke(element);
            }
        }

        public void Clear()
        {
            if (destroyAction != null)
            {
                foreach (var item in objectStack)
                {
                    destroyAction(item);
                }
            }

            objectStack.Clear();
            CountAll = 0;
        }

        public void Dispose()
        {
            Clear();
        }
    }
}