
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] TrainSegment segmentPrefab;
    [SerializeField] Vector2 segmentCountRange;
    [SerializeField] Threat threat;
    void Start()
    {
        GenerateTrainBody();
    }

    private void GenerateTrainBody()
    {
        int bodyCount = Random.Range((int)segmentCountRange.x, (int)segmentCountRange.y);
        for (int i = 0; i < bodyCount; i++)
        {
            Vector3 spawnPos = transform.position + transform.forward * segmentPrefab.GetSegmentLeangh() * i;
            TrainSegment newSegment = Instantiate(segmentPrefab, spawnPos, Quaternion.identity);
            if(i == 0)
            {
                newSegment.SetHead();
            }
            newSegment.GetMovementComponent().CopyFrom(threat.GetMovementComponent());
        }
    }
}
