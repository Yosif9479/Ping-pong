using UnityEngine;

namespace Runtime.Basics
{
    public abstract class RoundRelatedBehaviour : MonoBehaviour
    {
        public Vector3 StartPosition { get; protected set; }
        public Quaternion StartRotation { get; protected set; }

        protected virtual void Awake()
        {
            StartPosition = transform.position;
            StartRotation = transform.rotation;
        }

        public virtual void Reset()
        {
            transform.position = StartPosition;
            transform.rotation = StartRotation;
        }
    }
}