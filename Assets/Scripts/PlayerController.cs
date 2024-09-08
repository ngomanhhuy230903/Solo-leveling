using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Mover
{
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
        {
            return;
        }

        base.ReceiveDamage(dmg);
        GameManager.Instance.OnHitPointChange();
    }
    protected override void Death()
    {
        isAlive = false;
        GameManager.Instance.deathMenuAnim.SetTrigger("Show");
        //hitPoints = maxHitPoints;
        //GameManager.Instance.OnHitPointChange();
        //GameManager.Instance.coin = 0;
        //GameManager.Instance.exp = 0;
        //GameManager.Instance.SaveState();
    }
    internal void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.Instance.playerSprites[skinId];
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if (isAlive)
        {
            UpdateMotor(new Vector3(x, y, 0));
        }
    }
    public void OnLevelUp()
    {
        maxHitPoints += 10;
        hitPoints = maxHitPoints;
    }
    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLevelUp();
        }
    }
    public void Heal(int healingAmount)
    {
        if (hitPoints >= maxHitPoints)
        {
            return;
        }
        hitPoints += healingAmount;
        if(hitPoints > maxHitPoints)
        hitPoints = maxHitPoints;
        GameManager.Instance.ShowText("+" + healingAmount.ToString(), 25, Color.green, 
            transform.position, Vector3.up * 30, 1.0f);
        GameManager.Instance.OnHitPointChange();
    }
    public void Respawn() {       
        isAlive = true;
        Heal(maxHitPoints);
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }
}
