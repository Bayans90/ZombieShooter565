using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Camera myCamera;
    public GameObject thisObject;
    private void Update()
    {
        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            thisObject.transform.position = raycastHit.point;
        }
        else
        {
            // If the ray doesn't hit anything, move the object along the ray's direction
            thisObject.transform.position = ray.origin + ray.direction * 100f; // Adjust 10f to desired distance
        }
    }
}
