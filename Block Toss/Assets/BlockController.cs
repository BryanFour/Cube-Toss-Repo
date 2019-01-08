using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://answers.unity.com/questions/1085373/how-to-do-ping-pong-motion.html - Ping Pong.

public class BlockController : MonoBehaviour
{	//General Stuff//
	public Rigidbody rb;					// Get access to the Ridgidbody.
	public int tossForce;					// The force of the "Toss".
	public int colForce;					// The force applyed to the cube when a collision is detected.
	private bool gameStart = false;			// A Bool to signify when the cube has been tossed.
	private bool gameOver = false;			// A bool to signify whether the Cube has touched the ground / The game has ended.
	//Left/Right Pos Stuff//
	public Transform targetPos;				// Assign these in inspector with "waypoint" gameobjects or something.
	public Transform startPos;				//   ""															""	
	private bool towards = true;			// A Bool to signify which Waypoint we are closer too. // I think.
	private bool leftRightSet = false;		// A Bool to signify if the player has set the Left/Right position.
	public float leftRightSpeed = 10f;		// The speed that the cube moves Left and Right prior to setting the Left/Right Position.
	//Angle Pos Stuff//
	private bool angleSet = false;			// A Bool to signify whether the Toss angle has been set.
	public float rotValue;					// This is used to determin the Max/Min rotation of the cube. (The Max/Min angle the player can select.)
	//public float anglePos;				// The angle that the cube is on. <--- Not Needed ????

		
	void Start()
    {
		rb = GetComponent<Rigidbody>();		// Get access to the Ridgidbody.
		//rb.useGravity = false;            // Set the Gravity to False.
		rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void Update()
    {
		Toss();								// Try to run the Toss() Function.

		if (leftRightSet == false)			// If the Left/Right Position has not been set...
		{
			LeftRight();					// Run the LeftRight() Function.
		}
		else if (leftRightSet == true && angleSet == false)		// Else if the Left/Right Position has been set and the Toss Angle has not been set...
		{
			Angle();						// Run the Angle() Function.
		}
    }
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void LeftRight()																		//		The LeftRight Function.
	{																						//
		if(towards == true && leftRightSet == false)										//
		{																					//
			transform.LookAt(targetPos.position);											//
			transform.position += transform.forward * leftRightSpeed * Time.deltaTime;		//
			if (Vector3.Distance(transform.position, targetPos.position) < 1.0f)			//
			{																				//
				towards = false;															//
			}																				//
		}																					//
		else if(towards == false && leftRightSet == false)									//
		{																					//
			transform.LookAt(startPos.position);											//
			transform.position += transform.forward * leftRightSpeed * Time.deltaTime;		//
			if (Vector3.Distance(transform.position, startPos.position) < 1.0f)				//
			{																				//
				{																			//
					towards = true;															//
				}																			//
			}																				//
		}																					//
																							//		To Stop the Cube moving Left/Right AKA Set the Left Right Position...
		if (Input.GetMouseButtonDown(0) && leftRightSet == false)							// If we click the mouse and the left right position isnt set.
		{																					//
			leftRightSet = true;															// Set the leftRightSet Bool to True.
			//Debug.Log("posSet = " + leftRightSet);										//
		}																					//
	}
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////																		
	void Angle()																			//
	{																						//
		if(leftRightSet == true && angleSet == false)										// If we have set the Left/Right Position but have not set the Angle...
		{																					//
			float anglePos = Mathf.Sin(Time.time) * rotValue;								// Tweak the rotValue to change frequency(How much the cube rotates.)
			transform.rotation = Quaternion.AngleAxis(anglePos, Vector3.forward);			// 
			//Debug.Log(anglePos);															//
		}																					//		To Set the Angle..
		if (Input.GetMouseButtonDown(0) && leftRightSet == true && angleSet == false)		// If we click the mouse and the Left/Right position is set and the Angle is not set...
		{																					//
			angleSet = true;																// Set the angle.
			//Debug.Log("angleSet = " + angleSet);											//
		}																					//																						
	}																						//
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	void Toss()																				//
	{																						//		To Toss the Cube.
		if (Input.GetMouseButtonDown(0) && leftRightSet == true && angleSet == true)		// If we click the mouse and the Left right position is set and the Angle is set...
		{                                                                                   //
			rb.constraints = RigidbodyConstraints.None;										// Unfreeze all Constraints.
			rb.constraints = RigidbodyConstraints.FreezePositionZ;							// Freeze Position Z Constraint.
			//rb.useGravity = true;															// Turn gravity on.
			rb.AddForce(transform.up * tossForce);											// Add Force along the Cubes Y-axis, Multiplyed by the tossForce.
			gameStart = true;																// Set the gameStart Bool to true.
		}																					//
	}																						//
	///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private void OnCollisionEnter(Collision col)											//
	{																						//
		if(col.gameObject.tag == "Bounce")													// If the Cube collides with an object Tagged with "Bounce"...
		{																					//
			rb.AddForce(0, colForce, 0);													// Add Force along the Worlds Y-axis (Upwards).
			//Debug.Log("Collided with bounce tag");										//
		}																					//																					
	}																						//
	////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
}
