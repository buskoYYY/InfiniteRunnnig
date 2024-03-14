using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Transform startPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] GameObject[] roadBlocks;
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
            nextBlockPosition += (endPoint.position - startPoint.position).normalized * bloackLeanth;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
