namespace Chars.Tools
{
    public abstract class State
    {
        protected FiniteStateMachine finiteStateMachine;
        public State(FiniteStateMachine finiteStateMachine) => this.finiteStateMachine = finiteStateMachine;
        public abstract void Enter();
        public abstract void Exit();
        public abstract void Update();
    }
}

