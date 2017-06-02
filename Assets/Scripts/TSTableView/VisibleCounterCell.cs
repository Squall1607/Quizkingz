using UnityEngine;
using System.Collections;
using Tacticsoft;
using UnityEngine.UI;

namespace Tacticsoft
{
	//Inherit from TableViewCell instead of MonoBehavior to use the GameObject
	//containing this component as a cell in a TableView
	public class VisibleCounterCell : TableViewCell
	{
		public Text _rowNum;
		public Image _avatar;
		public Text _uname;
		public Text _score;
		public GameObject _fbic;

		public void SetRowData (PlayerData p, int row)
		{
			_avatar.sprite = Resources.Load<Sprite> ("default-ava");
			_rowNum.text = row.ToString ();
			_uname.text = p.Display.ToUpper ();
			_score.text = p.Score.ToString ();
			if (p.FacebookID != "-1") {
				_fbic.SetActive (true);
			} else {
				_fbic.SetActive (false);
			}
		}

	}
}
