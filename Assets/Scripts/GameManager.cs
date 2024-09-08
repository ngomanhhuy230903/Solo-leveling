using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if(GameManager.Instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }
        //delete all state
        //PlayerPrefs.DeleteAll();
        Instance = this;
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    //Resource
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
    //References
    public PlayerController player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public Animator deathMenuAnim;
    public GameObject hud;
    public GameObject menu;
    //floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }
    //Logic
    public int coin;
    public int exp;
    //upgrade weapon
    public bool TryUpgradeWeapon()
    {
        //is max level
        if(weapon.weaponLevel >= weaponPrices.Count)
        {
            return false;
        }
        if(coin >= weaponPrices[weapon.weaponLevel])
        {
            coin -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }
    private void Update()
    {
        GetCurrentLevel();
    }
    //Hitpoint bar
    public void OnHitPointChange()
    {
        float ratio = (float)player.hitPoints / (float)player.maxHitPoints;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }
    //Experience system
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;
        while(exp >= add)
        {
            add += xpTable[r];
            r++;
            if(r == xpTable.Count)
            {
                return r;
            }
        }
        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;
        while(r < level)
        {
            xp += xpTable[r];
            r++;
        }
        return xp;
    }
    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        exp += xp;
        if(currLevel < GetCurrentLevel())
        {
            OnLevelUp();
        }
    }
    public void OnLevelUp()
    {
        player.OnLevelUp();
        OnHitPointChange();
    }
    //on scene load 
    public void OnSceneLoad(UnityEngine.SceneManagement.Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }
    //Death menu and respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        player.Respawn();
    }
    //save state 
    /*
     * int preferedSkin
     * int coin
     * int exp
     * int weaponlevel
     */

    public void SaveState()
    {
        string s = "";
        s += "0" + "|";
        s += coin.ToString() + "|";
        s += exp.ToString() + "|";
        s += weapon.weaponLevel.ToString();
        PlayerPrefs.SetString("SaveState", s);
    }
    public void LoadState(UnityEngine.SceneManagement.Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;
        if (!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        //Change Skin
        coin = int.Parse(data[1]);
        //exp 
        exp = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
        player.SetLevel(GetCurrentLevel());
        //Change Weapon level
        weapon.setWeaponLevel(int.Parse(data[3]));
    }
}
