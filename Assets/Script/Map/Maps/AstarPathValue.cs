using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class AstarPathValue : MonoBehaviour
{

    [SerializeField] private TMP_Text m_startCelllValue;
    [SerializeField] private TMP_Text m_endCelllValue;
    [SerializeField] private TMP_Text m_distanceValue;



    public void OnFindPath(List<Tile> path)
    {
        if (path != null && path.Count > 0)
        {
            m_distanceValue.text = path.Count.ToString();
        }
        else
        {
            m_distanceValue.text = "path no find";
        }
    }

    public void OnStartCellSelect(Tile tile)
    {
        m_startCelllValue.text = tile.ToString();
    }
    public void OnEndCellSelect(Tile tile)
    {
        m_endCelllValue.text = tile.ToString();
    }
}
