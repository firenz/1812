using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// <c>PersistentSingleton</c> que controla los graficos personalizados del cursor del juego.
/// </summary>
public class CustomCursorController : PersistentSingleton<CustomCursorController> {
	/// <summary>
	/// Valor de solo lectura que indica si el cursor esta invisible o no.
	/// </summary>
	/// <value>Devuelve <c>true</c> si es cursor no es visible; en otro caso, <c>false</c>.</value>
	public bool isCursorHidden { get; private set;}

	/// <summary>
	/// Ruta hacia el directorio de Assets que contiene los graficos personalizados del cursor.
	/// </summary>
	[SerializeField]
	private string pathCursorSprites = "Graphics/GUI/Cursor/";

	/// <summary>
	/// Componente <c>Transform</c> del objeto que contiene el cursor.
	/// </summary>
	private Transform cursorTransform;
	/// <summary>
	/// Componente <c>SpriteRenderer</c> que controla el grafico del objeto que contiene el cursor.
	/// </summary>
	private SpriteRenderer cursorRenderer;
	/// <summary>
	/// Grafico actual del cursor.
	/// </summary>
	private Sprite currentCursorSprite;
	/// <summary>
	/// Grafico del cursor en los menus del juego.
	/// </summary>
	private Sprite menuCursorSprite;
	/// <summary>
	/// Grafico del cursor en las zonas jugables del juego.
	/// </summary>
	private Sprite ingameCursorSprite;
	/// <summary>
	/// Grafico del cursor cuando esta encima de un objeto interactivo en las zonas jugables del juego.
	/// </summary>
	private Sprite overInteractiveElementCursorSprite;
	/// <summary>
	/// Grafico del cursor cuando esta encima de un elemento interactivo (como un boton) de la interfaz grafica o
	/// los menus.
	/// </summary>
	private Sprite overUIElementCursorSprite;
	/// <summary>
	/// Grafico del cursor cuando esta encima de un objeto interactivo que puede trasladarte a otra zona jugable del juego.
	/// </summary>
	private Sprite exitSceneCursorSprite;

	/// <summary>
	/// Sobrecarga del metodo <c>PersistentSingleton.InitializeOnAwake</c>. En el inicializa todos los graficos
	/// del cursor, vuelve invisible el cursor por defecto del sistema operativo, y cambia el grafico del cursor del juego
	/// al de por defecto, dependiendo de si es un menu o una zona jugable.
	/// </summary>
	protected override void InitializeOnAwake (){
		isCursorHidden = false;
		Cursor.visible = isCursorHidden;
		cursorTransform = this.transform.FindChild("Cursor");
		cursorRenderer = cursorTransform.GetComponent<SpriteRenderer>();
		menuCursorSprite = Resources.Load<Sprite>(pathCursorSprites + "MenuDefaultCursor");
		ingameCursorSprite = Resources.Load<Sprite>( pathCursorSprites + "IngameDefaultCursor");
		overInteractiveElementCursorSprite = Resources.Load<Sprite>( pathCursorSprites + "OverInteractiveElementCursor");
		overUIElementCursorSprite = Resources.Load<Sprite>(pathCursorSprites + "OverUIElementCursor");
		exitSceneCursorSprite = Resources.Load<Sprite>(pathCursorSprites + "ExitSceneCursor");

		currentCursorSprite = menuCursorSprite;
		ChangeCursorToDefault();
	}

	/// <summary>
	/// Actualiza la posicion del grafico del cursor.
	/// </summary>
	private void Update(){
		Vector2 _mouseWorldPosition =  Camera.main.ScreenToWorldPoint(Input.mousePosition);
		cursorTransform.position = _mouseWorldPosition; 
	}

	/// <summary>
	/// Cambia el grafico del cursor al de por defecto del escenario, dependiendo de si es un menu o zona jugable.
	/// </summary>
	public void ChangeCursorToDefault(){
		cursorRenderer.sprite = currentCursorSprite;
		transform.eulerAngles = Vector3.zero;
	}

	/// <summary>
	/// Cambia el grafico del cursor por el <c>Sprite</c> que le indiquemos.
	/// </summary>
	/// <param name="newSprite">Nuevo grafico para el cursor.</param>
	public void ChangeCursorSprite(Sprite newSprite){
		cursorRenderer.sprite = newSprite;
	}

	/// <summary>
	/// Cambia el grafico del cursor cuando esta encima de un objeto interactivo en una zona jugable.
	/// </summary>
	public void ChangeCursorOverInteractiveElement(){
		cursorRenderer.sprite = overInteractiveElementCursorSprite;
	}

	/// <summary>
	/// Cambia el grafico del cursor por el grafico por defecto de una zona jugable.
	/// </summary>
	public void ChangeCursorIngameScene(){
		cursorRenderer.sprite = ingameCursorSprite;
	}

	/// <summary>
	/// Cambia el grafico del cursor cuando esta encima de un objeto interactivo que puede trasladar
	/// a otra zona jugable.
	/// </summary>
	/// <param name="rotation">Rotacion del grafico en grados.</param>
	public void ChangeCursorOverWarpElement(float rotation = 0f){
		cursorRenderer.sprite = exitSceneCursorSprite;
		this.transform.eulerAngles = new Vector3(0f, 0f, rotation);
	}

	/// <summary>
	/// Cambia el grafico del cursor cuando esta encima de un elemento de la interfaz grafica que sea interactivo.
	/// </summary>
	public void ChangeCursorOverUIElement(){
		cursorRenderer.sprite = overUIElementCursorSprite;
	}

	/// <summary>
	/// Cambia al grafico del cursor por defecto de menu a zona jugable.
	/// </summary>
	public void ChangeMenuCursorToIngame(){
		cursorRenderer.sprite = ingameCursorSprite;
		currentCursorSprite =  ingameCursorSprite;
	}

	/// <summary>
	/// Cambia el grafico del cursor por defecto de zona jugable a menu.
	/// </summary>
	public void ChangeIngameCursorToMenu(){
		cursorRenderer.sprite = menuCursorSprite;
		currentCursorSprite = menuCursorSprite;
	}

	/// <summary>
	/// Oculta el cursor.
	/// </summary>
	public void HideCursor(){
		isCursorHidden = true;
		cursorRenderer.color = Color.clear;
	}

	/// <summary>
	/// Termina de ocultar el cursor
	/// </summary>
	public void UnhideCursor(){
		isCursorHidden = false;
		cursorRenderer.color = Color.white;
	}

}
