using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
 		float x = transform.position.x + transform.forward.x;   
 		float y = transform.position.y + transform.forward.y;      
 		float z = transform.position.z - transform.forward.z;      
 		transform.position = new Vector3(x, y, z);
   
    }
}
