using UnityEngine;
using System.Collections;

public class cameraMovement : MonoBehaviour {

	/*
	 * This script is responsible for handling the camera movements,
	 * and making sure the person controlling the camera does not go
	 * underneath the map
	 */

	//speed of the camera
	public float speed;
	// how fast the camera rolls
	public float rollSpeed;
	Terrain currentTerrain;
	Vector3 terrainSize;

	// boundary on the map
	private const int minX = 30;
	private const int minZ = 30;
	// offset to not allow the camera to go underneath the map
	private const int terrainOffset = 10;



	void Start () {


		currentTerrain = Terrain.activeTerrain;
		terrainSize = currentTerrain.terrainData.size;
		transform.position = new Vector3 (283.32f, 394.742f, 475.003f);
		transform.Rotate(new Vector3 (66.87f, 172.768f, 350.946f));
			
	}
	
	void Update () {

		float axisX = Input.GetAxis ("Horizontal");
		float axisZ = Input.GetAxis ("Vertical");

		//get the height at the current camera position
		float terrainHeightAtPosition = currentTerrain.SampleHeight (transform.position)+terrainOffset;

	
		transform.Translate (new Vector3 (axisX, 0, axisZ)*Time.deltaTime*speed);

		/* code to make sure player does not go below the map*/
		if (terrainHeightAtPosition > transform.position.y) {
			transform.position = new Vector3 (transform.position.x, 
				terrainHeightAtPosition, transform.position.z);
		}

		/* code to make sure player does not go outside the bounds of the map*/
		if (transform.position.z > terrainSize.z) {
			transform.position = new Vector3 (transform.position.x, 
				transform.position.y, terrainSize.z);
		}
		if (transform.position.z < minZ) {
			transform.position = new Vector3 (transform.position.x, 
				transform.position.y, minZ);
		}

		if (transform.position.x > terrainSize.x) {
			transform.position = new Vector3 (terrainSize.x, 
				transform.position.y, transform.position.z);
		}
		if (transform.position.x < minX) {
			transform.position = new Vector3 (minX, 
				transform.position.y, transform.position.z);
		}

		/* get inputs from the mouse and keys and adjust the player position accordingly*/
		if (Input.GetKey (KeyCode.Q))
			transform.Rotate (Vector3.forward, rollSpeed * Time.deltaTime);
		
		if (Input.GetKey (KeyCode.E))
			transform.Rotate (Vector3.back, rollSpeed * Time.deltaTime);

		if (Input.GetAxis ("Mouse X") < 0) {
			transform.Rotate (Vector3.down, rollSpeed * Time.deltaTime);
		}
		if (Input.GetAxis ("Mouse X") > 0) {
			transform.Rotate (Vector3.up, rollSpeed * Time.deltaTime);
		}
		if (Input.GetAxis ("Mouse Y") < 0) {
			transform.Rotate (Vector3.left, rollSpeed * Time.deltaTime);
		}
		if (Input.GetAxis ("Mouse Y") > 0) {
			transform.Rotate (Vector3.right, rollSpeed * Time.deltaTime);
		}
			
	}

}
