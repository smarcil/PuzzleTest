using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akerue.PuzzleTest.Behaviours.SceneManagers{
	public class PuzzleSceneSmanager : MonoBehaviour {

		#region  ### FIELDS ###

		#region ** SERIALIZE FIELDS **

		[SerializeField, Tooltip("Start zone for pieces")]
		RectTransform _startZone;

		#endregion

		static PuzzleSceneSmanager _instance;

		#endregion
		#region ### PROPERTIES ###

		public static RectTransform startZone{get{return _instance._startZone;}}

		#endregion
		#region ### UNITY CALLBACK ###

		void Awake(){
			_instance = this;
		}

		#endregion
	}
}