using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class Weapon
{
	public float minDistance = 1f;
	public float distance = 10f;
	public float damage = 1f;
	public float reloadTime = 2f;
	public float penetration = 0.5f;
	
	private bool isLoad = true;
	
	public bool IsLoad
	{
		get{ return isLoad; }	
	}
	
	public void Fire(Vector3 position, float direction)
	{
		isLoad = false;

		RaycastHit2D[] hits = Physics2D.RaycastAll( 
		                                           new Vector2(position.x + minDistance * direction, position.y), 
		                                           new Vector2(direction, 0f), distance - minDistance);

		float givenDamage = damage;

		foreach( RaycastHit2D hit in hits)
		{
			IDestructable<float> destructObject = hit.collider.gameObject.GetComponent(typeof(IDestructable<float>)) as IDestructable<float>;

			if( destructObject == null )
				continue;

			destructObject.TakeDamage( givenDamage, direction );
			givenDamage *= penetration;
			
		}

		Debug.DrawLine(position + new Vector3(minDistance * direction, 0f, 0f), 
		               position + new Vector3((distance - minDistance) * direction, 0f, 0f), 
		               Color.red, 1f);
	}
	
	public IEnumerator ReloadWeapon()
	{
		yield return new WaitForSeconds(reloadTime);
		isLoad = true;
	}
}