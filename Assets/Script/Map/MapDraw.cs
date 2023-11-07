using csDelaunay;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;
using System;

public class MapDraw : MonoBehaviour
{
    public List<Vector2> siteList;
    public Sprite DrawVoronoiToSprite(Voronoi voronoi)
    {
        Rect rect = voronoi.PlotBounds;
        int width = Mathf.RoundToInt(rect.width);
        int height = Mathf.RoundToInt(rect.height);
        Color[] pixelColors = Enumerable.Repeat(Color.white, width * height).ToArray();
        var siteCoords = voronoi.SiteCoords();
        siteList = siteCoords;
        int a = 0;
        // �����߽� �׸���
        foreach (var coord in siteCoords)
        {
            var x = Mathf.RoundToInt(coord.x);
            var y = Mathf.RoundToInt(coord.y);

            var index = x * width + y;
            pixelColors[index] = Color.red;
            GameObject tileObject = Instantiate(Map.instance.readingStick, new Vector3(siteList[a].x, 0, siteList[a].y), Quaternion.identity);
            a += 1;
        }

        // �𼭸� �׸���

        // ��� �������� ���´�.
        foreach (var site in voronoi.Sites)
        {
            // �� �������� ��� �̿� �������� ���´�.
            var neighbors = site.NeighborSites();
            foreach (var neighbor in neighbors)
            {
                // �̿��� ������鿡�Լ� ��ġ�� �����ڸ�(edge)�� �����س���.
                var edge = voronoi.FindEdgeFromAdjacentPolygons(site, neighbor);

                if (edge.ClippedVertices is null)
                {
                    continue;
                }

                // �����ڸ��� �̷�� �𼭸� ����(vertex) 2���� ���´�.
                var corner1 = edge.ClippedVertices[LR.LEFT];
                var corner2 = edge.ClippedVertices[LR.RIGHT];

                // 1�� �Լ��� �׷����� �׸����� �ؽ��Ŀ� �����ڸ� ������ �׸���.
                var targetPoint = corner1;
                var delta = 1 / (corner2 - corner1).magnitude;
                var lerpRatio = 0f;

                while ((int)targetPoint.x != (int)corner2.x ||
                    (int)targetPoint.y != (int)corner2.y)
                {
                    targetPoint = Vector2.Lerp(corner1, corner2, lerpRatio);
                    lerpRatio += delta;

                    var x = Mathf.Clamp((int)targetPoint.x, 0, width - 1);
                    var y = Mathf.Clamp((int)targetPoint.y, 0, height - 1);

                    var index = x * width + y;
                    pixelColors[index] = Color.black;
                }
            }
        }

        var size = new Vector2Int(width, height);
        return DrawSprite(size, pixelColors);
    }

    public Sprite DrawSprite(Vector2Int size, float[] monochromeColorDatas)
    {
        var colors = monochromeColorDatas.Select(c => new Color(c, c, c)).ToArray();
        return DrawSprite(size, colors);
    }

    public Sprite DrawSprite(Vector2Int size, Color[] colorDatas)
    {
        var texture = new Texture2D(size.x, size.y);
        texture.filterMode = FilterMode.Point;
        texture.SetPixels(colorDatas);
        texture.Apply();

        var rect = new Rect(0, 0, size.x, size.y);
        var sprite = Sprite.Create(texture, rect, Vector2.one * 0.5f);
        return sprite;
    }

    public float[] GetRadialGradientMask(Vector2Int size, int maskRadius)
    {
        var colorData = new float[size.x * size.y];

        var center = size / 2;
        var radius = center.y;
        var index = 0;
        for (int y = 0; y < size.y; ++y)
        {
            for (int x = 0; x < size.x; ++x)
            {
                var position = new Vector2Int(x, y);
                var distFromCenter = Vector2Int.Distance(center, position) + (radius - maskRadius);
                var colorFactor = distFromCenter / radius;
                colorData[index++] = 1 - colorFactor;
            }
        }
        return colorData;
    }

    public void DrawVoronoi(Voronoi voronoi)
    {
        Rect rect = voronoi.PlotBounds;
        int width = Mathf.RoundToInt(rect.width);
        int height = Mathf.RoundToInt(rect.height);
        Color[] pixelColors = Enumerable.Repeat(Color.white, width * height).ToArray();
        var siteCoords = voronoi.SiteCoords();
        siteList = siteCoords;

        // �����߽� �׸���
        foreach (var coord in siteCoords)
        {
            var x = Mathf.RoundToInt(coord.x);
            var y = Mathf.RoundToInt(coord.y);

            var index = x * width + y;
            pixelColors[index] = Color.red;
        }

        // �𼭸� �׸���

        // ��� �������� ���´�.
        foreach (var site in voronoi.Sites)
        {
            // �� �������� ��� �̿� �������� ���´�.
            var neighbors = site.NeighborSites();
            foreach (var neighbor in neighbors)
            {
                // �̿��� ������鿡�Լ� ��ġ�� �����ڸ�(edge)�� �����س���.
                var edge = voronoi.FindEdgeFromAdjacentPolygons(site, neighbor);

                if (edge.ClippedVertices is null)
                {
                    continue;
                }

                // �����ڸ��� �̷�� �𼭸� ����(vertex) 2���� ���´�.
                var corner1 = edge.ClippedVertices[LR.LEFT];
                var corner2 = edge.ClippedVertices[LR.RIGHT];

                // 1�� �Լ��� �׷����� �׸����� �ؽ��Ŀ� �����ڸ� ������ �׸���.
                var targetPoint = corner1;
                var delta = 1 / (corner2 - corner1).magnitude;
                var lerpRatio = 0f;

                while ((int)targetPoint.x != (int)corner2.x ||
                    (int)targetPoint.y != (int)corner2.y)
                {
                    targetPoint = Vector2.Lerp(corner1, corner2, lerpRatio);
                    lerpRatio += delta;

                    var x = Mathf.Clamp((int)targetPoint.x, 0, width - 1);
                    var y = Mathf.Clamp((int)targetPoint.y, 0, height - 1);

                    var index = x * width + y;
                    pixelColors[index] = Color.black;
                }
            }
        }

        var size = new Vector2Int(width, height);
    }
}
