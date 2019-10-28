using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WegYeetScript : MonoBehaviour
{
    private Vector3 mOffset;
    private Vector3 hitPoint;
    private Vector3 releasePoint;
    private float mZCoord;
    public GameObject currentObject;
    public float CustomMass = 1f;
    public float ForceToAdd = 1f;

    Ray GenerateMouseray()
    {
        Vector3 mousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 mousePosF = Camera.main.ScreenToWorldPoint(mousePosFar);
        Vector3 mousePosN = Camera.main.ScreenToWorldPoint(mousePosNear);

        Ray mr = new Ray(mousePosN, mousePosF - mousePosN);
        return mr;
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && currentObject == null)
        {
            Ray mouseRay = GenerateMouseray();
            RaycastHit hit;
            if (Physics.Raycast(mouseRay.origin, mouseRay.direction, out hit))
            {
                //currentObject = hit.transform.gameObject.GetComponentInParent<IsDraggable>().gameObject;
                currentObject = hit.transform.gameObject;
                hitPoint = hit.point;
                Debug.Log(currentObject);
            }

            mZCoord = Camera.main.WorldToScreenPoint(currentObject.transform.position).z;
            // Store offset = gameobject world pos - mouse world pos
            mOffset = currentObject.transform.position - GetMouseAsWorldPoint();
        }
        else if (Input.GetMouseButton(0) && currentObject)
        {
            currentObject.AddComponent<Rigidbody>();
            currentObject.GetComponent<Rigidbody>().mass = CustomMass;
            currentObject.GetComponent<Rigidbody>().isKinematic = false;
            currentObject.transform.position = GetMouseAsWorldPoint() + mOffset;
        }
        else if (Input.GetMouseButtonUp(0) && currentObject)
        {
            releasePoint = currentObject.transform.position;
            currentObject.GetComponent<Rigidbody>().AddForce((releasePoint - hitPoint) * ForceToAdd);
            currentObject = null;
        }
    }


    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;
        // z coordinate of game object on screen
        mousePoint.z = mZCoord;
        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

}

