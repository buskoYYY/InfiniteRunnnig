using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class WorldGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] Transform[] buildingSpawnPoints;
    [SerializeField] Transform[] streetLightSpawnPoints;
    [SerializeField] GameObject[] roadBlocks;
    [SerializeField] GameObject[] buildings;
    [SerializeField] GameObject streetLight;

    [Header("Settings")]
    [SerializeField] float envMoveSpeed = 4;
    [SerializeField] Vector2 buildingSpawnScaleRange = new Vector2(0.6f,0.8f);
    Vector3 moveDirection;
    void Start()
    {
        Vector3 nextBlockPosition = startPoint.position;
        float endPointDistance = Vector3.Distance(startPoint.position, endPoint.position);
        moveDirection = (endPoint.position - startPoint.position).normalized;

        while (Vector3.Distance(startPoint.position, nextBlockPosition) < endPointDistance)
        {
            GameObject newBlock = SpawnNewBlock(nextBlockPosition, moveDirection);
            float bloackLeanth = newBlock.GetComponent<Renderer>().bounds.size.z;
            nextBlockPosition += moveDirection * bloackLeanth;

        }
    }

    private GameObject SpawnNewBlock(Vector3 spawnPos, Vector3 moveDirection)
    {
        int pick = Random.Range(0, roadBlocks.Length);
        GameObject pickedBlock = roadBlocks[pick];
        GameObject newBlock = Instantiate(pickedBlock);
        newBlock.transform.position = spawnPos;
        MovementComp movementComp = newBlock.GetComponent<MovementComp>();

        if (movementComp != null)
        {
            movementComp.SetMoveSpeed(envMoveSpeed);
            movementComp.SetMoveDir(moveDirection);
            movementComp.SetDestination(endPoint.position);
        }

        SpawnBuilding(newBlock);
        SpawnStreetLights(newBlock);

        return newBlock;
    }

    private void SpawnStreetLights(GameObject parentBlock)
    {
        foreach(Transform streetLightSpawnPoint in streetLightSpawnPoints)
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
        if(other.gameObject != null)
        {
            GameObject newBlock = SpawnNewBlock(other.transform.position,moveDirection);
            float newBlockHalfWidth = newBlock.GetComponent<Renderer>().bounds.size.z/2f;
            float previousBlockHalfWidth = other.GetComponent<Renderer>().bounds.size.z/2f;

            Vector3 newBlockSpawnOffset = -(newBlockHalfWidth + previousBlockHalfWidth) * moveDirection;
            newBlock.transform.position += newBlockSpawnOffset;
        }
    }
}
