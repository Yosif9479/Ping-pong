using Runtime.BallScripts;
using Runtime.PlayerScripts;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.GoalScripts
{
    [RequireComponent(typeof(Collider2D))]
    public class GoalZone : MonoBehaviour
    {
        public static event UnityAction<Player> GoalReached;
        
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            HandleGoal(other);
        }

        private static void HandleGoal(Collider2D enteredCollider)
        {
            if (enteredCollider.GetComponent<Ball>() is Ball ball)
            {
                GoalReached?.Invoke(ball.LastHitPlayer);
            }
        }
    }
}