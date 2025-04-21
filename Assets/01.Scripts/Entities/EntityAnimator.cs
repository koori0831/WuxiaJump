using UnityEngine;

namespace Scripts.Entities
{
    public class EntityAnimator : MonoBehaviour, IEntityCompo
    {
        private Animator _animator;
        private Entity _entity;
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _animator = GetComponent<Animator>();
        }
        
        public void SetAnim(int hash, bool value) => _animator.SetBool(hash, value);
        public void SetAnim(int hash, float value) => _animator.SetFloat(hash, value);
        public void SetAnim(int hash, int value) => _animator.SetInteger(hash, value);
        public void SetAnim(int hash) => _animator.SetTrigger(hash);
    }
}