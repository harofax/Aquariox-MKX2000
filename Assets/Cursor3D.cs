using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

enum CursorState
{
    Idle,
    Grab,
    Click
}
public class Cursor3D : MonoBehaviour
{
    [SerializeField] 
    private GameObject cursor;
    
    [Space(10)]
    
    [SerializeField] 
    private Mesh cursorIdle;
    [SerializeField]
    private Mesh cursorPoint;
    [SerializeField]
    private Mesh cursorGrab;
    
    [Space(10)]
    
    [SerializeField]
    private float rotationSpeed;
    [SerializeField] 
    private Transform rotationVectorOrigin;
    
    [Space(10)]
    
    [SerializeField] 
    private ParticleSystem clickParticles;
    [SerializeField]
    private float pokeForce;
    
    [Space(10)]
    
    private Camera cam;
    private MeshFilter cursorMesh;

    private const int InteractableLayerMask = 1 << 6;
    private const int TerrainLayerMask = 1 << 7;
    
    private int combinedLayerMask;
    private int currentLayerMask;

    private bool click;
    private bool rightclick;

    private CursorState currentState = CursorState.Idle;
    

    void Start()
    {
        combinedLayerMask = InteractableLayerMask | TerrainLayerMask;
        currentLayerMask = combinedLayerMask;
        
        cam = Camera.main;
        
        cursorMesh = cursor.GetComponentInChildren<MeshFilter>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        click = Input.GetKeyDown(KeyCode.Mouse0);
        rightclick = Input.GetKeyDown(KeyCode.Mouse1);

        SetCursorGraphic();
    }

    private void SetCursorGraphic()
    {
        Mesh newMesh;
        switch (currentState)
        {
            case CursorState.Idle: 
                newMesh = cursorIdle; 
                break;
            
            case CursorState.Click:
                newMesh = cursorPoint;
                break;
    
            case CursorState.Grab:
                newMesh = cursorGrab;
                break;
            
            default:
                newMesh = cursorIdle;
                break;
        }

        cursorMesh.mesh = newMesh;
    }

    void FixedUpdate() {
        Vector3 mousePos = cam.ScreenToViewportPoint(Input.mousePosition);
        Ray ray = cam.ViewportPointToRay(mousePos);

        if (!Physics.Raycast(ray, out var hit, float.MaxValue, currentLayerMask)) return;
        
        if (click)
        {
            if (hit.transform.TryGetComponent(out FishBase fish))
            {
                hit.rigidbody.AddForce(ray.direction * pokeForce, ForceMode.Impulse);
                clickParticles.Play();
            }
            currentState = CursorState.Click;
        } else if (rightclick)
        {
            if (hit.transform.TryGetComponent(out FishBase fish))
            {
                hit.rigidbody.AddForce(-ray.direction * pokeForce, ForceMode.Impulse);
            }

            currentState = CursorState.Grab;
        }
        else
        {
            currentState = CursorState.Idle;
        }

        cursor.transform.position = hit.point;
        Vector3 fwd = rotationVectorOrigin.position - hit.point;
        var proj = fwd - (Vector3.Dot(fwd, hit.normal)) * hit.normal;

        Quaternion lookRotation = Quaternion.LookRotation( -proj, hit.normal);

        var curRotation = cursor.transform.rotation;

        cursor.transform.rotation =
            Quaternion.Slerp(curRotation, lookRotation, Time.fixedDeltaTime * rotationSpeed);
    }
}
