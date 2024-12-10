using System.Collections.Generic;
using System.Linq;
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

        private void OnGoalReached(List<Player> hitPlayers, Player zoneOwner)
        {
            Player player = hitPlayers.LastOrDefault(x => x != zoneOwner);
            
            player?.Score.Increment();
            
            _audioSource.Play();
            Handled?.Invoke();
        }
    }
}