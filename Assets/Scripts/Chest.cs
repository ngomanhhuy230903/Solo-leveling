using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    [SerializeField] Sprite emptyChest;
    [SerializeField] int coinsAmount = 10;
    protected override void OnCollide(Collider2D coll)
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            //increase coins
            GameManager.Instance.ShowText("+" + coinsAmount + " coins", 30, Color.yellow, 
                transform.position, Vector3.up * 50, 1.0f);
        }
    }
}
