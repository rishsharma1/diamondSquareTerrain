Name: Rishabh Sharma
Username: rishabhs
Student ID: 694739

## Note
Due to the random nature of this algorithm, water will not always be present on the map, and occasionaly there may be a dominance of more grass or more snow. To ensure the best terrain is produced, ensure that you generate the terrain until it has snow, grass, and water.

## How the terrain is generated ?
The terrain is generated through the use of the diamond square algorithm. I implement the algorithm by creating a height map for Unity's terrain object. This is done by:

1. Setting the corners of the height map to some inital value. 
2. Split into 4 subsquares
3. Calcualte each of the sqaures centre point and then move it by some random offset
4. Split those into subsquares
5. Repeat 2-4, each time reducing the value of the offset

## How are textures applied to the terrain ?
The terrain object provides an alpha map, which can be used to apply textures at specific points on the map.
I used three textures grass, snow and dirt. These can be found in the textures/ folder.
I first calculate the height at a specific point and depending on my thresholds for snow height, grass height and dirt height, I will assign the relevant texture at that point.

## Code References
### Diamond Square
http://answers.unity3d.com/questions/784960/procedural-terrain-square-diamond-algorithm.html
### Phong Shading
https://unity3d.com/get-unity/download/archive (built-in shaders)