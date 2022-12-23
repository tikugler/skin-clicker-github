using UnityEngine;

public class Parallax : MonoBehaviour
{
    [Header("Camera Settings")]
    public float cameraMoveSpeed = 1.5f;
    [Header("Layer Settings")]
    public float[] layerSpeed = new float[5];
    public GameObject[] layerObjects = new GameObject[5];

    private Transform mainCamera;
    private float[] startPos = new float[5];
    private float boundsSizeX;
    private float localScaleX;
    private int layerSize;
    /// <summary>
    /// Setting up the layers
    /// </summary>
    void Start()
    {
        BackgroundArrays.Start();
        SwitchBackground(BackgroundArrays.Desert);
        //layerObjects[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Backgtounds/2_Desert/Layer_0");
        layerSize = layerSpeed.Length;
        mainCamera = Camera.main.transform;
        localScaleX = layerObjects[0].transform.localScale.x;
        boundsSizeX = layerObjects[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        for (int i = 0; i < layerSize; i++)
        {
            startPos[i] = mainCamera.position.x;
        }
    }

    /// <summary>
    /// Update Method, which moves the main Camera slightly and moves the other layer objects relative to the camera.
    /// </summary>
    void Update()
    {
        mainCamera.position += Vector3.right * Time.deltaTime * cameraMoveSpeed;
        for (int i = 0; i < layerSize; i++)
        {
            float temp = mainCamera.position.x * (1 - layerSpeed[i]);
            float distance = mainCamera.position.x * layerSpeed[i];
            layerObjects[i].transform.position = new Vector2(startPos[i] + distance, mainCamera.position.y);
            if (temp > startPos[i] + boundsSizeX * localScaleX)
            {
                startPos[i] += boundsSizeX * localScaleX;
            }
            else if (temp < startPos[i] - boundsSizeX * localScaleX)
            {
                startPos[i] -= boundsSizeX * localScaleX;
            }
        }
    }

    public void SwitchBackground(string id) {
        Sprite[] newBackground = BackgroundArrays.backgroundDictionary[id];
        for (int i = 0; i < newBackground.Length; i++) {
            layerObjects[i].GetComponent<SpriteRenderer>().sprite = newBackground[i];
        }
    }
}
