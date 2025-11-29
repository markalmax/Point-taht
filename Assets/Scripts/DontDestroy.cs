using UnityEngine;
using System.Collections.Generic;
public class DontDestroy : MonoBehaviour
{
    private static Dictionary<string, GameObject> instances = new Dictionary<string, GameObject>();

    private void Awake()
    {

        if (instances.ContainsKey(gameObject.name) && instances[gameObject.name] != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instances[gameObject.name] = gameObject;
            DontDestroyOnLoad(gameObject);
        }
    }
}
