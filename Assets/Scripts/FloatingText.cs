using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
    public Vector3 worldPosition;
    public bool active;
    public GameObject go;
    public TextMeshProUGUI txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;
    public void Show()
    {
        active = true;
        lastShown = Time.time;
        go.SetActive(true);
    }
    public void Hide() {  
        go.SetActive(false);
        active = false;
    }
    public void UpdateFloatingText()
    {
        if (!active)
        {
            return;
        }

        if (Time.time - lastShown > duration)
        {
            Hide();
        }

        worldPosition += motion * Time.deltaTime;
        go.transform.position = Camera.main.WorldToScreenPoint(worldPosition);
    }
}
