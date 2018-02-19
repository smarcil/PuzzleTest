using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Akerue.PuzzleTest.Behaviours.SceneManagers{
	public class SoundsSmanager : MonoBehaviour {
		#region ### FIELDS ###

		#region ** SERIALIZE FIELDS **

		[SerializeField, Tooltip("Sound for wrong place")]
		AudioClip _wrongSound;
		[SerializeField, Tooltip("Sound for correct place")]
		AudioClip _correctSound;
		[SerializeField, Tooltip("Sound for win")]
		AudioClip _successSound;

		#endregion

		AudioSource _fxSource;
		static SoundsSmanager _instance;

		#endregion
		#region ### PROPERTIES ###

		AudioSource fxSource{
			get{
				if(_fxSource == null){
					_fxSource = gameObject.AddComponent<AudioSource>();
				}
				return _fxSource;
			}
		}

		public static AudioClip wrongSound{get{return _instance._wrongSound;}}
		public static AudioClip correctSound{get{return _instance._correctSound;}}
		public static AudioClip successSound{get{return _instance._successSound;}}

		#endregion
		#region ### UNITY CALLBACK ###

		void Awake(){
			_instance = this;
		}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		#endregion
		#region ### METHODES ###

		public static void PlaySingle(AudioClip pClip){
			_instance.fxSource.clip = pClip;
			_instance.fxSource.Play();
		}

		#endregion
	}
}