using UnityEngine;

namespace Runtime.SceneContexts
{
    [DefaultExecutionOrder(-1)]
    public abstract class SceneContextBase<T> : MonoBehaviour where T : SceneContextBase<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance == null || Instance.Equals(this)) Instance = this as T;
        }
    }
}