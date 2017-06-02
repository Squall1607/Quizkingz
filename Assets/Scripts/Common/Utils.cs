using UnityEngine;
using System.Collections;
using SimpleJSON;

public class Utils
{
	public static IEnumerator LoadImage (string url, System.Action<Sprite> rs)
	{
		if (url != "") {
			WWW www = new WWW (url);
			float elapsedTime = 0.0f;

			while (!www.isDone) {
				elapsedTime += Time.deltaTime;
				if (elapsedTime >= 10.0f)
					break;
				yield return null;
			}

			if (!www.isDone || !string.IsNullOrEmpty (www.error)) {
				Debug.LogError ("Load Failed "+ www.error);
				yield break;
			}

			Sprite sprite = new Sprite ();
			sprite = Sprite.Create (www.texture, new Rect (0, 0, www.texture.width, www.texture.height), Vector2.zero);

			rs (sprite);
		}
	}

	public static IEnumerator UploadImage (byte[] img)
	{
		string uploadURL = "https://quizkingz-api.herokuapp.com/upload/avatar";
		WWWForm postForm = new WWWForm ();
		postForm.AddBinaryData ("avatar", img, "temp.png", "image/png");
		WWW upload = new WWW (uploadURL, postForm);    
		yield return upload;
		if (upload.error == null) {
			JSONClass rootNode = new JSONClass ();
			var N = JSON.Parse (upload.text);
			var imageLink = N ["result"] ["url"].Value;   
			var imageName = N ["result"] ["name"].Value;
			var link = "http://" + imageLink + imageName;
			BaseService.Instance.playerService.sendChangeAvatar (link);
		} else {
			Debug.Log ("Error during upload: " + upload.error);
		}

	}
}
