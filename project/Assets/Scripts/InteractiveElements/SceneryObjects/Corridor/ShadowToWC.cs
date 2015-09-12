using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class ShadowToWC : WarperElement {
	private string nameToWCSFX = "OpenWindow_v2";
	private GameObject blackImage;

	private void OnEnable(){
		Librarian.finishedChoiceWithLibrarian += ActivateShadow;
	}
	
	private void OnDisable(){
		Librarian.finishedChoiceWithLibrarian -= ActivateShadow;
	}
	
	protected override void InitializeInformation (){
		blackImage = GameObject.Find("BlackImage");
		blackImage.SetActive(false);
		if(!GameState.LevelCorridorData.isDialogueChoiceMadeWithLibrarianFinished){
			SetInactive();
		}
	}

	protected override IEnumerator WaitForLeftClickAction(){
		float _distanceBetweenActorAndInteractivePosition = Mathf.Abs(Vector2.Distance(Player.Instance.currentPosition, interactivePosition));

		if(_distanceBetweenActorAndInteractivePosition >= minDistance){
			Player.Instance.GoTo(interactivePosition);
			do{
				yield return null;
			}while(Player.Instance.isWalking);
		}
		
		if(Player.Instance.originalTargetedPosition == interactivePosition){
			BeginAction();
			
			Player.Instance.LookToTheLeft();

			yield return new WaitForSeconds(0.2f);

			Player.Instance.Speak(groupID, nameID, "INTERACTION");
			
			do{
				yield return null;
			}while(Player.Instance.isSpeaking);

			yield return new WaitForSeconds(0.2f);

			Player.Instance.GoTo(GameObject.Find("CutscenePoint_4").transform.position);
			do{
				yield return null;
			}while(Player.Instance.isWalking);

			AudioManager.PlaySFX(nameToWCSFX);
			do{
				yield return null;
			}while(AudioManager.IsPlayingSFX(nameToWCSFX));

			EndAction();
			
			yield return StartCoroutine(BlackOut());
		}
	}

	private IEnumerator BlackOut(){
		Inventory.Instance.Disable();
		CustomCursorController.Instance.HideCursor();
		blackImage.SetActive(true);
		blackImage.transform.SetAsLastSibling();

		yield return StartCoroutine(FadeAlphaColorImage(blackImage.GetComponent<Image>(), 1f, 1f));

		GameController.WarpToLevel(destinationScene);
	}

	private IEnumerator FadeAlphaColorSpriteRenderer(SpriteRenderer sprite, float alpha, float time){
		float _alphaSprite = sprite.color.a;
		
		for(float t = 0f; t < 1f; t += (Time.deltaTime / time)){
			Color _newColor = sprite.color;
			_newColor.a = Mathf.Lerp(_alphaSprite, alpha, t);
			sprite.color = _newColor;
            yield return null;
        }
    }

	private IEnumerator FadeAlphaColorImage(Image image, float alpha, float time){
		float _alphaImage = image.color.a;
		
		for(float t = 0f; t < 1f; t += (Time.deltaTime / time)){
			Color _newColor = image.color;
			_newColor.a = Mathf.Lerp(_alphaImage, alpha, t);
			image.color = _newColor;
			yield return null;
		}
	}

	private void ActivateShadow(){
		SetActive();
		Color _alphaColor = Color.white;
		thisSpriteRenderer.color = _alphaColor;
		_alphaColor.a = 0f;
		thisSpriteRenderer.color = _alphaColor;
		StartCoroutine(FadeAlphaColorSpriteRenderer(thisSpriteRenderer, 1f, 0.3f));
	}
}
