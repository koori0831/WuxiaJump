using Scripts.Entities;
using Scripts.FSM;
using System;
using UnityEngine;

namespace Scripts.Players.State
{
    public class PlayerIdleState : EntityState
    {
        private Player _player;
        private EntityMover _mover;
        private EntityRenderer _renderer;
        public PlayerIdleState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
            _mover = _player.GetCompo<EntityMover>();
            _renderer = _player.GetCompo<EntityRenderer>();
        }

        public override void Enter()
        {
            base.Enter();
            _renderer.faceDirection = (int)_player.transform.localScale.x * -1;
            _mover.StopImmediately();
            _mover.GravityScale = 0f;
            _player.PlayerInputSo.OnTouch += HandleTapEvent;
            _player.PlayerInputSo.OnSwipePerformed += HandleSwipeEvent;
        }

        private void HandleSwipeEvent(Vector2 direction)
        {
            if (direction.y > 0 && Math.Sign(direction.x) == _renderer.faceDirection)
                _player.ChangeState("Attack");
        }

        public override void Update()
        {
            base.Update();
            if (!_mover.IsGround())
            {
                _player.ChangeState("Fall");
            }
        }

        public override void Exit()
        {
            base.Exit();
            _player.PlayerInputSo.OnTouch -= HandleTapEvent;
            _player.PlayerInputSo.OnSwipePerformed -= HandleSwipeEvent;
        }

        private void HandleTapEvent()
        {
            if (_mover.IsGround())
            {
                _player.ChangeState("Jump");
            }
        }
    }
}