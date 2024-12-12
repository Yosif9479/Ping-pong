using Enums;
using Runtime.BallScripts;
using Runtime.Basics;
using Runtime.GoalScripts;
using Runtime.PlayerScripts;
using UnityEngine;

namespace Runtime.SceneContexts
{
    public class GameSceneContext : SceneContextBase<GameSceneContext>
    {
        private RoundRelatedBehaviour[] _roundRelatedObjects;
        private Player[] _players;
        private Ball _ball;
        
        public bool PlayBot { get; set; }
        public BotDifficulty BotDifficulty { get; set; } = BotDifficulty.Impossible;

        protected override void Awake()
        {
            _roundRelatedObjects = FindObjectsByType<RoundRelatedBehaviour>(FindObjectsSortMode.InstanceID);
            _players = FindObjectsByType<Player>(FindObjectsSortMode.InstanceID);
            _ball = FindAnyObjectByType<Ball>();
        }

        private void OnEnable()
        {
            GoalHandler.Handled += OnRoundFinished;
        }

        private void OnDisable()
        {
            GoalHandler.Handled -= OnRoundFinished;
        }
        
        private void OnRoundFinished()
        {
            ResetRoundRelatedObjects();
        }

        private void ResetRoundRelatedObjects()
        {
            foreach (RoundRelatedBehaviour obj in _roundRelatedObjects)
            {
                obj.Reset();
            }
        }
    }
}