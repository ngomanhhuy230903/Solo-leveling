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
            return;
        }
        //delete all state
        //PlayerPrefs.DeleteAll();
        Instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }
    //Resource
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;
    //References
    public PlayerController player;
    //public weapon ...
    public FloatingTextManager floatingTextManager;

    //Logic
    public int coin;
    public int exp;
    //save state 
    /*
     * int preferedSkin
     * int coin
     * int exp
     * int weaponlevel
     */
    //floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
           floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }
    public void SaveState()
    {
        string s = "";
        s += "0" + "|";
        s += coin.ToString() + "|";
        s += exp.ToString() + "|";
        s += "0";
        PlayerPrefs.SetString("SaveState", s);
    }
    public void LoadState(UnityEngine.SceneManagement.Scene s, LoadSceneMode mode)
    {
        if(!PlayerPrefs.HasKey("SaveState"))
        {
            return;
        }
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');
        //Change Skin
        coin = int.Parse(data[1]);
        exp = int.Parse(data[2]);
        //Change Weapon level

    }
}
