using UnityEngine;
using System.Collections;

public class cameraMovement : MonoBehaviour {

	private float speed;
	private float rollSpeed;
	// Use this for initialization
	void Start () {

		speed = 450;
		rollSpeed = 200;

	}
	
	// Update is called once per frame
	void Update () {

		float axisX = Input.GetAxis ("Horizontal");
		float axisZ = Input.GetAxis ("Vertical");

		transform.Translate (new Vector3 (axisX, 0, axisZ)*Time.deltaTime*speed);

		if (Input.GetKey (KeyCode.Q))
			transform.Rotate (Vector3.forward, rollSpeed * Time.deltaTime);
		
		if (Input.GetKey (KeyCode.E))
			transform.Rotate (Vector3.back, rollSpeed * Time.deltaTime);

		if (Input.GetAxis ("Mouse X") < 0) {
			transform.Rotate (Vector3.up, rollSpeed * Time.deltaTime);
		}
		if (Input.GetAxis ("Mouse X") > 0) {
			transform.Rotate (Vector3.down, rollSpeed * Time.deltaTime);
		}
		if (Input.GetAxis ("Mouse Y") < 0) {
			transform.Rotate (Vector3.left, rollSpeed * Time.deltaTime);
		}
		if (Input.GetAxis ("Mouse Y") > 0) {
			transform.Rotate (Vector3.right, rollSpeed * Time.deltaTime);
		}
			
	}

	void OnCollisionEnter(Collision col) {


	}
}
