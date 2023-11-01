using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile_Building_Type : MonoBehaviour
{
    [SerializeField] List<GameObject> monsters;
    [SerializeField] Transform monsterTrasnform;

    public Building_Type building_Type;
    public enum Building_Type
    {
        MonsterTile,
        BossTile,
        KingdomTile,
        VillageTile,
        EventTile
    };

    private void Awake()
    {
        if(monsters == null)
        {
            monsters = null;
        }
    }


}
