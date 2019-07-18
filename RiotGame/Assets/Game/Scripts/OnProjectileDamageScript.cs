using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using Eliot.Utility;
using Eliot.AgentComponents;
//using Unit = Eliot.Environment.Unit;

public class OnProjectileDamageScript : MonoBehaviour
{
    protected AudioSource audioSource;


    //Initial impact explosion
    public GameObject ExplosionPrefab;
    public AudioClip ExplosionAudioClip;

    //Additional effects applied to target
    public GameObject EffectsPrefab;
    public AudioClip EffectsAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {

        Debug.Log("OnProjectileDamageScript: other.tag: " + other.gameObject.tag);

        if ((other.gameObject.GetComponent<Agent>() == null) ||
            (other.gameObject.tag == "Thug")) return;

        //Check their is an explosion prefab set
        if (ExplosionPrefab != null)
        {
            //Create an empty gameobject for impact explosion and audio creation
            GameObject explosion = Instantiate(ExplosionPrefab, other.transform.position, other.transform.rotation) as GameObject;

            //If Explosion audio clip is loaded, play it
            if (ExplosionAudioClip != null)
            {
                if (!audioSource.isPlaying)
                {
                    AudioSource.PlayClipAtPoint(ExplosionAudioClip, transform.position);
                }
            }

            //Destroy the explosion
            if (explosion != null)
            {
                Destroy(explosion, 5.0f);
            }
        }

        //Apply effects if set to target
        if (EffectsPrefab != null)
        {
            GameObject effects = Instantiate(EffectsPrefab, other.transform.position, other.transform.rotation) as GameObject;
            effects.transform.parent = other.gameObject.transform;

            //If Effects audio clip is loaded, play it
            if (EffectsAudioClip != null)
            {
                if (!audioSource.isPlaying)
                {
                    AudioSource.PlayClipAtPoint(EffectsAudioClip, transform.position);
                }
            }

            //Destroy the effects
            if (effects != null)
            {
                Destroy(effects, 5.0f);
            }
        }
    }
}
