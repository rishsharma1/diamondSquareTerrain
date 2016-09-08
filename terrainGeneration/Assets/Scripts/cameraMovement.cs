using UnityEngine;
using System.Collections;

public class cameraMovement : MonoBehaviour {

	private float speed;
	private float rollSpeed;
	Terrain currentTerrain;
	Vector3 terrainSize;
	int minX,minZ;
	// Use this for initialization
	void Start () {

		speed = 450;
		rollSpeed = 200;
		minX = minZ = 30;
		currentTerrain = Terrain.activeTerrain;
		terrainSize = currentTerrain.terrainData.size;
		transform.position = new Vector3 (283.32f, 394.742f, 475.003f);
		transform.Rotate (new Vector3 (66.87f, 172.768f, 350.946f));
			
	}
	
	// Update is called once per frame
	void Update () {

		float axisX = Input.GetAxis ("Horizontal");
		float axisZ = Input.GetAxis ("Vertical");
		float terrainHeightAtPosition = currentTerrain.SampleHeight (transform.position);

		transform.Translate (new Vector3 (axisX, 0, axisZ)*Time.deltaTime*speed);

		if (terrainHeightAtPosition > transform.position.y) {
			transform.position = new Vector3 (transform.position.x, 
				terrainHeightAtPosition, transform.position.z);
		}

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

}
