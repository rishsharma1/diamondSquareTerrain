using UnityEngine;
using System.Collections;

public class diamondSquare : MonoBehaviour {

	public const float DETAIL = 9;
	public const float ROUGNESS = 0.7f	;

	Terrain currentTerrain;
	TerrainData tData;
	Vector3 terrainSize;
	int size;
	int max;

	// Use this for initialization
	void Start () {

		currentTerrain = Terrain.activeTerrain;
		tData = currentTerrain.terrainData;
		terrainSize = tData.size;
		this.size = 257;
		this.max = this.size - 1;

		float[,] data = PopulateDataArray ();
		generateTerrain (data);

	}
	
	// Update is called once per frame
	void Update () {


	}


	private float [,] PopulateDataArray() {


		float[,] data = new float[size, size];
		float val, rnd;
		float h = 0.5f;

		int x, y, sideLength, halfSide = 0;


		// set the four corner points to inital values
		data [0,0] = 1;
		data [max,0] = 1;
		data [0,max] = 1;
		data [max,max] = 1;


		for( sideLength = size - 1; sideLength >= 2; sideLength /= 2) {

			halfSide = sideLength / 2;


			for (x = 0; x < size - 1; x += sideLength) {


				for (y = 0; y < size - 1; y += sideLength) {


					val = data [x, y];
					val += data [x + sideLength, y];
					val += data [x, y + sideLength];
					val += data [x + sideLength, y + sideLength];

					val /= 4.0f;

					rnd = (Random.value * 2.0f * h) - h;
					val = Mathf.Clamp01 (val + rnd);

					data [x + halfSide, y + halfSide] = val;

				}

			}

			
		}

		for (x = 0; x < size - 1; x += halfSide) {

			for (y = (x + halfSide) % sideLength; y < size - 1; y += sideLength) {

				val = data [(x - halfSide + size - 1) % (size - 1), y];
				val += data [(x + halfSide) % (size - 1), y];
				val += data [x, (y + halfSide) % (size - 1)];
				val += data [x, (y - halfSide + size - 1) % (size - 1)];

				val /= 4.0f;

				rnd = (Random.value * 2.0f * h) - h;
				val = Mathf.Clamp01 (val + rnd);

				data [x, y] = val;

				if (x == 0)
					data [size - 1, y] = val;

				if (y == 0)
					data [x, size - 1] = val;
			}

			h /= 2.0f;
		}

		return data;

	}


	private void generateTerrain(float [,] data) {

		if (!currentTerrain) {
			return;
		}

		if (tData.heightmapResolution != size) {
			tData.heightmapResolution = size;
		}

		tData.SetHeights (0, 0, data);

	}




}
