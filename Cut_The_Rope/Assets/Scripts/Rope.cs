using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour {

    [SerializeField]
    private int linkCount;

    [SerializeField]
    private float linkBetweenDistance;

    [SerializeField]
    private Transform linkParents;

    [SerializeField]
    private Rigidbody2D hookJointPointRb;

    [SerializeField]
    private Ball ball;

    [SerializeField]
    private Link linkPrefab;

    private bool isGenerateDone, isCut;
    private List<Link> nodes = new List<Link>();

    private DistanceJoint2D ballDistanceJoint;

    public bool IsCut
    {
        get
        {
            return isCut;
        }

        set
        {
            isCut = value;
        }
    }

    private void Start()
    {
        GenerateRope();
    }

    private void Update()
    {
        if (isGenerateDone && nodes.Count > 1 && linkParents != null)
            RenderLine();        
    }

    private void GenerateRope()
    {
        Rigidbody2D previousRB = hookJointPointRb;

        float distance = Vector2.Distance(hookJointPointRb.transform.position, ball.transform.position);

        Vector2 unitVector = (ball.transform.position - hookJointPointRb.transform.position).normalized;
        unitVector *= (distance / linkCount);

        for (int i = 0; i < linkCount; i++)
        {
            Link link = Instantiate(linkPrefab, previousRB.position + unitVector, Quaternion.identity);
            link.Rope = this;
            link.transform.SetParent(linkParents.transform);           
            link.ConnectLink(previousRB, linkBetweenDistance);

            nodes.Add(link);
            previousRB = link.Rb;
        }

        ball.ConnectRopeEnd(previousRB);
        ballDistanceJoint = ball.ConnectRopeDistance(hookJointPointRb, linkBetweenDistance * (linkCount + 1));

        isGenerateDone = true;
    }

    public void CutTheRope(Link link)
    {
        isCut = true;
        Destroy(link.gameObject);
        Destroy(ballDistanceJoint);

        Destroy(linkParents.gameObject, 3f);
    }

    private void RenderLine()
    {
        Vector3 lastLinkPosition = hookJointPointRb.transform.position;
        LineRenderer lr;

        for (int i = 1; i < nodes.Count; i++)
        {
            if (nodes[i] != null)
            {
                lr = nodes[i].GetComponent<LineRenderer>();
                lr.SetPosition(0, lastLinkPosition);
                lr.SetPosition(1, nodes[i].transform.position);
                lastLinkPosition = nodes[i].transform.position;
            }
            else
            {
                int temp = i + 1;
                if (nodes[temp] != null)
                {
                    lastLinkPosition = nodes[temp].transform.position;
                }
            }
        }
    }

}
