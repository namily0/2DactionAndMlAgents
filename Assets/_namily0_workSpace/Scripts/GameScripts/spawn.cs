using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Player;

public class spawn: MonoBehaviour
{
    
    public GameObject playerPrefab;
    public GameObject spawnPoint;
    private GameObject Player;
    public TextMeshProUGUI testText;
    public TextMeshProUGUI ctest;

    private playerCon playerTest;//���X�N���v�g����̎Q�ƁA�����playerCon.cs

    void Awake()
    {
        Spawn();
        playerTest = Player.GetComponent<playerCon>();
    }

    void Update()
    {
        testText.text = "HP:" + playerTest.HP.ToString() + "  Bullet:" + playerTest.bulletNum.ToString();//������ϊ�
        playerTest.TextFrame = testText;

        if(playerTest.clearFlag)
        { 
            ctest.text = "GAME CLEAR!!!";
            playerTest.ClearText = ctest;
        }
    }

    void Spawn()
    {
        Player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
    }
}
