using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RopeInteraction : MonoBehaviour
{
    public GameObject bubble;   //E키를 유도하는 UI
    public GameObject rope;
    public GameObject ropeFinish;
    public bool ropeSwitch = false;

    void Update()
    {
        if (ropeSwitch == true && Input.GetKeyDown(KeyCode.E))
        {
            if (bubble.activeSelf == true)
            {
                bubble.SetActive(false);
            }
            rope.SetActive(true);
            ropeFinish.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //E키 bubble 활성화
        if (!ropeSwitch && !rope.activeSelf && collision.gameObject.CompareTag("Rope1"))
        {
            bubble.SetActive(true);
            ropeSwitch = true;
        }

        if (collision.gameObject.CompareTag("RopeFinish"))
        {
            rope.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //사다리를 내릴 수 있는 공간에 있으면
        if (collision.gameObject.CompareTag("Rope1"))
        {
            bubble.SetActive(false);
            ropeSwitch = false;
        }
    }
}
