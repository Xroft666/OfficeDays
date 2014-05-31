using UnityEngine;

public class AnimatedPerson : Person
{
	protected Animator animator = null;
	
	protected void Start()
	{
		animator = personRender.GetComponent<Animator>();
		animator.SetBool("walk", false);
	}
}
