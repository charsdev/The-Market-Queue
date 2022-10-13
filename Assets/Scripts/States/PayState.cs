using Chars.Tools;

namespace SuperMarketGame
{
    public class PayState : State
    {
        private Client client;
        private LambdaTimer lambdaTimer = new LambdaTimer(2);

        public PayState(FiniteStateMachine finiteStateMachine, Client client) : base(finiteStateMachine)
        {
            this.client = client;
        }

        public override void Enter()
        {
            client.MoveToPay();
            if (client.hasStory)
            {
                StoryDirector.Instance.currentActor.Value = client;
            }
        }

        public override void Update()
        {
            if (client.isStoryFinished())
            {
                lambdaTimer.Delay(() => finiteStateMachine.SetCurrentState(finiteStateMachine.GetState((int)Client.States.LEAVING)));
            }
        }

        public override void Exit()
        {
        }

    }
}
