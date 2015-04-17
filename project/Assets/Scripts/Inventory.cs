using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : PersistentSingleton<Inventory> {
	public bool isEnabled = true;
	private bool isClosed = true;
	private bool isClicked = false;
	private const float speedInOpeningInventory = 1.5f;
	private const float openedInventoryHeight = 30f;
	private const int maxItemsCapacity = 7;
	private int itemsCount = 0;
	private float widthBetweenItems = 40f;
	private GameObject[] items = null;

	// Use this for initialization
	void Start () {
		items = new GameObject[maxItemsCapacity];

		for(int i = 0; i < items.Length; i++){
			items[i] = null;
		}

		AddItem("FailedTestInventory");
	}
	
	// Update is called once per frame
	void Update () {
		if(isClicked){
			if(isClosed){
				if(this.transform.position.y < openedInventoryHeight){
					this.transform.position = new Vector2(0.0f , this.transform.position.y + speedInOpeningInventory);
				}
				else{
					this.Open();
				}
			}
			else{
				if(this.transform.position.y > 0f){
					this.transform.position = new Vector2( 0.0f , this.transform.position.y - speedInOpeningInventory);
				}
				else{
					this.Close();
				}
			}
		}
	}

	public void AddItem(string nameItem){
		Vector3 _originalItemPosition = this.transform.FindChild("OriginalItemPosition").transform.position;

		if(itemsCount < maxItemsCapacity){
			GameObject _itemToLoad = Resources.Load<GameObject>("Prefabs/Inventory/Items/" + nameItem);
			_itemToLoad.transform.position = new Vector3(_originalItemPosition.x + itemsCount * widthBetweenItems, _originalItemPosition.y, _originalItemPosition.z);
			GameObject _newItemAdded = Instantiate(_itemToLoad) as GameObject;
			_newItemAdded.name = nameItem;
			_newItemAdded.transform.parent = this.transform;
			items[itemsCount] = _newItemAdded;
			itemsCount++;
		}
	}

	public void RemoveItem(string nameItem){

		int _indexOfItemToRemove = 0;

		while(items[_indexOfItemToRemove].name != nameItem && _indexOfItemToRemove < maxItemsCapacity){
			_indexOfItemToRemove++;
		}

		if(_indexOfItemToRemove < maxItemsCapacity){
			Destroy(items[_indexOfItemToRemove]);
			items[_indexOfItemToRemove] = null;
			for(int i = _indexOfItemToRemove; i < maxItemsCapacity - 1; i++){
				items[i] = items[i+1];
			}
			items[maxItemsCapacity - 1] = null;
			itemsCount--;
		}
	}

	public List<GameObject> GetItems(){
		List<GameObject> _currentItemsOnInventory = new List<GameObject>();
		
		for(int index = 0; index < items.Length; index++){
			if(items[index] != null){
				_currentItemsOnInventory.Add(items[index]);
			}
		}
		
		return _currentItemsOnInventory;
	}

	public List<string> GetItemsName(){
		List<string> _currentItemsNameOnInventory = new List<string>();

		for(int index = 0; index < items.Length; index++){
			if(items[index] != null){
				_currentItemsNameOnInventory.Add(items[index].name);
			}
		}

		return _currentItemsNameOnInventory;
	}

	public void Open(){
		isClosed = false;
		isClicked = false;
		this.transform.FindChild("ClosedInventory").GetComponent<Renderer>().enabled = false;
	}
	
	public void Close(){
		isClosed = true;
		isClicked = false;
		transform.FindChild("ClosedInventory").GetComponent<Renderer>().enabled = true;
	}

	public void Enable(){
		isEnabled = true;
		this.transform.position = new Vector3(0f, 0f, -2f);
		Close();
	}

	public void Disable(){
		isEnabled = false;
		this.transform.position = new Vector3(0f, -10f, -2f);
		Close();
	}

	public bool IsClosed(){
		return isClosed;
	}

	public bool IsEnabled(){
		return isEnabled;
	}

	private void OnMouseDown() {
		if(Input.GetMouseButtonDown(0)){
			if(!Player.Instance.IsSpeaking() && !Player.Instance.IsInteracting() && !Player.Instance.IsUsingItemInventory()){
				isClicked = true;
			}
		}
	}
}
