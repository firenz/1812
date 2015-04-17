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
		previousSortingLayerName = this.gameObject.GetComponent<Renderer>().sortingLayerName;
		previousSortingPosition = this.gameObject.GetComponent<Renderer>().sortingOrder;
	}
	
	// Update is called once per frame
	void Update () {
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
