using UnityEngine;

public class Person : MonoBehaviour, IDestructable<float>
{
	protected float hitPoints = 100f;
	protected Transform personRender;

	protected void Awake()
	{
		personRender = transform.FindChild("renderer");
	}
	
	public float HitPoints
	{
		get{ return hitPoints; }
		set{ hitPoints = value; }
	}

	virtual public void TakeDamage( float damage, float direction )
	{
		hitPoints -= damage;
		
		Color color = new Color(1f, hitPoints / 100f, hitPoints / 100f, 1f);
		personRender.renderer.material.SetColor("_Color", color);
		
		GameObject bloodPS = GameObject.Instantiate( 
		                                            Resources.Load( "Effects/BloodPS", typeof(GameObject)), 
		                                            transform.position, Quaternion.Euler(270f, 0f,0f)) as GameObject;
		
		int rotate = (direction == -1) ? 1 : 0;
		bloodPS.transform.Rotate(0f, 180f * rotate, 0f, Space.World);
		
		Object.Destroy(bloodPS, 0.5f);
		
		if( hitPoints <= 0f )
			GameObject.Destroy(gameObject);
	}
}
