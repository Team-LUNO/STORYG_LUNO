using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTriggerSection : MonoBehaviour
{
    BoxCollider2D collid;

    // Start is called before the first frame update
    void Start()
    {
        collid = GetComponent<BoxCollider2D>();
        SettingTriggerSection();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SettingTriggerSection()
    {
        collid.size = new Vector2(2f, 1f);
    }


}
