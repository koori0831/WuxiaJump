using Scripts.Entities;

namespace Scripts.FSM
{
    public abstract class EntityState
    {
        protected Entity _entity;
        protected int _animationHash;
        protected EntityAnimator _animator;
        protected EntityAnimationTrigger _animTrigger;

        protected bool _isEndTriggerCall;

        public EntityState(Entity entity, int animationHash)
        {
            _entity = entity;
            _animationHash = animationHash;
            _animator = _entity.GetCompo<EntityAnimator>();
            _animTrigger = _entity.GetCompo<EntityAnimationTrigger>();
        }

        public virtual void Enter()
        {
            _animTrigger.OnAnimationEndTrigger += AnimationEndTrigger;
            _animator.SetAnim(_animationHash, true);
            _isEndTriggerCall = false;
        }

        public virtual void Update()
        {
        }

        public virtual void Exit()
        {
            _animTrigger.OnAnimationEndTrigger -= AnimationEndTrigger;
            _animator.SetAnim(_animationHash, false);
        }

        private void AnimationEndTrigger() => _isEndTriggerCall = true;
    }
}