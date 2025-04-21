using Scripts.Entities;
using Scripts.FSM;
using UnityEngine;

namespace Scripts.Players.State
{
    public class PlayerJumpState : EntityState
    {
        private Player _player;
        private EntityMover _mover;
        public PlayerJumpState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
            _mover = _player.GetCompo<EntityMover>();
        }
        public override void Enter()
        {
            base.Enter();
            _mover.SetMovementDirection(Vector2.zero);
            _mover.GravityScale = 1f;
            _mover.Jump();
        }

        public override void Update()
        {
            base.Update();
            if (_isEndTriggerCall)
            {
                _player.ChangeState("Fall");
            }
        }
    }
}