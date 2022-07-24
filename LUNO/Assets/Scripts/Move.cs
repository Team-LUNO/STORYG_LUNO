using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public float jumpTimeLimit = 0.1f;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {

        //����
        if (Input.GetButtonDown("Jump") && !anim.GetBool("IsJump")) { 
           rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
           anim.SetBool("IsJump", true);
        }


        if (Input.GetButtonUp("Horizontal")){       //��ư�� ���� ������ ����  
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.000000001f, rigid.velocity.y);
        }

        if(Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }


        //�ȴ¸��, ����Ʈ����� ��ȯ
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("IsWalk", false);
        }
        else
        {
            anim.SetBool("IsWalk", true);
        }
    }


    void FixedUpdate()
    {

        //aswd�� �����̴� ���
        float h = Input.GetAxis("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);


        // �÷��̾��� �ִ� �ӵ��� �����ϴ� ���
        if(rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if(rigid.velocity.x < maxSpeed*(-1))
        {
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
        }

        //�浹����(�÷����� ��Ҵ°�?)
        if(rigid.velocity.y < 0) { 
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("platform"));

            if (rayHit.collider != null)
            {
                if(rayHit.distance < 0.5f)
                    anim.SetBool("IsJump", false);
            }
        }
    }

}