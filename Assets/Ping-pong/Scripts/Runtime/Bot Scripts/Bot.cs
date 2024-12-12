using Runtime.BallScripts;
using Runtime.PlayerScripts;
using UnityEngine;

namespace Runtime.BotScripts
{
    public class Bot : Player
    {
        private Vector2 _floor;
        private Vector2 _ceiling;
        
        private Vector2 _targetPosition;
        
        private Ball _ball;
        
        protected override void Awake()
        {
            base.Awake();
            
            _ball = FindAnyObjectByType<Ball>();
            
            _floor = MainCamera.ScreenToWorldPoint(new Vector2(0, 0));
            _ceiling = MainCamera.ScreenToWorldPoint(new Vector2(0, Screen.height));
        }

        private void OnEnable()
        {
            _ball.Bounced += OnBallBounced;
        }

        private void OnDisable()
        {
            _ball.Bounced -= OnBallBounced;
        }
        
        protected override Vector2 GetTargetTranslation()
        {
            Vector2 targetPosition = _targetPosition;

            float y = targetPosition.y;
            
            if (Mathf.Abs(y - transform.position.y) < 0.01f) y = 0;
            else y = y > transform.position.y ? 1 : -1;

            
            return new Vector2(0, y);
        }

        private Vector2 GetTargetPosition()
        {
            Vector2 ballPosition = _ball.transform.position;
            Vector2 ballVelocity = _ball.Velocity;

            float timeToReachBot = Mathf.Abs(ballPosition.x - transform.position.x) / Mathf.Abs(ballVelocity.x);

            float yDisplacement = ballPosition.y + ballVelocity.y * timeToReachBot;

            float fieldHeight = Mathf.Abs(_ceiling.y - _floor.y);

            float relativeY = yDisplacement - _floor.y;
            int bounces = Mathf.FloorToInt(relativeY / fieldHeight);

            float targetY = relativeY % fieldHeight;

            if (targetY < 0)
            {
                targetY += fieldHeight;
            }

            if (bounces % 2 != 0)
            {
                targetY = fieldHeight - targetY;
            }

            targetY += _floor.y;

            return new Vector2(transform.position.x, targetY);
        }
        
        private void OnBallBounced()
        {
            _targetPosition = GetTargetPosition();
        }
    }
}