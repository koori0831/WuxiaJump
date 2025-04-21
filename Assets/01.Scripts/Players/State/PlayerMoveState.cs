using Scripts.Entities;
using Scripts.FSM;

namespace Scripts.Players.State
{
    public class PlayerMoveState : EntityState
    {
        public PlayerMoveState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }
    }
}