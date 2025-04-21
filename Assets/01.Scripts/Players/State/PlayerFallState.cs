using Scripts.Entities;
using Scripts.FSM;
using System;
using UnityEngine;

namespace Scripts.Players.State
{
    public class PlayerFallState : EntityState
    {
        private Player _player;
        private EntityMover _mover;
        public PlayerFallState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
            _mover = _player.GetCompo<EntityMover>();
        }

        public override void Update()
        {
            base.Update();
            if (_mover.IsGround())
            {
                _player.ChangeState("Idle");
            }
        }
    }
}