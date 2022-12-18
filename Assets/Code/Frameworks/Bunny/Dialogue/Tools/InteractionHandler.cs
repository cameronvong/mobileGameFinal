using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public GameObject InteractionZone;
    public string EventName;
    public string TableName;
    public BunnyEventEntry dialogueTrigger;
    // Start is called before the first frame update
    void Start()
    {
        dialogueTrigger = BunnyDialogueManager.Instance.GetEvent(TableName, EventName);
        if(dialogueTrigger.IsSatisfied())
        {
            Debug.Log("Can show!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        print("collision satisfied" + dialogueTrigger.IsSatisfied());
        if(other.gameObject.tag == "Player" && dialogueTrigger.IsSatisfied())
        {
             int newID = BunnyDialogueManager.Instance.GetFact("demo", "convo_on_talk_inits").ID;
            dialogueTrigger.Raise();
        } else if(!dialogueTrigger.IsSatisfied()) {
            Debug.Log("Dialog conditions not satisfied!");
        }
    }
}
