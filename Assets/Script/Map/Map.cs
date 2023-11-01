using csDelaunay;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    public static Map instance;
    public MapDraw mapDraw;
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
    [SerializeField] Rect wolrdRect;
    [SerializeField] float wolrdRectx = 45;
    [SerializeField] float wolrdRecty = 49.36345f;
    int wolrdRectNum = 1;
    int wolrdRectNum1 = 1;

    public GameObject grass;
    public GameObject desert;
    public GameObject jungle;
    public GameObject hexagon;
    public GameObject playerprefab;
    public GameObject testPlayer;
    GameObject tileObject;
    public GameObject player;

    public bool isPlayerOnEndTile = false;
    public Tile startTile;

    public float playerMoveSpeed;
    int tileNum = 0;
    public List<GameObject> tileObjectList;
    public List<Tile> pathTileObjectList;

    public Vector3 centerPosition;
    public List<Vector3> linePoint;
    public GameObject testCube;
    public GameObject testCube1;


    Voronoi voronoi;

    private void Awake()
    {
        instance = this;
        wolrdRect = new Rect(22.5f, 25.114f, 47, 51);
        GenerateMap();
        voronoi = GenerateVoronoi(new Vector2(wolrdRect.width, wolrdRect.height), nodeAmount, lloydIterationCount);
        voronoiMapRenderer.sprite = mapDraw.DrawVoronoiToSprite(voronoi);
        tileObjectList[1].GetComponent<Tile>().tileState = Tile.TileState.MonsterTile;
    }

    private void Start()
    {
        GameManager.instance.GetLobbyAvatar(tileObjectList[0].transform.position);
        //player = Instantiate(testPlayer, tileObjectList[0].transform.position, Quaternion.identity, transform);
        wolrdRect = new Rect(22.2f, 24.2f, 47, 51);
        TestClimate();
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
    int a = 0;
    void TestClimate()
    {
        bool fineCenter = false;
        Transform a;

        foreach (var site in voronoi.Sites)
        {
            var neighbors = site.NeighborSites();
            foreach (var neighbor in neighbors)
            {
                var edge = voronoi.FindEdgeFromAdjacentPolygons(site, neighbor);

                if (edge.ClippedVertices is null)
                {
                    continue;
                }

                Vector2 corner1 = edge.ClippedVertices[LR.LEFT];
                Vector2 corner2 = edge.ClippedVertices[LR.RIGHT];

                Vector3 p0 = new Vector3(corner1.x, 0, corner1.y);
                Vector3 p1 = new Vector3(corner2.x, 0, corner2.y);
                Vector3 p01 = p1-p0;
                //Gizmos.color = Color.black;
                //Gizmos.DrawLine(p0, p1);
                Debug.Log(p0 + "P0");
                Debug.Log(p1 + "P1");
                GameObject aa = Instantiate(testCube,new Vector3(p0.x, p0.y+0.1f, p0.z),Quaternion.identity);
                GameObject bb = Instantiate(testCube1,new Vector3(p1.x, p1.y + 0.1f, p1.z),Quaternion.identity);
                GameObject cc = Instantiate(testCube1,new Vector3(p01.x, p01.y + 0.1f, p01.z),Quaternion.identity);
                cc.transform.localScale = new Vector3(p01.x, p01.y + 0.1f, p01.z);
                aa.name = "p0";
                bb.name = "p1";
                cc.name = "cc";
                //linePoint.Add(p1);
                //if (!fineCenter)
                //{
                //    centerPosition = p0;
                //    fineCenter = true;
                //}
                //if (linePoint0.Count <= 3)
                //{
                //    linePoint0.Add(p0);
                //    linePoint0.Add(p1);
                //}
                //else if (linePoint0.Count >= 4 && linePoint1.Count <= 3)
                //{
                //    linePoint1.Add(p0);
                //    linePoint1.Add(p1);
                //}
                //else if (linePoint1.Count >= 4 && linePoint2.Count <= 3)
                //{
                //    linePoint2.Add(p0);
                //    linePoint2.Add(p1);
                //}
            }
        }
    }

    void CaculateS()
    {

    }
    //void CaculateS()
    //{
    //    var s0 = (linePoint0[0].x * linePoint0[1].y - linePoint0[1].x *
    //   linePoint0[0].y + linePoint0[1].x * linePoint0[2].y - linePoint0[2].x *
    //   linePoint0[1].y + linePoint0[3].x * linePoint0[0].y - linePoint0[0].x *
    //   linePoint0[3].y) / 2;
    //    var s1 = (linePoint1[0].x * linePoint1[1].y - linePoint1[1].x *
    //       linePoint1[0].y + linePoint1[1].x * linePoint1[2].y - linePoint1[2].x *
    //       linePoint1[1].y + linePoint1[3].x * linePoint1[0].y - linePoint1[0].x *
    //       linePoint1[3].y) / 2;
    //    var s2 = (linePoint2[0].x * linePoint2[1].y - linePoint2[1].x *
    //       linePoint2[0].y + linePoint2[1].x * linePoint2[2].y - linePoint2[2].x *
    //       linePoint2[1].y + linePoint2[3].x * linePoint2[0].y - linePoint2[0].x *
    //       linePoint2[3].y) / 2;
    //    Debug.Log(s0 + "S0");
    //    Debug.Log(s1 + "S1");
    //    Debug.Log(s2 + "S2");
    //}
    public void GenerateMap()
    {
        var noiseColors = CreateMapShape(size, noiseFrequency, noiseOctave);

        noiseMapRenderer.sprite = mapDraw.DrawSprite(size, noiseColors);
    }

    private Voronoi GenerateVoronoi(Vector2 size, int nodeAmount, int lloydIterationCount)
    {
        centroids = new List<Vector2>();

        // 무게 중심을 nodeAmount만큼 생성
        for (int i = 0; i < nodeAmount; ++i)
        {
            float x = Random.Range(0, wolrdRect.width);
            float y = Random.Range(0, wolrdRect.height);

            centroids.Add(new Vector2(x, y));
            tileObject = Instantiate(hexagon, new Vector3(centroids[i].x, 0, centroids[i].y), Quaternion.identity, transform);
            tileObject.name = "centroid" + i;
        }

        Rect Rect = new Rect(0f, 0f, wolrdRect.width, wolrdRect.height);
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
        var mask = mapDraw.GetRadialGradientMask(size, noiseMaskRadius);
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
                if (color > 0)
                {
                    if (x % 2 == 0)
                    {
                        tileObject = Instantiate(hexagon, new Vector3(x * 1.5f, 0, y * Mathf.Sqrt(3)), Quaternion.identity, transform);
                        Tile tile = tileObject.GetComponent<Tile>();
                        tile.position = new Vector3Int(Convert.ToInt32(tileObject.transform.position.x), 0, Convert.ToInt32(tileObject.transform.position.z));
                        tileObject.name = "Tile" + tileNum;
                        tileObjectList.Add(tileObject);
                        //if (tileNum > 1)
                        //{
                        //    if (tileObjectList[wolrdRectNum].transform.localPosition.x > tileObjectList[wolrdRectNum - 1].transform.localPosition.x)
                        //    {
                        //        wolrdRectx = tileObjectList[wolrdRectNum].transform.localPosition.x;                              
                        //    }
                        //    else
                        //    {
                        //        wolrdRectx = tileObjectList[wolrdRectNum - 1].transform.localPosition.x;
                        //    }
                        //    if (tileObjectList[wolrdRectNum].transform.localPosition.z > tileObjectList[wolrdRectNum - 1].transform.localPosition.z)
                        //    {
                        //        wolrdRecty = tileObjectList[wolrdRectNum].transform.localPosition.z;
                        //    }
                        //    else
                        //    {
                        //        wolrdRecty = tileObjectList[wolrdRectNum - 1].transform.localPosition.z;
                        //    }
                        //}
                        tileNum += 1;
                    }
                    else
                    {
                        tileObject = Instantiate(hexagon, new Vector3(x * 1.5f, 0, y * Mathf.Sqrt(3) + Mathf.Sqrt(3) / 2), Quaternion.identity, transform);
                        Tile tile = tileObject.GetComponent<Tile>();
                        tile.position = new Vector3Int(Convert.ToInt32(tileObject.transform.position.x), 0, Convert.ToInt32(tileObject.transform.position.z));
                        tileObject.name = "Tile" + tileNum;
                        tileObjectList.Add(tileObject);
                        //if (tileNum > 1)
                        //{
                        //    if (tileObjectList[wolrdRectNum].transform.localPosition.x > tileObjectList[wolrdRectNum - 1].transform.localPosition.x)
                        //    {
                        //        wolrdRectx = tileObjectList[wolrdRectNum].transform.position.x;
                        //    }
                        //    else
                        //    {
                        //        wolrdRectx = tileObjectList[wolrdRectNum - 1].transform.localPosition.x;
                        //    }
                        //    if (tileObjectList[wolrdRectNum].transform.localPosition.z > tileObjectList[wolrdRectNum - 1].transform.localPosition.z)
                        //    {
                        //        wolrdRecty = tileObjectList[wolrdRectNum].transform.localPosition.z;
                        //    }
                        //    else
                        //    {
                        //        wolrdRecty = tileObjectList[wolrdRectNum - 1].transform.localPosition.z;
                        //    }
                        //}
                        tileNum += 1;
                        wolrdRectNum += 1;
                    }
                }
                ++index;
            }
        }
        return colorDatas;
    }


    public void ChangeScene(int mapNum)
    {
        SceneManager.LoadScene(mapNum);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(22.5f, 0, 22.5f), new Vector3(wolrdRectx, 0, wolrdRecty));
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(22.2f, 0, 24.2f), new Vector3(wolrdRect.width, 0, wolrdRect.height));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(0, 0, 0), new Vector3(size.x, 0, size.y));
        //Gizmos.color = Color.black;
        //TestClimate();
    }
}