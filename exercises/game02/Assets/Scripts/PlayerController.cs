﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	float speed = 8f;
	float rotateSpeed = 120f;
	int score = 0;

	// This value is set in the Unity editor by dragging the Text object
	// into the slot in the inspector.
	public Text scoreText;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// Rotate on the y axis based on the hAxis value
		// NOTE: If the player isn't pressing left or right, hAxis will be 0 and there 
		// will be no rotation.
		// NOTE: We add the modifer "Space.World" to make it so that the rotation
		// works the way that we expect.
		float hAxis = Input.GetAxis("Horizontal");
		transform.Rotate(0, hAxis * rotateSpeed * Time.deltaTime, 0, Space.World);

		// Move forward
		//transform.position = transform.position + transform.forward * Time.deltaTime;
		// NOTE: We add the modifer "Space.World" to make it so that the movement
		// works the way that we expect (i.e. using the global coordinate system).
		transform.Translate(transform.forward * speed * Time.deltaTime);//, Space.World);		

		// --- Simulating skateboarding ---
		// Make speed go down over time.
		if (speed > 0)
		{
			speed -= 4 * Time.deltaTime;
		}
		// Make it so we move faster when we press space
		if (Input.GetKeyDown(KeyCode.Space))
		{
			speed += 5;
		}
		// Make sure speed doesn't get less than zero or greater than 15.
		speed = Mathf.Clamp(speed, 0, 15);
	}

	void OnTriggerEnter(Collider other)
	{

		if (other.CompareTag("pika"))
		{
			Destroy(other.gameObject);
		}

	}
}