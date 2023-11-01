using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaChecker : MonoBehaviour
{
    public int climateNum;
    BoxCollider boxCol;
    void Awake()
    {
        boxCol = gameObject.GetComponent<BoxCollider>();
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
                        other.transform.parent = gameObject.transform;
                        Map.instance.grassTileObjectList.Add(other.gameObject);
                        break;
                    case 1:
                        other.GetComponent<Tile>().climate = Tile.Climate.DESERT;
                        other.GetComponent<MeshRenderer>().material = other.GetComponent<Tile>().climateMaterials[1];
                        other.transform.parent = gameObject.transform;
                        Map.instance.desertTileObjectList.Add(other.gameObject);
                        break;
                    case 2:
                        other.GetComponent<Tile>().climate = Tile.Climate.JUNGLE;
                        other.GetComponent<MeshRenderer>().material = other.GetComponent<Tile>().climateMaterials[2];
                        other.transform.parent = gameObject.transform;
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
