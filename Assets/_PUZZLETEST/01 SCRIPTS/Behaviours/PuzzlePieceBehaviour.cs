using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Akerue.PuzzleTest.Behaviours.SceneManagers;

namespace Akerue.PuzzleTest.Behaviours{
	public class PuzzlePieceBehaviour : MonoBehaviour, DraggerSmanager.IDragable {
		#region ### FIELDS ###

		static List<PuzzlePieceBehaviour> PuzzlePieces = new List<PuzzlePieceBehaviour>();

		Vector3 _piecePosition;
		Vector3 _startPos;

		bool _replace = false;

		bool _settedOnGoodPlace = false;

		Vector3 velocity = Vector3.zero;
		float _height = 0;
		float _targetHeight = 0;
		GameObject _shadow;

		#endregion
		#region ### UNITY CALLBACK ###
		
		// Use this for initialization
		void Start () {
			PuzzlePieces.Add(this);
			setStartPosition();
			createShadow();
		}
		
		// Update is called once per frame
		void Update () {
			replacePuzzlePiece();
			updateHeight();
		}

		void LateUpdate(){
			updateShadow();
		}

		#endregion
		#region ### METHODES ###

		static bool checkSuccess(){
			foreach(PuzzlePieceBehaviour PuzzlePiece in PuzzlePieces){
				if(!PuzzlePiece._settedOnGoodPlace)return false;
			}
			return true;
		}

		void replacePuzzlePiece(){
			if(!_replace)return;
			transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _startPos, ref velocity, 0.3f);
		}

		void setStartPosition(){
			_settedOnGoodPlace = false;
			_piecePosition = transform.localPosition;
			transform.SetAsLastSibling();
			Vector3 pos = PuzzleSceneSmanager.startZone.transform.localPosition;
			float w = PuzzleSceneSmanager.startZone.rect.width;
			float h = PuzzleSceneSmanager.startZone.rect.height;

			pos.x += Random.Range(0, w);
			pos.y += Random.Range(0,h);

			transform.localPosition = pos;
			_startPos = pos;
		}

		public bool startDrag(){
			_replace = false;
			if(!_settedOnGoodPlace){
				_targetHeight = 1;
			}
			return !_settedOnGoodPlace;
		}

		public void Drop(){
			_targetHeight = 0;
			float distance = Vector3.Distance(_piecePosition, transform.localPosition);
			if(distance<PuzzleSceneSmanager.puzzlePinDistance){
				transform.localPosition = _piecePosition;
				_settedOnGoodPlace = true;
				if(checkSuccess()){
					successBehaviours();
				}else{
					SoundsSmanager.PlaySingle(SoundsSmanager.correctSound);
				}
			}else{
				SoundsSmanager.PlaySingle(SoundsSmanager.wrongSound);
				_replace = true;
			}
		}

		void successBehaviours(){
			SoundsSmanager.PlaySingle(SoundsSmanager.successSound);
			PuzzleSceneUISmanager.displaySuccessMessage();
		}

		public static void Replay(){
			foreach (PuzzlePieceBehaviour item in PuzzlePieces)
			{
				item.setStartPosition();
			}
		}

		void updateHeight(){
			if(_targetHeight < _height){
				_height -= Time.deltaTime*PuzzleSceneSmanager.shadowSpeed;
				if(_targetHeight > _height)_height = _targetHeight;
			}else if(_targetHeight > _height){
				_height += Time.deltaTime*PuzzleSceneSmanager.shadowSpeed;
				if(_targetHeight < _height)_height = _targetHeight;
			}
		}

		void updateShadow(){
			setShadowPosition();
			if(_height > 0){
				setShadowAlpha(PuzzleSceneSmanager.shadowAlpha);
			}else{
				setShadowAlpha(0f);
			}
		}

		void createShadow(){
			_shadow = Instantiate(gameObject, transform.parent, false);
			DestroyImmediate(_shadow.GetComponent<PuzzlePieceBehaviour>());
			_shadow.GetComponent<Image>().color = new Color(0,0,0,0f);
		}

		void setShadowPosition(){
			Vector3 shadowDirection = PuzzleSceneSmanager.shadowPos;
			Vector3 pos = transform.localPosition + shadowDirection * _height;
			_shadow.transform.localPosition = pos;
			_shadow.transform.SetSiblingIndex(transform.GetSiblingIndex()-1);
		}

		void setShadowAlpha(float pAlpha){
				Color shadowColor = _shadow.GetComponent<Image>().color;
				shadowColor.a = pAlpha;
				_shadow.GetComponent<Image>().color = shadowColor;
		}

		#endregion
	}
}