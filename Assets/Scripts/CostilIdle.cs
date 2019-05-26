using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostilIdle : MonoBehaviour
{
    public GameObject obj;

    // Update is called once per frame
    void Awake()
    {
        obj.transform.rotation = Quaternion.identity;
    }
}
