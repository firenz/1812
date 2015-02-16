using UnityEngine;
using System.Collections;

public class Singleton<T> : MonoBehaviour
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
			//instance = new GameObject(this.name).AddComponent<T>(); //To be fixed
			DontDestroyOnLoad(instance.gameObject);
			InitializeOnAwake();
		}
		else{
			Destroy(this.gameObject);
		}
	}
	
	protected virtual void InitializeOnAwake(){} //...Put here another code which needs to be done in the Awake() method
}
