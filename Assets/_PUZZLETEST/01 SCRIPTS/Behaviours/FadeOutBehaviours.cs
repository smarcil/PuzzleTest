using UnityEngine;
using UnityEngine.UI;

namespace Akerue.PuzzleTest.Behaviours{
	public class FadeOutBehaviours : MonoBehaviour {

		#region ### FIELDS ###

		Image _fadeOutImage;

		#endregion
		#region  ### UNITY CALLBACK ###

		// Use this for initialization
		void Start () {
			_fadeOutImage = GetComponentInChildren<Image>();
		}
		
		// Update is called once per frame
		void Update () {
			Color c = _fadeOutImage.color;
			c.a -= Time.deltaTime;
			_fadeOutImage.color = c;
			if(_fadeOutImage.color.a <= 0){
				DestroyImmediate(this.gameObject);
			}
		}

		#endregion
	}
}