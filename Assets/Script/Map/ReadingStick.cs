using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadingStick : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tile"))
        {
            Map.instance.kingdomTile.Add(other.GetComponent<Tile>());
            other.GetComponent<Tile>().isKingdomTile = true;
            other.GetComponent<Tile>().SelectClimate(Map.instance.kingdomTile.Count);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
            other.GetComponent<Tile>().MakeKingdom();
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("This is Not Tile");
            Destroy(gameObject);
        }
    }
}
