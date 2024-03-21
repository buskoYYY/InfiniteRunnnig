using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitcher : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Transform defaultSubUI;
    Transform currentActivateUI;


    void Start()
    {
        foreach(Transform child in transform)
        {
            if(child.parent == transform)
            {
                child.gameObject.SetActive(false);  
            }
        }

        SetActiveUI(defaultSubUI);
    }

    public void SetActiveUI(Transform newActiveUI)
    {
        if (newActiveUI == currentActivateUI)
        {
            return;
        }
        
        if(currentActivateUI != null)
        {
            currentActivateUI.gameObject.SetActive(false);
        }
        newActiveUI.gameObject.SetActive(true);
        currentActivateUI = newActiveUI;
    }

}
