using Chars.Tools;

namespace SuperMarketGame
{
    public class CheckState : State
    {
        private LambdaTimer lambdaTimer = new LambdaTimer(2);
        private Client client;

        public CheckState(FiniteStateMachine finiteStateMachine, Client client) : base(finiteStateMachine)
        {
            this.client = client;
        }

        public override void Enter()
        {
            if (client.current)
                client.MoveToCheck();
        }

        public override void Update()
        {
            lambdaTimer.Delay(() => {
                finiteStateMachine.SetCurrentState(
                    finiteStateMachine.GetState(
                        (int)Client.States.PAYING)
                    );
            });
        }

        public override void Exit()
        {
        }
    }
}
