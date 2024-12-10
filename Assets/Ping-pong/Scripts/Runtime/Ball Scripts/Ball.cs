using System.Collections;
using Runtime.Basics;
using Runtime.PlayerScripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Runtime.BallScripts
{
    [RequireComponent(typeof(Collider2D))]
    public class Ball : RoundRelatedBehaviour
    {
        [Header("Movement")] 
        [SerializeField] private float _initialSpeed = 5f;
        [SerializeField] private float _incrementPerHit = 0.5f;
        [SerializeField] private float _maxSpeed = 15f;
        
        [Header("Settings")] 
        [SerializeField] private float _delaySecondsBeforeShoot = 1f;
        [Tooltip("Minimal x coordinate on unit circle")]
        [SerializeField] private float _maxShootAngle = 45f;

        private Vector2 _movementDirection = Vector2.zero;

        public Player LastHitPlayer { get; private set; }
        
        private float _currentMovementSpeed;

        private void Start()
        {
            _currentMovementSpeed = _initialSpeed;
            StartCoroutine(ShootRandomAngleCoroutine());
        }

        private void FixedUpdate()
        {
            transform.Translate(_movementDirection * (_currentMovementSpeed * Time.fixedDeltaTime));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            InvertDirection(collision.GetContact(0).normal);
            UpdateLastHitPlayer(collision.collider);
            IncrementSpeed();
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
        
        private void IncrementSpeed()
        {
            if (_currentMovementSpeed >= _maxSpeed) return;
            
            _currentMovementSpeed += _incrementPerHit;
        }

        private IEnumerator ShootRandomAngleCoroutine()
        {
            yield return new WaitForSeconds(_delaySecondsBeforeShoot);

            float x = Random.Range(Mathf.Cos(_maxShootAngle), 1f);

            if (RandomBool()) x = -x;

            float y = Mathf.Sin(Mathf.Acos(x));
            
            if (RandomBool()) y = -y;
            
            _movementDirection = new Vector2(x, y);

            yield break;
            
            bool RandomBool() => Random.Range(0, 2) is 0;
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