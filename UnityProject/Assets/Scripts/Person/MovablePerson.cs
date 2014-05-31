using UnityEngine;

public class MovablePerson : AnimatedPerson
{
	public enum MoveDirection
	{
		MD_RIGHT = 1,
		MD_LEFT = -1
	}
	
	public enum PersonState
	{
		PS_IDLE = 0,
		PS_WALK = 1
	}
	
	protected MoveDirection direction = MoveDirection.MD_RIGHT;
	protected PersonState personState = PersonState.PS_IDLE;
	
	public float speed = 1f;
	
	
	protected void Walk( MoveDirection walkDirection )
	{
		Turn( walkDirection );

		personState = PersonState.PS_WALK;
		animator.SetBool("walk", true);
	}
	
	protected void Idle( )
	{
		personState = PersonState.PS_IDLE;
		animator.SetBool("walk", false);
	}
	
	protected void Update()
	{
		if( personState == PersonState.PS_WALK )
			transform.Translate(/*(float) direction * */speed * Time.deltaTime, 0f, 0f);
	}

	protected void Turn(  MoveDirection turnDirection )
	{
		direction = turnDirection;

		if( turnDirection == MoveDirection.MD_LEFT )
			transform.eulerAngles = new Vector2(0,180);
		else
			transform.eulerAngles = new Vector2(0,0);

		foreach( Transform child in transform )
			child.SendMessage("TurnEvent", (float) direction, SendMessageOptions.DontRequireReceiver);
    }
}
