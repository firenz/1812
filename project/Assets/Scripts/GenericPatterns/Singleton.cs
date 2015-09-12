using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : MonoBehaviour
	where T : Component{
	
	protected static T instance;
	
	public static T Instance{
		get{
			return instance;
		}
	}
	
	protected void OnDestroy() {
		if (instance == this) {
			instance = null;
		}
	}
	
	protected void OnApplicationQuit(){
		instance = null;
	}
	
	protected void Awake(){
		if(instance == null){
			instance = this as T;
			InitializeOnAwake();
		}
		else{
			Destroy(this.gameObject);
		}
	}

	//...Put here another code which needs to be done in the Awake() method
	protected abstract void InitializeOnAwake(); 
}
