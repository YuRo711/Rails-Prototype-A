using System;

namespace Utils
{
    /// <summary>
    /// Data structure of a pair that can be edited in Unity editor
    /// </summary>
    [Serializable]
    public class SerializablePair<T1, T2>
    {
        public T1 left;
        public T2 right;

        public SerializablePair(T1 _left, T2 _right)
        {
            left = _left;
            right = _right;
        }
    }
}