using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace Player//namespace によってフォルダ分けしているイメージ
{
    public class playerCon2 : MonoBehaviour
    {
        //animation
        private Animator anim;

        //UI
        public TextMeshProUGUI TextFrame;
        public TextMeshProUGUI ClearText;

        //moving
        private Rigidbody2D rb;
        private float speed = 2.0F;
        private float jumpForce = -10F;
        private float jumpInterval = 2.0F;
        private float jumptime = 0F;

        //shot
        private float bulletTime = 0F;
        private float bulletSpan = 1.0F;
        private float bulletSpeed = -5.0F;
        public int bulletNum = 0;//これもpublicにしたが良いのか。
        public GameObject bulletPrefab;

        //playerInfo
        private int _HP = 2;
        public int HP
        {
            get { return _HP; }
        }

        public bool clearFlag = false;
        private float HPtime = 0F;
        private float HPspan = 2.0F;


        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }


        void Update()
        {
            if (clearFlag == false && anim.GetBool("isDead") == false)
            {
                Move();
                Jump();
                Shot();
            }
        }

        void Shot()
        {
            // 弾を発射する処理
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (bulletTime > bulletSpan && bulletNum > 0)
                {
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    if (this.GetComponent<SpriteRenderer>().flipX == false)
                        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * bulletSpeed;

                    if (this.GetComponent<SpriteRenderer>().flipX == true)
                        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * bulletSpeed;

                    bulletTime = 0F;
                    bulletNum -= 1;
                }
            }
            bulletTime += Time.deltaTime;
        }

        void Move()
        {
            float horizontalkey = Input.GetAxis("Horizontal");
            if (horizontalkey < 0)//右に行く
            {
                this.GetComponent<SpriteRenderer>().flipX = false;//右を向く
                rb.velocity = new Vector2(speed, rb.velocity.y);
                anim.SetBool("isMoving", true);
            }
            else if (horizontalkey > 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;//左を向く
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }

            HPtime += Time.deltaTime;
        }

        void Jump()
        {
            jumptime += Time.deltaTime;
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && jumptime > jumpInterval)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumptime = 0;
            }
        }


        void OnTriggerEnter2D(Collider2D col)//クリア判定
        {
            if (col.gameObject.tag == "clear")
            {
                Debug.Log("クリアしました");
                anim.SetBool("victory", true);
                clearFlag = true;
            }
            else
            {
                anim.SetBool("victory", false);
            }
        }
        void OnCollisionEnter2D(Collision2D col)//敵に当たったときを書く
        {
            if (col.gameObject.tag == "enemy" || col.gameObject.tag == "boss")
            {
                Debug.Log("敵に接触してしまいました。");
                Debug.Log(anim.GetBool("isDead"));
                if (HPtime > HPspan)
                {
                    _HP -= 1;
                    HPtime = 0F;
                }
                anim.SetBool("hurt", true);
                if (_HP <= 0)
                {
                    Debug.Log("死んでしまいました");
                    anim.SetBool("isDead", true);
                }
            }
            else
            {
                anim.SetBool("hurt", false);
            }

            if (col.gameObject.tag == "enemyHead")
            {
                Debug.Log("敵の頭を踏みました");
                bulletNum += 1;//球数を増やす
                Destroy(col.gameObject);//頭を破壊
                HPtime = 0F;
            }
        }

        void OnCollisionStay2D(Collision2D col)
        {
            if (col.gameObject.tag == "Ground")
            {
                //Debug.Log("接地しています");
                anim.SetBool("isGrounded", true);
            }
            else
            {
                anim.SetBool("isGrounded", false);
            }

        }

        void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Ground")
            {
                //Debug.Log("空中にいます");
                anim.SetBool("isGrounded", false);
            }
            else
            {
                anim.SetBool("isGrounded", true);
            }
        }

        void Death()
        {
            Destroy(this.gameObject);
            Debug.Log("プレイヤー破壊");
        }
    }
}
