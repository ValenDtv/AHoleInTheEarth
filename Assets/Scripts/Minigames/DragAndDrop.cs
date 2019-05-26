using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    public Camera camera_;
    
    public Pivots pivots;

    void OnMouseDown()
    {
        Vector3 nearPivotPosition = pivots.Get(0).transform.position;
        int i_min = 0;
        for (int i = 1; i < pivots.Size(); i++)
        {
            if (pivots.IsUsed(i) && Vector3.Distance(nearPivotPosition, transform.position) > Vector3.Distance(pivots.Get(i).transform.position, transform.position))
            {
                nearPivotPosition = pivots.Get(i).transform.position;
                i_min = i;
            }
        }
        pivots.Free(i_min);
        screenPoint = camera_.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - camera_.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z-1f);
        Vector3 cursorPosition = camera_.ScreenToWorldPoint(cursorScreenPoint) + offset;

        transform.position = cursorPosition;
    }

    void OnMouseUp()
    {
        Vector3 nearPivotPosition = pivots.Get(0).transform.position;
        int i_min = 0;
        for (int i = 1; i < pivots.Size(); i++)
        {
            if (!pivots.IsUsed(i) && Vector3.Distance(nearPivotPosition, transform.position) > Vector3.Distance(pivots.Get(i).transform.position, transform.position))
            {
                nearPivotPosition = pivots.Get(i).transform.position;
                i_min = i;
            }
        }
        pivots.Occupy(i_min);
        /*foreach (GameObject o in pivots)
        {
            if (Vector3.Distance(nearPivotPosition, transform.position) > Vector3.Distance(o.transform.position, transform.position))
            {
                nearPivotPosition = o.transform.position;
            }
        }*/
        //transform.position = new Vector3(nearPivotPosition.x, transform.position.y, nearPivotPosition.z);
        transform.position = nearPivotPosition;
    }
}
