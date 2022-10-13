using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chars.Tools;
using System.Linq;

namespace SuperMarketGame
{
    public class ClientDirector: Singleton<ClientDirector>
    {
        [SerializeField] private GameObject BaseNPC;
        [SerializeField] private Transform startTransform;
        [SerializeField] private Transform checkTransform;
        [SerializeField] private Transform PayTransform;

        [SerializeField] private float padding = 2.8f;

        private Client currentClient;
        private Queue<Client> clientQueue = new Queue<Client>();
        [SerializeField] private int maxLimitQueue = 3;
        [SerializeField] private int clientServed = 0;

        public Transform GetPayTransform() => PayTransform;
        public Transform GetCheckTransform() => checkTransform;
        public Client GetCurrentClient() => currentClient;
        public void EnqueueClient(Client client) => clientQueue.Enqueue(client);
        public void IncrementServedClient() => clientServed++;
        public int GetServedClients() => clientServed;
        public int GetQuantityOfClientsWaiting() => clientQueue.Count;


        private void Start()
        {
            Populate();
            Populate();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Populate();
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                if (checkTransform.childCount == 0)
                {
                    ShiftQueue();
                    MoveToNextClient();
                }
            }
        }

        public void Populate()
        { 
            if (clientQueue.Count < maxLimitQueue)
            {

                GameObject client = GameObject.Instantiate(BaseNPC, startTransform);
                client.transform.position = new Vector3(
                    client.transform.position.x - (clientQueue.Count()) * padding,
                    client.transform.position.y
                );
                EnqueueClient(client.GetComponent<Client>());
            }
        }

        private void MoveToNextClient()
        {
            if (clientQueue.Count > 0)
                clientQueue.Dequeue().current = true;
        }

        private void ShiftQueue()
        {
            clientQueue.ToList().ForEach(
                client => client.transform.position = new Vector3(client.transform.position.x + padding, client.transform.position.y)
            );
        }


    }
}

