using UnityEngine;
using System.Collections;

public class SortingOrderLayerController : MonoBehaviour {
	public float changeAtPlayerPositionY;
	public string sortingLayerNameAfterChange;
	public int sortingLayerPositionAfterChange;

	private int previousSortingPosition;
	private string previousSortingLayerName;

	// Use this for initialization
	void Start () {
		previousSortingLayerName = this.gameObject.renderer.sortingLayerName;
		previousSortingPosition = this.gameObject.renderer.sortingOrder;
	}
	
	// Update is called once per frame
	void Update () {
		if(Player.Instance.transform.position.y > changeAtPlayerPositionY){
			this.gameObject.renderer.sortingLayerName = sortingLayerNameAfterChange;
			this.gameObject.renderer.sortingOrder = sortingLayerPositionAfterChange;
		}
		else{
			this.gameObject.renderer.sortingLayerName = previousSortingLayerName;
			this.gameObject.renderer.sortingOrder = previousSortingPosition;
		}
	}
}
