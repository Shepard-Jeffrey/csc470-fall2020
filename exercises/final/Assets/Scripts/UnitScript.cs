using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitScript : MonoBehaviour
{
	GameManager gm;

	public string unitName;
	public int health = 100;
	public int ATK;
	public int DEF;
	public int MAG;
	public MeterScript ATKMeter;
	public MeterScript DEFMeter;
	public MeterScript MAGMeter;
	public MeterScript updateMeter;

	public bool DragonFightStarted = false;

	public Animator unitanimator;
	public Animator ATKMineAnimator;
	public Animator DEFMineAnimator;
	public Animator MAGMineAnimator;

	// When the unit is selected, and the player clicks on the ground, it will instantiate a cube and add it to this
	// list. When the player presses the "Go!" button, the unit will start moving toward the first cube. When it
	// gets close to it, it will start moving toward the next one. When it gets to the last one, it will stop moving
	// and the path will be reset (and all the cubes will be destroyed).
	List<GameObject> path;
	int pathIndex = 0;
/*	bool moving = false;
*/
	// How fast the Unit will move forward.
	float speed = 5f;
	// How fast the Unit will rotate toward its targetPosition.
	float rotateSpeed = 4f;
	
	// Units will always rotate toward this position unless they are already close to it.
	public Vector3 targetPosition;

	// These two booleans are used to track the state based on the mouse (see the mouse functions below).
	public bool selected = false;
	public bool hover = false;

	// These colors are given values via the Unity inspector.
	public Color defaultColor;
	public Color hoverColor;
	public Color selectedColor;

	// This gets its value from the Unity inspector. We dragged the "Mesh Renderer" of the Prefab to do that.
	public Renderer rend;

	public CharacterController cc;

	// Start is called before the first frame update
	void Start()
	{

		// Set the color of the rendere's material based on the mouse state variables.
		UpdateVisuals();

		path = new List<GameObject>();

		// Initialize the targetPosition so that the Unit is initially close enough to its target to not want
		// to move and rotate toward it.
		targetPosition = transform.position;

		gm = GameObject.Find("GameManagerObject").GetComponent<GameManager>();

		//ATKMeter.SetMeter(ATK / 100);
		//DEFMeter.SetMeter(DEF / 100);
		//MAGMeter.SetMeter(MAG / 100);

	}

	// Update is called once per frame
	void Update()
	{
		if (DragonFightStarted == false)
		{
			if (selected)
			{
				// Input.GetMouseButtonDown(0) is how you detect that the left mouse button has been clicked.
				//
				// The IsPointerOverGameObject makes sure the pointer is over the UI. In this case,
				// we don't want to register clicks over the UI when determining what unit is 
				// selected or deselected.
				if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
				{
					// We will get in here if the Unit is selected, and the player has clicked the mouse.

					// The following code create's a "ray" at the position that the mouse is on the screen, and performs
					// a Raycast. This is a Physics utility function that will move in a direction and populate the 
					// values of a RaycastHit object if something was hit. If the Raycast doesn't hit anything (i.e. the
					// player clicks into the nothingness - where there are no GameObjects), the Physics.Raycast
					// returns null, and thus we will not go in the if statement's body.
					Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
					RaycastHit hit;
					if (Physics.Raycast(ray, out hit))
					{
						// If we get in here, it means that the mouse was "over" a GameObject when the player clicked.
						// Check to see if what we clicked on the the "Ground" via this layer check.
						if (hit.collider.gameObject.layer == LayerMask.NameToLayer("MinePlane"))
						{
							// // If we get in here, the raycast has hit the ground. 

							// // Spawn a cube, and store it in a list. These will be the "waypoints" that the unit will walk toward
							// // when the player clicks the "Go!" button.
							// GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
							// // Remove the default box collider that gets added when we use CreatePrimitive.
							// Destroy(obj.GetComponent<BoxCollider>()); 
							// obj.transform.position = hit.point;
							// path.Add(obj);

							targetPosition = hit.point;

						}
						if (hit.collider.gameObject.layer == LayerMask.NameToLayer("AttackMine"))
						{
							ATK += 1;

							//UpdateMeter(ATK);
							ATKMeter.SetMeter(ATK / 1000f); 
							// setting these to 1000 instead of 100 fixed the meters going up too fast. 
							// but it also caused the meters to be kinda screwy.
							// I'd rather set ATK/DEF/MAG to go up by 0.1, but they're ints and making them floats or doubles messed up a LOT of code elsewhere somehow. 

							//Start the ATKMine animation
							ATKMineAnimator.SetBool("ATKmining", true);
						}
						else
                        {
							ATKMineAnimator.SetBool("ATKmining", false);
						}
						if (hit.collider.gameObject.layer == LayerMask.NameToLayer("DefenseMine"))
						{
							DEF += 1;

							DEFMineAnimator.SetBool("DEFmining", true);
							//Start the DEFMine animation

							DEFMeter.SetMeter(DEF / 1000f);

						}
						else
                        {
							DEFMineAnimator.SetBool("DEFmining", false);
						}
						if (hit.collider.gameObject.layer == LayerMask.NameToLayer("MagicMine"))
						{
							MAG += 1;

							MAGMineAnimator.SetBool("MAGmining", true);
							//Start the MAGMine animation

							MAGMeter.SetMeter(MAG / 1000f);

						}
						else
                        {
							MAGMineAnimator.SetBool("MAGmining", false);
						}
					}
					else
					{
						//gm.SelectUnit(null);
					}
				}
			}
		}
		// MOVEMENT
		// If we are not close to our target position, rotate toward the targetPosition, and move forward.
		if (Vector3.Distance(transform.position, targetPosition) > 0.5f) {
			Vector3 vectorToTarget = targetPosition - transform.position;
			vectorToTarget = vectorToTarget.normalized;

			float step = rotateSpeed * Time.deltaTime;

			Vector3 newDirection = Vector3.RotateTowards(transform.forward, vectorToTarget, step, 1);
			transform.rotation = Quaternion.LookRotation(newDirection);
			
			cc.Move(transform.forward * speed * Time.deltaTime);

			unitanimator.SetBool("walking", true);

			// set walking true

		}
		else {
			unitanimator.SetBool("walking", false);
		/*	if (moving && path.Count > 0) {
				// If we get in here, we ARE close to our target position.
				pathIndex++;
				if (pathIndex == path.Count) {
					// If we get in here, we have arrived at the last target. In that case, stop moving, and destroy all the
					// path markers.
					foreach(GameObject pathObj in path) {
						Destroy(pathObj);
					}
					path = new List<GameObject>();
					moving = false;
					// set walking false
				} else {
					// Finally, if we get in here, we are going to set our target position to the location of the path marker.
					targetPosition = path[pathIndex].transform.position;
				}
			}*/
		}
	}

	/*public void UpdateMeter(int value)
	{
		updateMeter = value;
		MeterScript.SetMeter(updateMeter);

	}*/

	public void StartFollowingPath()
	{
		pathIndex = 0;
		if (path.Count > 0) {
			targetPosition = path[pathIndex].transform.position;
/*			moving = true;
*/			//anim.SetBool("walking", true);
		}
	}

	// This function is called manually by the mouse event functions whenever
	// the hover, or selection bools are modified.
	public void UpdateVisuals()
	{
		if (DragonFightStarted == false)
		{
			if (selected)
			{
				rend.material.color = selectedColor;
			}
			else
			{
				if (hover)
				{
					rend.material.color = hoverColor;
				}
				else
				{
					rend.material.color = defaultColor;
				}
			}
		}
		else
		{
			rend.material.color = defaultColor;
		}
	}

	private void OnMouseEnter()
	{
		hover = true;
		UpdateVisuals();
	}
	
	private void OnMouseExit()
	{
		hover = false;
		UpdateVisuals();
	}

	private void OnMouseDown()
	{
		selected = !selected;
		// If after clicking the unit is selected, tell the GameManager to select it.
		if (selected) {
			//gm.SelectUnit(this);
		}
	}
}