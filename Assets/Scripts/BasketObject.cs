using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketObject : MonoBehaviour
{
	[SerializeField] ColorController basketColor;
	[SerializeField] Transform duckUnchorPoint;
	public bool occupiedBasket;
	public delegate void BasketEvents();
	public event BasketEvents OccupieBasketEvent;
	[SerializeField] SpriteRenderController spriteRenderController;
	[SerializeField] Collider2D objCollider;
	[SerializeField] Animator animator;
	SoundPlayer soundPlayer;
	[SerializeField] ParticleSystem smokeParticles;
	[SerializeField] ParticleSystem starParticles;

	public void Initialize(SoundPlayer _soundPlayer)
	{
		soundPlayer = _soundPlayer;
	}
	public Color GetBasketColor()
	{
		return basketColor.activeSpriteColor;
	}
	public Transform GetAnchorPoint()
	{
		return duckUnchorPoint;
	}
	public void OccupieBasket(bool _occupie)
	{
		occupiedBasket = _occupie;
		OccupieBasketEvent?.Invoke();
		EmitStarParticles();
		Highlight(false);
	}
	public void SetColor(Color _color)
	{
		basketColor.ChangeColor(_color, false);
	}
	public void ResetBasket()
	{
		occupiedBasket = false;
	}
	public void ShowBasket(bool _show)
	{
		spriteRenderController.EnableRenderers(_show);
		objCollider.enabled = _show;
		if (!_show) { animator.Rebind(); }
		else
		{
			PlayAppearingAnimation();
			smokeParticles.Play();
			soundPlayer.PlaySound(soundPlayer.coreSounds.fx_smoke, true);
		}
	}
	void PlayAppearingAnimation()
	{
		animator.SetTrigger("Appear");
	}
	public void Highlight(bool _highlight)
	{
		if (_highlight && !occupiedBasket)
		{
			transform.localScale = new Vector3(1, 1, 1) * 1.1f;
			return;
		}
		transform.localScale = new Vector3(1,1,1);
	}
	public void EmitStarParticles()
	{
		starParticles.Play();
	}
}
