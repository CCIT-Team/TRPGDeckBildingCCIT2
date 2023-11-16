using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    [SerializeField] float dragonSpeed = 0;
    public DragonState dragonState;
    public List<Tile> moveList;
    int currentPositionNum = 1;
    int buringCount = 0;
    public Tile nestTile;
    public Tile currentDragonTile;
    public Tile targetPosition;

    public AStarPathfinding astar = new AStarPathfinding();

    bool isTargetSetting = false;
    bool isDestoyVillige = false;
    bool isdragonTurn = false;

    public enum DragonState
    {
        IDLE,
        FLYING,
        BURNING,
        REST
    };

    void Start()
    {
        nestTile = Map.instance.totalTileObjectList[347].GetComponent<Tile>();
    }


    void Update()
    {
        if (currentDragonTile == null)
        {
            currentDragonTile = Map.instance.dragonStartTile;
            TargetSetting();
        }
        if(Map.instance.wolrdTurn.turnNum % 3 == 0 && Map.instance.wolrdTurn.turnNum > 2)
        {
            MoveDragon();
        }
    }

    void TargetSetting()
    {
        if (!isTargetSetting && currentDragonTile != null)
        {
            targetPosition = Map.instance.kingdomTile[Random.Range(0, Map.instance.kingdomTile.Count)].GetComponent<Tile>();
            moveList = astar.FindPath(currentDragonTile, targetPosition); 
            if(moveList.Count > 0){isTargetSetting = true; Debug.Log("TargetSetting Complete!"); isdragonTurn = true; StartCoroutine(DragonMoveState()); }
        }
    }

    void MoveDragon()
    {
        if (isdragonTurn && moveList.Count > 0)
        {
            Debug.Log("Flydragon!");
            dragonState = DragonState.FLYING;
            //transform.Translate(new Vector3(moveList[currentPositionNum].gameObject.transform.position.x,
            //    0, moveList[currentPositionNum].gameObject.transform.position.z) * Time.deltaTime * 0.1f, Space.Self);
            transform.position = Vector3.MoveTowards(transform.position, moveList[currentPositionNum].gameObject.transform.position, dragonSpeed);
           // transform.LookAt(moveList[currentPositionNum].transform.localPosition);
            Debug.Log(Vector3.Distance(moveList[currentPositionNum].transform.position, transform.position) + "Distance Dragon");
            if (Vector3.Distance(moveList[currentPositionNum].transform.position, transform.position) <= 0.1f)
            {
                if (currentPositionNum < moveList.Count) { currentPositionNum += 1; }
                else { currentPositionNum = moveList.Count - 1; }
                dragonState = DragonState.IDLE;
                isdragonTurn = false;
                currentDragonTile = Map.instance.dragonStartTile;
            }
        }

        if (moveList.Count > 0 && Vector3.Distance(moveList[moveList.Count - 1].transform.position, transform.position) <= 0.1f)
        {
            isdragonTurn = false;
            dragonState = DragonState.IDLE;
            currentDragonTile = null;
            currentPositionNum = 1;
            moveList.Clear();
            Burning();
        }
    }

    void Burning()
    {
        if(buringCount < 3)
        {
            Debug.Log("Burning!!!!");
            dragonState = DragonState.BURNING;
            moveList[moveList.Count - 1].GetComponent<Tile>().isKingdomTile = false;
        }
        else
        {
            Rest();
        }
    }

    void Rest()
    {
        dragonState = DragonState.FLYING;
        transform.LookAt(nestTile.transform.position);
        transform.Translate(new Vector3(nestTile.gameObject.transform.position.x,
            0, nestTile.gameObject.transform.position.z) * Time.deltaTime * 0.1f, Space.Self);
        if (moveList.Count > 0 && Vector3.Distance(moveList[moveList.Count - 1].transform.position, transform.position) <= 0.1f)
        {
            isdragonTurn = false;
            dragonState = DragonState.REST;
            currentDragonTile = null;
            currentPositionNum = 1;
            moveList.Clear();
            Burning();
        }
    }

    IEnumerator DragonMoveState()
    {
        yield return new WaitUntil(() => (Map.instance.wolrdTurn.turnNum % 3 == 0));
        MoveDragon();
    }
}
