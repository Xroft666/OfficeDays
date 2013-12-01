using UnityEngine;
using System.Collections;

public class EnemyPerson : ArmedPerson
{
	new void Start () 
	{
		base.Start();
		tileAnimator.RegisterAnimation("Walk", 0, 7);	
	}
	
	new void TakeDamage( float damage, float dir )
	{
		base.TakeDamage(damage, dir);
		transform.Translate( damage * dir, 0f, 0f );
	}
}
