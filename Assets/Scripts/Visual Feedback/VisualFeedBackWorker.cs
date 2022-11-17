using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualFeedBackWorker : MonoBehaviour
{
    public Texture2D tex;
    private Sprite pickaxe;
    private SpriteRenderer sr;
    private Worker worker;

    void Awake()
    {
        sr = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sr.color = new Color(0.9f, 0.9f, 0.9f, 1.0f);

        transform.position = new Vector3(1.5f, 1.5f, 0.0f);
    }

    void Start()
    {
        pickaxe = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    public void  multipleworker()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Add sprite" ) && (worker.GetWorkerAmount()  % 5) == 0)
        {
            sr.sprite = pickaxe;
        }
    }

   
}

