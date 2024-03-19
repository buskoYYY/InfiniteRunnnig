using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrainSegment : MonoBehaviour
{
    [SerializeField] Mesh HeadMesh;
    [SerializeField] Mesh [] segmentMeshes;

    [SerializeField] MeshFilter trainMesh;
    [SerializeField] BoxCollider trainCollider;
    [SerializeField] MovementComp movementComp;

    bool isHead = false;
    void Start()
    {
        RandomTrainMesh();
    }

    private void RandomTrainMesh()
    {
        if (isHead) { return; }
        int pick = Random.Range(0, segmentMeshes.Length);
        trainMesh.mesh = segmentMeshes[pick];
    }

    public float GetSegmentLeangh()
    {
        return trainCollider.size.z;
    }

    internal void SetHead()
    {
        trainMesh.mesh = HeadMesh;
        isHead = true;
    }

    internal MovementComp GetMovementComponent()
    {
        return movementComp;
    }
}
