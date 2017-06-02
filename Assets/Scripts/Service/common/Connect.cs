using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sfs2X.Entities.Data;
using System.IO;
using Facebook.Unity;
using UnityEngine.UI;

//using System.

public class Connect : MonoBehaviour
{
	public CameraController blur;
	public GameObject splashPanda;
	//public Panel03Controller home;
	public GameObject ErrorPopup, PanelBlurPopUp;
	public QK04B1_2_GiftShop_Controller QK04;
	//public GameObject newInvitePopup, QK13_4_InvitationSent;
	//public GameObject DualInvi, RumbleInvi;
	public static List<GameObject> lstInvi = new List<GameObject> ();
	// Use this for initialization

	void Start ()
	{

		Application.runInBackground = true;

		//PlayerPrefs.DeleteAll ();
		//BaseService.Instance.OnJoinRoomDelegate += home.SceneSwitch;
		if (PlayerPrefs.HasKey ("username")) {
			connect ();

		} else {
			splashPanda.SetActive (true);
			Invoke ("connect", 2.2f);
		}

		//splashPanda.SetActive(true);
		//Invoke("connect", 2.2f);

		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		if (FB.IsLoggedIn) {
			Debug.Log ("Login Roiiiiiiiiiiiiiiiiiii");
			BaseService.Instance.playerService.SendToken (AccessToken.CurrentAccessToken.TokenString);

		} else {
			Debug.Log ("Login cccccccccccccccccccc");
		}

	}

	void connect ()
	{
		
		BaseService.Instance.Connect ();
		splashPanda.SetActive (false);



		BaseService.Instance.errorHandler.errorDelegate += ErrorPopup.GetComponent<ErrorPopUp_Controller> ().turnOn;

		BaseService.Instance.OnConnectionLostDelegate += ErrorPopup.GetComponent<ErrorPopUp_Controller> ().turnOnDisconnect;

		BaseService.Instance.OnLoginErrorDelegate += ErrorPopup.GetComponent<ErrorPopUp_Controller> ().turnOnDisconnect;

		BaseService.Instance.OnLogoutDelegate += ErrorPopup.GetComponent<ErrorPopUp_Controller> ().turnOnDisconnect;

		BaseService.Instance.OnServerOffDelegate += ErrorPopup.GetComponent<ErrorPopUp_Controller> ().turnOnDisconnect;

		BaseService.Instance.playerService.LoginDelegate += loadImage;

		//BaseService.Instance.gameService.SendStartGameMultiDelegate += turnOnPopup;
		//BaseService.Instance.gameService.SendStartGameMultiDelegate -= turnOnPopup;

		BaseService.Instance.gameService.ProductDelegate += QK04.InstantiateItem;
	}

	void loadImage ()
	{
		StartCoroutine (Utils.LoadImage (GameData.playerData.AvatarURL, value => GameData.playerData.Avartar = value));
	}



	//    void turnOnPopup()
	//    {
	//        //Debug.Log("POPUPPPPPPPPPPPPPPPPPPPPPPPPPPPPPP");
	//        if (DuelData.senderTime > 0)
	//        {
	//            //Debug.Log(DuelData.senderTime);
	//            QK13_4_InvitationSent.SetActive(true);
	//        }
	//        else if (DuelData.receiverTime > 0)
	//        {
	//            //Debug.Log(DuelData.receiverTime);
	//            DualInvi = new GameObject();
	//            lstInvi.Add(DualInvi);
	//            newInvitePopup.SetActive(true);
	//        }
	//    }

	void Update ()
	{
		NetworkReachability isConnect = Application.internetReachability;
		string reachability = isConnect.ToString ();
		if (reachability == "NotReachable") {
            //blur.turnBlurOn ();
            PanelBlurPopUp.SetActive(true);

            ErrorPopup.GetComponentsInChildren<Text> (true) [1].text = "Open your internet connection to play! ";
			ErrorPopup.SetActive (true);
		}

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit, 100))
        //    {
        //        Debug.Log(hit.transform.gameObject.name);
        //    }
        //}

    }

	void OnApplicationQuit()
	{
		Debug.Log("Application ending after " + Time.time + " seconds");
		BaseService.Instance.smartFox.Disconnect();
	}

	void FixedUpdate ()
	{		
		BaseService.Instance.smartFox.ProcessEvents ();

	}

}
