using UnityEngine;
using System;
using System.Collections;

public class PersonController : ArmedPerson 
{	
	new void Start()
	{
		base.Start();
	}
	
	new void Update()
	{
		base.Update();

		if( Input.GetKeyDown( KeyCode.RightArrow ) )
		{
			Walk( MoveDirection.MD_RIGHT );
		}

		if( Input.GetKeyDown( KeyCode.LeftArrow ) )
		{
			Walk( MoveDirection.MD_LEFT );
		}
		
		if( !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
		{
			if( PersonState.PS_IDLE != personState )
			{
				Idle();
			}
		}
		
		if( Input.GetKeyDown( KeyCode.Space ) && gun.IsLoad)
		{
			gun.Fire(transform.position, (float) direction);
			StartCoroutine( gun.ReloadWeapon() );
		}
	}
}
			

