﻿using System;

namespace CodeBase.Tools
{
    public enum ApplicationStates
    {
        Loading = 0,
        Gameplay = 1,
    }

    public enum GameplayStates
    {
        
    }

    public interface IState<T>
    {
        T State { get; }
        IStateMachine<T> stateMachine { get; set; }
        void Prepare();
        void Enter();
        void Exit();

        void InitCompletationStateCallback(Action completationCallback);
    }

    public abstract class BaseState<T> : IState<T>
    {
        private Action _stateCompletationCallback;

        public abstract T State { get; }

        public IStateMachine<T> stateMachine { get; set; }

        public abstract void Enter();

        public abstract void Exit();

        public void InitCompletationStateCallback(Action completationCallback) =>
            _stateCompletationCallback = completationCallback;

        public virtual void Prepare()
        { }

        public virtual void Update()
        { }

        protected virtual void CompleteState() =>
            _stateCompletationCallback?.Invoke();
    }
}