using Chars.Tools;

namespace SuperMarketGame
{
    public class WaitState : State
    {
        private Client client;
        public WaitState(FiniteStateMachine finiteStateMachine, Client client) : base(finiteStateMachine)
        {
            this.client = client;
        }

        public override void Enter()
        {

        }

        public override void Update()
        {
            if (client.current)
            {
                finiteStateMachine.SetCurrentState(
                finiteStateMachine.GetState(
                (int)Client.States.CHECKING)
                );
            }

        }

        public override void Exit()
        {
        }
    }
}
