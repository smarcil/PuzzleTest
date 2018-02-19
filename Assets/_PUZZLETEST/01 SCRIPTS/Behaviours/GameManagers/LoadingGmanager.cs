using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace Akerue.PuzzleTest.Behaviours.GameManagers
{
    public class LoadingGmanager : MonoBehaviour {

		#region ### Initiate Loading game manager ###

		static LoadingGmanager __instance;
		static LoadingGmanager instance{
			get{
				initiateLoadingGmanager();
				return __instance;
			}
		}
		static void initiateLoadingGmanager(){
			if(__instance != null)return;

			GameObject go = new GameObject("LoadingGmanager");
			go.AddComponent<LoadingGmanager>();
			DontDestroyOnLoad(go);
			__instance = go.GetComponent<LoadingGmanager>();
		}

		#endregion
		#region ### ENUMS ###

		public enum scenes{
			PuzzleScene
		}

		#endregion
		#region ### FIELDS ###

		#region ** SERIALIZE FIELDS **

		[SerializeField, Tooltip("Time of transition in seconde")]
		float transitionTime = 0.5f;

		#endregion

		Scene[] _scenes = new Scene[2];
		Camera[] _cameras = new Camera[2];
		float currentTransitionTime;

		#endregion
		#region ### PROPERTIES ###

		

		#endregion
		#region ### UNITY CALLBACK ###

		void Update(){
			updateTransitionAnimation();
		}

		#endregion
		#region ### METHODS ###

		void updateTransitionAnimation(){
			if(_cameras[1] == null)return;

			currentTransitionTime += Time.deltaTime;
			if(currentTransitionTime >= transitionTime){
				_cameras[1].rect = new Rect(0,0,1,1);
				_cameras[1] = null;
				removeFirstScene();
				return;
			}

			float x = 1-currentTransitionTime/transitionTime;
			float w = x;

			_cameras[0].rect = new Rect(0,0,w,1);
			_cameras[1].rect = new Rect(x,0,1,1);
		}

		public static void LoadScene(scenes pScene){
			instance.StartCoroutine( instance.loading(pScene.ToString()) );
		}

		IEnumerator loading(string pSceneName){
			AsyncOperation async = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
			async.allowSceneActivation = false;

			_scenes[0] = SceneManager.GetActiveScene();
			_scenes[1] = getSceneByName(pSceneName);

			_cameras[0] = getCameraByScene(_scenes[0]);

			while(async.isDone == false){
				if(async.progress == 0.9f){
					_cameras[0].GetComponent<AudioListener>().enabled = false;
					async.allowSceneActivation = true;
				}
				yield return null;
			}
			_cameras[1] = getCameraByScene(_scenes[1]);
			currentTransitionTime = 0;
			_cameras[1].rect = new Rect(1,0,1,1);
		}

		Scene getSceneByName(string pName){
			for(int i=0; i<SceneManager.sceneCountInBuildSettings;i++){
				Scene scene = SceneManager.GetSceneByBuildIndex(i);
				if(scene.name == pName)return scene;
			}
			throw new System.Exception("Scene "+pName+" not found!");
		}

		Camera getCameraByScene(Scene pScene){
			foreach ( GameObject item in pScene.GetRootGameObjects() )
			{
				Camera camera = item.GetComponent<Camera>();
				if(camera != null)return camera;
			}
			throw new System.Exception("Camera not found!");
		}

		void removeFirstScene(){
			SceneManager.SetActiveScene(_scenes[1]);
			SceneManager.UnloadSceneAsync(_scenes[0]);
		}

		#endregion
	}
}