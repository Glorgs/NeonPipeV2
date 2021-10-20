using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : MySingleton<TagManager>
{
    public GameObject[] tags;

    void Start()
    {
        tags = new GameObject[2];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (tags[0] != null && tags[0] == tags[1])
        {
            Debug.Log("Combo");
        }

        tags[0] = null;
        tags[1] = null;
    }
}
