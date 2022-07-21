using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBalloonSelection : MonoBehaviour
{
    [SerializeField]
    private GameObject[] selections;

    [SerializeField]
    private Sprite tws;

    [SerializeField]
    private Sprite twsok;

    private SelectionBalloon selectionBalloon;
    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        selectionBalloon = gameObject.GetComponent<SelectionBalloon>();
    }

    // Update is called once per frame
    void Update()
    {
        // 방향키 조작
        if (index < selections.Length - 1 && Input.GetKeyDown(KeyCode.DownArrow))
        {
            selections[index].GetComponent<SpriteRenderer>().sprite = tws;
            index++;
            selections[index].GetComponent<SpriteRenderer>().sprite = twsok;
        }
        else if (index > 0 && Input.GetKeyDown(KeyCode.UpArrow))
        {
            selections[index].GetComponent<SpriteRenderer>().sprite = tws;
            index--;
            selections[index].GetComponent<SpriteRenderer>().sprite = twsok;
        }

        // 숫자키 조작
        if(selections.Length >= 2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                for(int i = 0; i < selections.Length; i++)
                {
                    selections[i].GetComponent<SpriteRenderer>().sprite = tws;
                }
                selections[0].GetComponent<SpriteRenderer>().sprite = twsok;
                index = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                for (int i = 0; i < selections.Length; i++)
                {
                    selections[i].GetComponent<SpriteRenderer>().sprite = tws;
                }
                selections[1].GetComponent<SpriteRenderer>().sprite = twsok;
                index = 1;
            }
        }
        if(selections.Length >= 3)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                for (int i = 0; i < selections.Length; i++)
                {
                    selections[i].GetComponent<SpriteRenderer>().sprite = tws;
                }
                selections[2].GetComponent<SpriteRenderer>().sprite = twsok;
                index = 2;
            }
        }
        if(selections.Length >= 4)
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                for (int i = 0; i < selections.Length; i++)
                {
                    selections[i].GetComponent<SpriteRenderer>().sprite = tws;
                }
                selections[3].GetComponent<SpriteRenderer>().sprite = twsok;
                index = 3;
            }
        }

        // 엔터 눌렀을 때
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(index == 0)
            {
                selectionBalloon.First();
            }
            else if(index == 1)
            {
                selectionBalloon.Second();
            }
            else if(index == 2)
            {
                selectionBalloon.Third();
            }
            else if(index == 3)
            {
                selectionBalloon.Fourth();
            }
        }
    }
}
