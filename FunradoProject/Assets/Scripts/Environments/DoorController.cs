using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorController : MonoBehaviour
{
    public DoorChecker doorChecker;
    public Vector3 turnRotation;
    public float turnSpeed;


    private void Start()
    {
        doorChecker.OnDoorOpened += DoorOpener;  
    }

    public void DoorOpener()
    {
        transform.DOLocalRotate(turnRotation, turnSpeed);
    }

}
