using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour {

    public GameObject bladeTrail;
    public float minCuttingVelocity = .001f;

    public static System.Action OnSlicedHolder;
    public static System.Action OnSlicedRelease;

    private bool isCutting = false;

    private Vector2 previousPosition;

    private GameObject currentBladeTrail;

    private Rigidbody2D rb;

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();
        }
        else if (Input.GetMouseButtonUp(0) )
        {
            StopCutting();
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                StartCutting();
            }
            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                StopCutting();
            }
        }

        if (isCutting)
            UpdateCut();
    }

    private void UpdateCut()
    {
        Vector2 newPosition = camera.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPosition;

        float velocity = (newPosition - previousPosition).magnitude;

        if (velocity > minCuttingVelocity)
        {
            bladeTrail.SetActive(true);
            if (OnSlicedHolder != null)
                OnSlicedHolder();
        }
        else {
            bladeTrail.SetActive(false);
            if (OnSlicedRelease != null)
                OnSlicedRelease();
        }
        previousPosition = newPosition;
    }

    private void StartCutting()
    {
        isCutting = true;
        previousPosition = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void StopCutting()
    {
        isCutting = false;
        bladeTrail.SetActive(false);
        if (OnSlicedRelease != null)
            OnSlicedRelease();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Link" && isCutting)
        {
            Link link = collision.GetComponent<Link>();

            if (!link.Rope.IsCut)
                link.Rope.CutTheRope(link);
        }
    }    
}
