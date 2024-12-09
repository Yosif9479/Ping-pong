using System;
using UnityEngine.Events;

namespace Runtime.PlayerScripts
{
    [Serializable]
    public class Score
    {
        public event UnityAction ValueChanged;
        
        public uint Value { get; private set; }

        public void Increment()
        {
            Value++;
            ValueChanged?.Invoke();
        }

        public void Reset()
        {
            Value = 0;
            ValueChanged?.Invoke();
        }
    }
}