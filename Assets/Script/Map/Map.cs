using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using csDelaunay;

public class Map : MonoBehaviour
{
    [SerializeField]
    private Vector2Int size;

    [SerializeField]
    private int nodeAmount = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Voronoi GenerateVoronoi(Vector2Int size, int nodeAmount)
    {
        var centroids = new List<Vector2>();

        // 무게 중심을 nodeAmount만큼 생성
        for (var i = 0; i < nodeAmount; ++i)
        {
            var x = Random.Range(0, size.x);
            var y = Random.Range(0, size.y);

            centroids.Add(new Vector2(x, y));
        }

        var Rect = new Rect(0f, 0f, size.x, size.y);
        var voronoi = new Voronoi(centroids, Rect);
        return voronoi;
    }
}
