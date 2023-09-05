using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossCon : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        Debug.Log("test");
    }

    void Update()
    {
        DeathCheck();
    }

    void DeathCheck()
    {
        if (this.gameObject.transform.root.gameObject == null)//親が壊されたらという意味
        {
            bc.enabled = false;
            enemyDeath();
        }
    }

    void DestroySlime()
    {
        Destroy(this.gameObject);
        Debug.Log("スライム破壊された");
    }

    void enemyDeath()
    {
        Debug.Log("スライム死亡");
        anim.SetBool("death", true);
    }
}
