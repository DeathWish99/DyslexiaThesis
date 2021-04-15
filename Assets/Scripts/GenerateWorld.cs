
using UnityEngine;

public class GenerateWorld : MonoBehaviour
{
    public GameObject plane;
    public GameObject[] decorations;

    public int depth = 20;
    public int width = 256;
    public int height = 256;

    public float scale = 20;
    public float offsetX = 100f;
    public float offsetY = 100f;

    [System.Serializable]
    public struct PuzzleSets
    {
        public GameObject puzzleSetPrefab;
        public int spawnNum;
        public bool stopSpawning;
    }

    // Start is called before the first frame update
    void Start()
    {
        offsetX = Random.Range(0f, 9999f);
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];

        for(int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                heights[i, j] = CalculateHeight(i, j);
            }
        }

        return heights;
    }

    float CalculateHeight(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
