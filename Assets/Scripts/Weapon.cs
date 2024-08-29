using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Dame struct
    public int[] damage = {1,2,3,4,5,6,7};
    public float[] pushForce = { 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f};
    //upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;
    //swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;
    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
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
                damageAmount = damage[weaponLevel],
                pushForce = pushForce[weaponLevel]
            };
            coll.SendMessage("ReceiveDamage", dmg);
        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.Instance.weaponSprites[weaponLevel];
        //change stats
    }
    public void setWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.Instance.weaponSprites[weaponLevel];
    }
}
