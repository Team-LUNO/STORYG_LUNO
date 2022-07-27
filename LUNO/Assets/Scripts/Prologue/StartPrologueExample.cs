using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPrologueExample : MonoBehaviour
{
    [SerializeField]
    private PrologueManager prologueManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // E를 누르면 대화를 시작하는 예시
        if (Input.GetKeyDown(KeyCode.E))
        {
            prologueManager.StartPrologue();
        }
    }
}
