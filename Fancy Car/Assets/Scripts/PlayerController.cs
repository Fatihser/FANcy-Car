using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public Vector3 speed;
	private void FixedUpdate()
	{
		transform.position =new Vector3(transform.position.x+speed.x,transform.position.y,transform.position.z+speed.z);
	}
}
