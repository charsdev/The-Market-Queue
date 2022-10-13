using System.Collections;
using UnityEngine;
using Chars.Tools;
using System;

namespace SuperMarketGame
{
    public class GameController : Singleton<GameController>
    {
        public enum Status { BeforeGameStart, GameInProgress, GameOver };
        public Status CurrentStatus;
        public void SetStatus(Status value) => CurrentStatus = value;
        public int servedClientForVictory = 8;
        public int maxBadAnswers = 2;
        public int currentBadAnswers = 0;
        public int maxQueueClients = 3;

        protected void Start()
		{
			Application.targetFrameRate = 60;
			SetStatus(Status.BeforeGameStart);
        }

        protected virtual void Update()
        {
            if (VictoryCondition())
            {
               // Debug.Log("Victory!");
            }

            if (LoseConditionByBadAnswers() || LoseConditionByALotOfClientInQueue())
            {
                // Debug.Log("Lose!");
            }

            //if (CurrentStatus == Status.GameOver)
            //    HandleGameOver();

            //if (CurrentStatus != Status.GameInProgress) 
            //    return;

        }

        //private void HandleGameOver()
        //{
           
        //}

        private bool VictoryCondition()
        {
           return ClientDirector.instance.GetServedClients() >= servedClientForVictory;
        }

        private bool LoseConditionByBadAnswers()
        {
            return currentBadAnswers >= maxBadAnswers;
        }

        private bool LoseConditionByALotOfClientInQueue()
        {
            return ClientDirector.instance.GetQuantityOfClientsWaiting() >= maxQueueClients;
        }
    }
}