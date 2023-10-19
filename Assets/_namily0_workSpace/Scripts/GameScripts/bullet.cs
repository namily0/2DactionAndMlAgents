using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public bool bossFlag = false;
    void Update()
    {
        // ��ʊO�ɏo����e���폜����
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "enemyHead")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "boss")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            bossFlag = true;
        }
    }
}
