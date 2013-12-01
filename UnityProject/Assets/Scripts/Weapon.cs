using UnityEngine;
using System;
using System.Collections;

[System.Serializable]
public class Weapon
{
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
		
		RaycastHit[] hits = Physics.RaycastAll(position, 
												new Vector3(direction, 0f, 0f), 
												distance);
		float givenDamage = damage;
		
		foreach( RaycastHit hit in hits )
		{
			IDestructable<float> destructObject = hit.collider.gameObject.GetComponent(typeof(IDestructable<float>)) as IDestructable<float>;
			destructObject.TakeDamage( givenDamage, direction );
			
			givenDamage *= penetration;
		}
		
		Debug.DrawLine(position, 
						position + new Vector3(distance * direction, 0f, 0f), 
						Color.red);
	}
	
	public void Reload()
	{
		isLoad = true;
	}
	
	public IEnumerator ReloadWeapon()
	{
		yield return new WaitForSeconds(reloadTime);
		Reload();
	}
}