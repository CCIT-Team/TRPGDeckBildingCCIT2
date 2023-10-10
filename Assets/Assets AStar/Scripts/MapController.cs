
using System;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public Action<Cell> OnStartCellSelect;
    public Action<Cell> OnEndCellSelect;
    public Action<IList<Cell>> OnPathFind;

    [SerializeField] private CellSelector m_cellSelector;
    [SerializeField] private CellAssets m_prefabs;
    [SerializeField] private Vector2Int mapSize;
    [SerializeField] private IDictionary<Vector2, Cell> cells;
    [SerializeField] private Map1 groundMap;
    [SerializeField] private Dictionary<Vector2, CellView> _cellsView;
    public PathFinder _pathFinder;
    [SerializeField] private Cell _cellStart;
    [SerializeField] private Cell _cellEnd;

    private void Awake()
    {
        groundMap = GetComponent<Map1>();
        _pathFinder = GetComponent<PathFinder>();
    }
    private void Start()
    {
        if (_pathFinder == null) { _pathFinder = GetComponent<PathFinder>(); }
        m_cellSelector.OnStartPoint += OnSetPointStart;
        m_cellSelector.OnEndPoint += OnSetPointEnd;
        _cellsView = new Dictionary<Vector2, CellView>();

        cells = groundMap.GetCells();
        mapSize = groundMap.GetMapSize();
        int a = 0;
        foreach (var cellPair in cells)
        {
            Vector2 point = cellPair.Key;
            Cell cell = cellPair.Value;
            CellPrefab prefabItem = m_prefabs.GetRandomPrefab(!cell.IsWall);
            Vector3 position = HexCoords2.GetHexVisualCoords(point, mapSize);
            GameObject go = Instantiate(prefabItem.Prefab, position, Quaternion.Euler(new Vector3(0,90,0)), groundMap.tileObject[a].transform);
            CellView cellView = go.GetComponent<CellView>();
            cellView.SetPoint(point);
            _cellsView[point] = cellView;
            a += 1;
        }
        //var point = cellPair.Key;
        //var cell = cellPair.Value;
        //Debug.Log(cell);
        //var prefabItem = m_prefabs.GetRandomPrefab(!cell.IsWall);
        //var position = HexCoords2.GetHexVisualCoords(point, mapSize);
        //var go = Instantiate(prefabItem.Prefab, position, Quaternion.identity, transform);
        ////var go = Instantiate(groundMap.hexagon, position, Quaternion.identity);
        //var cellView = go.GetComponent<CellView>();
        //var cells = go.GetComponent<Cell>();
        //cellView.SetPoint(point);
        //_cellsView[point] = cellView;
        //var go = Instantiate(groundMap.hexagon, position, Quaternion.identity);
        //groundMap.hexatile.transform.SetParent(transform);
        //var cellView = groundMap.GetComponentInChildren<CellView>();
        //var cells = groundMap.GetComponentInChildren<Cell>();
        // cellView.SetPoint(point);
        // _cellsView[point] = cellView;
    }

    public Vector2Int GetMapSize() => groundMap.GetMapSize();

    void OnSetPointStart(Vector2 point)
    {
        _cellStart = groundMap.GetCell(point);
        OnStartCellSelect?.Invoke(_cellStart);
        Calculate();
    }

    void OnSetPointEnd(Vector2 point)
    {
        _cellEnd = groundMap.GetCell(point);
        OnEndCellSelect?.Invoke(_cellEnd);
        Calculate();
    }

    void Calculate()
    {
        IList<Cell> path = _pathFinder.FindPathOnMap(_cellStart, _cellEnd, groundMap);
        OnPathFind?.Invoke(path);
    }

    private void OnDestroy()
    {
        m_cellSelector.OnStartPoint -= OnSetPointStart;
        m_cellSelector.OnEndPoint -= OnSetPointEnd;
    }
}
