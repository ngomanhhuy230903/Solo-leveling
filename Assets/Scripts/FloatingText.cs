using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText
{
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
        // 10 - 7 > 2
        if (Time.time - lastShown > duration)
        {
            Hide();
        }
        go.transform.position += motion * Time.deltaTime;
    }
}
