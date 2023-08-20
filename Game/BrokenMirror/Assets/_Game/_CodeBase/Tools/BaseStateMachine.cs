using System.Collections.Generic;
using DG.Tweening;

namespace CodeBase.Tools
{
    public interface IStateMachine<T>
    {
        IState<T> Current { get; }
        void SwitchToState(T state);
        void SwitchToStateDelayed(T state, float delay);
        void InitiateStateMachine(params IState<T>[] states);
    }

    public class BaseStateMachine<T> : IStateMachine<T>
    {
        protected IStateMachine<T> _stateMachine;
        private readonly Dictionary<T, IState<T>> _states = new();
        
        public IState<T> Current { get; private set; }

        public void SwitchToStateDelayed(T state, float delay) =>
            DOTween.Sequence().AppendInterval(delay).OnComplete(() => { SwitchToState(state); });

        public void SwitchToState(T state)
        {
            IState<T> nextState = _states[state];
            Current?.Exit();
            Current = nextState;
            Current.Prepare();
            Current.Enter();
        }

        public void InitiateStateMachine(params IState<T>[] states)
        {
            Current?.Exit();
            _states.Clear();

            foreach (IState<T> state in states) {
                _states[state.State] = state;
                state.stateMachine = this;
            }

            _stateMachine = this;
        }
    }
}