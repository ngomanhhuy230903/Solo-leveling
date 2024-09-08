using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    private Vector3 originalSize;
    protected Vector3 moveDelta;
    protected BoxCollider2D boxCollider;
    protected RaycastHit2D hit;
    public float ySpeed = 1.5f;
    public float xSpeed = 2.0f;
    protected virtual void Start()
    {
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xSpeed,input.y * ySpeed,0);
        //swap sprite direction, whether player is moving left or right
        if (moveDelta.x > 0)
        {
            transform.localScale = originalSize;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);
        }
        //add push vector, if any
        moveDelta += pushDirection;
        //reduce push force every frame, based off recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);
        //make sure we can move in this direction by casting a box there first, if the box
        //returns null, we're free to move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y),
            Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {
            //move player
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0),
            Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null)
        {

            //move player
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
