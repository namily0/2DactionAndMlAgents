using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


namespace Player//namespace �ｿｽﾉゑｿｽ�ｿｽ�ｿｽﾄフ�ｿｽH�ｿｽ�ｿｽ�ｿｽ_�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽﾄゑｿｽ�ｿｽ�ｿｽC�ｿｽ�ｿｽ�ｿｽ[�ｿｽW
{
    public class playerCon : MonoBehaviour
    {
        //animation
        private Animator anim;

        //camera
        private GameObject Camera;
        private camera _camera;

        //clear
        private GameObject LastWall;

        //UI
        public TextMeshProUGUI TextFrame;
        public TextMeshProUGUI ClearText;

        //moving
        private Rigidbody2D rb;
        private float speed = 2.0F;
        private float jumpForce = 10F;
        private float jumpInterval = 2.0F;
        private float jumptime = 0F;

        //shot
        private float bulletTime = 0F;
        private float bulletSpan = 1.0F;
        private float bulletSpeed = 5.0F;

        public int _bulletNum = 0;
        public int bulletNum
        {
            get { return _bulletNum; }
            set { _bulletNum = value; }
        }
        public GameObject bulletPrefab;

        //playerInfo
        private int _HP = 2;
        public int HP
        {
            get { return _HP; }
            set { _HP = value; }
        }

        public bool clearFlag = false;
        private float HPtime = 0F;
        private float HPspan = 2.0F;

        //playerFlag(for Agent)
        public bool jumpFlag = false;
        public bool bulletFlag = false;
        public bool deadFlag = false;
        public bool hurtFlag = false;
        public bool killFlag = false;


        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            Camera = GameObject.Find("Main Camera");
            _camera = Camera.GetComponent<camera>();
            LastWall = GameObject.Find("lastWall");
        }


        void Update()
        {
            if (clearFlag == false && anim.GetBool("isDead") == false)
            {
                if(_camera.PlayerFlag)
                {
                    if (this.gameObject.tag == "player1")//�ｿｽ�ｿｽ
                    {
                        Move();
                        Jump();
                        Shot();
                    }
                }
                else
                {
                    if(this.gameObject.tag == "player2")//�ｿｽ�ｿｽ
                    {
                        Move2();
                        Jump2();
                        Shot2();
                    }
                }
            }

            Debug.Log(bulletNum);
        }

        public void Shot()
        {
            // �ｿｽe�ｿｽｭ射ゑｿｽ�ｿｽ髀茨ｿｽ�ｿｽ
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

            bulletFlag = ((bulletTime > bulletSpan) && bulletNum > 0);
            bulletTime += Time.deltaTime;
        }

