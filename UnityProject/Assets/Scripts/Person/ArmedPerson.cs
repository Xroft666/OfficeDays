using UnityEngine;

public class ArmedPerson : MovablePerson
{
	public Weapon gun;
	
	protected void Shoot()
	{
		if( gun.IsLoad )
		{
			gun.Fire(transform.position, (float) direction);
		}
	}
	
	protected void Reload()
	{
		StartCoroutine(gun.ReloadWeapon());
	}
}
