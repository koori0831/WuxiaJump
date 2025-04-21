using Scripts.Entities;
using Scripts.FSM;

namespace Scripts.Players.State
{
    public class PlayerDeathState : EntityState
    {
        public PlayerDeathState(Entity entity, int animationHash) : base(entity, animationHash)
        {

        }
    }
}
