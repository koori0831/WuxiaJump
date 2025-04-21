using Scripts.Entities;
using Scripts.FSM;
using UnityEngine;

namespace Scripts.Players.State
{
    public class PlayerAttackState : EntityState
    {
        private Player _player;
        private EntityMover _mover;
        public PlayerAttackState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
            _mover = _player.GetCompo<EntityMover>();
        }
        public override void Enter()
        {
            base.Enter();
            _mover.SetMovementDirection(Vector2.zero);
            _mover.Jump(_player.PlayerInputSo.SwipeDirection);
            Debug.Log(_player.PlayerInputSo.SwipeDirection);
            _player.PlayerInputSo.OnSwipePerformed += HandleSwipeEvent;
        }

        private void HandleSwipeEvent(Vector2 direction)
        {
            _player.ChangeState("Attack");
        }

        public override void Update()
        {
            base.Update();
            if (_isEndTriggerCall)
            {
                _player.ChangeState("Fall");
            }
        }

        public override void Exit()
        {
            base.Exit();
            _player.Attack();
            _player.PlayerInputSo.OnSwipePerformed -= HandleSwipeEvent;
        }
    }
}
