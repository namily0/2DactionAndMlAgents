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

    private playerCon playerTest;//他スクリプトからの参照、今回はplayerCon.cs

    void Awake()
    {
        Spawn();
        playerTest = Player.GetComponent<playerCon>();
    }

    void Update()
    {
        testText.text = "HP:" + playerTest.HP.ToString() + "  Bullet:" + playerTest.bulletNum.ToString();//文字列変換
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
