using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Akerue.PuzzleTest.Behaviours.SceneManagers.MainUIControllers;

namespace Akerue.PuzzleTest.Behaviours.SceneManagers{
	public class MainUISmanager : MonoBehaviour {

		#region ### FIELDS ###

		#region ** SERIALIZE FIELDS **

		[SerializeField]
		ButtonsController _buttonsController;

		#endregion

		#endregion
		#region  ### UNITY CALLBACK ###

		void Awake(){
			_buttonsController.InitController();
		}
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		#endregion
	}
}