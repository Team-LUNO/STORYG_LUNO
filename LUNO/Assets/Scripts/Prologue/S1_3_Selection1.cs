using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1_3_Selection1 : SelectionBalloon
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
        
    }

    public override void First()
    {
        prologueManager.IncreaseOrder();
    }

    public override void Second()
    {
        Debug.Log("책 다시 읽기");
        prologueManager.IncreaseOrder();
    }
}
