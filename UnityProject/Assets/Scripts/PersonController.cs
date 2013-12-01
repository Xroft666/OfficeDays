using UnityEngine;
using System;
using System.Collections;

public class PersonController : ArmedPerson 
{	
	new void Start()
	{
		base.Start();
		tileAnimator.RegisterAnimation("Walk", 0, 7);
	}
	
	void Update()
	{
		bool keyPressed = false;
		if( Input.GetKeyDown( KeyCode.RightArrow ) )
		{
			direction = MoveDirection.MD_RIGHT;
			keyPressed = true;
		}
		
		
		if( Input.GetKeyDown( KeyCode.LeftArrow ) )
		{
			direction = MoveDirection.MD_LEFT;
			keyPressed = true;
		}

		if( keyPressed )
		{
			personState = PersonState.PS_WALK;
			tileAnimator.SetAnimation("Walk");
			
			Vector3 scale = transform.localScale;
			transform.localScale = new Vector3(Mathf.Abs(scale.x) * (-(float)direction), scale.y, scale.z);
		}
		
		if( !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
		{
			if( PersonState.PS_IDLE != personState )
			{
				personState = PersonState.PS_IDLE;
				
				Vector3 scale = transform.localScale;
				transform.localScale = new Vector3(Mathf.Abs(scale.x) * (float)direction, scale.y, scale.z);
				
				tileAnimator.SetAnimation("Idle");
			}
		}
		
		if( Input.GetKeyDown( KeyCode.Space ) && gun.IsLoad)
		{
			gun.Fire(transform.position, (float) direction);
			StartCoroutine( gun.ReloadWeapon() );
		}
		
		transform.Translate(Input.GetAxis("Horizontal"), 0f, 0f);	
	}
}
			

