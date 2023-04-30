using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Vector3 offset;
    Vector3 newPos;
    public GameObject playerObject;

    // Start is called before the first frame update
    void Start()
    {
        offset = playerObject.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        newPos = transform.position;
        newPos.x = playerObject.transform.position.x - offset.x;
        newPos.z = playerObject.transform.position.x - offset.z;


        transform.position = newPos;
    }
}
