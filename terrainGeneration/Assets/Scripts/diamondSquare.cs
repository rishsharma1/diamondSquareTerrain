using UnityEngine;
using System.Collections;

public class diamondSquare : MonoBehaviour {

	/*
	 * This script is responsible for generating the
	 * terrain, using the diamond square algorithm.
	 * It is also responsible for mapping the textures
	 * to the terrain object using alpha values 
	 */

	// defines the number of verticies used in the height map
	public int detail;
	public float roughness;

	// defines where the grass and snow should be formed
	public int snowHeight;
	public int grassHeight;

	// index's into the textures used for the map
	private const int GRASS = 0;
	private const int DIRT = 1;
	private const int SNOW = 2;



	Terrain currentTerrain;
	TerrainData tData;
	//size - 1
	int max;
	// 2^(detail) + 1
	int size;

	void Start () {

		currentTerrain = Terrain.activeTerrain;
		tData = currentTerrain.terrainData;

		//initialise the max and size parameters
		InitialiseTerrain (detail);

		//populate the heightmap data
		float[,] data = PopulateDataArray ();

		//generate the terrain
		generateTerrain (data);
		// add the grass,snow, and dirt
		addTextures ();


	}
		
	private void InitialiseTerrain(int detail) {
		this.size = (int) Mathf.Pow(2,detail)+1;
		this.max = this.size - 1;
	}

	/*
	 * This function uses alpha maps, where it will set the texture value for a
	 * point on the map. It will first determine the height at that point in the map.
	 * If the height is > snow height it will assign it the snow texture, if the height
	 * is > grass height it will assign it the grass texture, otherwise the dirt texture
	 * will be assigned
	 */
	private void addTextures() {

		float[,,] alphaData = tData.GetAlphamaps (0, 0, 
		tData.alphamapWidth, tData.alphamapHeight);

		for (int y = 0; y < tData.alphamapHeight; y++) {

			for (int x = 0; x < tData.alphamapWidth; x++) {

				float height = currentTerrain.SampleHeight (new Vector3 (y,0, x));

				if (height > snowHeight) {
					alphaData [x, y, SNOW] = 1;
					alphaData [x, y, DIRT] = 0;
					alphaData [x, y, GRASS] = 0;

				} else if (height > grassHeight) {
					alphaData [x, y, GRASS] = 1;
					alphaData [x, y, DIRT] = 0;
					alphaData [x, y, SNOW] = 0;
				
				} else {
					alphaData [x, y, DIRT] = 1;
					alphaData [x, y, GRASS] = 0;
					alphaData [x, y, SNOW] = 0;
				}
			}
		}

		tData.SetAlphamaps (0, 0, alphaData);

	}

	/*
	 * The diamond square algorithm
	 * This is based from: 
	 * http://answers.unity3d.com/questions/784960/procedural-terrain-square-diamond-algorithm.html
	 */
	private float [,] PopulateDataArray() {


		float[,] data = new float[size, size];
		float val, rnd;
		float h = 0.5f*roughness;

		int x, y, sideLength, halfSide = 0;


		// set the four corner points to inital values
		data [0,0] = 0;
		data [max,0] = 0;
		data [0,max] = 0;
		data [max,max] = 0;


		for (sideLength = max; sideLength >= 2; sideLength /= 2) {

			halfSide = sideLength / 2;


			for (x = 0; x < max; x += sideLength) {


				for (y = 0; y < max; y += sideLength) {



					float average = getAverage(data [x, y],
						data [x + sideLength, y],data[x, y + sideLength],
						data[x + sideLength, y + sideLength]);

					// add random
					rnd = (Random.value* 2.0f * h) - h;
					val = Mathf.Clamp01 (average + rnd);

					data [x + halfSide, y + halfSide] = val;

				}

			}

			//diamond values
			for (x = 0; x < max; x += halfSide) {


				for (y = (x + halfSide) % sideLength; y < max; y += sideLength) {


					float average = getAverage (data [(x - halfSide + max) % (max), y],
						                data [(x + halfSide) % (max), y], data [x, (y + halfSide) % (max)],
						                data [x, (y - halfSide + max) % (max)]);

					// add random
					rnd = (Random.value* 2.0f * h) - h;
					val = Mathf.Clamp01 (average + rnd);

					data [x, y] = average;

					if (x == 0)
						data [max, y] = val;

					if (y == 0)
						data [x, max] = val;
				}

			}
			h /= 2.0f;
		}
		return data;

	}

	// get the average for the four values
	private float getAverage(float a,float b,float c,float d) {
		return (a + b + c + d) / 4.0f;
	}


	// set the heights from the height map of the terrain
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
