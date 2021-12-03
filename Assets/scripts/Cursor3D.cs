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

    [SerializeField]
    private float pokeAnimationDuration = 1f;
    
    [SerializeField]
    private int explosionMax;

    [SerializeField]
    private float explosionRadius;

    [SerializeField]
    private float explosionForce;
    
    private Camera cam;
    private MeshFilter cursorMesh;

    private const int InteractableLayerMask = 1 << 6;
    private const int TerrainLayerMask = 1 << 7;
    
    private int combinedLayerMask;
    private int currentLayerMask;

    private bool click;
    private bool rightclick;

    private CursorState currentState = CursorState.Idle;
    private AudioSource audio;
    

    void Start()
    {
        combinedLayerMask = InteractableLayerMask | TerrainLayerMask;
        currentLayerMask = combinedLayerMask;
        
        cam = Camera.main;
        
        cursorMesh = cursor.GetComponentInChildren<MeshFilter>();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && click == false)
        {
            click = true;
            currentState = CursorState.Click;
            SetCursorGraphic();
            StopAllCoroutines();
            StartCoroutine(ResetPoke(pokeAnimationDuration));
        }
        rightclick = Input.GetKeyDown(KeyCode.Mouse1);
        
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (Input.GetKeyDown(KeyCode.M)) audio.Stop();
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

    private IEnumerator ResetPoke(float afterInSeconds)
    {
        yield return new WaitForSeconds(afterInSeconds);
        click = false;
        currentState = CursorState.Idle;
        SetCursorGraphic();
    }

    void FixedUpdate() {
        Vector3 mousePos = cam.ScreenToViewportPoint(Input.mousePosition);
        Ray ray = cam.ViewportPointToRay(mousePos);

        if (!Physics.Raycast(ray, out var hit, float.MaxValue, currentLayerMask)) return;
        
        if (click)
        {
            clickParticles.Play();
            click = false;
            Collider[] explosionColliders = new Collider[explosionMax];
            int hits = Physics.OverlapSphereNonAlloc(hit.point, explosionRadius, explosionColliders, InteractableLayerMask);

            for (int i = 0; i < hits; i++)
            {
                var rb = explosionColliders[i].attachedRigidbody;
                Vector3 forceVector = (rb.position - hit.point).normalized;
                rb.transform.rotation = Quaternion.LookRotation(forceVector, transform.up);
                rb.AddForceAtPosition(forceVector * explosionForce, hit.point, ForceMode.Impulse);
            }
            // if (hit.transform.TryGetComponent(out FishBase fish))
            // {
            //     hit.rigidbody.AddForce(ray.direction * pokeForce, ForceMode.Impulse);
            //     clickParticles.Play();
            //     click = false;
            // }
            currentState = CursorState.Click;
        } else if (rightclick)
        {
            if (hit.transform.TryGetComponent(out FishBase fish))
            {
                currentState = CursorState.Grab;
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
