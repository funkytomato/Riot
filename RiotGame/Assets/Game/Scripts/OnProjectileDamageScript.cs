using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using Eliot.Utility;
using Eliot.AgentComponents;
//using Unit = Eliot.Environment.Unit;

public class OnProjectileDamageScript : MonoBehaviour
{
    AudioSource audioSource;


    //Initial impact explosion
    public GameObject ExplosionPrefab;
    public AudioClip ExplosionAudioClip;

    //Additional effects applied to target
    public GameObject EffectsPrefab;
    public AudioClip EffectsAudioClip;

    // Start is called before the first frame update
    void Start()
    {
        //audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {

        Debug.Log("OnProjectileDamageScript: other.tag: " + other.gameObject.tag);

        audioSource = other.gameObject.GetComponent<AudioSource>();

        if ((other.gameObject.GetComponent<Agent>() == null) ||
            (other.gameObject.tag == "Thug")) return;


        //Check their is an explosion prefab set
        if (ExplosionPrefab != null)
        {
            Instantiate(ExplosionPrefab, other.transform.position, other.transform.rotation);
            if (ExplosionAudioClip != null)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = ExplosionAudioClip;
                    audioSource.Play();
                }
            }
        }

        //Apply effects if set to target
        if (EffectsPrefab != null)
        {
            Instantiate(EffectsPrefab, other.transform.position, other.transform.rotation);
            //audioSource.PlayOneShot(EffectsSoundPrefab, 1.0F);
        }
    }
}
