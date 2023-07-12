using UnityEngine;

public class Car : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("PowerUp"))
		{
			Destroy(other.gameObject);
			Player.instance.PowerUp();
		}
		else if (other.CompareTag("PowerDown"))
		{
			Destroy(other.gameObject);
			Player.instance.PowerDown();
		}
	}
}
