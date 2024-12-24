using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TalkScripts : MonoBehaviour
{
    public GameObject DiaLogPanel;
    public Text DiaLogText;
    public string[] DiaLog;
    private int index;

    public GameObject Btn;
    public float Wspeed;
    public bool Playercome;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && Playercome)
        {
            if(DiaLogPanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                DiaLogPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        if(DiaLogText.text == DiaLog[index])
        {
            Btn.SetActive(true);
        }
    }

    public void zeroText()
    {
        DiaLogText.text = "";
        index = 0;
        DiaLogPanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in DiaLog[index].ToCharArray())
        {
            DiaLogText.text += letter;
            yield return new WaitForSeconds(Wspeed);
        }
    }

    public void NextLine()
    {
        Btn.SetActive(false);

        if (index < DiaLog.Length - 1)
        {
            index++;
            DiaLogText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText() ;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Inovar"))
        {
            Playercome = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Inovar"))
        {
            Playercome = false;
            zeroText();
        }
    }
}
