using System.Collections;
using Runtime.Basics;
using Runtime.PlayerScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Runtime.BallScripts
{
    [RequireComponent(typeof(Collider2D))]
    public class Ball : RoundRelatedBehaviour
    {
        [Header("Movement")] 
        [SerializeField] private float _movementSpeed = 5f;
        
        [Header("Settings")] 
        [SerializeField] private float _delaySecondsBeforeShoot = 1f;

        private Vector2 _movementDirection = Vector2.zero;

        public Player LastHitPlayer { get; private set; }

        private void Start()
        {
            StartCoroutine(ShootRandomAngleCoroutine());
        }

        private void FixedUpdate()
        {
            transform.Translate(_movementDirection * (_movementSpeed * Time.fixedDeltaTime));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            InvertDirection(collision.GetContact(0).normal);
            UpdateLastHitPlayer(collision.collider);
        }

        private void InvertDirection(Vector2 collisionNormal)
        {
            _movementDirection = Vector2.Reflect(_movementDirection, collisionNormal);
        }

        private void UpdateLastHitPlayer(Collider2D hitCollider)
        {
            if (hitCollider.GetComponent<Player>() is Player player)
            {
                LastHitPlayer = player;
            }
        }

        private IEnumerator ShootRandomAngleCoroutine()
        {
            yield return new WaitForSeconds(_delaySecondsBeforeShoot);
            
            float angle = Random.Range(0f, 360f);
            
            _movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }

        public override void Reset()
        {
            base.Reset();
            
            _movementDirection = Vector2.zero;
            
            LastHitPlayer = null;

            Start();
        }
    }
}