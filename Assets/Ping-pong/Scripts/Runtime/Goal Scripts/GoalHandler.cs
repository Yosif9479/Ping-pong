using Runtime.PlayerScripts;
using UnityEngine;
using UnityEngine.Events;

namespace Runtime.GoalScripts
{
    [RequireComponent(typeof(AudioSource))]
    public class GoalHandler : MonoBehaviour
    {
        public static event UnityAction Handled;

        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        private void OnEnable()
        {
            GoalZone.GoalReached += OnGoalReached;
        }

        private void OnDisable()
        {
            GoalZone.GoalReached -= OnGoalReached;
        }

        private void OnGoalReached(Player shooter)
        {
            shooter?.Score.Increment();
            _audioSource.Play();
            Handled?.Invoke();
        }
    }
}