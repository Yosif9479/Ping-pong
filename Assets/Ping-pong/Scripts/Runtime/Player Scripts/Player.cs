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

        public Score Score { get; protected set; } = new();

        protected Camera MainCamera;
        private SpriteRenderer _spriteRenderer;
        private float _maxY;
        private float _minY;
        
        #region INPUT_ACTIONS
        private InputAction _moveAction;
        #endregion

        protected override void Awake()
        {
            base.Awake();
            
            MainCamera = Camera.main;
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
            var translation = new Vector2(0, GetTargetTranslation().y);
            
            transform.Translate(translation * (_movementSpeed * Time.deltaTime));

            transform.position = new Vector2
            {
                x = transform.position.x,
                y = Mathf.Clamp(transform.position.y, _minY, _maxY),
            };
        }

        protected virtual Vector2 GetTargetTranslation()
        {
            return _moveAction.ReadValue<Vector2>();
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
            _maxY = MainCamera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y - _spriteRenderer.bounds.extents.y;
            _minY = MainCamera.ScreenToWorldPoint(new Vector2(0, 0)).y + _spriteRenderer.bounds.extents.y;
        }
    }
}