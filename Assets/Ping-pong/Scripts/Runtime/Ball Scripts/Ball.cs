using System.Collections;
using System.Collections.Generic;
using Runtime.Basics;
using Runtime.PlayerScripts;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Runtime.BallScripts
{
    [RequireComponent(typeof(Collider2D), typeof(AudioSource))]
    public class Ball : RoundRelatedBehaviour
    {
        public event UnityAction Bounced;
        
        [Header("Movement")] 
        [SerializeField] private float _initialSpeed = 5f;
        [SerializeField] private float _incrementPerHit = 0.5f;
        [SerializeField] private float _maxSpeed = 15f;
        
        [Header("Settings")] 
        [SerializeField] private float _delaySecondsBeforeShoot = 1f;
        [Tooltip("Minimal x coordinate on unit circle")]
        [SerializeField] private float _maxShootAngle = 45f;

        private AudioSource _audioSource;
        private Vector2 _movementDirection = Vector2.zero;
        private float _currentMovementSpeed;

        public readonly List<Player> HitPlayers = new();
        public Vector2 Velocity { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _currentMovementSpeed = _initialSpeed;
            StartCoroutine(ShootRandomAngleCoroutine());
        }

        private void FixedUpdate()
        {
            transform.Translate(_movementDirection * (_currentMovementSpeed * Time.fixedDeltaTime));
            UpdateVelocity();
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            _audioSource.Play();
            InvertDirection(collision.GetContact(0).normal);
            UpdateLastHitPlayer(collision.collider);
            IncrementSpeed();
        }

        private void InvertDirection(Vector2 collisionNormal)
        {
            _movementDirection = Vector2.Reflect(_movementDirection, collisionNormal);
            UpdateVelocity();
            Bounced?.Invoke();
        }
        
        private void UpdateLastHitPlayer(Collider2D hitCollider)
        {
            if (hitCollider.GetComponent<Player>() is not Player player) return;
            
            HitPlayers.Add(player);
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
            
            UpdateVelocity();

            Bounced?.Invoke();
            
            yield break;
            
            bool RandomBool() => Random.Range(0, 2) is 0;
        }

        private void UpdateVelocity()
        {
            Velocity = _movementDirection * _currentMovementSpeed;
        }
        
        public override void Reset()
        {
            base.Reset();
            
            _movementDirection = Vector2.zero;

            HitPlayers.Clear();

            Start();
        }
    }
}