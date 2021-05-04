using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{

    public int Dimensions = 10;

    public Octave[] Octaves;
    public float uvscale;
    protected MeshFilter MeshFilter;
    protected Mesh Mesh;

    // Start is called before the first frame update
    void Start()
    {
        Mesh = new Mesh();
        Mesh.name = gameObject.name;

        Mesh.vertices = GenerateVerts();
        Mesh.triangles = GenerateTriangles();
        Mesh.uv = GerneateUV();
        Mesh.RecalculateBounds();
        Mesh.RecalculateNormals();

        MeshFilter = gameObject.AddComponent<MeshFilter>();
        MeshFilter.mesh = Mesh;
    }

    private Vector2[] GerneateUV()
    {
        var uv = new Vector2[Mesh.vertices.Length];
        
        for (int i = 0; i <= Dimensions; i++)
        {
            for (int j = 0; j <= Dimensions; j++)
            {
                var vec = new Vector2((i / uvscale) % 2, (j / uvscale) % 2);
                uv[ind(i, j)] = new Vector2(vec.x <= 1 ? vec.x : 2 - vec.x, vec.y <= 1 ? vec.y : 2 - vec.y);
            }
        }

        return uv;
    }

    private Vector3[] GenerateVerts()
    {
        var result = new Vector3[(Dimensions + 1) * (Dimensions + 1)];
        for (int i = 0; i <= Dimensions; i++)
        {
            for(int j = 0; j <= Dimensions; j++)
            {
                result[ind(i, j)] = new Vector3(i, 0, j);
            }
        }

        return result;
    }

    private int ind(int x, int z)
    {
        return x * Dimensions + x + z;
    }

    private int[] GenerateTriangles()
    {
        var result = new int[Mesh.vertices.Length * 6];
        for (int i = 0; i < Dimensions; i++)
        {
            for (int j = 0; j < Dimensions; j++)
            {
                result[ind(i, j) * 6 + 0] = ind(i, j);
                result[ind(i, j) * 6 + 1] = ind(i + 1, j + 1);
                result[ind(i, j) * 6 + 2] = ind(i + 1, j);
                result[ind(i, j) * 6 + 3] = ind(i, j);
                result[ind(i, j) * 6 + 4] = ind(i, j + 1);
                result[ind(i, j) * 6 + 5] = ind(i + 1, j + 1);

            }
        }
                return result;
    }

    // Update is called once per frame
    void Update()
    {
        var verts = Mesh.vertices;
        for (int i = 0; i <= Dimensions; i++)
        {
            for (int j = 0; j <= Dimensions; j++)
            {
                var y = 0.0f;
                for (int k = 0; k < Octaves.Length; k++)
                {
                    if (Octaves[k].alternative)
                    {
                        var perl = Mathf.PerlinNoise(i * Octaves[k].scale.x / Dimensions, (j * Octaves[k].scale.y / Dimensions) * Mathf.PI * 2f);
                        y += Mathf.Cos(perl + Octaves[k].speed.magnitude * Time.time) * Octaves[k].height;
                    }
                    else
                    {
                        var perl = Mathf.PerlinNoise( (i * Octaves[k].scale.x + Time.time * Octaves[k].speed.x) / Dimensions, (j * Octaves[k].scale.y + Time.time * Octaves[k].speed.y) / Dimensions - 0.5f);
                        y += perl *  Octaves[k].height;
                    }
                }
                verts[ind(i, j)] = new Vector3(i, y, j);
            }
        }

        Mesh.vertices = verts;
        Mesh.RecalculateNormals();
    }

    [Serializable]
    public struct Octave
    {
        public Vector2 speed;
        public Vector2 scale;
        public float height;
        public bool alternative;
    }
}