        public void Move()
        {
            float horizontalkey = Input.GetAxis("Horizontal");
            if (horizontalkey > 0)//�ｿｽE�ｿｽﾉ行�ｿｽ�ｿｽ
            {
                this.GetComponent<SpriteRenderer>().flipX = false;//�ｿｽE�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ
                rb.velocity = new Vector2(speed, rb.velocity.y);
                anim.SetBool("isMoving", true);
            }
            else if (horizontalkey < 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;//�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }

            HPtime += Time.deltaTime;
        }

        public void Jump()
        {
            jumptime += Time.deltaTime;
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && jumptime > jumpInterval)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumptime = 0;
            }
            jumpFlag = (jumptime > jumpInterval);
        }

        void Shot2()
        {
            // �ｿｽe�ｿｽｭ射ゑｿｽ�ｿｽ髀茨ｿｽ�ｿｽ
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽﾊ打つ抵ｿｽ�ｿｽO");
                Debug.Log(bulletTime > bulletSpan);

                Debug.Log(bulletNum);

                if (bulletTime > bulletSpan && bulletNum > 0)
                {
                    Debug.Log("�ｿｽ�ｿｽ�ｿｽ�発�ｿｽ�ｿｽ");
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    if (this.GetComponent<SpriteRenderer>().flipX == false)
                        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * -bulletSpeed;

                    if (this.GetComponent<SpriteRenderer>().flipX == true)
                        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * -bulletSpeed;

                    bulletTime = 0F;
                    bulletNum -= 1;
                }
            }
            bulletFlag = ((bulletTime > bulletSpan) && bulletNum > 0);
            bulletTime += Time.deltaTime;
        }

        void Move2()
        {
            float horizontalkey = Input.GetAxis("Horizontal");
            if (horizontalkey < 0)//�ｿｽE�ｿｽﾉ行�ｿｽ�ｿｽ
            {
                this.GetComponent<SpriteRenderer>().flipX = false;//�ｿｽE�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ
                rb.velocity = new Vector2(speed * 2, rb.velocity.y);
                anim.SetBool("isMoving", true);
            }
            else if (horizontalkey > 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;//�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ
                rb.velocity = new Vector2(-speed * 2, rb.velocity.y);
                anim.SetBool("isMoving", true);
            }
            else
            {
                anim.SetBool("isMoving", false);
            }

            HPtime += Time.deltaTime;
        }

        void Jump2()
        {
            jumptime += Time.deltaTime;
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && jumptime > jumpInterval)
            {
                rb.velocity = new Vector2(rb.velocity.x, -jumpForce);
                jumptime = 0;
            }
            jumpFlag = (jumptime > jumpInterval);
        }


        void OnTriggerEnter2D(Collider2D col)//�ｿｽN�ｿｽ�ｿｽ�ｿｽA�ｿｽ�ｿｽ�ｿｽ�ｿｽ
        {
            if (col.gameObject.tag == "clear")
            {
                Debug.Log("�ｿｽN�ｿｽ�ｿｽ�ｿｽA�ｿｽ�ｿｽ�ｿｽﾜゑｿｽ�ｿｽ�ｿｽ");
                anim.SetBool("victory", true);
                clearFlag = true;
            }
            else
            {
                anim.SetBool("victory", false);
            }

            if (col.gameObject.tag == "clearCheck")
            {
                Destroy(LastWall);
            }
        }

        void OnCollisionEnter2D(Collision2D col)//�ｿｽG�ｿｽﾉ難ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽﾆゑｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ
        {
            if (col.gameObject.tag == "enemy" || col.gameObject.tag == "boss")
            {
                Debug.Log("�ｿｽG�ｿｽﾉ接触�ｿｽ�ｿｽ�ｿｽﾄゑｿｽ�ｿｽﾜゑｿｽ�ｿｽﾜゑｿｽ�ｿｽ�ｿｽ�ｿｽB");
                Debug.Log(anim.GetBool("isDead"));
                if (HPtime > HPspan)
                {
                    _HP -= 1;
                    HPtime = 0F;
                }
                anim.SetBool("hurt", true);
                hurtFlag = true;
                if (_HP <= 0)
                {
                    Debug.Log("�ｿｽ�ｿｽ�ｿｽ�ｿｽﾅゑｿｽ�ｿｽﾜゑｿｽ�ｿｽﾜゑｿｽ�ｿｽ�ｿｽ");
                    anim.SetBool("isDead", true);
                    deadFlag = true;
                }
                else
                {
                    deadFlag = false;
                }
            }
            else
            {
                anim.SetBool("hurt", false);
                hurtFlag = false;
            }

            if (col.gameObject.tag == "enemyHead")
            {
                Debug.Log("�ｿｽG�ｿｽﾌ難ｿｽ�ｿｽ･みまゑｿｽ�ｿｽ�ｿｽ");
                bulletNum += 1;//�ｿｽ�ｿｽ�ｿｽ�ｿｽ�ｿｽ揩竄ｷ
                Destroy(col.gameObject);//�ｿｽ�ｿｽ�ｿｽ�ｿｽj�ｿｽ�ｿｽ
                HPtime = 0F;
                killFlag = true;
            }
            else
            {
                killFlag = false;
            }
        }

        void OnCollisionStay2D(Collision2D col)
        {
            if (col.gameObject.tag == "Ground")
            {
                //Debug.Log("�ｿｽﾚ地�ｿｽ�ｿｽ�ｿｽﾄゑｿｽ�ｿｽﾜゑｿｽ");
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
                //Debug.Log("�ｿｽ�にゑｿｽ�ｿｽﾜゑｿｽ");
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
            Debug.Log("�ｿｽv�ｿｽ�ｿｽ�ｿｽC�ｿｽ�ｿｽ�ｿｽ[�ｿｽj�ｿｽ�ｿｽ");
        }

        public void Receive(playerCon pc)
        {
            HP = pc.HP;
            bulletNum = pc.bulletNum;
            Debug.Log("�ｿｽ�ｿｽ�ｿｽV�ｿｽ[�ｿｽu�ｿｽﾖ撰ｿｽ");
            Debug.Log(bulletNum);
        }

        public void ClearScene()
        {
            SceneManager.LoadScene("clear");
        }

        public void GameOver()
        {
            SceneManager.LoadScene("gameOver");
        }
    }

}
