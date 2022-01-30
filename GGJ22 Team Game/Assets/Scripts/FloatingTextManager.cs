using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefabNPC;
    public GameObject textPrefabWorld;

    public List<FloatingText> floatingTexts = new List<FloatingText>();

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration, int font)
    {
        FloatingText floatingText = GetFloatingText(font);

        floatingText.txt.text = msg;
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;

        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position); // transfer world space to screen space so we can use it in the UI
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();
    }

    private FloatingText GetFloatingText(int i)
    {
        FloatingText txt = floatingTexts.Find(t=> !t.active);

        if(i == 0)
        {
            if (txt == null)
            {
                txt = new FloatingText();
                txt.go = Instantiate(textPrefabNPC);
                txt.go.transform.SetParent(textContainer.transform);
                txt.txt = txt.go.GetComponent<TMP_Text>();

                floatingTexts.Add(txt);
            }
        }
        else if (i == 1)
        {
            if (txt == null)
            {
                txt = new FloatingText();
                txt.go = Instantiate(textPrefabWorld);
                txt.go.transform.SetParent(textContainer.transform);
                txt.txt = txt.go.GetComponent<TMP_Text>();

                floatingTexts.Add(txt);
            }
        }

        return txt;
    }   

    private void Update()
    {
        foreach (FloatingText txt in floatingTexts)
        {
            txt.UpdateFloatingText();
        }
    }
}
