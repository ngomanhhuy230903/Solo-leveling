using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public fields
    public int hitPoints = 10;
    public int maxHitPoints = 10;
    public float pushRecoverySpeed = 0.2f;

    //immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;
    //push
    protected Vector3 pushDirection;
    //All fighters can receive damage / die
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitPoints -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
            GameManager.Instance.ShowText(dmg.damageAmount.ToString(), 30, Color.red, transform.position,
                Vector3.up * 40, 1.0f);
            if (hitPoints <= 0)
            {
                hitPoints = 0;
                Death();
            }
        }
    }
    protected virtual void Death()
    {
        //die in some way
    }
}
