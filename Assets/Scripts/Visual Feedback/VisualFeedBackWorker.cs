using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualFeedBackWorker : MonoBehaviour
{
    public GameObject pickaxePrefab;
    public GameObject pickaxe;

    public float posX = 800f;
    public float posY = 450f;

    //creates and shows a worker on screen in a row  every time after buying 5 worker 
    public void MultipleWorker(int workerCount)
    {
        if ((workerCount % 5) == 0 && posX < 1070)
        {
            //sr.sprite = pickaxe;
            pickaxePrefab = Resources.Load("pickaxe") as GameObject;
            pickaxe = GameObject.Instantiate(pickaxePrefab, new Vector2(posX += 45f, posY), transform.rotation);
            Debug.Log("5 Worker bought" + workerCount);
        }
        else if ((workerCount % 5) == 0 && posX >= 1070)
        {
            posX = 800f;
            pickaxePrefab = Resources.Load("pickaxe") as GameObject;
            pickaxe = GameObject.Instantiate(pickaxePrefab, new Vector2(posX += 45, posY -= 45f), transform.rotation);
            Debug.Log("5 Worker bought" + workerCount);
        }
        if (posY <= 360)
            Destroy(pickaxe);
        }
}

