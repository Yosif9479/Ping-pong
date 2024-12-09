using Runtime.PlayerScripts;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.GoalScripts
{
    public class GoalHandler : MonoBehaviour
    {
        public static event UnityAction Handled;
        
        private void OnEnable()
        {
            GoalZone.GoalReached += OnGoalReached;
        }

        private void OnDisable()
        {
            GoalZone.GoalReached -= OnGoalReached;
        }

        private static void OnGoalReached(Player shooter)
        {
            shooter?.Score.Increment();
            Handled?.Invoke();
        }
    }
}