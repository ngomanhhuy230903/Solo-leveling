using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    public int damage = 1;
    public float pushForce = 2.0f;
    protected override void OnCollide(Collider2D coll)
    {
       if(coll.tag == "Fighter" && coll.name == "Player")
        {
            Damage dmg = new Damage
            {
                origin = transform.position,
                damageAmount = damage,
                pushForce = pushForce
            };
            coll.SendMessage("ReceiveDamage", dmg);
        } 
    }
}
