using UnityEngine;
using System.Collections;

public class NavigationPointCentroid : MonoBehaviour {
	public int colliderID{
		get{
			return belongsToColliderID;
		}
		private set{}
	}

	private int belongsToColliderID = -1;

	private void Awake(){
		this.GetComponent<SpriteRenderer>().enabled = false;
	}

	public void Initialize(int newColliderID){
		belongsToColliderID = newColliderID;
	}
}
