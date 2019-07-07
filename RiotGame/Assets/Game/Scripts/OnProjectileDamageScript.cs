using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnProjectileDamageScript : MonoBehaviour
{

    //Initial impact explosion
    public GameObject ExplosionPrefab;

    //Additional effects applied to target
    public GameObject EffectsPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {
        //Check their is an explosion prefab set
        if (ExplosionPrefab != null)
        {
            Instantiate(ExplosionPrefab, other.transform.position, other.transform.rotation);
        }

        //Apply effects if set to target
        //if (EffectsPrefab != null)
        //{
        //    Instantiate(EffectsPrefab, other.transform.position, other.transform.rotation);
        //}
    }
}
