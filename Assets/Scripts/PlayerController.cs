using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Mover
{
    private SpriteRenderer spriteRenderer;
    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        DontDestroyOnLoad(gameObject);
    }
    internal void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.Instance.playerSprites[skinId];
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        UpdateMotor(new Vector3(x, y, 0));
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
        GameManager.Instance.ShowText("+" + healingAmount.ToString(), 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
    }
}
