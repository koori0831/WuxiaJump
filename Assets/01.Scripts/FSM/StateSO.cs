using System;
using UnityEngine;

namespace Scripts.FSM
{
    [CreateAssetMenu(fileName = "StateSO", menuName = "SO/FSM/StateData", order = 0)]
    public class StateSO : ScriptableObject
    {
        public string stateName;
        public string className;
        public string animParamName;

        public int animationHash;

        private void OnValidate()
        {
            animationHash = Animator.StringToHash(animParamName);
        }
    }
}