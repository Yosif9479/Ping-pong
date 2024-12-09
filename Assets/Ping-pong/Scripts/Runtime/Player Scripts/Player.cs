using Constants;
using Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [Header("Movement")] 
        [SerializeField] private float _movementSpeed = 5f;

        [Header("Settings")] 
        [SerializeField] private ControlsType _controlsType;
        
        #region INPUT_ACTIONS
        private InputAction _moveAction;
        #endregion

        private void Awake()
        {
            InitInputs();
        }

        private void Update()
        {
            ApplyMovement();
        }

        private void ApplyMovement()
        {
            var translation = new Vector2(0, _moveAction.ReadValue<Vector2>().y);
            
            transform.Translate(translation * (_movementSpeed * Time.deltaTime));
        }

        private void InitInputs()
        {
            if (_controlsType is ControlsType.Primary) InitPrimaryInputs();
            else InitSecondaryInputs();
            
            return;

            void InitPrimaryInputs()
            {
                _moveAction = InputSystem.actions.FindAction(InputActionNames.PrimaryMove);
            }

            void InitSecondaryInputs()
            {
                _moveAction = InputSystem.actions.FindAction(InputActionNames.SecondaryMove);
            }
        }
    }
}