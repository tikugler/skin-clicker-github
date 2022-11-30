using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FindObjectHelper
{

    /// <summary>
    /// this method is used to find the object in parent by name,
    /// regardless of whether the object is active or inactive
    /// </summary>
    /// <param name="parent">parent object in the hierarchy</param>
    /// <param name="name">name of the descendant object</param>
    /// <returns>first occurance, if there is no match, then null</returns>
    public static GameObject FindObjectInParent(GameObject parent, string name)
    {
        Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
        foreach (Transform t in trs)
        {
            if (t.name == name)
            {
                return t.gameObject;

            }
        }
        return null;
    }
}
