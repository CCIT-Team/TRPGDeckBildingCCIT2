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
    [SerializeField] int wolrdRectx = 0;
    [SerializeField] int wolrdRecty = 0;
    Voronoi voronoi;

    [Header("Climate")]
    public GameObject grass;
    public GameObject desert;
    public GameObject jungle;

    [Header("TileObject")]
    GameObject tileObject;
    public GameObject hexagon;
    public GameObject readingStick;
    [HideInInspector] public Tile startTile;
    [HideInInspector] public Tile dragonStartTile;
    public List<GameObject> totalTileObjectList;
    //public List<GameObject> grassTileObjectList;
    //public List<GameObject> desertTileObjectList;
    //public List<GameObject> junglelTileObjectList;
    public List<Tile> pathTileObjectList;
    public List<Tile> kingdomTile;
    public List<Tile> monsterTile;
    int tileNum = 0;
    [Header("Player")]
    public List<GameObject> players = new List<GameObject>();
    public RuntimeAnimatorController[] wolrdPlayerAnimator = new RuntimeAnimatorController[3];
    public bool isOutofUI = false;
    [SerializeField] float playerSpeed = 0.05f;
    [Header("Monster")]
    public Dragon dragonScript;
    public GameObject dragon;
    public GameObject instantiateDragon;
    public List<GameObject> monsterList;
    public List<int> monsterIDList;
    [Header("Map UI")]
    public MapUI mapUI;
    public WolrdTurn wolrdTurn;
    public Tile currentInteracteUITile;
    public WolrdMission wolrdMission;
    public Tile currentMissionTile;
    public int missionNum = 0;

    [Header("Other")]
    public bool isBattle = false;
    public bool isFirst = true;

    public float playerMoveSpeed;
    public bool isPlayerOnEndTile = false;
    public bool isPlayerMoving = false;


    private void Awake()
    {
        if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
        if (GameManager.instance.map == null) { GameManager.instance.map = gameObject; }
        if (isFirst)
        {
            GenerateMap();
            isFirst = false;
        }
        wolrdRect = new Rect(22.5f, 24, wolrdRectx, wolrdRecty);
        size = new Vector2Int(Mathf.RoundToInt(wolrdRectx), Mathf.RoundToInt(wolrdRecty));
        voronoi = GenerateVoronoi(size, nodeAmount, lloydIterationCount);
        voronoiMapRenderer.sprite = mapDraw.DrawVoronoiToSprite(voronoi);
        GameManager.instance.GetLoadAvatar(
            new Vector3(totalTileObjectList[0].transform.position.x,
            totalTileObjectList[0].transform.position.y + 0.5f,
            totalTileObjectList[0].transform.position.z));
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        for (int i = 0; i < players.Count; i++)
        {
            players[i].name = players[i].GetComponent<Character_type>().nickname;
            if(players[i].GetComponent<Character_type>().major == PlayerType.Major.Fighter)
            {
                players[i].GetComponent<Animator>().runtimeAnimatorController = wolrdPlayerAnimator[0];
            }
            if (players[i].GetComponent<Character_type>().major == PlayerType.Major.Wizard)
            {
                players[i].GetComponent<Animator>().runtimeAnimatorController = wolrdPlayerAnimator[1];
            }
            if (players[i].GetComponent<Character_type>().major == PlayerType.Major.Cleric)
            {
                players[i].GetComponent<Animator>().runtimeAnimatorController = wolrdPlayerAnimator[2];
            }
        }
        MapSetting();
    }

    private void Start()
    {
        mapUI.SetTurnSlider(players);
    }

    #region 맵 이동시 함수
    public void ReSearchPlayer()
    {
        players.Clear();
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
    }
    #endregion
    #region 타일 UI 제어
    public void OnUIPlayerStop()
    {
        startTile = null;
        currentPositionNum = 1;
        pathTileObjectList.Clear();
        isPlayerOnEndTile = true;
        isPlayerMoving = false;
        wolrdTurn.currentPlayer.GetComponent<Animator>().SetBool("IsWalk", false);
    }

    public string GetMonsterName(int MonsterID)
    {
        switch (MonsterID)
        {
            case 30000001:
                return "해골 병사";
            case 30000002:
                return "해골 전사";
            case 30000003:
                return "되살아난 시체";
            case 30000004:
                return "살아있는 갑옷";
            case 30000005:
                return "해골 법사";
            default:
                return "해당 ID를 가진 몬스터가 없습니다";
        }
    }
    #endregion

    void MapSetting()
    {
        //Kingdom
        totalTileObjectList[75].GetComponent<Tile>().tileState = Tile.TileState.KingdomTile;
        kingdomTile.Add(totalTileObjectList[75].GetComponent<Tile>());
        totalTileObjectList[88].GetComponent<Tile>().tileState = Tile.TileState.KingdomTile;
        kingdomTile.Add(totalTileObjectList[88].GetComponent<Tile>());
        totalTileObjectList[178].GetComponent<Tile>().tileState = Tile.TileState.KingdomTile;
        kingdomTile.Add(totalTileObjectList[178].GetComponent<Tile>());
        totalTileObjectList[221].GetComponent<Tile>().tileState = Tile.TileState.KingdomTile;
        kingdomTile.Add(totalTileObjectList[221].GetComponent<Tile>());
        totalTileObjectList[412].GetComponent<Tile>().tileState = Tile.TileState.KingdomTile;
        kingdomTile.Add(totalTileObjectList[412].GetComponent<Tile>());
        totalTileObjectList[468].GetComponent<Tile>().tileState = Tile.TileState.KingdomTile;
        kingdomTile.Add(totalTileObjectList[468].GetComponent<Tile>());
        //Monster
        totalTileObjectList[1].GetComponent<Tile>().tileState = Tile.TileState.MonsterTile;
        monsterTile.Add(totalTileObjectList[1].GetComponent<Tile>());
        totalTileObjectList[50].GetComponent<Tile>().tileState = Tile.TileState.MonsterTile;
        monsterTile.Add(totalTileObjectList[50].GetComponent<Tile>());
        totalTileObjectList[138].GetComponent<Tile>().tileState = Tile.TileState.MonsterTile;
        monsterTile.Add(totalTileObjectList[138].GetComponent<Tile>());
        totalTileObjectList[198].GetComponent<Tile>().tileState = Tile.TileState.MonsterTile;
        monsterTile.Add(totalTileObjectList[198].GetComponent<Tile>());
        totalTileObjectList[368].GetComponent<Tile>().tileState = Tile.TileState.MonsterTile;
        monsterTile.Add(totalTileObjectList[368].GetComponent<Tile>());
        totalTileObjectList[275].GetComponent<Tile>().tileState = Tile.TileState.MonsterTile;
        monsterTile.Add(totalTileObjectList[275].GetComponent<Tile>());
        totalTileObjectList[185].GetComponent<Tile>().tileState = Tile.TileState.MonsterTile;
        monsterTile.Add(totalTileObjectList[185].GetComponent<Tile>());
        totalTileObjectList[323].GetComponent<Tile>().tileState = Tile.TileState.MonsterTile;
        monsterTile.Add(totalTileObjectList[323].GetComponent<Tile>());
        totalTileObjectList[518].GetComponent<Tile>().tileState = Tile.TileState.MonsterTile;
        monsterTile.Add(totalTileObjectList[518].GetComponent<Tile>());
        totalTileObjectList[544].GetComponent<Tile>().tileState = Tile.TileState.MonsterTile;
        monsterTile.Add(totalTileObjectList[544].GetComponent<Tile>());
        totalTileObjectList[129].GetComponent<Tile>().tileState = Tile.TileState.MonsterTile;
        monsterTile.Add(totalTileObjectList[129].GetComponent<Tile>());


        totalTileObjectList[347].GetComponent<Tile>().tileState = Tile.TileState.BossTile;
        instantiateDragon = Instantiate(dragon, new Vector3(totalTileObjectList[347].gameObject.transform.position.x,
            1.5f, totalTileObjectList[347].gameObject.transform.position.z), Quaternion.identity, transform);
        dragonScript = instantiateDragon.GetComponent<Dragon>();
    }

    public void PlayerMovePath(Tile objects)
    {
        pathTileObjectList.Add(objects);
    }
    int currentPositionNum = 1;
    void MovePlayer()
    {
        if (wolrdTurn.currentPlayer.isMyturn && pathTileObjectList.Count > 0 && !dragonScript.isdragonTurn)
        {
            isPlayerMoving = true;
            wolrdTurn.currentPlayer.GetComponent<Animator>().SetBool("IsWalk", true);
            Vector3 nextTilePosition = pathTileObjectList[currentPositionNum].gameObject.transform.position;
            nextTilePosition.y = nextTilePosition.y + .5f;

            wolrdTurn.currentPlayer.transform.rotation = Quaternion.LookRotation(nextTilePosition - wolrdTurn.currentPlayer.transform.position).normalized;
            wolrdTurn.currentPlayer.transform.position =
                Vector3.MoveTowards(wolrdTurn.currentPlayer.transform.position,
                nextTilePosition, playerSpeed);

            if (Vector3.Distance(pathTileObjectList[currentPositionNum].transform.position, wolrdTurn.currentPlayer.transform.position) <= 0.5f && isPlayerMoving)
            {
                if (currentPositionNum < pathTileObjectList.Count) { currentPositionNum += 1; }
                else { currentPositionNum = pathTileObjectList.Count - 1; }
                isPlayerMoving = false;
            }
        }

        if (pathTileObjectList.Count > 0 && Vector3.Distance(pathTileObjectList[pathTileObjectList.Count - 1].transform.position, wolrdTurn.currentPlayer.transform.position) <= 0.5f)
        {
            if (!isOutofUI)
            {
                wolrdTurn.currentPlayer.isMyturn = false;
                startTile = null;
                currentPositionNum = 1;
                pathTileObjectList.Clear();
                isPlayerOnEndTile = true;
                isPlayerMoving = false;
                wolrdTurn.currentPlayer.GetComponent<Animator>().SetBool("IsWalk", false);
            }
        }
    }
    int movePoint;
    int currentPoint = -1;
    private void Update()
    {
        MovePlayer();
    }

    public void GenerateMap()
    {
        var noiseColors = CreateMapShape(size, noiseFrequency, noiseOctave);

        noiseMapRenderer.sprite = mapDraw.DrawSprite(size, noiseColors);
    }

    //void TestClimate()
    //{
    //    foreach (var site in voronoi.Sites)
    //    {
    //        var neighbors = site.NeighborSites();
    //        foreach (var neighbor in neighbors)
    //        {
    //            var edge = voronoi.FindEdgeFromAdjacentPolygons(site, neighbor);

    //            if (edge.ClippedVertices is null)
    //            {
    //                continue;
    //            }

    //            Vector2 corner1 = edge.ClippedVertices[LR.LEFT];
    //            Vector2 corner2 = edge.ClippedVertices[LR.RIGHT];


    //            Vector3 p0 = new Vector3(corner1.x, 0, corner1.y);
    //            Vector3 p1 = new Vector3(corner2.x, 0, corner2.y);
    //            Vector3 p01 = p1 - p0;
    //            //Gizmos.color = Color.black;
    //            //Gizmos.DrawLine(p0, p1);
    //            Debug.Log(p0 + "P0");
    //            Debug.Log(p1 + "P1");
    //            GameObject aa = Instantiate(new GameObject("p0"), new Vector3(p0.x, p0.y + 0.1f, p0.z), Quaternion.identity);
    //            aa.AddComponent(typeof(BoxCollider));
    //            aa.GetComponent<BoxCollider>().size = (p1 - p0).normalized;
    //            GameObject bb = Instantiate(new GameObject("p1"), new Vector3(p1.x, p1.y + 0.1f, p1.z), Quaternion.identity);
    //            bb.AddComponent(typeof(BoxCollider));
    //            bb.GetComponent<BoxCollider>().size = (p1 - p0).normalized;
    //        }
    //    }
    //}
    private Voronoi GenerateVoronoi(Vector2 size, int nodeAmount, int lloydIterationCount)
    {
        centroids = new List<Vector2>();

        // 무게 중심을 nodeAmount만큼 생성
        for (int i = 0; i < nodeAmount; ++i)
        {
            float x = Random.Range(0, size.x);
            float y = Random.Range(0, size.y);

            centroids.Add(new Vector2(x, y));
        }

        Rect Rect = new Rect(0, 0, size.x, size.y);
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
                        tile.position = new Vector3Int(x, 0, y);
                        tileObject.name = "Tile" + tileNum;
                        totalTileObjectList.Add(tileObject);
                        if (wolrdRectx < totalTileObjectList[tileNum].transform.position.x)
                        {
                            wolrdRectx = Convert.ToInt32(totalTileObjectList[tileNum].transform.position.x);
                        }
                        if (wolrdRecty < totalTileObjectList[tileNum].transform.position.z)
                        {
                            wolrdRecty = Convert.ToInt32(totalTileObjectList[tileNum].transform.position.z);
                        }
                        tileNum += 1;
                    }
                    else
                    {
                        tileObject = Instantiate(hexagon, new Vector3(x * 1.5f, 0, y * Mathf.Sqrt(3) + Mathf.Sqrt(3) / 2), Quaternion.identity, transform);
                        Tile tile = tileObject.GetComponent<Tile>();
                        tile.position = new Vector3Int(x, 0, y);
                        tileObject.name = "Tile" + tileNum;
                        totalTileObjectList.Add(tileObject);
                        if (wolrdRectx < totalTileObjectList[tileNum].transform.position.x)
                        {
                            wolrdRectx = Convert.ToInt32(totalTileObjectList[tileNum].transform.position.x);
                        }
                        if (wolrdRecty < totalTileObjectList[tileNum].transform.position.z)
                        {
                            wolrdRecty = Convert.ToInt32(totalTileObjectList[tileNum].transform.position.z);
                        }
                        tileNum += 1;
                    }
                }
                ++index;
            }
        }
        return colorDatas;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(wolrdRect.x, 0, wolrdRect.y), new Vector3(size.x, 0, size.y));
    }
}