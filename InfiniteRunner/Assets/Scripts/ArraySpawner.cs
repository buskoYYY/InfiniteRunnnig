using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArraySpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] int ammount = 10;
    [SerializeField] float gap = 1f;
    void Start()
    {
       for(int i = 1; i < ammount; i++)
        {
            Vector3 spawnPOsition = transform.position + transform.forward * gap * i;
            GameObject nextCoin = Instantiate(gameObject, spawnPOsition, Quaternion.identity);
            nextCoin.GetComponent<ArraySpawner>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
