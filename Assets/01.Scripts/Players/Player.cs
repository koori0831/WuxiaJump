using Scripts.Entities;
using Scripts.FSM;
using UnityEngine;

namespace Scripts.Players
{
    public class Player : Entity
    {
        [field: SerializeField] public PlayerInputSO PlayerInputSo { get; private set; }
        [SerializeField] private StateSO[] states;
        [SerializeField] private int MaxAttackCount = 3;

        private readonly int _attackCountHash = Animator.StringToHash("AttackCount");
        private EntityStateMachine _stateMachine;
        private EntityAnimator _animator;
        private float attackCount = 0;

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, states);
            _animator = GetCompo<EntityAnimator>();
        }

        private void Start()
        {
            _stateMachine.ChangeState("SeatIdle");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        public void Attack()
        {
            if (attackCount < MaxAttackCount)
            {
                //블랜드 트리이므로 0~1까지의 값을 넣어야 한다.
                attackCount += 1;
                _animator.SetAnim(_attackCountHash, attackCount / MaxAttackCount);
            }
            else
            {
                attackCount = 0;
                _animator.SetAnim(_attackCountHash, attackCount);
            }
        }
        public void ChangeState(string newStateName) => _stateMachine.ChangeState(newStateName);
    }
}