using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

[RequireComponent(typeof(Button))]
/// <summary>
/// Clase abstracta para boton generico de las interfaces graficas de los menus presentes en el juego.
/// </summary>
public abstract class UIGenericButton : MonoBehaviour {

	/// <summary>
	/// ID del grupo (en este caso el grupo seria un menu que englobase todos los textos de los elementos que vaya a contener)
	 /// al que pertenece el texto localizado.
	/// </summary>
	[SerializeField]
	protected string localizedTextGroupID = "OPTIONS_MENU";
	/// <summary>
	/// ID del elemento (en este caso el elemento es un boton de la interfaz) al que pertenece el texto localizado.
	/// </summary>
	[SerializeField]
	protected string localizedTextElementID = "SETTINGS";
	/// <summary>
	/// ID de la cadena de texto que hay que mostrar para el elemento especifico (en este caso boton) que hay
	/// en el texto localizado.
	/// </summary>
	[SerializeField]
	protected string localizedTextStringID = "NAME";
	/// <summary>
	/// Nombre del sonido por defecto que se escucha al pulsar en el boton generico.
	/// </summary>
	[SerializeField]
	protected string sfxOnClick = "BlipSelect";

	/// <summary>
	/// Componente del objeto que contiene esta clase, controla las acciones que se pueden hacer con el boton.
	/// </summary>
	protected Button thisButton;
	/// <summary>
	/// Componente del objeto que contiene esta clase, muestra el texto localizado de lo que hace el boton.
	/// </summary>
	protected Text buttonText;

	/// <summary>
	/// Al activarse el objeto que contiene una instancia de esta clase, se activan las escuchas de los eventos
	/// <c>ModalWindowHandler.beginQuestion</c>, <c>ModalWindowHandler.endedQuestion</c> y
	/// <c>LocatedTextManager.currentLanguageChanged</c>.
	/// </summary>
	protected virtual void OnEnable(){
		ModalWindowHandler.beginQuestion += DisableButton;
		ModalWindowHandler.endedQuestion += EnableButton;
		LocatedTextManager.currentLanguageChanged += ChangeButtonTextLanguage;
	}

	/// <summary>
	/// Al volverse inactivo el objeto que contiene una instancia de esta clase, desactiva la escucha de los eventos
	/// <c>ModalWindowHandler.beginQuestion</c>, <c>ModalWindowHandler.endedQuestion</c> y
	/// <c>LocatedTextManager.currentLanguageChanged</c>.
	/// </summary>
	protected virtual void OnDisable(){
		ModalWindowHandler.beginQuestion -= DisableButton;
		ModalWindowHandler.endedQuestion -= EnableButton;
		LocatedTextManager.currentLanguageChanged -= ChangeButtonTextLanguage;
	}

	/// <summary>
	/// Al iniciarse la instancia, obtiene el componente <c>Button</c> del objeto al que esta unido esta clase. 
	/// Despues intenta obtener el componente <c>Text</c> del boton del objeto y le asigna el texto localizado,
	/// en caso de que no posea el componente <c>Text</c>, se inicializa a <c>null</c>.
	/// </summary>
	protected virtual void Start(){
		thisButton = this.GetComponent<Button>();
		try{
			buttonText = this.transform.FindChild("Text").GetComponent<Text>();
			buttonText.text = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextElementID, localizedTextStringID)[0];
		}catch(NullReferenceException){
			buttonText = null;
		}
	}

	/// <summary>
	/// Metodo que se ejecuta cuando se inicia el evento de click en el boton,
	/// primero comprueba que el boton es interactivo y que el cursor no esta oculto,
	/// si es asi, ejecuta la corrutina <c>UIGenericButton.WaitForSFXAndExecuteClickAction</c>.
	/// </summary>
	public virtual void OnClick(){
		if(thisButton.interactable && !CustomCursorController.Instance.isCursorHidden){
			AudioManager.PlaySFX(sfxOnClick);
			StartCoroutine(WaitForSFXAndExecuteClickAction());
		}
	}

	/// <summary>
	/// Corrutina que espera que se ejecute el sonido de pulsado del boton,
	/// y luego llama al metodo <c>UIGenericButton.ActionOnClick</c> que contiene
	/// las acciones a realizar por el boton al ser pulsado.
	/// </summary>
	protected IEnumerator WaitForSFXAndExecuteClickAction(){
		do{
			yield return null;
		}while(AudioManager.IsPlayingSFX(sfxOnClick));
		ActionOnClick();
	}

	/// <summary>
	/// Metodo abstracto que debe ser sobrecargado en las clases herederas para definir las acciones
	/// a realizar por el boton al ser pulsado.
	/// </summary>
	protected abstract void ActionOnClick();

	/// <summary>
	/// Metodo que se ejecuta cuando se inicia el evento del cursor encima del boton.
	/// Comprueba que el boton es interactivo y que el cursor no esta oculto,
	/// si es asi, cambia el grafico del cursor al de estar encima de un elemento
	/// de la interfaz grafica con <c>CustomCursorController.ChangeCursorOverUIElement</c>.
	/// </summary>
	/// <seealso cref="CustomCursorController"/>
	public virtual void OnMouseOver(){
		if(thisButton.interactable && !CustomCursorController.Instance.isCursorHidden){
			CustomCursorController.Instance.ChangeCursorOverUIElement();
		}
	}

	/// <summary>
	/// Raises the mouse exit event.
	/// Metodo que se ejecuta cuando se inicia el evento de que el cursor ha dejado de estar encima del boton.
	/// Comprueba que el boton es interactivo y que el cursor no esta oculto,
	/// si es asi, cambia el grafico del cursor al que hay por defecto.
	/// </summary>
	/// <seealso cref="CustomCursorController"/>
	public virtual void OnMouseExit(){
		if(thisButton.interactable && !CustomCursorController.Instance.isCursorHidden){
			CustomCursorController.Instance.ChangeCursorToDefault();
		}
	}

	/// <summary>
	/// Metodo que habilita que el boton sea interactivo.
	/// </summary>
	public virtual void EnableButton(){
		thisButton.interactable = true;
	}

	/// <summary>
	/// Metodo que inhabilita que el boton sea interactivo.
	/// </summary>
	public virtual void DisableButton(){
		thisButton.interactable = false;
	}

	/// <summary>
	/// Metodo que se ejecuta en caso de que suceda el evento de que se cambie el idioma,
	/// en ese caso, comprueba si el boton tiene el componente <c>Text</c>, si es asi,
	/// entonces le cambia el texto localizado por el del nuevo idioma actual.
	/// </summary>
	/// <seealso cref="LocatedTextManager.ChangeCurrentLanguage"/>
	protected virtual void ChangeButtonTextLanguage(){
		if(buttonText != null){
			buttonText.text = LocatedTextManager.GetLocatedText(localizedTextGroupID, localizedTextElementID, localizedTextStringID)[0];
		}
	}
}
