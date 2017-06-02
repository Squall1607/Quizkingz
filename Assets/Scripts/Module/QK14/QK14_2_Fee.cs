using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class QK14_2_Fee : BasePageController
{
    //public CameraController cam;
 //   public List<Text> lstCoin;
 //   public List<GameObject> lstSpr;
    public static int fee;
    public GameObject QK13_3_Find, QK13Duel, fakeMain, PanelBlurPopUp;
    public bool checkRevenge = false;
    public botController bot;
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
        //cam.turnBlurOn();
        PanelBlurPopUp.SetActive(true);
        //        int coin = (int)GameData.playerData.Coin;
        //        
        //        if (GameData.playerData != null)
        //        {
        //            Debug.Log(GameData.entryFee.Length);
        //            for (int i = 1; i < GameData.entryFee.Length; i++)
        //            {
        //                lstCoin[i - 1].text = GameData.entryFee[i].ToString();
        //
        //            }
        //
        //            for (int i = 0; i < lstCoin.Count; i++)
        //            {
        //                if (coin < int.Parse(lstCoin[i].text))
        //                {
        //                    lstSpr[i].SetActive(true);
        //                }
        //
        //            }

        //	    }
    }

    void OnDisable()
    {
        fakeMain.SetActive(true);
        //cam.turnBlurOff();
        PanelBlurPopUp.SetActive(false);
        //        foreach (var item in lstSpr)
        //        {
        //            item.SetActive(false);
        //        }
    }

    public void onClick(Text lblFee)
    {
        if (checkRevenge)
        {
            if (lblFee.text == "FREE")
            {
                fee = 0;
                List<int> lstid = new List<int>();
                int id = int.Parse(bot.id);
                lstid.Add(id);
                //print((int)GameData.playerData.Id);
                BaseService.Instance.gameService.sendStartGameMulti(1, lstid, 0, fee);
                this.DisplayScene(false);
            }
            else
            {
                fee = int.Parse(lblFee.text);
                List<int> lstid = new List<int>();
                int id = int.Parse(bot.id);
                lstid.Add(id);
                //print((int)GameData.playerData.Id);
                BaseService.Instance.gameService.sendStartGameMulti(1, lstid, 0, fee);
                this.DisplayScene(false);
                Debug.Log(fee);
            }
        }
        else
        {
            if (lblFee.text == "FREE")
            {
                fee = 0;
                QK13Duel.SetActive(false);
                QK13_3_Find.SetActive(true);
                this.DisplayScene(false);
            }
            else
            {
                fee = int.Parse(lblFee.text);
                QK13Duel.SetActive(false);
                QK13_3_Find.SetActive(true);
                this.DisplayScene(false);
                Debug.Log(fee);
            }
        }
       
        
    }
}
