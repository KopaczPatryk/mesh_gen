using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Camera viewport;
	private CharacterController controller;
	private Rigidbody rb;
	public float jumpforce = 1;
	public float walkSpeed = 1;
	
	void Awake()
	{
		Cursor.lockState = CursorLockMode.Locked;
		viewport = GetComponentInChildren<Camera>();
		rb = GetComponent<Rigidbody>();
		//controller = GetComponent<CharacterController>();
	}
	// Update is called once per frame
	void Update () {
		viewport.transform.Rotate(new Vector3(1,0,0), -Input.GetAxisRaw("Mouse Y"));
		gameObject.transform.Rotate(new Vector3(0,1,0), Input.GetAxisRaw("Mouse X"));
		//print((controller.collisionFlags & CollisionFlags.Below) != 0 & Input.GetAxisRaw("Jump") == 1);
		//if ((controller.collisionFlags & CollisionFlags.Below) != 0 & Input.GetAxisRaw("Jump") == 1)
	}
	void OnCollisionStay(Collision other)
	{
		rb.AddRelativeForce(new Vector3(Input.GetAxisRaw("StrafeLeft"), 0, Input.GetAxisRaw("WalkForward"))*walkSpeed, ForceMode.VelocityChange);
		if (Input.GetAxisRaw("Jump") == 1)
		{
			print("jump");
            rb.AddForce(new Vector3(0,1,0)*jumpforce, ForceMode.VelocityChange);
		}
		
	}
}
