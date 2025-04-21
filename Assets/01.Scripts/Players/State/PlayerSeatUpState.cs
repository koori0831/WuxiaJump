using Scripts.Entities;
using Scripts.FSM;
using UnityEngine;

namespace Scripts.Players.State
{
    public class PlayerSeatUpState : EntityState
    {
        private Player _player;
        private EntityRenderer _renderer;

        public PlayerSeatUpState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
            _renderer = entity.GetCompo<EntityRenderer>();
        }

        public override void Update()
        {
            base.Update();
            if (_isEndTriggerCall)
            {
                _player.ChangeState("Idle");
            }
        }

        public override void Exit()
        {
            base.Exit();
            _renderer.faceDirection = -1;
        }
    }
}