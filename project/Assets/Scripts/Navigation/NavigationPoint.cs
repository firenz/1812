using UnityEngine;
using System.Collections;

public class NavigationPoint : MonoBehaviour {
	public int connectPointID_1{
		get{
			return connectColliderID_1;
		}
		private set{}
	}
	public int connectPointID_2{
		get{
			return connectColliderID_2;
		}
		private set{}
	}

	public int pointID{
		get{
			return navPointID;
		}
		private set{}
	}

	[SerializeField]
	private int connectColliderID_1;
	[SerializeField]
	private int connectColliderID_2;

	private int navPointID = -1;

	private void Awake(){
		this.GetComponent<SpriteRenderer>().enabled = false;
	}

	public void InitializeID(int newID){
		navPointID = newID;
		this.name = "Linker_" + navPointID;
	}
}
