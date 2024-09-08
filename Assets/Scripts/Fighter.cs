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

            ShowDamageText(dmg.damageAmount, dmg.origin);

            if (hitPoints <= 0)
            {
                hitPoints = 0;
                Death();
            }
        }
    }
    private void ShowDamageText(int damage, Vector3 origin)
    {
        Vector3 textPosition = transform.position + new Vector3(1, GetComponent<Collider2D>().bounds.extents.y, 0) ;
        Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(0.5f, 1f), 0);
        float displayTime = Mathf.Clamp(damage / 10f, 0.5f, 2f);

        GameManager.Instance.ShowText(
            damage.ToString(),
            30,
            Color.red,
            textPosition,
            randomOffset,
            displayTime
        );
    }
    protected virtual void Death()
    {
        //die in some way
    }
}
