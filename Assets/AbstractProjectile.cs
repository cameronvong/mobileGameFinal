using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bunny.Tools;

public class AbstractProjectile : MonoBehaviour
{
    public AbstractProjectileData projectileData;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLifeCycle());
    }

    private IEnumerator StartLifeCycle()
    {
        yield return new WaitForSeconds(projectileData.Lifetime);
        Destroy(this.gameObject);
    }

     void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Collided with" + col.gameObject.tag);
        if (col.gameObject.tag == "Player")
        {
            // Do something
            BunnyEventManager.Instance.Fire<float>("DamagePlayerRequest", new BunnyMessage<float>(10f, this));
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
