using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "New Actor", menuName = "Dialog/Actor")]
public class ActorData : ScriptableObject
{
    [SerializeField] public TextAsset inkJSONAsset = null;
    [SerializeField] public Sprite avatar = null;
    [SerializeField] public Sprite render = null;
    [SerializeField] public string actorName = null;
    [SerializeField] [Range(1, 50)] public float talkingSpeed = 20.0f;

}
