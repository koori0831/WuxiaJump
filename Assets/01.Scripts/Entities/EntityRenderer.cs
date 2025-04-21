using UnityEngine;

namespace Scripts.Entities
{
    public class EntityRenderer : MonoBehaviour, IEntityCompo
    {
        private Entity _entity;
        private EntityMover _entityMover;
        public int faceDirection
        {
            get
            {
                return (int)_entity.transform.localScale.x;
            }
            set
            {
                _entity.transform.localScale = new Vector3(value, 1, 1);
            }
        }
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _entityMover = _entity.GetCompo<EntityMover>();
        }
    }
}