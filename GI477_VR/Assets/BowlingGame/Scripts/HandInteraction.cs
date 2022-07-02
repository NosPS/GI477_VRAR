using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInteraction : MonoBehaviour
{
    public OVRInput.Axis1D inputActive;
    public Transform holdObjPoint;
    public List<string> ignoreTags;
    public float activeThresold;

    private bool bIsActive;
    public GameObject selectedObj;

    public void OnGrabObject()
    {
        if (selectedObj == null)
        {
            return;
        }

        selectedObj.GetComponent<Rigidbody>().isKinematic = true;
        selectedObj.transform.SetParent(holdObjPoint);
        selectedObj.transform.localPosition = Vector3.zero;
    }

    public void OnReleaseObject()
    {
        if (selectedObj == null)
        {
            return;
        }

        selectedObj.GetComponent<Rigidbody>().isKinematic = false;
        selectedObj.transform.SetParent(null);
    }

    private void Update() {

        //Debug.Log(inputActive + " : " + OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger));
        UpdateGrabObj();
    }

    private void UpdateGrabObj() {
        if(OVRInput.Get(inputActive) >= activeThresold && !bIsActive) {
            OnGrabObject();
            bIsActive = true;
        }
        else if(OVRInput.Get(inputActive) < activeThresold && bIsActive) {
            OnReleaseObject();
            bIsActive = false;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach (var tag in ignoreTags)
        {
            if (other.tag == tag)
            {
                return;
            }
        }

        selectedObj = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == selectedObj)
        {
            selectedObj = null;
        }
    }
}
