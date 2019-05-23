using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogBehavior : MonoBehaviour
{
    public bool isRotate = false;
    public int dir = 1;
    public bool isStatic = false;
    public List<GameObject> childs = new List<GameObject>();
    
    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "cog")
        {
            
            if (coll.gameObject.GetComponent<CogBehavior>().isRotate && !this.isRotate)
            {
                if (!isStatic)
                    coll.gameObject.GetComponent<CogBehavior>().childs.Add(this.gameObject);
                isRotate = true;
                dir = coll.gameObject.GetComponent<CogBehavior>().dir * (-1);
            };
        }
    }
    void Update()
    {
        if (isRotate)
            transform.Rotate(0, 1*dir, 0, Space.Self);
    }
    
    void OnMouseDrag()
    {
        if (!isStatic && isRotate)
        {
            StopRotation();
            
        }
    }
    public void StopRotation()
    {
        if (!isRotate)
            return;
        isRotate = false;
        foreach (GameObject o in childs)
        {
            o.GetComponent<CogBehavior>().StopRotation();
        }
        childs.Clear();
    }
}
