using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossCon2 : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D bc;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

}
