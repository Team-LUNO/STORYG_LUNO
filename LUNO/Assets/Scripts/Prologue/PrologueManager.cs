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
    private GameObject selectionBubble;
    private bool firstPlay = true;
    private bool typeDone = false;

    private bool start = false;

    public void StartPrologue()
    {
        start = true;
    }

    public void IncreaseOrder()
    {
        order++;
        firstPlay = true;
        typeDone = false;
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

                        if (prologues[order - 1].size == 9 || prologues[order - 1].size == 10)
                        {
                            backBubble = frontBubble.transform.GetChild(int.Parse(prologues[order - 1].sentence)).gameObject;
                        }
                        else
                        {
                            backBubble = frontBubble;
                        }

                        frontBubble = prologues[order].speaker.transform.GetChild(0).GetChild(prologues[order].size - 1).gameObject;

                        if (backBubble != frontBubble)
                        {
                            if (prologues[order - 1].size == 9 || prologues[order - 1].size == 10)
                            {
                                StartCoroutine(PopDown(backBubble, backBubble.transform.childCount));
                                StartCoroutine(PopDown(selectionBubble));
                            }
                            else if(prologues[order].size == 9 || prologues[order].size == 10)
                            {
                                selectionBubble = backBubble;
                            }
                            else
                            {
                                StartCoroutine(PopDown(backBubble));
                            }

                            StartCoroutine(PopUp(frontBubble));
                        }
                        
                        if (prologues[order].size == 9 || prologues[order].size == 10)
                        {
                            frontBubble.transform.GetChild(int.Parse(prologues[order].sentence)).gameObject.SetActive(true);
                        }
                        else
                        {
                            StartCoroutine(TypeSentence(frontBubble, prologues[order].sentence));
                        }
                    }

                    /*
                    if(typeDone && prologues[order + 1].size == 9)
                    {
                        //StartCoroutine(PopDown(backBubble));
                        GameObject selectionBubble = prologues[order + 1].speaker.transform.GetChild(0).GetChild(prologues[order + 1].size - 1).gameObject;
                        selectionBubble.SetActive(true);
                        selectionBubble.transform.GetChild(int.Parse(prologues[order + 1].sentence)).gameObject.SetActive(true);
                        order++;
                    }
                    */
                    
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
                if (frontBubble.transform.GetChild(0).childCount == 0)
                {
                    StartCoroutine(PopDown(frontBubble));
                }
                else
                {
                    StartCoroutine(PopDown(frontBubble.transform.GetChild(int.Parse(prologues[order - 1].sentence)).gameObject, frontBubble.transform.childCount));
                    StartCoroutine(PopDown(backBubble));
                }
            }
        }
    }

    IEnumerator PopUp(GameObject bubble)
    {
        bubble.SetActive(true);
        yield return new WaitForSeconds(0.2f);
    }

    IEnumerator PopDown(GameObject bubble)
    {
        bubble.GetComponent<Animator>().SetBool("close", true);
        yield return new WaitForSeconds(0.2f);
        bubble.SetActive(false);
    }

    IEnumerator PopDown(GameObject bubble, int num)
    {
        for(int i = 0; i < num; i++)
        {
            bubble.transform.GetChild(i).GetComponent<Animator>().SetBool("close", true);
        }
        yield return new WaitForSeconds(0.5f);
        bubble.SetActive(false);
    }

    IEnumerator TypeSentence(GameObject bubble, string sentence)
    {
        Text text = bubble.transform.GetChild(0).GetComponent<Text>();
        text.text = string.Empty;

        yield return new WaitForSeconds(0.2f);

        foreach (var letter in sentence)
        {
            text.text += letter;
            yield return new WaitForSeconds(0.01f);
        }

        typeDone = true;
    }
}
