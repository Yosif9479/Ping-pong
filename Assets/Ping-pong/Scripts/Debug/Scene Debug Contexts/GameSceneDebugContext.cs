using Runtime.Basics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Debug.SceneContexts
{
    public class GameSceneDebugContext : MonoBehaviour
    {
        #region INPUT_ACTIONS

        private InputAction _resetButton;
        
        #endregion

        private RoundRelatedBehaviour[] _roundRelatedObjects;
        
        private void Awake()
        {
            _resetButton = InputSystem.actions.FindAction("Reset");
            
            _roundRelatedObjects = FindObjectsByType<RoundRelatedBehaviour>(FindObjectsSortMode.None);
        }

        private void OnEnable()
        {
            _resetButton.started += ResetScene;
        }

        private void OnDisable()
        {
            _resetButton.started -= ResetScene;
        }

        private void ResetScene(InputAction.CallbackContext callbackContext)
        {
            foreach (RoundRelatedBehaviour obj in _roundRelatedObjects)
            {
                obj.Reset();
            }
        }
    }
}