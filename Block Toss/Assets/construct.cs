using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class construct : MonoBehaviour
{
	public Rigidbody rb;
	public float tossPos;

	void Start()
    {
		rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
		tossPos = Mathf.PingPong(Time.time, 6) - 3;
		Debug.Log(tossPos);

		rb.AddForce(tossPos, 0, 0);
    }
}
