using UnityEngine;
using System.Collections;

public class EnemyPerson : ArmedPerson
{
	private enum ActionState
	{
		AS_WANDERING,
		AS_SHOOTING
	}

	private Transform target = null;
	private ActionState currentState = ActionState.AS_WANDERING;

	override public void TakeDamage( float damage, float dir )
	{
		base.TakeDamage(damage, dir);

		//Idle ();
		if( target != null )
			return;

		MoveDirection checkDirection = (MoveDirection) (-dir);

		if( direction != checkDirection )
			Turn( checkDirection );
    }
    
    public void EyeSensorTrigger( Transform triggerReason )
	{
		if( target != null )
			return;

		target = triggerReason;
		Idle ();
	}

	new void Start()
	{
		base.Start();
		StartCoroutine( "Wander" );
	}

	new void Update()
	{
		base.Update();

		// states controller
		if( currentState == ActionState.AS_WANDERING && target != null )
		{
			currentState = ActionState.AS_SHOOTING;
			StopCoroutine( "Wander" );
			StartCoroutine( "Shooting" );
		}

		if( currentState == ActionState.AS_SHOOTING && target == null )
		{
			currentState = ActionState.AS_WANDERING;
			StopCoroutine( "Shooting" );
			StartCoroutine( "Wander" );
		}
	}

	IEnumerator Wander()
	{
		while( true )
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
		while( true )
		{
			Vector3 dirVector = target.position - transform.position;
			MoveDirection checkDirection = dirVector.x > 0 ? MoveDirection.MD_RIGHT : MoveDirection.MD_LEFT;
			
			if( direction != checkDirection )
                Turn( checkDirection );

			// the target is too far
			if( dirVector.magnitude > gun.distance )
			{
				Walk(direction);
				yield return 0;
			}
			// the target is too close
			else if (dirVector.magnitude < gun.minDistance )
			{
				Walk((MoveDirection)((int) direction * -1));
				yield return 0;
			}
			// the target is in range
			else
			{
				Idle();
				gun.Fire(transform.position, (float) direction);
				yield return StartCoroutine( gun.ReloadWeapon() );
			}
		}
	}
}
