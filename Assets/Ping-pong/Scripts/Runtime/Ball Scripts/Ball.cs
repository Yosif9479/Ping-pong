using System.Collections;
using UnityEngine;

namespace Runtime.BallScripts
{
    [RequireComponent(typeof(Collider2D))]
    public class Ball : MonoBehaviour
    {
        [Header("Movement")] 
        [SerializeField] private float _movementSpeed = 5f;
        
        [Header("Settings")] 
        [SerializeField] private float _delaySecondsBeforeShoot = 1f;

        private Vector2 _movementDirection = Vector2.zero;

        private void Start()
        {
            StartCoroutine(ShootRandomAngleCoroutine());
        }

        private void Update()
        {
            transform.Translate(_movementDirection * (_movementSpeed * Time.deltaTime));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            InvertDirection(collision.GetContact(0).normal);
        }

        private void InvertDirection(Vector2 collisionNormal)
        {
            _movementDirection = Vector2.Reflect(_movementDirection, collisionNormal);
        }

        private IEnumerator ShootRandomAngleCoroutine()
        {
            yield return new WaitForSeconds(_delaySecondsBeforeShoot);
            
            float angle = Random.Range(0f, 360f);
            
            _movementDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }
    }
}