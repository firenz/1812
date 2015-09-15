using UnityEngine;
using System.Collections;

/// <summary>
/// Clase generica y abstracta cuyo propósito es asegurarse que solo exista una instancia de una clase y proporciona un acceso fácil y global a la clase.
/// </summary>
/// <typeparam name="T">Clase que hereda a <c>Singleton</c> para que solo pueda haber una instancia y el acceso global de ella.</typeparam>
public abstract class Singleton<T> : MonoBehaviour
	where T : Component{

	/// <summary>
	/// Atributo estatico que guarda la clase heredera de <c>Singleton</c> a la que esta apuntando en tiempo de ejecucion
	/// </summary>
	/// <typeparam name="T">Clase heredada de <c>Singleton</c> de la instancia actual</typeparam>
	protected static T instance;

	/// <summary>
	/// Atributo de acceso publico en modo de lectura del atributo <c>instance</c>
	/// </summary>
	/// <value>Obtiene el valor del atributo <c>instance</c></value>
	public static T Instance{
		get{
			return instance;
		}
	}

	/// <summary>
	/// En caso de destruirse el objeto al que esta unido una clase <c>Singleton</c>, 
	/// si el atributo <c>instance</c> apuntaba a esta clase en ejecucion en concreto,
	/// la desasigna para indicar que actualmente no hay ningun <c>Singleton</c> de esta
	/// clase en concreto.
	/// </summary>
	protected void OnDestroy() {
		if (instance == this) {
			instance = null;
		}
	}

	/// <summary>
	/// En caso de cerrar el juego, el atributo <c>instance</c> se desasigna para que el recolector
	/// de basura elimine los datos de memoria.
	/// </summary>
	protected void OnApplicationQuit(){
		instance = null;
	}

	/// <summary>
	/// Al despertar el objeto al que este unido una clase en ejecucion que herede de <c>Singleton</c>, comprobara si ya hay una 
	/// instancia previa de dicha clase. En caso de que no haya ninguna instancia previa, asigna <c>instance</c> a la clase en ejecucion
	/// actual, en caso contrario, significa que ya habia una instancia, y destruye esta instancia de esta clase en ejecucion.
	/// </summary>
	protected void Awake(){
		if(instance == null){
			instance = this as T;
			InitializeOnAwake();
		}
		else{
			Destroy(this.gameObject);
		}
	}
	
	/// <summary>
	/// Metodo para inicializar otros atributos de la clase heredera de <c>Singleton</c> durante el metodo <c>Awake</c>
	/// </summary>
	protected abstract void InitializeOnAwake(); 
}
