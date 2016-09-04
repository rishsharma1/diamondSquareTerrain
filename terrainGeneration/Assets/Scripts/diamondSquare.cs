using UnityEngine;
using System.Collections;

public class diamondSquare : MonoBehaviour {

	Terrain currentTerrain;
	TerrainData tData;
	Vector3 terrainSize;
	// Use this for initialization
	void Start () {

		currentTerrain = Terrain.activeTerrain;
		tData = currentTerrain.terrainData;
		terrainSize = tData.size;
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log (terrainSize);

	}
}
