using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Akerue.PuzzleTest.Behaviours.SceneManagers
{

    public class DraggerSmanager : MonoBehaviour {
		#region ### INTERFACES ###

		public interface IDragable
		{
			bool startDrag();
			void Drop();
		}

		#endregion
		#region ### FIELDS ###

		bool dragging = false;

		Vector2 originalPosition;

		Transform objectToDrag;
		Image objectToDragImage;

		List<RaycastResult> hitObjects = new List<RaycastResult>();

		#endregion
		#region ### UNITY CALLBACK ###

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			checkMouseButton();
			draggingBehaviour();
		}

		#endregion
		#region ### METHODS ###

		void checkMouseButton(){
			if(Input.GetMouseButtonDown(0)){
				objectToDrag = GetDraggableTransformUnderMouse();

				if(objectToDrag != null){
					dragging = true;

					objectToDrag.SetAsLastSibling();

					originalPosition = objectToDrag.position;
					objectToDragImage = objectToDrag.GetComponent<Image>();
					objectToDragImage.raycastTarget = false;
				}
			}else if(Input.GetMouseButtonUp(0) && dragging){
				objectToDrag.GetComponent<IDragable>().Drop();
				objectToDragImage.raycastTarget = true;
				objectToDrag = null;
				dragging = false;
			}
		}

		void draggingBehaviour(){
			if(!dragging)return;

			objectToDrag.localPosition = Input.mousePosition;
		}

		GameObject GetObjectUnderMouse(){
			PointerEventData pointer = new PointerEventData(EventSystem.current);

			pointer.position = Input.mousePosition;

			EventSystem.current.RaycastAll(pointer, hitObjects);

			if(hitObjects.Count <= 0)return null;

			return hitObjects.First().gameObject;
		}

		Transform GetDraggableTransformUnderMouse(){
			GameObject clickedObject = GetObjectUnderMouse();

			IDragable iDrag = clickedObject.GetComponent<IDragable>();
			if(iDrag != null){
				if( !iDrag.startDrag() )return null;
				return clickedObject.transform;
			}
			return null;
		}

		#endregion
	}
}