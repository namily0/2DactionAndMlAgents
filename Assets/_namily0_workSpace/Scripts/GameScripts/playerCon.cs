using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


namespace Player//namespace �ɂ���ăt�H���_�������Ă���C���[�W
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
                    if (this.gameObject.tag == "player1")//��
                    {
                        Move();
                        Jump();
                        Shot();
                    }
                }
                else
                {
                    if(this.gameObject.tag == "player2")//��
                    {
                        Move2();
                        Jump2();
                        Shot2();
                    }
                }
            }

            Debug.Log(bulletNum);
        }

        void Shot()
        {
            // �e�𔭎˂��鏈��
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

        void Move()
        {
            float horizontalkey = Input.GetAxis("Horizontal");
            if (horizontalkey > 0)//�E�ɍs��
            {
                this.GetComponent<SpriteRenderer>().flipX = false;//�E������
                rb.velocity = new Vector2(speed, rb.velocity.y);
                anim.SetBool("isMoving", true);
            }
            else if (horizontalkey < 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;//��������
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
            jumpFlag = (jumptime > jumpInterval);
        }

        void Shot2()
        {
            // �e�𔭎˂��鏈��
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("�����ʑł��O");
                Debug.Log(bulletTime > bulletSpan);

                Debug.Log(bulletNum);

                if (bulletTime > bulletSpan && bulletNum > 0)
                {
                    Debug.Log("���딭��");
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
            if (horizontalkey < 0)//�E�ɍs��
            {
                this.GetComponent<SpriteRenderer>().flipX = false;//�E������
                rb.velocity = new Vector2(speed * 2, rb.velocity.y);
                anim.SetBool("isMoving", true);
            }
            else if (horizontalkey > 0)
            {
                this.GetComponent<SpriteRenderer>().flipX = true;//��������
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


        void OnTriggerEnter2D(Collider2D col)//�N���A����
        {
            if (col.gameObject.tag == "clear")
            {
                Debug.Log("�N���A���܂���");
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

        void OnCollisionEnter2D(Collision2D col)//�G�ɓ��������Ƃ�������
        {
            if (col.gameObject.tag == "enemy" || col.gameObject.tag == "boss")
            {
                Debug.Log("�G�ɐڐG���Ă��܂��܂����B");
                Debug.Log(anim.GetBool("isDead"));
                if (HPtime > HPspan)
                {
                    _HP -= 1;
                    HPtime = 0F;
                }
                anim.SetBool("hurt", true);
                if (_HP <= 0)
                {
                    Debug.Log("����ł��܂��܂���");
                    anim.SetBool("isDead", true);
                }
            }
            else
            {
                anim.SetBool("hurt", false);
            }

            if (col.gameObject.tag == "enemyHead")
            {
                Debug.Log("�G�̓��𓥂݂܂���");
                bulletNum += 1;//�����𑝂₷
                Destroy(col.gameObject);//����j��
                HPtime = 0F;
            }
        }

        void OnCollisionStay2D(Collision2D col)
        {
            if (col.gameObject.tag == "Ground")
            {
                //Debug.Log("�ڒn���Ă��܂�");
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
                //Debug.Log("�󒆂ɂ��܂�");
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
            Debug.Log("�v���C���[�j��");
        }

        public void Receive(playerCon pc)
        {
            HP = pc.HP;
            bulletNum = pc.bulletNum;
            Debug.Log("���V�[�u�֐�");
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
