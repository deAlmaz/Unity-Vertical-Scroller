﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float smoothing;
    private Vector2 origin;
    private Vector2 direction;
    private Vector2 smoothDirection;
    private bool touched;
    private int pointerID;

    void Awake ()
    {
        direction = Vector2.zero;
        touched = false;
    }

    public void OnPointerDown (PointerEventData data)
    // Set our startpoint
    {
        if (!touched)
        {
            touched = true;
            pointerID = data.pointerId;
            origin = data.position;
        }        
    }
    public void OnDrag (PointerEventData data)
    // Compare the difference between our start point and current pointer position
    {
        if (data.pointerId == pointerID)
        {
            Vector2 currentPosition = data.position;
            Vector2 directionRaw = currentPosition - origin;
            direction = directionRaw.normalized;
        }
        
//      Debug.log (direction);
    }
    public void OnPointerUp (PointerEventData data)
    // Reset everything
    {
        if (data.pointerId == pointerID)
        {
            direction = Vector2.zero;
            touched = false;
        }        
    }
    public Vector2 GetDirection ()
    {
        smoothDirection = Vector2.MoveTowards (smoothDirection, direction, smoothing);
        return direction;
    }
}
