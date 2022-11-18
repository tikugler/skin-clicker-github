using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualFeedBackWorker : MonoBehaviour
{
    public Texture2D tex;
    private Sprite pickaxe;
    private SpriteRenderer sr;
    
    void Awake()
    {
        //sr = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        

        //transform.position = new Vector3(1.5f, 1.5f, 0.0f);
    }

    void Start()
    {
        //pickaxe = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    public void  MultipleWorker(int workerCount)
    {
        if ((workerCount % 5) == 0)
        {
            //sr.sprite = pickaxe;
            Debug.Log("5 Worker bought" + workerCount);
        }
    }

   
}

