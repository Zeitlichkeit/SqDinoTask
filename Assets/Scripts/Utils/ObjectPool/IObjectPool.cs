using System;
namespace Utils.ObjectPool
{
    public interface IObjectPool<T> where T : class
    {
        void Release(T element);
        void Clear();
        int CountInactive { get; }
        T Get();
    }
}