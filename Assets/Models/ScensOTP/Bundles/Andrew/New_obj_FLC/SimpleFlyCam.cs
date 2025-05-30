﻿using UnityEngine;

public class SimpleFlyCam : MonoBehaviour
{
    /*
       EXTENDED FLYCAM
           Desi Quintans (CowfaceGames.com), 17 August 2012.
           Based on FlyThrough.js by Slin (http://wiki.unity3d.com/index.php/FlyThrough), 17 May 2011.
 
       LICENSE
           Free as in speech, and free as in beer.
 
       FEATURES
           WASD/Arrows:    Movement
                     Q:    Climb
                     E:    Drop
                         Shift:    Move faster
                       Control:    Move slower
                           End:    Toggle cursor locking to screen (you can also press Ctrl+P to toggle play mode on and off).
       */

    public float cameraSensitivity = 500;
    public float normalMoveSpeed = 100;

    public float cameraSensivityStep = 50;
    
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
#if UNITY_EDITOR
        if (!Input.GetKey(KeyCode.Mouse1))
            return;
#endif

        rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.fixedDeltaTime;
        rotationY += Input.GetAxis("Mouse Y") * cameraSensitivity * Time.fixedDeltaTime;
        rotationY = Mathf.Clamp(rotationY, -90, 90);

        transform.localRotation = Quaternion.AngleAxis(rotationX, Vector3.up);
        transform.localRotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

        var moveVector = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")) *
                         normalMoveSpeed * Time.fixedDeltaTime;

        //rigidbody.MovePosition(transform.position + transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.fixedDeltaTime);
        //rigidbody.MovePosition(transform.position + transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.fixedDeltaTime);

        rigidbody.velocity = moveVector;

        if (Input.GetKeyDown(KeyCode.Plus))
            cameraSensitivity = Mathf.Clamp(cameraSensitivity + cameraSensivityStep, 0, float.MaxValue);

        if (Input.GetKeyDown(KeyCode.Minus))
            cameraSensitivity = Mathf.Clamp(cameraSensitivity - cameraSensivityStep, 0, float.MaxValue);
    }
}