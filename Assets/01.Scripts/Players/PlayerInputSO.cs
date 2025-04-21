using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

namespace Scripts.Players
{
    [CreateAssetMenu(fileName = "PlayerInput", menuName = "SO/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject, IPlayerActions
    {
        public event Action OnTouch;
        public event Action OnTouchPressStart;
        public event Action OnTouchPressEnd;

        public delegate void SwipeEvent(Vector2 direction);
        public event SwipeEvent OnSwipePerformed;

        public Vector2 TouchPos { get; private set; }
        public Vector2 SwipeDirection { get; private set; }

        private Controls _controls;
        private Vector2 _initialTouchPos;

        [SerializeField] private float swipeResistance = 100f;

        private void OnEnable()
        {
            if (_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Player.Enable();
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }

        public void OnPrimaryTouchPosition(InputAction.CallbackContext context)
        {
            TouchPos = context.ReadValue<Vector2>();
        }

        public void OnPrimaryTouchPress(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnTouchPressStart?.Invoke();
                _initialTouchPos = TouchPos;
            }
            else if (context.canceled)
            {
                OnTouchPressEnd?.Invoke();
                DetectSwipe();
            }
        }

        public void OnTap(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnTouch?.Invoke();
            }
        }

        public Vector2 GetWorldPosition()
        {
            return Camera.main.ScreenToWorldPoint(TouchPos);
        }

        private void DetectSwipe()
        {
            Vector2 delta = TouchPos - _initialTouchPos;
            Vector2 direction = Vector2.zero;

            if (Mathf.Abs(delta.x) > swipeResistance)
            {
                direction.x = Mathf.Sign(delta.x);
            }

            if (Mathf.Abs(delta.y) > swipeResistance)
            {
                direction.y = Mathf.Sign(delta.y);
            }

            if (direction != Vector2.zero && OnSwipePerformed != null)
            {
                SwipeDirection = delta.normalized;
                OnSwipePerformed(direction);
            }
        }
    }
}