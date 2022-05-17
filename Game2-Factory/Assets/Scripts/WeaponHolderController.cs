using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolderController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = transform.parent.position;
        //transform.rotation = transform.parent.rotation;
        //transform.localRotation = Quaternion.Euler(MouseLook.xRotation, 0f, 0f);
        transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
    }
}
