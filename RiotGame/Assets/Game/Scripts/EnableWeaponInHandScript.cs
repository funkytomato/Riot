using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWeaponInHandScript : MonoBehaviour
{

    private GameObject weaponObject;
    private GameObject rightHand;

    // Start is called before the first frame update
    void Start()
    {
        rightHand = GameObject.FindGameObjectWithTag("RightHand");
        //weaponObject = gameObject.        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
