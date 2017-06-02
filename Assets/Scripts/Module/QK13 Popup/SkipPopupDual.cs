using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkipPopupDual : BasePageController
{
    public GameObject txtDetail, txtDetail1Point, txt1point, txtNoPenalty, PanelBlurPopUp;
    public QK07Duel_Controller QK07Duel_Controller;
    //public CameraController blur;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    void OnEnable(){
        //blur.turnBlurOn();

        PanelBlurPopUp.SetActive(true);
        if (GameData.currentQuestion > 9)
        {
            txtDetail1Point.SetActive(true);
            txt1point.SetActive(true);
            txtDetail.SetActive(false);
            txtNoPenalty.SetActive(false);
        }
        else
        {
            txtDetail.SetActive(true);
            txtNoPenalty.SetActive(true);
            txtDetail1Point.SetActive(false);
            txt1point.SetActive(false);
        }
    }

    void OnDisable()
    {
        //blur.turnBlurOff();
        PanelBlurPopUp.SetActive(false);
    }

    public void skipClick() {
        QK07Duel_Controller.btnSkipClick();
        this.DisplayScene(false);
    }
}
