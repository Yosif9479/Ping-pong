using System.Collections.Generic;
using Runtime.PlayerScripts;
using Runtime.BallScripts;
using UnityEngine.Events;
using UnityEngine;

namespace Runtime.GoalScripts
{
    [RequireComponent(typeof(Collider2D))]
    public class GoalZone : MonoBehaviour
    {
        public static event UnityAction<List<Player>, Player> GoalReached;

        [SerializeField] private Player _player;
        
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

        private void HandleGoal(Collider2D enteredCollider)
        {
            if (enteredCollider.GetComponent<Ball>() is Ball ball)
            {
                GoalReached?.Invoke(ball.HitPlayers, _player);
            }
        }
    }
}