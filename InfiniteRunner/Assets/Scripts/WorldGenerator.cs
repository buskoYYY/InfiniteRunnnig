using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class WorldGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] GameObject[] roadBlocks;

    [Header("Settings")]
    [SerializeField] float envMoveSpeed = 4;
    void Start()
    {
        Vector3 nextBlockPosition = startPoint.position;
        float endPointDistance = Vector3.Distance(startPoint.position, endPoint.position);

        while (Vector3.Distance(startPoint.position, nextBlockPosition) < endPointDistance)
        {
            int pick = Random.Range(0, roadBlocks.Length);
            GameObject pickedBlock = roadBlocks[pick];
            GameObject newBlock = Instantiate(pickedBlock);
            newBlock.transform.position = nextBlockPosition;
            float bloackLeanth = newBlock.GetComponent<Renderer>().bounds.size.z;
            Vector3 incrementDirection = (endPoint.position - startPoint.position).normalized;
            nextBlockPosition += incrementDirection * bloackLeanth;
            MovementComp movementComp = newBlock.GetComponent<MovementComp>();

            if (movementComp != null)
            {
                movementComp.SetMoveSpeed(envMoveSpeed);
                movementComp.SetMoveDir(incrementDirection);
                movementComp.SetDestination(endPoint.position);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
    }
}
