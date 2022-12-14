using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class VisualFeedBackWorker : MonoBehaviour
{   
    public GameObject pickaxePrefab;
    public GameObject pickaxe;

    //creates and showss a worker on screen every time after buying 5 worker 
    public void MultipleWorker(int workerCount)
    {
        if ((workerCount % 5) == 0)
        {
            GameObject canvasGameObject = GameObject.Find("Canvas");
            GameObject feedbackWorker = FindObjectHelper.
                FindObjectInParent(canvasGameObject, "FeedbackWorker");
            pickaxePrefab = Resources.Load("pickaxePrefab") as GameObject;
            pickaxe = Instantiate<GameObject>(pickaxePrefab, feedbackWorker.transform);
        }
    }
}

