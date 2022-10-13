using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine;


public class Actor : MonoBehaviour, IActor
{

    [SerializeField] protected TextAsset inkJSONAsset = null;
    public Story story = null;
    public bool hasStory = false;
    public Sprite avatar;
    public string actorName;
    public ActorData actorData;


    public virtual bool isStoryFinished()
    {
        if (story == null)
        {
            return true;
        }
        else
        {
            return !story.canContinue && story.currentChoices.Count == 0;
        }
    }

    protected virtual void Awake()
    {
        if (actorData != null)
        {
            name = actorData.actorName;
            avatar = actorData.avatar;
            inkJSONAsset = actorData.inkJSONAsset;
            GetComponent<SpriteRenderer>().sprite = actorData.render;

        }

    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

}
