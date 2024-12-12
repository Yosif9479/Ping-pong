using Constants;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ui.Modules
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private bool _stopTimeOnPause = true;

        private bool _isPaused;
        
        #region INPUT_ACTIONS

        private InputAction _pauseAction;
        
        #endregion

        private void Awake()
        {
            _pauseAction = InputSystem.actions.FindAction(InputActionNames.Pause);

            Time.timeScale = 1;
        }

        private void OnEnable()
        {
            _pauseAction.started += OnPauseActionPerformed;
            _pauseButton.onClick.AddListener(TogglePause);
        }

        private void OnDisable()
        {
            _pauseAction.started -= OnPauseActionPerformed;
            _pauseButton.onClick.RemoveListener(TogglePause);
        }

        private void OnPauseActionPerformed(InputAction.CallbackContext context) => TogglePause();
        
        private void TogglePause()
        {
            _isPaused = !_isPaused;
            _panel.SetActive(_isPaused);
            
            if (_stopTimeOnPause is false) return;
            
            Time.timeScale = _isPaused ? 0 : 1;
        }
        
        public void Resume()
        {
            Time.timeScale = 1;
            _panel.SetActive(false);
        }
        
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        public void QuitGame()
        {
            SceneManager.LoadScene("Menu");
        }
    }
}