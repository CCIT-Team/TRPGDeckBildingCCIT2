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
        if (other.CompareTag("Tile") && other.GetComponent<Tile>() != null)
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
                        other.GetComponent<Tile>().climate = Tile.Climate.GOLDENPLACE;
                        other.GetComponent<MeshRenderer>().material = other.GetComponent<Tile>().climateMaterials[0];
                        break;
                    case 1:
                        other.GetComponent<Tile>().climate = Tile.Climate.TEADELOSDESERT; 
                        other.GetComponent<MeshRenderer>().material = other.GetComponent<Tile>().climateMaterials[1];
                        break;
                    case 2:
                        other.GetComponent<Tile>().climate = Tile.Climate.FORGOTTENFOREST;
                        other.GetComponent<MeshRenderer>().material = other.GetComponent<Tile>().climateMaterials[2];
                        break;
                    case 3:
                        other.GetComponent<Tile>().climate = Tile.Climate.WITCHSSWAMPLAND;
                        other.GetComponent<MeshRenderer>().material = other.GetComponent<Tile>().climateMaterials[3];
                        break;
                    case 4:
                        other.GetComponent<Tile>().climate = Tile.Climate.VOLANO;
                        other.GetComponent<MeshRenderer>().material = other.GetComponent<Tile>().climateMaterials[4];
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
            Map.instance.isClimateSet = true;
        }
    }
}
