using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	float speed = 8f;
	float rotateSpeed = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	float hAxis = 0; //Input.GetAxis("Horizontal");
    	transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0);

    	transform.Translate(-transform.forward * Time.deltaTime * speed);
        
    }

    void OnTriggerEnter (Collider other) {

    	if (other.CompareTag("pika")) {
    	Destroy(other.gameObject);
    	}


    }
}
