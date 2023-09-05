using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCon : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private float speed = 2.0F;
    private float checkTime = 0F;
    private float distanceTime = 1.0F;

    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        DeathCheck();
    }

    void Move()
    {
        checkTime += Time.deltaTime;
        if (checkTime < distanceTime)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            this.GetComponent<SpriteRenderer>().flipX = false;//�E������
            anim.SetBool("run", true);

        }
        else if (checkTime < 2 * distanceTime)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            this.GetComponent<SpriteRenderer>().flipX = true;//��������
            anim.SetBool("run", true);
        }
        else
        {
            anim.SetBool("run", false);
            checkTime = 0F;
        }
    }

    void DeathCheck()
    {
        if(this.gameObject.transform.childCount == 0)
        {
            Debug.Log("�����j�󂳂ꂽ");
            bc.enabled = false;
            enemyDeath();
        }
    }

    void DestroySlime()
    {
        Destroy(this.gameObject);
        Debug.Log("�X���C���j�󂳂ꂽ");
    }

    void enemyDeath()
    {
        Debug.Log("�X���C�����S");
        anim.SetBool("death", true);
    }
}
