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

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
