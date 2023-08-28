using csDelaunay;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Map : MonoBehaviour
{
    [SerializeField]
    private Vector2Int size;
    [SerializeField]
    private int nodeAmount = 0;
    [SerializeField]
    private SpriteRenderer voronoiMapRenderer = null;
    [SerializeField]
    private int lloydIterationCount = 0;
    [SerializeField, Range(0f, 0.4f)]
    private float noiseFrequency = 0;
    [SerializeField]
    private int noiseOctave = 0;
    [SerializeField]
    private int noiseMaskRadius = 0;
    [SerializeField, Range(0f, 0.5f)]
    private float landNoiseThreshold = 0;
    [SerializeField]
    private SpriteRenderer noiseMapRenderer = null;
    [SerializeField]
    private bool lockSeed = true;
    [SerializeField]
    private int _seed = 0;

    public GameObject hexagon;

    private void Awake()
    {
        var voronoi = GenerateVoronoi(size, nodeAmount, lloydIterationCount);
        voronoiMapRenderer.sprite = MapDraw.DrawVoronoiToSprite(voronoi);

        GenerateMap();
    }

    public void GenerateMap()
    {
        var noiseColors = CreateMapShape(
            size,
            noiseFrequency,
            noiseOctave);

        noiseMapRenderer.sprite = MapDraw.DrawSprite(size, noiseColors);

    }

    private Voronoi GenerateVoronoi(Vector2Int size, int nodeAmount, int lloydIterationCount)
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
        var voronoi = new Voronoi(centroids, Rect, lloydIterationCount);
        return voronoi;
    }

    private float[] CreateMapShape(Vector2Int size, float frequency, int octave)
    {
        var seed = (_seed == 0) ? Random.Range(1, int.MaxValue) : _seed;

        var noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);
        noise.SetFractalType(FastNoiseLite.FractalType.FBm);
        noise.SetFrequency(frequency);
        noise.SetFractalOctaves(octave);
        noise.SetSeed(seed);

        var mask = MapDraw.GetRadialGradientMask(size, noiseMaskRadius);
        var colorDatas = new float[size.x * size.y];
        var index = 0;
        for (int y = 0; y < size.y; ++y)
        {
            for (int x = 0; x < size.x; ++x)
            {
                var noiseColorFactor = noise.GetNoise(x, y);
                noiseColorFactor = (noiseColorFactor + 1) * 0.5f;
                noiseColorFactor *= mask[index];
                var color = noiseColorFactor > landNoiseThreshold ? 1f : 0f;
                colorDatas[index] = color;
                ++index;
            }
        }
        return colorDatas;
    }

    private void OnValidate()
    {
        GenerateMap();
    }
}