using UnityEngine;
using System.Collections;

public class GUITextButton : MonoBehaviour {

	public delegate void OnButtonMouseUpEvent();
	public event OnButtonMouseUpEvent OnButtonMouseUp;
	public string identifier;
	public Color highlightedColor = Color.red;
	public Color normalColor = Color.white;
	public AudioSource higlightSFX;
	public AudioSource clickSFX;

	//http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	void OnMouseUp(){
		if (clickSFX)clickSFX.Play();
						
		OnButtonMouseUp();
	}

	void OnMouseEnter(){
		if (higlightSFX)higlightSFX.Play();

		this.GetComponent<GUIText>().color = highlightedColor;
	}

	void OnMouseExit(){

		this.GetComponent<GUIText>().color = normalColor;
	}

}
