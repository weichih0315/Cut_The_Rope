using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour {

    [SerializeField]
    private Rope rope;
    public Rope Rope
    {
        get
        {
            return rope;
        }

        set
        {
            rope = value;
        }
    }
    
    private Rigidbody2D rb;
    public Rigidbody2D Rb
    {
        get
        {
            return rb;
        }

        set
        {
            rb = value;
        }
    }
    
    private HingeJoint2D hingeJoint;
    private DistanceJoint2D distanceJoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hingeJoint = GetComponent<HingeJoint2D>();
        distanceJoint = GetComponent<DistanceJoint2D>();
    }

    public void ConnectLink(Rigidbody2D targetRB, float distance)
    {
        hingeJoint.connectedBody = targetRB;
        hingeJoint.connectedAnchor = new Vector2(0f, -distance);

        distanceJoint.connectedBody = targetRB;
        distanceJoint.distance = distance;
    }
}
