using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class QK14_2_Rumble_Fee : BasePageController
{
    public CameraController cam;
    public List<Text> lstCoin;
    public List<GameObject> lstSpr;
    public static int fee;
	public GameObject QK14_3_StartGame,QK14RumbleCreateGame, PanelBlurPopUp;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        int coin = (int)GameData.playerData.Coin;
       // cam.turnBlurOn();
        PanelBlurPopUp.SetActive(true);
        if (GameData.playerData != null)
        {
            Debug.Log(GameData.entryFee.Length);
            for (int i = 1; i < GameData.entryFee.Length; i++)
            {
                lstCoin[i - 1].text = GameData.entryFee[i].ToString();

            }

            for (int i = 0; i < lstCoin.Count; i++)
            {
                if (coin < int.Parse(lstCoin[i].text))
                {
                    lstSpr[i].SetActive(true);
                }

            }

        }
    }

    void OnDisable()
    {
        //cam.turnBlurOff();
        PanelBlurPopUp.SetActive(false);
        foreach (var item in lstSpr)
        {
            item.SetActive(false);
        }
    }

    public void onClick(Text lblFee)
    {
        if (lblFee.text == "FREE")
        {
            fee = 0;
			QK14_3_StartGame.SetActive(true);
            this.DisplayScene(false);
            QK14RumbleCreateGame.SetActive(false);
        }
        else
        {
            fee = int.Parse(lblFee.text);
			QK14_3_StartGame.SetActive(true);
            this.DisplayScene(false);
            QK14RumbleCreateGame.SetActive(false);
            Debug.Log(fee);
        }
        
    }
}
