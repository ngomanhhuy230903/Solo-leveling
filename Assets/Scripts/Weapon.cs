using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Dame struct
    public int damage = 1;
    public float pushForce = 2.0f;
    //upgrade
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;
    //swing
    private float cooldown = 0.5f;
    private float lastSwing;
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
            {
                return;
            }
            Damage dmg = new Damage
            {
                origin = transform.position,
                damageAmount = damage,
                pushForce = pushForce
            };
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing()
    {

    }

}
