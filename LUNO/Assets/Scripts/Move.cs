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

    bool isLadder = false;
    public float speed = 10f;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {

        //점프
        if (Input.GetButtonDown("Jump") && !anim.GetBool("IsJump")) { 
           rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
           anim.SetBool("IsJump", true);
        }


        if (Input.GetButtonUp("Horizontal")){       //버튼을 떼면 빠르게 멈춤  
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.000000001f, rigid.velocity.y);
        }

        if(Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }


        //걷는모션, 디폴트모션의 전환
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

        //aswd로 움직이는 기능
        float h = Input.GetAxis("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // 플레이어의 최대 속도를 제한하는 기능
        if(rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if(rigid.velocity.x < maxSpeed*(-1))
        {
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
        }

        //충돌감지(플랫폼에 닿았는가?)
        if(rigid.velocity.y < 0) { 
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("platform"));

            if (rayHit.collider != null)
            {
                if(rayHit.distance < 0.5f)
                    anim.SetBool("IsJump", false);
            }
        }

        //사다리 타기
        if(isLadder)
        {
            float v = Input.GetAxis("Vertical");
            rigid.velocity = new Vector2(rigid.velocity.x, v * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //사다리 위에 있을 때
        if(collision.gameObject.CompareTag("Rope2"))
        {
            isLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //사다리 아닐때
        if (collision.gameObject.CompareTag("Rope2"))
        {
            isLadder = false;
        }
    }
}