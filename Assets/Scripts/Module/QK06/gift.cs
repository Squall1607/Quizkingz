using UnityEngine;
using System.Collections;

public class gift : MonoBehaviour {

	void Start()
	{
		lifeTime ();
	}

	void lifeTime()
	{
		iTween.ScaleFrom (gameObject,iTween.Hash("scale",gameObject.transform.localScale,"time",0.001f,"delay",2,"oncomplete","destroy","oncompletetarget",gameObject));
	}
	void destroy()
	{
		Destroy (gameObject);

	}
}
