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
            Debug.Log("Chest collected");
        }
    }
}
