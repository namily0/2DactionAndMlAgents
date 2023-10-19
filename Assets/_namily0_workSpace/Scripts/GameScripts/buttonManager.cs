using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class buttonManager : MonoBehaviour
{
    public GameObject stageB1;
    public GameObject stageB2;
    public GameObject stageB3;
    private Button button1;
    private Button button2;
    private Button button3;

    // Start is called before the first frame update
    void Start()
    {
        button1 = stageB1.GetComponent<Button>();
        button2 = stageB2.GetComponent<Button>();
        button3 = stageB3.GetComponent<Button>();

        button1.onClick.AddListener(OnClickButton1);
        button2.onClick.AddListener(OnClickButton2);
        button3.onClick.AddListener(OnClickButton3);

        Debug.Log("starting");
        Debug.Log(SceneManager.GetActiveScene().name);
    }

    void OnClickBase()
    {

    }

    public void OnClickButton1()
    {
        Debug.Log("stage1 start");
        SceneManager.LoadScene("game1");
    }

    public void OnClickButton2()
    {
        Debug.Log("stage2 start");
    }

    public void OnClickButton3()
    {
        Debug.Log("stage3 start");
    }


    void Update()
    {
    }
}
