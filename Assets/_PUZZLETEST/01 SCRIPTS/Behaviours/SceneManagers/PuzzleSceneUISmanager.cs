using UnityEngine;
using UnityEngine.UI;

namespace Akerue.PuzzleTest.Behaviours.SceneManagers
{
    public class PuzzleSceneUISmanager : MonoBehaviour {

		#region ### FIELDS ###

		#region ** SERIALIZE FIELDS **

		[SerializeField, Tooltip("Panel to display the success message")]
		GameObject _SuccessPanel;
		[SerializeField, Tooltip("Button for replay")]
		Button _replayBtn;

		#endregion

		static PuzzleSceneUISmanager _instance;

		#endregion
		#region  ### UNITY CALLBACK ###

		void Awake(){
			_instance = this;
			_replayBtn.onClick.AddListener(delegate{onClickReplay(_replayBtn);});
		}

		// Use this for initialization
		void Start () {
			_SuccessPanel.SetActive(false);
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		#endregion
		#region ### METHODS ###

		public static void displaySuccessMessage(){
			_instance._SuccessPanel.SetActive(true);
		}

		void onClickReplay(Button pButton){
			PuzzlePieceBehaviour.Replay();
			_instance._SuccessPanel.SetActive(false);
		}

		#endregion
	}
}