using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player;

public class camera : MonoBehaviour
{
    private float CSpeed = 1.0F;
    private GameObject Cplayer;
    private GameObject Cplayer2;
    private playerCon p1;
    private playerCon p2;
    private Vector3 currentVelocity;
    public bool PlayerFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        if(GameObject.Find("Player(Clone)") == null)
        {
            Debug.Log("player1が見つかりません");
        }
        else
        {
            Cplayer = GameObject.Find("Player(Clone)");
        }

        if (GameObject.Find("Player2(Clone)") == null)
        {
            Debug.Log("player2が見つかりません");
        }
        else
        {
            Cplayer2 = GameObject.Find("Player2(Clone)");
        }

        p1 = Cplayer.GetComponent<playerCon>();
        p2 = Cplayer.GetComponent<playerCon>();
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.X))//true = 白
        {
            PlayerFlag = !PlayerFlag;
            if(PlayerFlag)
            {
                p1.Receive(p2);
            }
            else
            {
                p2.Receive(p1);
            }
        }

        if(PlayerFlag)
        {
            if (Cplayer != null)
            {
                if (Cplayer.GetComponent<SpriteRenderer>().flipX == false)
                {
                    Vector3 target1 = new Vector3(Cplayer.transform.position.x + 4.0F, Cplayer.transform.position.y + 1.0F, Cplayer.transform.position.z - 10F);//ここ記事にしたい。元々Z軸まで進んでしまっていたから、カメラが映らなくなっていた。
                    transform.position = Vector3.SmoothDamp(transform.position, target1, ref currentVelocity, CSpeed);
                }
                else
                {
                    Vector3 target2 = new Vector3(Cplayer.transform.position.x - 4.0F, Cplayer.transform.position.y + 1.0F, Cplayer.transform.position.z - 10F);
                    transform.position = Vector3.SmoothDamp(transform.position, target2, ref currentVelocity, CSpeed);
                }
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                Debug.Log("プレイヤーが見つかりません");
            }
        }
        else
        {
            if (Cplayer2 != null)
            {
                if (Cplayer2.GetComponent<SpriteRenderer>().flipX == false)
                {
                    Vector3 target3 = new Vector3(Cplayer2.transform.position.x + 4.0F, Cplayer2.transform.position.y -1.0F, Cplayer2.transform.position.z - 10F);//ここ記事にしたい。元々Z軸まで進んでしまっていたから、カメラが映らなくなっていた。
                    transform.position = Vector3.SmoothDamp(transform.position, target3, ref currentVelocity, CSpeed);
                }
                else
                {
                    Vector3 target4 = new Vector3(Cplayer2.transform.position.x - 4.0F, Cplayer2.transform.position.y -1.0F, Cplayer2.transform.position.z - 10F);
                    transform.position = Vector3.SmoothDamp(transform.position, target4, ref currentVelocity, CSpeed);
                }
                transform.eulerAngles = new Vector3(0, 0, 180);
            }
            else
            {
                Debug.Log("プレイヤー2が見つかりません");
            }
        }

    }
}
