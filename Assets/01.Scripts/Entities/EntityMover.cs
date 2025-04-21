using System;
using UnityEngine;

namespace Scripts.Entities
{
    public class EntityMover : MonoBehaviour, IEntityCompo
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float jumpForce;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Vector2 groundCheckBox;

        private Rigidbody2D _rigidbody;
        private EntityRenderer _renderer;

        public bool CanManualMovement { get; set; } = true;
        public float GravityScale
        {
            get => _rigidbody.gravityScale;
            set => _rigidbody.gravityScale = value;
        }
        private Vector2 _autoMovement;
        
        public bool IsGround()
        {
            if (groundCheck == null) return false;
            var hit = Physics2D.OverlapBox(groundCheck.position, groundCheckBox, 0f, groundLayer);
            return hit != null;
        }
        private Vector2 _velocity;
        public Vector2 Velocity => _velocity;
        
        public Vector2 _movementDirection;
        
        private Entity _entity;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _rigidbody = GetComponent<Rigidbody2D>();
            if (_rigidbody == null)
            {
                Debug.LogError("Rigidbody2D component is missing!");
            }
            _renderer = _entity.GetCompo<EntityRenderer>();
        }

        private void FixedUpdate()
        {
            CalculateMovement();
            //Move();
        }

        public void SetMovementDirection(Vector2 movementInput)
        {
            _movementDirection = new Vector2(movementInput.x, movementInput.y).normalized;
        }

        private void CalculateMovement()
        {
            if (CanManualMovement)
            {
                _velocity = _entity.transform.rotation * _movementDirection;
                _velocity *= moveSpeed * Time.fixedDeltaTime;
            }
            else
            {
                _velocity = _autoMovement * Time.fixedDeltaTime;
            }
        }

        private void Move()
        {
            var movement = CanManualMovement ? _velocity : _autoMovement;
            _rigidbody.linearVelocity = new Vector2(movement.x, _rigidbody.linearVelocity.y);
        }


        public void SetAutoMovement(Vector2 autoMovement) => _autoMovement = autoMovement;

        public void StopImmediately()
        {
            _movementDirection = Vector2.zero;
            _velocity = Vector2.zero;
            _rigidbody.linearVelocity = Vector2.zero;
        }

        //바라보고 있는 방향의 대각선 위로 점프 후 벽에 붙기.
        public void Jump(Vector2? direction = null)
        {
            Vector2 dir = direction ?? Vector2.one;
            Vector2 jumpDirection = new Vector2(_renderer.faceDirection, dir.y).normalized;
            _rigidbody.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
            Debug.Log(jumpDirection);
        }

        private void OnDrawGizmos()
        {
            if (groundCheck != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(groundCheck.position, groundCheckBox);
            }
        }
    }
}