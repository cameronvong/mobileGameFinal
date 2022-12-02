using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
