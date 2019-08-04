using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float distanceFromChainEnd = 0.6f;

    public HingeJoint2D  ConnectRopeEnd(Rigidbody2D endRB)
    {
        HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedBody = endRB;
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = new Vector2 (0f, distanceFromChainEnd);

        return joint;
    }

    public DistanceJoint2D ConnectRopeDistance(Rigidbody2D ropeRB,float distance)
    {
        DistanceJoint2D joint = gameObject.AddComponent<DistanceJoint2D>();
        joint.connectedBody = ropeRB;
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = Vector2.zero;
        joint.connectedAnchor = Vector2.zero;

        joint.autoConfigureDistance = false;
        joint.distance = distance;
        joint.maxDistanceOnly = true;

        return joint;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Frog")
        {
            Animator animator = collision.GetComponent<Animator>();
            animator.SetBool("isEatting",true);
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;

            GameManager.instance.Win();
        }
        if (collision.tag == "Star")
        {
            Destroy(collision.gameObject);
            GameManager.instance.starCode = GameManager.instance.starCode * 2 + 1;
        }
    }
}
