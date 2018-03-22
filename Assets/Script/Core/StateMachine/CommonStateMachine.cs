using System.Collections.Generic;
using Core.Utils;

namespace Core.StateMachine
{
    public class CommonStateMachine<Target, Trigger, UpdateInfo> : IStateMachine<AState<Target, UpdateInfo>, Trigger, UpdateInfo>
    {
        private IDictionary<Tuple<AState<Target, UpdateInfo>, Trigger>, AState<Target, UpdateInfo>> m_transitionVariants;
        private AState<Target, UpdateInfo> m_currentState;
        private Target m_target;

        public CommonStateMachine(Target target, AState<Target, UpdateInfo> initialState)
        {
            m_target = target;
            m_currentState = initialState;
            m_transitionVariants = new Dictionary<Tuple<AState<Target, UpdateInfo>, Trigger>, AState<Target, UpdateInfo>>();
        }

        public bool AddTransition(AState<Target, UpdateInfo> srcState, Trigger trigger, AState<Target, UpdateInfo> dstState)
        {
            var transitionCondition = new Tuple<AState<Target, UpdateInfo>, Trigger>(srcState, trigger);
            AState<Target, UpdateInfo> state;
            bool isExists = m_transitionVariants.TryGetValue(transitionCondition, out state);
            if (!isExists) 
            {
                m_transitionVariants.Add(transitionCondition, dstState);
                return true;
            }

            return false;
        }

        public void ApplyTrigger(Trigger trigger)
        {
            var transitionCondition = new Tuple<AState<Target, UpdateInfo>, Trigger>(m_currentState, trigger);
            AState<Target, UpdateInfo> newState;
            bool isExists = m_transitionVariants.TryGetValue(transitionCondition, out newState);
            if (isExists) 
            {
                m_currentState.OnExit(m_target);
                m_currentState = newState;
                m_currentState.OnEnter(m_target);
            }
        }

        public void UpdateCurrentState(UpdateInfo info)
        {
            m_currentState.Update(m_target, info);
        }
    }
}
