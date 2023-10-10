using csDelaunay;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    public static Map instance;
    public Vector2Int size;
    [SerializeField] private int nodeAmount = 0;
    [SerializeField] List<Vector2> centroids;
    [SerializeField] private SpriteRenderer voronoiMapRenderer = null;
    [SerializeField] private int lloydIterationCount = 0;
    [SerializeField, Range(0f, 0.4f)] private float noiseFrequency = 0;
    [SerializeField] private int noiseOctave = 0;
    [SerializeField] private int noiseMaskRadius = 0;
    [SerializeField, Range(0f, 0.5f)] private float landNoiseThreshold = 0;
    [SerializeField] private SpriteRenderer noiseMapRenderer = null;
    [SerializeField] private bool lockSeed = true;
    [SerializeField] private int _seed = 0;
    
    public GameObject hexagon;
    public GameObject playerprefab;
    public GameObject testPlayer;
    GameObject tileObject;
    [HideInInspector] public GameObject player;

    public bool isPlayerOnEndTile = false;
    public Tile startTile;

    public float playerMoveSpeed;
    int tileNum = 0;
    public List<GameObject> tileObjectList;
    public List<Tile> pathTileObjectList;
    Voronoi voronoi;

    private void Awake()
    {
        instance = this;
        voronoi = GenerateVoronoi(size, nodeAmount, lloydIterationCount);
        voronoiMapRenderer.sprite = MapDraw.DrawVoronoiToSprite(voronoi);
        GenerateMap();
    }

    private void Start()
    {
        //GameManager.instance.GetLobbyAvatar(tileObjectList[0].transform.position);
        player = Instantiate(testPlayer, tileObjectList[0].transform.position, Quaternion.identity, transform);
    }

    public void PlayerMovePath(Tile objects)
    {
        // player.transform.position = new Vector3(objects.gameObject.transform.position.x,0, objects.gameObject.transform.position.z);
        pathTileObjectList.Add(objects);
    }
    int movePoint;
    int currentPoint = -1;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            currentPoint += 1;
            player.transform.position = new Vector3(pathTileObjectList[currentPoint].gameObject.transform.position.x, 0, pathTileObjectList[currentPoint].gameObject.transform.position.z);
            if (player.transform.position == pathTileObjectList.Last().transform.position)
            {
                Debug.Log("Finished");
                currentPoint = -1;
                pathTileObjectList.Clear();
                isPlayerOnEndTile = true;
            }
        }
    }

    public void GenerateMap()
    {
        var noiseColors = CreateMapShape(size, noiseFrequency, noiseOctave);

        noiseMapRenderer.sprite = MapDraw.DrawSprite(size, noiseColors);
    }

    private Voronoi GenerateVoronoi(Vector2Int size, int nodeAmount, int lloydIterationCount)
    {
        centroids = new List<Vector2>();

        // 무게 중심을 nodeAmount만큼 생성
        for (int i = 0; i < nodeAmount; ++i)
        {
            int x = Random.Range(0, size.x);
            int y = Random.Range(0, size.y);

            centroids.Add(new Vector2(x, y));
        }

        Rect Rect = new Rect(0f, 0f, size.x, size.y);
        Voronoi voronoi = new Voronoi(centroids, Rect, lloydIterationCount);
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

        for (int x = 0; x < size.x; ++x)
        {
            for (int y = 0; y < size.y; ++y)
            {
                var noiseColorFactor = noise.GetNoise(x, y);
                noiseColorFactor = (noiseColorFactor + 1) * 0.5f;
                noiseColorFactor *= mask[index];
                float color = noiseColorFactor > landNoiseThreshold ? 1f : 0f;
                colorDatas[index] = color;
                int num = 0;
                if (color > 0)
                {
                    if (x % 2 == 0)
                    {
                        tileObject = Instantiate(hexagon, new Vector3(x * 1.5f, 0, y * Mathf.Sqrt(3)), Quaternion.identity, transform);
                        Tile tile = tileObject.GetComponent<Tile>();
                        tile.position = new Vector3Int(Convert.ToInt32(tileObject.transform.position.x), 0, Convert.ToInt32(tileObject.transform.position.z));
                        tileObject.name = "Tile" + tileNum;
                        tileObjectList.Add(tileObject);  
                        tileNum += 1;
                    }
                    else
                    {
                        tileObject = Instantiate(hexagon, new Vector3(x * 1.5f, 0, y * Mathf.Sqrt(3) + Mathf.Sqrt(3) / 2), Quaternion.identity, transform);
                        Tile tile = tileObject.GetComponent<Tile>();
                        tile.position = new Vector3Int(Convert.ToInt32(tileObject.transform.position.x), 0, Convert.ToInt32(tileObject.transform.position.z));
                        tileObject.name = "Tile" + tileNum;
                        tileObjectList.Add(tileObject);        
                        tileNum += 1;
                    }
                }
                ++index;
            }
        }
        return colorDatas;
    }

    //private void OnValidate()
    //{
    //     //GenerateMap();
    //}
}