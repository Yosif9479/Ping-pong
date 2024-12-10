using Constants;
using Enums;
using Runtime.Basics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.PlayerScripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Player : RoundRelatedBehaviour
    {
        [Header("Movement")] 
        [SerializeField] private float _movementSpeed = 5f;

        [Header("Settings")] 
        [SerializeField] private ControlsType _controlsType;

        public Score Score { get; private set; } = new();

        private Camera _camera;
        private SpriteRenderer _spriteRenderer;
        private float _maxY;
        private float _minY;
        
        #region INPUT_ACTIONS
        private InputAction _moveAction;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            
            _camera = Camera.main;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            
            InitInputs();
            InitMaxYPosition();
        }

        private void Update()
        {
            ApplyMovement();
        }

        private void ApplyMovement()
        {
            var translation = new Vector2(0, _moveAction.ReadValue<Vector2>().y);
            
            transform.Translate(translation * (_movementSpeed * Time.deltaTime));

            transform.position = new Vector2
            {
                x = transform.position.x,
                y = Mathf.Clamp(transform.position.y, _minY, _maxY),
            };
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

        private void InitMaxYPosition()
        {
            _maxY = _camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - _spriteRenderer.bounds.extents.y;
            _minY = _camera.ScreenToWorldPoint(new Vector2(0, 0)).y + _spriteRenderer.bounds.extents.y;
        }
    }
}