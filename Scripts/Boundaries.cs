using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
 

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -8f, 8f), Mathf.Clamp(transform.position.y, -10f, 10f), transform.position.z);

    }
}
