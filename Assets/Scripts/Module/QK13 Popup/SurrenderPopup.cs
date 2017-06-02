using UnityEngine;
using System.Collections;

public class SurrenderPopup : BasePageController
{
    //public CameraController blur;
    public QK07Duel_Controller QK07Duel_Controller;
    public GameObject PanelBlurPopUp;
    
    void OnEnable()
    {

        PanelBlurPopUp.SetActive(true);
        BaseService.Instance.gameService.GameStopDelegate += gameStopResponse;
    }

    void OnDisable()
    {

        PanelBlurPopUp.SetActive(false);
    }

    public void SurrenderClick(){
        BaseService.Instance.gameService.sendStopGame();        
    }

    void gameStopResponse()
    {
        this.DisplayScene(false);
    }
}
