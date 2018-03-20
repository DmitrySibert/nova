using System.Collections.Generic;
using Core.Utils;

namespace Core.StateMachine
{
    public class CommonStateMachine<Trigger, UpdateInfo> : IStateMachine<IState<UpdateInfo>, Trigger, UpdateInfo>
    {
        private IDictionary<Tuple<IState<UpdateInfo>, Trigger>, IState<UpdateInfo>> transitionVariants;
        private IState<UpdateInfo> currentState;

        public CommonStateMachine(IState<UpdateInfo> initialState)
        {
            currentState = initialState;
            transitionVariants = new Dictionary<Tuple<IState<UpdateInfo>, Trigger>, IState<UpdateInfo>>();
        }

        public bool AddTransition(IState<UpdateInfo> srcState, Trigger trigger, IState<UpdateInfo> dstState)
        {
            var transitionCondition = new Tuple<IState<UpdateInfo>, Trigger>(srcState, trigger);
            IState<UpdateInfo> state;
            bool isExists = transitionVariants.TryGetValue(transitionCondition, out state);
            if (!isExists) 
            {
                transitionVariants.Add(transitionCondition, dstState);
                return true;
            }

            return false;
        }

        public void ApplyTrigger(Trigger trigger)
        {
            var transitionCondition = new Tuple<IState<UpdateInfo>, Trigger>(currentState, trigger);
            IState<UpdateInfo> newState;
            bool isExists = transitionVariants.TryGetValue(transitionCondition, out newState);
            if (isExists) 
            {
                currentState.OnExit();
                currentState = newState;
                currentState.OnEnter();
            }
        }

        public void UpdateCurrentState(UpdateInfo info)
        {
            currentState.Update(info);
        }
    }
}
