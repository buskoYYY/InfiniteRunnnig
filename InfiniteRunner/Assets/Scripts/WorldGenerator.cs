using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using System;
using Random = UnityEngine.Random;

public class WorldGenerator : MonoBehaviour
{
    [Serializable]
    public struct RoadSpawnDefinition
    {
        public GameObject roadBlock;
        public float weight;
    }

    [Header("Elements")]
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] Transform[] buildingSpawnPoints;
    [SerializeField] Transform[] streetLightSpawnPoints;
    [SerializeField] Transform[] lanes;
    [SerializeField] RoadSpawnDefinition[] roadBlocks;
    [SerializeField] GameObject[] buildings;
    [SerializeField] PickUp[] pickUps;
    [SerializeField] GameObject streetLight;
    [SerializeField] Threat[] threats;

    [Header("Settings")]
    [SerializeField] Vector2 buildingSpawnScaleRange = new Vector2(0.6f, 0.8f);
    [SerializeField] Vector3 occupationDetectionHalfExtend;
    Vector3 moveDirection;
    float roadWeightTotalWeight;

    bool GetRandomSpawnPoint(out Vector3 spawnPoint, string OccupationCheckTag)
    {
        Vector3[] spawPoints = GetAvalibleSpawnPoints(OccupationCheckTag);
        if (spawPoints.Length == 0)
        {
            spawnPoint = new Vector3(0, 0, 0);
            return false;
        }

        int pick = Random.Range(0, spawPoints.Length);
        spawnPoint = spawPoints[pick];
        return true;
    }

    Vector3[] GetAvalibleSpawnPoints(string OccupationCheckTag)
    {
        List<Vector3> AvailibleSpawnPoints = new List<Vector3>();
        foreach (Transform spawnTrans in lanes)
        {
            Vector3 spawnPoint = spawnTrans.position + new Vector3(0, 0, startPoint.position.z);
            if (!GamePlayStatic.IsPositionOccupied(spawnPoint, occupationDetectionHalfExtend, OccupationCheckTag))
            {
                AvailibleSpawnPoints.Add(spawnPoint);
            }
        }
        return AvailibleSpawnPoints.ToArray();
    }
    void Start()
    {
        roadWeightTotalWeight = 0;

        for (int i = 0; i < roadBlocks.Length; i++)
        {
            roadWeightTotalWeight += roadBlocks[i].weight;
        }

        Vector3 nextBlockPosition = startPoint.position;
        float endPointDistance = Vector3.Distance(startPoint.position, endPoint.position);
        moveDirection = (endPoint.position - startPoint.position).normalized;

        while (Vector3.Distance(startPoint.position, nextBlockPosition) < endPointDistance)
        {
            GameObject pickedBlock = roadBlocks[0].roadBlock;
            GameObject newBlock = Instantiate(pickedBlock);
            newBlock.transform.position = nextBlockPosition;
            MovementComp movementComp = newBlock.GetComponent<MovementComp>();

            if (movementComp != null)
            {
                movementComp.SetMoveDir(moveDirection);
                movementComp.SetDestination(endPoint.position);
            }
            SpawnBuilding(newBlock);
            SpawnStreetLights(newBlock);

            float bloackLeanth = newBlock.GetComponent<Renderer>().bounds.size.z;
            nextBlockPosition += moveDirection * bloackLeanth;
        }

        StartSpawnElements();

        PickUp newPickUp = Instantiate(pickUps[0], startPoint.position, Quaternion.identity);
        newPickUp.GetComponent<MovementComp>().SetDestination(endPoint.position);
        newPickUp.GetComponent<MovementComp>().SetMoveDir(moveDirection);
    }

    private void StartSpawnElements()
    {
        foreach (Threat threat in threats)
        {
            StartCoroutine(SpawnElementsCourutine(threat));
        }

        foreach (PickUp pickUp in pickUps)
        {
            StartCoroutine(SpawnElementsCourutine(pickUp));
        }
    }

    IEnumerator SpawnElementsCourutine(Spawnable elements)
    {
        while (true)
        {
            if (GetRandomSpawnPoint(out Vector3 spawnPoint, elements.gameObject.tag))
            {
                Spawnable newThreat = Instantiate(elements, spawnPoint, Quaternion.identity);
                newThreat.GetMovementComponent().SetDestination(endPoint.position);
                newThreat.GetMovementComponent().SetMoveDir(moveDirection);
            }

            yield return new WaitForSeconds(elements.SpawnInterval);
        }
    }

    private GameObject SpawnNewBlock(Vector3 spawnPos, Vector3 moveDirection)
    {
        GameObject pickedBlock = GetRandomBlockToSpawn();
        GameObject newBlock = Instantiate(pickedBlock);
        newBlock.transform.position = spawnPos;
        MovementComp movementComp = newBlock.GetComponent<MovementComp>();

        if (movementComp != null)
        {
            movementComp.SetMoveDir(moveDirection);
            movementComp.SetDestination(endPoint.position);
        }
        SpawnBuilding(newBlock);
        SpawnStreetLights(newBlock);

        return newBlock;
    }

    private GameObject GetRandomBlockToSpawn()
    {
        float pickWeight = Random.Range(0, roadWeightTotalWeight);
        float totalWeight = 0;
        int pick = 0;

        for (int i = 0; i < roadBlocks.Length; i++)
        {
            totalWeight += roadBlocks[i].weight;
            if (pickWeight < totalWeight)
            {
                pick = i;
                break;
            }
        }
        return roadBlocks[pick].roadBlock;
    }

    private void SpawnStreetLights(GameObject parentBlock)
    {
        foreach (Transform streetLightSpawnPoint in streetLightSpawnPoints)
        {
            Vector3 spawnLoc = parentBlock.transform.position + (streetLightSpawnPoint.position - startPoint.position);
            Quaternion spawnRot = Quaternion.LookRotation((startPoint.position - streetLightSpawnPoint.position).normalized, Vector3.up);
            Quaternion SpawnRotOffset = Quaternion.Euler(0, -90, 0);
            GameObject newStreetLight = Instantiate(streetLight, spawnLoc, spawnRot * SpawnRotOffset, parentBlock.transform);
        }
    }

    private void SpawnBuilding(GameObject parentBlock)
    {
        foreach (Transform buildingSpawnPoint in buildingSpawnPoints)
        {
            Vector3 buildingSpawnLoc = parentBlock.transform.position + (buildingSpawnPoint.position - startPoint.position);
            int rotationOffsetBy90 = Random.Range(0, 3);
            Quaternion spawnRotation = Quaternion.Euler(0, rotationOffsetBy90 * 90, 0);
            Vector3 buildingSpawnSize = Vector3.one * Random.Range(buildingSpawnScaleRange.x, buildingSpawnScaleRange.y);

            int buildingPick = Random.Range(0, buildings.Length);

            GameObject newBuilding = Instantiate(buildings[buildingPick], buildingSpawnLoc, spawnRotation, parentBlock.transform);
            newBuilding.transform.localScale = buildingSpawnSize;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != null && other.gameObject.tag == "RoadModule")
        {
            GameObject newBlock = SpawnNewBlock(other.transform.position, moveDirection);
            float newBlockHalfWidth = newBlock.GetComponent<Renderer>().bounds.size.z / 2f;
            float previousBlockHalfWidth = other.GetComponent<Renderer>().bounds.size.z / 2f;

            Vector3 newBlockSpawnOffset = -(newBlockHalfWidth + previousBlockHalfWidth) * moveDirection;
            newBlock.transform.position += newBlockSpawnOffset;
        }
    }
}
