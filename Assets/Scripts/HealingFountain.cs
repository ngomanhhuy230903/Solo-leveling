using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collectable
{
    public int healAmount = 2;
    private float healCooldown = 1.0f;
    private float lastHeal;
    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag != "Fighter")
        {
            return;
        }
        if (Time.time - lastHeal > healCooldown)
        {
            lastHeal = Time.time;
            GameManager.Instance.player.Heal(healAmount);

        }
    }
}
