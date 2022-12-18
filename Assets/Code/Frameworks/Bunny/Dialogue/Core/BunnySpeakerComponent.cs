using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnySpeakerComponent : MonoBehaviour
{
    public DialogueSpeakerMap Map;
    public BunnyFactEntry speaker;
    public string speakerKey;
    //public BunnyFactEntry or maybe a descendant of it that's the SO

    private void Awake()
    {

    }

    private void OnEnable()
    {
        // Later we want to only add to it if the entity is near the player
        // In other words, if the player is within the interaction region of the entity, the runtimeset includes it
        // Later, I'll make it so you can edit in the fact itself that's available.
        Map = BunnyDialogueManager.Instance.Speakers;
        string objectName = gameObject.name;
        speaker = BunnyDialogueManager.Instance.GetFact("speakers", speakerKey);
        Map.Add(speaker, this);
    }

    private void OnDisable()
    {
        Map.Remove(speaker);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Speak(string text)
    {
        int newID = BunnyDialogueManager.Instance.GetEvent("demo", "on_talk").ID;
        Debug.Log($"New ID of event is: {newID}");
        Debug.Log($"Speaker[{gameObject.name}] says: {text}");
    }
}
