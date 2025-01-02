using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class TalkScripts : MonoBehaviour
{
    public GameObject DiaLogPanel;
    public Text DiaLogText;
    public string[] DiaLog;
    private int index;

    public Transform PlayerCheck;
    public float PlayerCheckDis;
    public LayerMask whatPlayer;

    public float Wspeed;
    public bool Playercome;

    void Update()
    {
        CheckPlayer();
        if (Input.GetKeyDown(KeyCode.E) && Playercome)
        {
            if(DiaLogPanel.activeInHierarchy)
            {
                NextLine();
            }
            else
            {
                DiaLogPanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }
        if (!Playercome)
        {
            zeroText();
        }
    }

    private void CheckPlayer()
    {
        Playercome = Physics2D.Raycast(PlayerCheck.position, transform.right, PlayerCheckDis, whatPlayer);
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(PlayerCheck.position, new Vector2(PlayerCheck.position.x + PlayerCheckDis, PlayerCheck.position.y));
    }
}
