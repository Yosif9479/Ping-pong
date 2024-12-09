using Runtime.PlayerScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Modules
{
    [RequireComponent(typeof(Text))]
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Player _player;
        
        private Text _scoreText;
        private Score _score;

        private void Awake()
        {
            _scoreText = GetComponent<Text>();
            _score = _player.Score;
            UpdateText();
        }

        private void OnEnable()
        {
            _score.ValueChanged += OnScoreChanged;
        }

        private void OnDisable()
        {
            _score.ValueChanged -= OnScoreChanged;
        }

        private void OnScoreChanged()
        {
            UpdateText();
        }

        private void UpdateText()
        {
            _scoreText.text = _score.Value.ToString();
        }
    }
}