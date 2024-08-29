using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour
{
    //Text field
    public TextMeshProUGUI hitPointsText,levelText,coinText,upgradeCostText,xpText;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    //Character Selection
    public void OnArrowClick(bool right)
    {
        if(right)
        {
            currentCharacterSelection++;
            if(currentCharacterSelection == GameManager.Instance.playerSprites.Count)
            {
                currentCharacterSelection = 0;
            }
            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;
            if(currentCharacterSelection < 0)
            {
                currentCharacterSelection = GameManager.Instance.playerSprites.Count - 1;
            }
            OnSelectionChanged();
        }
    }
    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.Instance.playerSprites[currentCharacterSelection];
        GameManager.Instance.player.SwapSprite(currentCharacterSelection);
    }
    //Weapon Upgrade
    public void OnUpgradeClick()
    {
        if(GameManager.Instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    //Update the character information
    public void UpdateMenu()
    {
        //Weapon
        weaponSprite.sprite = GameManager.Instance.weaponSprites[GameManager.Instance.weapon.weaponLevel];
        if(GameManager.Instance.weapon.weaponLevel < GameManager.Instance.weaponPrices.Count)
        {
            upgradeCostText.text = GameManager.Instance.weaponPrices[GameManager.Instance.weapon.weaponLevel].ToString();
        }
        else
        {
            upgradeCostText.text = "MAX";
        }
       //meta
        levelText.text = GameManager.Instance.GetCurrentLevel().ToString();
        hitPointsText.text = GameManager.Instance.player.hitPoints.ToString();
        coinText.text = GameManager.Instance.coin.ToString();
        //xp bar
        int currLevel = GameManager.Instance.GetCurrentLevel();
        if(currLevel == GameManager.Instance.xpTable.Count)
        {
            xpText.text = GameManager.Instance.exp.ToString() + " total exp points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.Instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.Instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currentXpIntoLevel = GameManager.Instance.exp - prevLevelXp;
            float completionRatio = (float)currentXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = $"{currentXpIntoLevel} / {diff}";
        }
    }
}
