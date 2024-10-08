using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class MenuController : MonoBehaviour
{
    [Header("Volume setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSlider = null;
    [Header("Confirmation")]
    [SerializeField] private GameObject confirmationPrompt = null; 

    [Header("Levels to load")]
    public string newGameLevel;
    private string leveltoLoad;
    [SerializeField] private GameObject noSaveGameDialog = null;
    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(newGameLevel);
    }
    public void LoadGameDialogYes()
    {
        if(PlayerPrefs.HasKey("SavedLevel"))
        {
            leveltoLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(leveltoLoad);
        }
        else
        {

           noSaveGameDialog.SetActive(true);
        }
    }
    public void  ExitButton()
    {
        Application.Quit();
    }
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }
    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }
    public IEnumerator ConfirmationBox()
    {
        confirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmationPrompt.SetActive(false);
    }
}
