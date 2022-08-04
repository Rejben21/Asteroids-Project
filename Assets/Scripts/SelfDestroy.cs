using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public bool waitToDestroy;
    public float timeToDestroy;

    // Update is called once per frame
    void Update()
    {
        if(waitToDestroy)
        {
            Destroy(this.gameObject, timeToDestroy);
        }
    }

    public void DestroyObject()
    {
        Destroy(this.gameObject);
    }
}
