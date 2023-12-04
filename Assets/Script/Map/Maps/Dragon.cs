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
    public Animator dragonAni;

    public AStarPathfinding astar = new AStarPathfinding();

    bool isTargetSetting = false;
    bool isDestoyVillige = false;
    public bool isdragonTurn = false;
    bool isDragonNest = false;
    bool isDragonAbleMove = false;
    bool isBack = false;

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
        currentDragonTile = Map.instance.dragonStartTile;
    }


    void Update()
    {
        if (currentDragonTile == null)
        {
            currentDragonTile = Map.instance.dragonStartTile;
            if (isDragonAbleMove)
            {
                TargetSetting();
            }
        }
        //if (isDragonAbleMove && Map.instance.wolrdTurn.turnNum % 12 == 0 && Map.instance.wolrdTurn.turnNum > Map.instance.wolrdTurn.turnNum - 1)
        //{
        //    isdragonTurn = true;
        //    MoveDragon();
        //}

        //if (isDragonNest)
        //{
        //    RestNest();
        //}
        if (Map.instance.wolrdMission.mainMissionNum == 9)
        {
            if (!isTargetSetting && currentDragonTile != null && Map.instance.totalTileObjectList[216].GetComponent<Tile>() != null)
            {
                targetPosition = Map.instance.totalTileObjectList[216].GetComponent<Tile>();
                moveList = astar.FindPath(currentDragonTile, targetPosition);
                dragonState = DragonState.IDLE;
                dragonAni.SetTrigger("Idle");
                if (moveList.Count > 0) { isTargetSetting = true; isdragonTurn = true; Debug.Log("Mission Start!"); }
            }
            SpatialMove();
        }
        if (isBack)
        {
            BacktoNestAndReadytoStart();
        }

    }
    #region 미션
    public void SpatialMove()//미션용
    {
        if (isdragonTurn && moveList.Count > 0)
        {
            Debug.Log("Flydragon!");
            dragonState = DragonState.FLYING;
            dragonAni.SetTrigger("Fly");
            Vector3 nextTilePosition = targetPosition.transform.position;
            nextTilePosition.y = nextTilePosition.y + 2f;

            transform.rotation = Quaternion.LookRotation(nextTilePosition - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, nextTilePosition, dragonSpeed);
        }
        //else if (moveList.Count == 0 && !isTargetSetting)
        //{
        //    targetPosition = Map.instance.totalTileObjectList[216].GetComponent<Tile>();
        //    moveList = astar.FindPath(currentDragonTile, targetPosition);
        //}

        if (moveList.Count > 0 && Vector3.Distance(moveList[moveList.Count - 1].transform.position, transform.position) <= 2f)
        {
            currentPositionNum = 1;
            dragonState = DragonState.BURNING;
            dragonAni.SetTrigger("Burn");
            moveList[moveList.Count - 1].GetComponent<Tile>().DestroyVilege();
            moveList.Clear();
            currentDragonTile = Map.instance.dragonStartTile;
            Map.instance.wolrdMission.mainMissionNum = 10;
            targetPosition = nestTile;
            moveList = astar.FindPath(currentDragonTile, nestTile);
            Map.instance.wolrdMission.mainMissionNum = 11;
            StartCoroutine(TurnEndDragon());
        }
    }
    void BacktoNestAndReadytoStart()
    {
        dragonState = DragonState.FLYING;
        dragonAni.SetTrigger("Fly");
        Vector3 nextTilePosition = targetPosition.transform.position;
        nextTilePosition.y = nextTilePosition.y + 2f;
        transform.rotation = Quaternion.LookRotation(nextTilePosition - transform.position).normalized;
        transform.position = Vector3.MoveTowards(transform.position, nextTilePosition, dragonSpeed);

        if (moveList.Count > 0 && Vector3.Distance(moveList[moveList.Count - 1].transform.position, transform.position) <= 2f)
        {
            currentPositionNum = 1;
            moveList.Clear();
            currentDragonTile = Map.instance.dragonStartTile;
            isDragonAbleMove = true;
            isTargetSetting = false;
            isBack = false;
        }
    }
    #endregion
    void TargetSetting()
    {
        if (!isTargetSetting && currentDragonTile != null)
        {
            targetPosition = Map.instance.kingdomTile[Random.Range(0, Map.instance.kingdomTile.Count)];
            moveList = astar.FindPath(currentDragonTile, targetPosition);
            dragonState = DragonState.IDLE;
            dragonAni.SetTrigger("Idle");
            if (moveList.Count > 0) { isTargetSetting = true; Debug.Log("TargetSetting Complete!"); }
        }
    }

    void TargetSettingNestTile()
    {
        if (!isTargetSetting && currentDragonTile != null)
        {
            targetPosition = nestTile;
            moveList = astar.FindPath(currentDragonTile, targetPosition);
            dragonState = DragonState.IDLE;
            dragonAni.SetTrigger("Idle");
            if (moveList.Count > 0) { isTargetSetting = true; Debug.Log("NestTile Path Complete!"); }
        }
    }

    void MoveDragon()
    {
        if (isdragonTurn && moveList.Count > 0)
        {
            Debug.Log("Flydragon!");
            dragonState = DragonState.FLYING;
            dragonAni.SetTrigger("Fly");
            Vector3 nextTilePosition = moveList[currentPositionNum].transform.position;
            nextTilePosition.y = nextTilePosition.y + 2f;

            transform.rotation = Quaternion.LookRotation(nextTilePosition - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, nextTilePosition, dragonSpeed);

            Debug.Log(Vector3.Distance(moveList[currentPositionNum].transform.position, transform.position) + "Distance Dragon");
            if (Vector3.Distance(moveList[currentPositionNum].transform.position, transform.position) <= 2f)
            {
                if (currentPositionNum < moveList.Count) { currentPositionNum += 1; } //Map.instance.wolrdTurn.turnNum += 1; }
                else { currentPositionNum = moveList.Count - 1; } //Map.instance.wolrdTurn.turnNum += 1; }
                currentDragonTile = Map.instance.dragonStartTile;
            }
        }
        else if (moveList.Count == 0 && !isTargetSetting)
        {
            TargetSetting();
        }

        if (moveList.Count > 0 && Vector3.Distance(moveList[moveList.Count - 1].transform.position, transform.position) <= 2f)
        {
            currentPositionNum = 1;
            Burning();
        }
    }

    void Burning()
    {
        if (buringCount < 4)
        {
            Debug.Log("Burning!!!!");
            Map.instance.wolrdTurn.turnNum += 1;
            buringCount += 1;
            dragonState = DragonState.BURNING;
            dragonAni.SetTrigger("Burn");
            moveList[moveList.Count - 1].GetComponent<Tile>().DestroyKingdom();
            moveList.Clear();
            currentDragonTile = Map.instance.dragonStartTile;
            isTargetSetting = false;
            StartCoroutine(TurnEndDragon());
        }
        else
        {
            Debug.Log("NestTime");
            moveList.Clear();
            currentDragonTile = Map.instance.dragonStartTile;
            TargetSettingNestTile();
            Rest();
        }
    }

    void Rest()
    {
        if (moveList.Count > 0)
        {
            Debug.Log("Back Nest dragon!");
            dragonState = DragonState.FLYING;
            dragonAni.SetTrigger("Fly");
            Vector3 nextTilePosition = moveList[currentPositionNum].transform.position;
            nextTilePosition.y = nextTilePosition.y + 1f;

            transform.rotation = Quaternion.LookRotation(nextTilePosition - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, nextTilePosition, dragonSpeed);

            Debug.Log(Vector3.Distance(moveList[currentPositionNum].transform.position, transform.position) + "Distance Dragon");
            if (Vector3.Distance(moveList[currentPositionNum].transform.position, transform.position) <= 1f)
            {
                if (currentPositionNum < moveList.Count) { currentPositionNum += 1; } //Map.instance.wolrdTurn.turnNum += 1; }
                else { currentPositionNum = moveList.Count - 1; } //Map.instance.wolrdTurn.turnNum += 1; }
                currentDragonTile = Map.instance.dragonStartTile;
            }
        }

        if (moveList.Count > 0 && Vector3.Distance(moveList[moveList.Count - 1].transform.position, transform.position) <= 1f)
        {
            currentPositionNum = 1;
            isDragonNest = true;
        }
    }

    void RestNest()
    {
        if (Map.instance.wolrdTurn.turnNum % 12 == 0 && Map.instance.wolrdTurn.turnNum > 23)
        {
            TargetSetting();
            isDragonNest = false;
            isTargetSetting = false;
        }
    }

    IEnumerator TurnEndDragon()
    {
        yield return new WaitForSeconds(2f);
        isdragonTurn = false;
    }
}
