using UnityEngine;
using System.Collections;

public class SortingOrderLayerController : MonoBehaviour {
	public float changeAtPlayerPositionY;
	public string sortingLayerNameAfterChange;
	public int sortingLayerPositionAfterChange;

	private int previousSortingPosition;
	private string previousSortingLayerName;

	private void Start () {
		previousSortingLayerName = this.gameObject.GetComponent<Renderer>().sortingLayerName;
		previousSortingPosition = this.gameObject.GetComponent<Renderer>().sortingOrder;
	}

	private void Update () {
		if(Player.Instance.transform.position.y > changeAtPlayerPositionY){
			this.gameObject.GetComponent<Renderer>().sortingLayerName = sortingLayerNameAfterChange;
			this.gameObject.GetComponent<Renderer>().sortingOrder = sortingLayerPositionAfterChange;
		}
		else{
			this.gameObject.GetComponent<Renderer>().sortingLayerName = previousSortingLayerName;
			this.gameObject.GetComponent<Renderer>().sortingOrder = previousSortingPosition;
		}
	}
}
