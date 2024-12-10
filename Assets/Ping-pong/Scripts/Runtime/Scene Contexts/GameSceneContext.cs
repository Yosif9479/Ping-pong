using Runtime.BallScripts;
using Runtime.Basics;
using Runtime.GoalScripts;
using Runtime.PlayerScripts;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Runtime.SceneContexts
{
    public class GameSceneContext : MonoBehaviour
    {
        private RoundRelatedBehaviour[] _roundRelatedObjects;
        private Player[] _players;
        private Ball _ball;

        private void Awake()
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