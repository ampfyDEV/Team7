using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceForward : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        //TODO: Make into event instead of leaving it inside of update
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Camera.main.transform.rotation.eulerAngles.y, transform.rotation.z);
    }
}
