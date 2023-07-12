using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    public float xPosition;

    private void FixedUpdate()
    {
        //x'i belli bir degerin altinda veya altinda oldugu zaman pozisyon degiscek.
        Vector3 desiredPosition = new Vector3(target.position.x+offset.x, target.position.y + offset.y, target.position.z + offset.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
