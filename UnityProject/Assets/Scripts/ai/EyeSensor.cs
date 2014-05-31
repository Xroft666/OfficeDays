using UnityEngine;
using System.Collections;

public class EyeSensor : MonoBehaviour {

	public float distance = 3f;

	void Start()
	{
		(collider2D as BoxCollider2D).center = new Vector2( distance * 0.5f, 0f);
		(collider2D as BoxCollider2D).size = new Vector2( distance, 0.5f);
	}

	void OnTriggerEnter2D( Collider2D other )
	{
		SendMessageUpwards("EyeSensorTrigger", other.transform );
	}

	// called by MovablePerson.cs
	void TurnEvent( float turnDirection)
	{
		(collider2D as BoxCollider2D).center = new Vector2( turnDirection * distance * 0.5f, 0f);
	}
}
