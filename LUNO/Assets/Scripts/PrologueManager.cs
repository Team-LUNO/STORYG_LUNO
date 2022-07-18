using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
class Prologue
{
    public string sentence;
    public GameObject speaker;
    public int size;
}

public class PrologueManager : MonoBehaviour
{
    [SerializeField]
    private Prologue[] prologues;

    private int order = 0;
    private GameObject frontBubble;
    private GameObject backBubble;
    private bool firstPlay = true;
    private bool typeDone = false;

    private bool start = false;

    public void StartPrologue()
    {
        start = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (order < prologues.Length)
            {
                if (order == 0)
                {
                    if (firstPlay)
                    {
                        firstPlay = false;

                        frontBubble = prologues[order].speaker.transform.GetChild(0).GetChild(prologues[order].size - 1).gameObject;
                        StartCoroutine(PopUp(frontBubble));
                        StartCoroutine(TypeSentence(frontBubble, prologues[order].sentence));
                    }
                    if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && typeDone)
                    {
                        order++;
                        firstPlay = true;
                        typeDone = false;
                    }
                }
                else
                {
                    if (firstPlay)
                    {
                        firstPlay = false;

                        backBubble = frontBubble;
                        frontBubble = prologues[order].speaker.transform.GetChild(0).GetChild(prologues[order].size - 1).gameObject;

                        if (backBubble != frontBubble)
                        {
                            StartCoroutine(PopDown(backBubble));
                            StartCoroutine(PopUp(frontBubble));
                        }
                        StartCoroutine(TypeSentence(frontBubble, prologues[order].sentence));
                    }
                    if ((Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)) && typeDone)
                    {
                        order++;
                        firstPlay = true;
                        typeDone = false;
                    }
                }
            }
            else if (order == prologues.Length)
            {
                StartCoroutine(PopDown(frontBubble));
            }
        }
    }

    IEnumerator PopUp(GameObject bubble)
    {
        Debug.Log("up");
        bubble.SetActive(true);
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator PopDown(GameObject bubble)
    {
        Debug.Log("down");
        bubble.GetComponent<Animator>().SetBool("close", true);
        yield return new WaitForSeconds(0.5f);
        bubble.SetActive(false);
    }

    IEnumerator TypeSentence(GameObject bubble, string sentence)
    {
        Text text = bubble.transform.GetChild(0).GetComponent<Text>();
        text.text = string.Empty;

        yield return new WaitForSeconds(0.3f);

        foreach (var letter in sentence)
        {
            text.text += letter;
            yield return new WaitForSeconds(0.01f);
        }

        typeDone = true;
    }
}
