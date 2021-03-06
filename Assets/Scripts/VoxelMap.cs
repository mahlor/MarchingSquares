﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

//https://catlikecoding.com/unity/tutorials/marching-squares/

public class VoxelMap : MonoBehaviour
{
    [SerializeField] private float size = 2f;
    [SerializeField] int voxelResolution = 8;
    [SerializeField] int chunkResolution = 2;
    [SerializeField] VoxelGrid voxelGridPrefab;
    private VoxelGrid[] chunks;
    private float chunkSize, voxelSize, halfSize;

    private void Awake()
    {
        halfSize = size * 0.5f;
        chunkSize = size / chunkResolution;
        voxelSize = chunkSize / voxelResolution;

        chunks = new VoxelGrid[chunkResolution * chunkResolution];
        for (int i = 0, y = 0; y < chunkResolution; y++)
        {
            for (int x = 0; x < chunkResolution; x++, i++)
            {
                CreateChunk(i, x, y);
            }
        }
        BoxCollider box = gameObject.AddComponent<BoxCollider>();
        box.size = new Vector3(size, size);
    }

    private void CreateChunk(int i, int x, int y)
    {
        VoxelGrid chunk = Instantiate(voxelGridPrefab) as VoxelGrid;
        chunk.Initialize(voxelResolution, chunkSize);
        chunk.transform.parent = transform;
        chunk.transform.localPosition = new Vector3(x * chunkSize - halfSize, y * chunkSize - halfSize);
        chunks[i] = chunk;
    }

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hitInfo;
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                if (hitInfo.collider.gameObject == gameObject)
                {
                    EditVoxels(transform.InverseTransformPoint(hitInfo.point));
                }
            }
        }
    }

    private void EditVoxels(Vector3 point)
    {
        int voxelX = (int)((point.x + halfSize) / voxelSize);
        int voxelY = (int)((point.y + halfSize) / voxelSize);
        int chunkX = voxelX / voxelResolution;
        int chunkY = voxelY / voxelResolution;
        voxelX -= chunkX * voxelResolution;
        voxelY -= chunkY * voxelResolution;
        chunks[chunkY * chunkResolution + chunkX].SetVoxel(voxelX, voxelY, true);

//        Debug.Log(voxelX + ", " + voxelY + " in chunk " + chunkX + ", " + chunkY);


    }
}
