using csDelaunay;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    [SerializeField]
    private Vector2Int size;
    [SerializeField]
    private int nodeAmount = 0;
    [SerializeField]
    private SpriteRenderer voronoiMapRenderer = null;
    [SerializeField]
    private int lloydIterationCount = 0;
    [SerializeField, Range(0f, 0.4f)]
    private float noiseFrequency = 0;
    [SerializeField]
    private int noiseOctave = 0;
    [SerializeField]
    private int noiseMaskRadius = 0;
    [SerializeField, Range(0f, 0.5f)]
    private float landNoiseThreshold = 0;
    [SerializeField]
    private SpriteRenderer noiseMapRenderer = null;
    [SerializeField]
    private bool lockSeed = true;
    [SerializeField]
    private int _seed = 0;

    public GameObject hexagon;
    public GameObject empty;
    GameObject tileObject;
    GameObject xwidth;
    public int xNum = 0;
    int tileNum = 0;
    public GameObject[] tile = new GameObject[1];

    private void Awake()
    {
        var voronoi = GenerateVoronoi(size, nodeAmount, lloydIterationCount);
        voronoiMapRenderer.sprite = MapDraw.DrawVoronoiToSprite(voronoi);
        GenerateMap();
    }

    public void GenerateMap()
    {
        var noiseColors = CreateMapShape(size, noiseFrequency, noiseOctave);

        noiseMapRenderer.sprite = MapDraw.DrawSprite(size, noiseColors);
    }

    private Voronoi GenerateVoronoi(Vector2Int size, int nodeAmount, int lloydIterationCount)
    {
        var centroids = new List<Vector2>();

        // 무게 중심을 nodeAmount만큼 생성
        for (var i = 0; i < nodeAmount; ++i)
        {
            var x = Random.Range(0, size.x);
            var y = Random.Range(0, size.y);

            centroids.Add(new Vector2(x, y));
        }

        var Rect = new Rect(0f, 0f, size.x, size.y);
        var voronoi = new Voronoi(centroids, Rect, lloydIterationCount);
        return voronoi;
    }

    private float[] CreateMapShape(Vector2Int size, float frequency, int octave)
    {
        var seed = (_seed == 0) ? Random.Range(1, int.MaxValue) : _seed;

        var noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        noise.SetFractalType(FastNoiseLite.FractalType.FBm);
        noise.SetFrequency(frequency);
        noise.SetFractalOctaves(octave);
        noise.SetSeed(seed);
        var mask = MapDraw.GetRadialGradientMask(size, noiseMaskRadius);
        float[] colorDatas = new float[size.x * size.y];
        var index = 0;
        //test
        tile = new GameObject[size.x * size.y];
        xwidth = Instantiate(empty, gameObject.transform);
        xwidth.gameObject.name = "x" + xNum;
        for (int x = 0; x < size.x; ++x)
        {
            for (int y = 0; y < size.y; ++y)
            {
                var noiseColorFactor = noise.GetNoise(x, y);
                noiseColorFactor = (noiseColorFactor + 1) * 0.5f;
                noiseColorFactor *= mask[index];
                var color = noiseColorFactor > landNoiseThreshold ? 1f : 0f;
                colorDatas[index] = color;
                if (color > 0)
                {
                    if (x % 2 == 0)
                    {
                        tileObject = Instantiate(hexagon, new Vector3(x * 1.5f, 0, y * Mathf.Sqrt(3)), Quaternion.identity);
                        tileObject.name = "Tile" + tileNum;
                        tile[tileNum] = tileObject;
                    }
                    else
                    {
                        tileObject = Instantiate(hexagon, new Vector3(x * 1.5f, 0, y * Mathf.Sqrt(3) + Mathf.Sqrt(3) / 2), Quaternion.identity);
                        tileObject.name = "Tile" + tileNum;
                        tile[tileNum] = tileObject;
                    }
                    //GameObject tileObject = Instantiate(hexagon, new Vector3(x * 1.5f, 0, y * Mathf.Sqrt(3)), Quaternion.identity);
                    //tileObject.name = "Tile" + tileNum;
                    //tile[tileNum] = tileObject;

                    if (tileNum > 0)
                    {
                        if (tile[tileNum].transform.position.x == tile[tileNum - 1].transform.position.x)
                        {
                            tile[tileNum - 1].gameObject.transform.parent = xwidth.transform;
                            tileNum += 1;
                        }
                        else
                        {
                            Debug.Log("1");
                            xwidth = Instantiate(empty, gameObject.transform);
                            xwidth.gameObject.name = "x" + xNum;
                            tileObject.gameObject.transform.parent = xwidth.transform;
                            xNum += 1;
                            tileNum += 1;
                        }
                    }
                    else { tileNum += 1; }
                }
                ++index;
            }
        }
        return colorDatas;
    }

    private void OnValidate()
    {
        //GenerateMap();
    }
}