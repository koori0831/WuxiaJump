using Scripts.Entities;
using Scripts.FSM;
using System;
using UnityEngine;

namespace Scripts.Players.State
{
    public class PlayerSeatState : EntityState
    {
        private Player _player;
        public PlayerSeatState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
        }
        public override void Enter()
        {
            base.Enter();
            _player.PlayerInputSo.OnTouch += HandleTapEvent;
        }

        private void HandleTapEvent()
        {
            _isEndTriggerCall = true;
        }
        public override void Update()
        {
            base.Update();
            if (_isEndTriggerCall)
            {
                _player.ChangeState("SeatUp");
            }
        }
        public override void Exit()
        {
            base.Exit();
            _player.PlayerInputSo.OnTouch -= HandleTapEvent;
        }
    }
}