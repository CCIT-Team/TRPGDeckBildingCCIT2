using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChecker : MonoBehaviour
{
    public int climateNum;
    CapsuleCollider boxCol;
    void Awake()
    {
        boxCol = gameObject.GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tile"))
        {
            if (other == null)
            {
                other = null;
            }
            else
            {
                switch (climateNum)
                {
                    case 0:
                        other.GetComponent<Tile>().climate = Tile.Climate.GRASS;
                        other.GetComponent<MeshRenderer>().material = other.GetComponent<Tile>().climateMaterials[0];
                        Map.instance.grassTileObjectList.Add(other.gameObject);
                        break;
                    case 1:
                        other.GetComponent<Tile>().climate = Tile.Climate.DESERT;
                        other.GetComponent<MeshRenderer>().material = other.GetComponent<Tile>().climateMaterials[1];
                        Map.instance.desertTileObjectList.Add(other.gameObject);
                        break;
                    case 2:
                        other.GetComponent<Tile>().climate = Tile.Climate.JUNGLE;
                        other.GetComponent<MeshRenderer>().material = other.GetComponent<Tile>().climateMaterials[2];
                        Map.instance.junglelTileObjectList.Add(other.gameObject);
                        break;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Tile"))
        {
            boxCol.enabled = false;
        }
    }
}
