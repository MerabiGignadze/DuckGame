using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckObject : MonoBehaviour
{
	[SerializeField] ColorController duckColor;
	[SerializeField] SpriteRenderController spriteRenderController;
	public bool inBasket;
	float colorThreshold = 0.01f;
	float anchoringDistance = 2.0f;
	[SerializeField] float swimSpeed;
	BasketObject activeBasket;
	public enum ReleaseSpot { OnRightBasket, OnWrongBasket, NotOnBasket }
	[SerializeField]
	Transform originSpot;
	[SerializeField]
	SpriteRenderer blob;
	[SerializeField]
	Collider2D objCollider;
	[SerializeField] Animator animator;
	[SerializeField] DraggableObject draggableObject;
	SoundPlayer soundPlayer;
	[SerializeField] FlashHighlighter flashHighlighter;

	public void Initilize(SoundPlayer _soundPlayer)
	{
		soundPlayer = _soundPlayer;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Basket")
		{
			activeBasket = collision.GetComponent<BasketObject>();
			activeBasket.Highlight(true);
		}
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == "Basket")
		{
			collision.GetComponent<BasketObject>().Highlight(false);
		}
	}
	public void PickDuck()
	{
		EnableBlob(false);
		soundPlayer.PlayRandomSound(soundPlayer.coreSounds.fx_duck, true);
	}
	public void ReleaseDuck()
	{
		switch (OnRightBasket())
		{
			case ReleaseSpot.OnRightBasket:
				AnchorInBasket();
				activeBasket.OccupieBasket(true);
				EnableBlob(false);
				draggableObject.ChangeObjectScale(0.65f);
				break;
			case ReleaseSpot.OnWrongBasket:
				EnableBlob(true);
				ResetOnOriginSpot();
				draggableObject.ChangeObjectScale(1f);
				soundPlayer.PlaySound(soundPlayer.coreSounds.fx_wrong, true);
				break;
			case ReleaseSpot.NotOnBasket:
				EnableBlob(true);
				ResetOnOriginSpot();
				draggableObject.ChangeObjectScale(1f);
				soundPlayer.PlaySound(soundPlayer.coreSounds.fx_wrong, true);
				break;
		}
		PlayAppearAnimation();
	}
	void ResetOnOriginSpot()
	{
		transform.position = originSpot.position;
	}
	void AnchorInBasket()
	{
		inBasket = true;
		transform.position = activeBasket.GetAnchorPoint().position;
		EnableCollider(false);
		//soundPlayer.PlaySound(soundPlayer.coreSounds.fx_correct, true);
	}
	ReleaseSpot OnRightBasket()
	{
		if (!activeBasket) { return ReleaseSpot.NotOnBasket; }
		bool basketInDistance = Vector3.Distance(transform.position, activeBasket.transform.position) < anchoringDistance;
		if (!basketInDistance) {  activeBasket = null; return ReleaseSpot.NotOnBasket; }
		if (SameColor(duckColor.activeSpriteColor, activeBasket.GetBasketColor()))
		{
			return ReleaseSpot.OnRightBasket;
		}
		return ReleaseSpot.OnWrongBasket;
	}
	void EnableBlob(bool _enable)
	{
		blob.enabled = _enable;
	}
	bool SameColor(Color _color1, Color _color2)
	{
		return (Mathf.Abs(_color1.r - _color2.r) <= colorThreshold &&
				Mathf.Abs(_color1.g - _color2.g) <= colorThreshold &&
				Mathf.Abs(_color1.b - _color2.b) <= colorThreshold &&
				Mathf.Abs(_color1.a - _color2.a) <= colorThreshold);
	}
	public void SetColor(Color _color)
	{
		duckColor.ChangeColor(_color, false);
	}
	public Color GetColor()
	{
		return duckColor.activeSpriteColor;
	}
	public void ResetDuck(Vector3 _position)
	{
		transform.position = _position;
		activeBasket = null;
		draggableObject.ChangeObjectScale(1);
		inBasket = false;
	}
	public void ShowDuck(bool _show)
	{
		spriteRenderController.EnableRenderers(_show);
		EnableBlob(_show);
		if (!_show) { animator.Rebind(); }
		else
		{
			PlayIdleAnimation();
			StartCoroutine(SwimToTarget());
		}	
	}
	void EnableCollider(bool _enable)
	{
		objCollider.enabled = _enable;
	}
	void PlayAppearAnimation()
	{
		animator.SetTrigger("Appear");
	}
	void PlayIdleAnimation()
	{
		animator.SetTrigger("Idle");
	}
	public void PlaySplashSound()
	{
		soundPlayer.PlaySound(soundPlayer.coreSounds.fx_waterSplash, true);
	}
	IEnumerator SwimToTarget()
	{
		Vector3 velocity = Vector3.zero;
		if (transform.position.x < originSpot.position.x)
		{
			transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
		}
		else
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
		while(Vector3.Distance (transform.position, originSpot.position) > 0.1f)
		{
			transform.position = Vector3.SmoothDamp(transform.position, originSpot.position, ref velocity, swimSpeed);
			yield return new WaitForSeconds(Time.deltaTime);
		}
		EnableCollider(true);
		soundPlayer.PlayRandomSound(soundPlayer.coreSounds.fx_duck, true);
		flashHighlighter.Flash();
	}
}
