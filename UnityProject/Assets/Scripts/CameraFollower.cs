﻿using UnityEngine;
using System.Collections;

public class CameraFollower : MonoBehaviour 
{
	public Transform followTarget;

	void Update () 
	{
		if( followTarget != null )
		transform.position = new Vector3(followTarget.position.x, transform.position.y, transform.position.z);
	}
}
