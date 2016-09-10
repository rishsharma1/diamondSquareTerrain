using UnityEngine;
using System.Collections;

public class sunRotation : MonoBehaviour {

	/*
	 * This script is responsible for rotating the sun around the z axis
	 */
	public float speed;
	float size;

	// Use this for initialization
	void Start () {
		size = Terrain.activeTerrain.terrainData.size.x;

	}
	
	// Update is called once per frame
	void Update () {

		transform.RotateAround( new Vector3(size/2,0, size/2),new Vector3(0,0,-1),
		speed*Time.deltaTime);
	}
}
