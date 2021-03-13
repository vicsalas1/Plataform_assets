using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	Animator anim;
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Quaternion lookLeft;
	private Quaternion lookRight;
	private Vector3 moveDirection = Vector3.zero;
	private CharacterController controller;
	void Start(){
		controller = GetComponent<CharacterController>();
		Cursor.visible = false;
		anim = GetComponent<Animator>();
		lookRight = transform.rotation;
		lookLeft = lookRight * Quaternion.Euler(0, 180, 0); 
	}

	void Update() {
	
		if (controller.isGrounded) {

		//	anim.SetBool ("IsRunning", false);

			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

		

			if (Input.GetAxis("Horizontal")<0){

				transform.rotation = lookLeft;
				moveDirection = transform.TransformDirection(moveDirection);
				//Debug.Log(moveDirection);
				moveDirection *= speed;

			//  anim.SetBool ("IsRunning", true);

			}

			if (Input.GetAxis("Horizontal")>0){
				transform.rotation = lookRight;
				moveDirection = transform.TransformDirection(-moveDirection);
				moveDirection *= speed;
			//	anim.SetBool ("IsRunning", true);
			}

			if (Input.GetButton("Jump"))
			{
				moveDirection.y = jumpSpeed;
			//	anim.SetTrigger("Jump");
			}
				
		}
		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}
}