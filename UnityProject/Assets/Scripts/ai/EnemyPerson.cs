using UnityEngine;
using System.Collections;

public class EnemyPerson : ArmedPerson
{
	private bool isAlarmed = false;
	private Transform target = null;

	new void Start()
	{
		base.Start ();
		StartCoroutine( Wander () );
	}
	
	override public void TakeDamage( float damage, float dir )
	{
		Debug.Log("I am taking damage");
		base.TakeDamage(damage, dir);
		//transform.Translate( damage * dir, 0f, 0f );

		isAlarmed = true;
		Idle ();

		MoveDirection checkDirection = (MoveDirection) (-dir);

		if( direction != checkDirection )
			Turn( checkDirection );
    }
    
    public void EyeSensorTrigger( Transform triggerReason )
	{
		Debug.Log("I see a target");

		target = triggerReason;
		isAlarmed = true;
		Idle ();
		StartCoroutine( Shooting() );
	}

	IEnumerator Wander()
	{
		Debug.Log("Wandering");
		while(!isAlarmed)
		{
			int decisionSeed = Random.Range(0, 3);

			switch( decisionSeed )
			{
			// go right
			case 0:
				Walk(MoveDirection.MD_RIGHT);
				break;
			// go left
			case 1:
				Walk(MoveDirection.MD_LEFT);
				break;
			// stay idle
			case 2:
				Idle();
				break;
			}
			yield return new WaitForSeconds( Random.Range(2f, 7f) );
		}
	}

	IEnumerator Shooting()
	{
		Debug.Log("Attacking");
		while( target != null )
		{
			Vector3 dirVector = target.position - transform.position;
			MoveDirection checkDirection = dirVector.x > 0 ? MoveDirection.MD_RIGHT : MoveDirection.MD_LEFT;
			
			if( direction != checkDirection )
                Turn( checkDirection );

			if( dirVector.magnitude < gun.distance )
			{
				Debug.Log("Shooting");
				gun.Fire(transform.position, (float) direction);
				yield return StartCoroutine( gun.ReloadWeapon() );
			}
			else
			{
				Debug.Log("Getting closer");
				Walk(direction);
				yield return 0;
			}
		}
	}

}
