using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akerue.PuzzleTest.Behaviours.SceneManagers{
	public class PuzzleSceneSmanager : MonoBehaviour {

		#region  ### FIELDS ###

		#region ** SERIALIZE FIELDS **

		[SerializeField, Tooltip("Start zone for pieces")]
		RectTransform _startZone;

		[SerializeField, Tooltip("Distance to pin the puzzle piece in good place")]
		float _puzzlePinDistance = 20f;

		[SerializeField, Tooltip("Speed of puzzle piece shadow movement")]
		float _shadowSpeed = 5;

		[SerializeField, Tooltip("shadow alpha color")]
		float _shadowAlpha = 0.5f;

		[SerializeField, Tooltip("position of the shadow")]
		Vector3 _shadowPos = new Vector3(10f, -5f, 0);

		#endregion

		static PuzzleSceneSmanager _instance;

		#endregion
		#region ### PROPERTIES ###

		public static RectTransform startZone{get{return _instance._startZone;}}
		public static float puzzlePinDistance{get{return _instance._puzzlePinDistance;}}
		public static float shadowSpeed{get{return _instance._shadowSpeed;}}
		public static float shadowAlpha{get{return _instance._shadowAlpha;}}
		public static Vector3 shadowPos{get{return _instance._shadowPos;}}

		#endregion
		#region ### UNITY CALLBACK ###

		void Awake(){
			_instance = this;
		}

		#endregion
	}
}