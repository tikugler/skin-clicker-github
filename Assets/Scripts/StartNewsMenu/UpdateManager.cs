using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{

    public GameObject UpdatePopUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OpenUpdatePopUp() { 
    
    UpdatePopUp.SetActive(true);
    
    }

    public void CloseUpdatePopUp()
    {

        UpdatePopUp.SetActive(false);

    }

}
