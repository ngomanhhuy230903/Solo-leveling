using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //Exp
    public int xpValue = 1;
    //logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;
    //hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitBox;
    private Collider2D[] hits = new Collider2D[10];
    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.Instance.player.transform;
        startingPosition = transform.position;
        hitBox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }
    private void FixedUpdate()
    {         
        //is player in range?
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
            {
                chasing = true;
            }
            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            chasing = false;
            UpdateMotor(startingPosition - transform.position);
        }
        //check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
            {
                continue;
            }
            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }
            //clean up array
            hits[i] = null;
        }
    }
    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.Instance.exp += xpValue;
        GameManager.Instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position,
            Vector3.up * 40, 1.0f);

    }
}
