using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DLLExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string WhatsMyPlatform = 
            EditorClassLibrary.MyPluginClass.GetPlatform;
        Debug.Log(WhatsMyPlatform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
