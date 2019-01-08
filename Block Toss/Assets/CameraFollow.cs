using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	// https://www.youtube.com/watch?v=MFQhpwc6cKE
	/*public Transform target;
	public float smoothSpeed = 10f;
	public Vector3 offSet;

	private void LateUpdate()
	{
		Vector3 desiredPosition = target.position + offSet;
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
		transform.position = smoothedPosition;

		//transform.LookAt(target);
	}
	*/

	// https://gamedev.stackexchange.com/questions/147526/only-make-camera-follow-player-on-x-axis
	public GameObject target;
	public Vector3 offSet;
	
	void LateUpdate()
	{ 
		transform.position = new Vector3(0f, target.transform.position.y, 0f) + offSet;
	}
}
