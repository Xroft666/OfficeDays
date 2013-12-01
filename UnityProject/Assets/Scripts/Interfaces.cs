using UnityEngine;

public class Person : MonoBehaviour, IDestructable<float>
{
	protected float hitPoints = 100f;
	
	public float HitPoints
	{
		get{ return hitPoints; }
		set{ hitPoints = value; }
	}
	
	public void TakeDamage( float damage, float direction )
	{
		hitPoints -= damage;
		
		Color color = new Color(1f, hitPoints / 100f, hitPoints / 100f, 1f);
		renderer.material.SetColor("_MainColor", color);
	
		if( hitPoints <= 0f )
			GameObject.Destroy(gameObject);
	}
}

[RequireComponent(typeof(AnimateTiledTexture))]
public class AnimatedPerson : Person
{
	protected AnimateTiledTexture tileAnimator = null;
	
	protected void Start()
	{
		tileAnimator = GetComponent<AnimateTiledTexture>() as AnimateTiledTexture;
		tileAnimator.RegisterAnimation("Idle", 8, 15);
		
		tileAnimator.SetAnimation("Idle");
		tileAnimator.Play();
	}
}

public class MovablePerson : AnimatedPerson
{
	public enum MoveDirection
	{
		MD_RIGHT = 1,
		MD_LEFT = -1
	}
	
	public enum PersonState
	{
		PS_IDLE,
		PS_WALK
	}
	
	protected MoveDirection direction = MoveDirection.MD_RIGHT;
	protected PersonState personState = PersonState.PS_IDLE;
}

public class ArmedPerson : MovablePerson
{
	public Weapon gun;
}


public interface IDestructable<T>
{
	void TakeDamage(T damage, T direction);
}
