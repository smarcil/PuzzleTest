using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Akerue.PuzzleTest.Behaviours.GameManagers;

namespace Akerue.PuzzleTest.Behaviours.SceneManagers.MainUIControllers{
	[System.Serializable]
	public class ButtonsController {

		#region ### FIELDS ###

		#region ** SERIALIZE FIELDS **

		[SerializeField, Tooltip("Button for starting the game")]
		Button _StartPuzzleBtn;

		#endregion

		#endregion
		#region  ### METHODS ###

		public void InitController(){
			_StartPuzzleBtn.onClick.AddListener(delegate{onClickStartPuzzleBtn(_StartPuzzleBtn);});
		}

		void onClickStartPuzzleBtn(Button pButton){
			LoadingGmanager.LoadScene(LoadingGmanager.scenes.PuzzleScene);
		}

		#endregion
	}
}