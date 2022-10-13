using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using Chars.Tools;

namespace SuperMarketGame
{
    public partial class Client : Actor
    {
        public enum States
        {
            WAITING = 0,
            CHECKING,
            PAYING,
            LEAVING
        }

        [SerializeField] private FiniteStateMachine finiteStateMachine = new FiniteStateMachine();
        public bool current;
        public List<ActorData> scriptableObjects = new List<ActorData>();

        protected override void Awake()
        {
            int random = Random.Range(0, scriptableObjects.Count);
            actorData = scriptableObjects[random];
            base.Awake();

            story = GenerateStory(inkJSONAsset);
            hasStory = story != null;
            SetupFiniteStateMachine();
            finiteStateMachine.Start();

        }

        protected override void Update()
        {
            finiteStateMachine.Update();
        }

        private void SetupFiniteStateMachine()
        {
            finiteStateMachine.Add((int)States.WAITING, new WaitState(finiteStateMachine, this));
            finiteStateMachine.Add((int)States.CHECKING, new CheckState(finiteStateMachine, this));
            finiteStateMachine.Add((int)States.PAYING, new PayState(finiteStateMachine, this));
            finiteStateMachine.Add((int)States.LEAVING, new LeaveState(finiteStateMachine, this));
        }

        private Story GenerateStory(TextAsset textAsset)
        {
           // int percentageOfStory = Random.Range(0, 100);
           // return Random.Range(0, percentageOfStory) >= percentageOfStory / 2 ? new Story(textAsset.text) : null;
            return true ? new Story(textAsset.text) : null;

        }

        public void MoveToCheck()
        {
            transform.parent = ClientDirector.Instance.GetCheckTransform();
            transform.position = ClientDirector.Instance.GetCheckTransform().position;
        }

        public void MoveToPay()
        {
            transform.parent = ClientDirector.Instance.GetPayTransform();
            transform.position = ClientDirector.Instance.GetPayTransform().position;
        }

        public void Leave()
        {
            Destroy(gameObject);
        }
    }
}
  
