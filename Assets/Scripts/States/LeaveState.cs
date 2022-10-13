using Chars.Tools;

namespace SuperMarketGame
{
    public class LeaveState : State
    {
        private Client client;
        public LeaveState(FiniteStateMachine finiteStateMachine, Client client) : base(finiteStateMachine)
        {
            this.client = client;
        }

        public override void Enter()
        {
            ClientDirector.Instance.IncrementServedClient();
            client.Leave();
        }

        public override void Exit()
        {

        }

        public override void Update()
        {
        }
    }
}
  