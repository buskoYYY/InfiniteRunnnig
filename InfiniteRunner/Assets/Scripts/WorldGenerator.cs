using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class WorldGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] Transform[] buildingSpawnPoint;
    [SerializeField] GameObject[] roadBlocks;
    [SerializeField] GameObject[] buildings;

    [Header("Settings")]
    [SerializeField] float envMoveSpeed = 4;
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
        return newBlock;
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
