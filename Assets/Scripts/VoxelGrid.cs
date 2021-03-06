﻿using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

[SelectionBase] public class VoxelGrid : MonoBehaviour
{
    [SerializeField] private int resolution;
    [SerializeField] private GameObject voxelPrefab;
    private float voxelSize;
    private bool[] voxels;


    public void Initialize(int resolution, float size)
    {
        this.resolution = resolution;
        voxelSize = size / resolution;
        voxels = new bool[resolution * resolution];
        for (int i = 0, y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++, i++)
            {
                CreateVoxel(i, x, y);
            }
        }
    }

    public void SetVoxel(int x, int y, bool state)
    {
        voxels[y * resolution + x] = state;
    }

    private void CreateVoxel(int i, int x, int y)
    {
        GameObject o = Instantiate(voxelPrefab) as GameObject;
        
        o.transform.parent = transform;
        o.transform.localPosition = new Vector3((x + 0.5f) * voxelSize, (y + 0.5f) * voxelSize);
        o.transform.localScale = Vector3.one * voxelSize * 0.9f;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
