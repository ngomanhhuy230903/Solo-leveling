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
    [SerializeField] List<Sprite> playerSprites;
    [SerializeField] List<Sprite> weaponSprites;
    [SerializeField] List<int> weaponPrices;
    [SerializeField] List<int> xpTable;
    //References
    [SerializeField] PlayerController player;
    //public weapon ...

    //Logic
    [SerializeField] int coin;
    [SerializeField] int exp;
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
