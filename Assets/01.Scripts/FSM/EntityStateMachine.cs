using Scripts.Entities;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Scripts.FSM
{
    public class EntityStateMachine
    {
        public EntityState CurrentState { get; set; }
        private Dictionary<string, EntityState> _states;

        public EntityStateMachine(Entity entity, StateSO[] stateList)
        {
            _states = new Dictionary<string, EntityState>();
            foreach (StateSO state in stateList)
            {
                Type type = Type.GetType(state.className);
                Debug.Assert(type != null, $"Finding type is null : {state.className}");
                EntityState entityState = Activator.CreateInstance(type, entity, state.animationHash) as EntityState;
                _states.Add(state.stateName, entityState);
            }
        }

        public void ChangeState(string stateName, bool forced = false)
        {
            EntityState newState = _states.GetValueOrDefault(stateName);
            Debug.Assert(newState != null, $"State {stateName} not found");

            if (forced == false && CurrentState == newState)
                return;

            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }

        public void UpdateStateMachine()
        {
            CurrentState?.Update();
        }
    }
}