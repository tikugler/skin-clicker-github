using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class VisualFeedBackWorker : MonoBehaviour
{   
    public GameObject pickaxePrefab;
    public GameObject pickaxe;

    //creates and showss a worker on screen every time after buying 5 worker 
    public void MultipleWorker(int workerCount)
    {
        if ((workerCount % 5) == 0)
        {
            pickaxePrefab = Resources.Load("pickaxePrefab") as GameObject;
            pickaxe = GameObject.Instantiate(pickaxePrefab);
            pickaxe.transform.parent = GameObject.Find("pickaxescroll").transform;
            Debug.Log("5 Worker bought" + workerCount);
        }
        
        }
}

