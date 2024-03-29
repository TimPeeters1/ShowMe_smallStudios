﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragScript : MonoBehaviour
{
 
    private Vector3 mOffset;
    private Vector3 hitPoint;
    private Vector3 releasePoint;
    private float mZCoord;
    public GameObject currentObject;
    public float CustomMass = 1f;
    public float ForceToAdd = 1f;
    Touch touchOne;

    RaycastHit hit;

    Ray GenerateTouchray()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 touchPosFar = new Vector3(touch.position.x, touch.position.y, Camera.main.farClipPlane);
        Vector3 touchPosNear = new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane);
        Vector3 touchPosF = Camera.main.ScreenToWorldPoint(touchPosFar);
        Vector3 touchPosN = Camera.main.ScreenToWorldPoint(touchPosNear);

        Ray mr = new Ray(touchPosN, touchPosF - touchPosN);
        return mr;
    }

    void Update()
    {
        
        if (Input.touchCount > 0 && currentObject == null)
        {
            Ray touchRay = GenerateTouchray();
            //Debug.DrawRay(touchRay.origin, touchRay.direction * 100f, Color.red);

            if (Physics.Raycast(touchRay.origin, touchRay.direction, out hit) && hit.transform.GetComponent<IsDraggable>())
            {
                //currentObject = hit.transform.gameObject.GetComponentInParent<IsDraggable>().gameObject;
                currentObject = hit.transform.gameObject;
                hitPoint = hit.point;

                
                hit.transform.gameObject.GetComponentInParent<Collider>().enabled = false;

                mZCoord = Camera.main.WorldToScreenPoint(currentObject.transform.position).z;
                // Store offset = gameobject world pos - mouse world pos
                mOffset = currentObject.transform.position - GetTouchAsWorldPoint();
            }
        }
        else if (Input.touchCount > 0 && currentObject)
        {

            if (currentObject.GetComponent<Rigidbody>() == null)
            {
                currentObject.AddComponent<Rigidbody>();
            }

            currentObject.GetComponent<Rigidbody>().mass = CustomMass;
            currentObject.GetComponent<Rigidbody>().isKinematic = false;
            currentObject.transform.position = GetTouchAsWorldPoint() + mOffset;
        }

        if (Input.touchCount == 0 && currentObject)
        {
            releasePoint = currentObject.transform.position;
            currentObject.GetComponent<Rigidbody>().AddForce((releasePoint - hitPoint) * ForceToAdd);
            currentObject = null;
        }
    }


    private Vector3 GetTouchAsWorldPoint()
    {
        Touch touch = Input.GetTouch(0);
        // Pixel coordinates of mouse (x,y)
        Vector3 touchPoint = touch.position;
        // z coordinate of game object on screen
        touchPoint.z = mZCoord;
        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(touchPoint);
    }

    
}
