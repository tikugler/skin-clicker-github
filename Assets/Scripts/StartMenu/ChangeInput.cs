using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeInput : MonoBehaviour
{

    public Button SubmitButton;  
    public List<Selectable> elements;   // add UI elements in inspector in desired tabbing order
    int index;


    void Start()
    {
        index = -1;           // always leave at -1 initially
        //elements[0].Select(); // uncomment to have focus on first element in the list
    }

    /// <summary>
    /// selects the first element in the elements list
    /// </summary>
    public void SelectFirstSelectable()
    {
        elements[0].Select();
    }

    /// <summary>
    /// user can select the next selectable gameobject in elements list by pressing tab
    /// user can select the previous selectable gameobject in elements list by pressing tab
    /// user can select Submutbutton by pressin Enter if it is interactable
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i].gameObject.Equals(EventSystem.current.currentSelectedGameObject))
                {
                    index = i;
                    break;
                }
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                index = index > 0 ? --index : index = elements.Count - 1;
            }
            else
            {
                index = index < elements.Count - 1 ? ++index : 0;
            }
            elements[index].Select();
        }

        else if (SubmitButton.interactable && Input.GetKeyDown(KeyCode.Return))
        {
            SubmitButton.onClick.Invoke();
        }
    }
}
