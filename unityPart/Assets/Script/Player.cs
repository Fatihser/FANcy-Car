using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameObject[] carList;
    private int currentCar = 0;
    public float moveSpeed;
    public float currentmoveXSpeed;
    private Animator anim;


    public bool canControl = true;
    public float rotateSpeed;
    public float positiveMoveXSpeed;
    public float negativeMoveXSpeed;
    public float decreaseMoveXSpeed;
    public float decreaseRotateSpeed;

    public static Player instance;
    public GameObject PowerUpEffect;
    public GameObject PowerDownEffect;

    private float moveSpeedDump;
    public GameObject startPanel;
    public GameObject finishPanel;
    public GameObject endEffect;

    private AudioSource streo;
    public AudioClip clipDown;
    public AudioClip clipUp;
    public AudioClip clipEnd;

	private void Awake()
	{
        instance = this;
        moveSpeedDump = moveSpeed;
        moveSpeed = 0;
	}
	private void Start()
    {
        Debug.Log(carList.Length);
        anim = GetComponent<Animator>();
        streo = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        move();
    }
    private void move()
    {

        transform.position += new Vector3(currentmoveXSpeed * Time.deltaTime, 0, moveSpeed * Time.deltaTime);
        if (canControl)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    if (Input.GetTouch(0).deltaPosition.x > 0&&transform.position.x<2.0f)
                    {
                        currentmoveXSpeed = positiveMoveXSpeed;
                        if(transform.rotation.y<0.3)
                        {
                            transform.Rotate(new Vector3(0, rotateSpeed * Time.deltaTime, 0));
                        }
                    }
                    else if (Input.GetTouch(0).deltaPosition.x < 0&&transform.position.x>-1.80f)
                    {
                        currentmoveXSpeed = negativeMoveXSpeed;
                        if (transform.rotation.y > -0.3)
                        {
                            transform.Rotate(new Vector3(0, -rotateSpeed * Time.deltaTime, 0));
                        }
                    }
					else if (transform.position.x>2.0f||transform.position.x<-1.80f)
					{
                        currentmoveXSpeed = 0;
					}
                }
                else if (Input.GetTouch(0).phase != TouchPhase.Began)
                {
                    if (currentmoveXSpeed > 0)
                    {
                        currentmoveXSpeed -= decreaseMoveXSpeed;
                        if (transform.rotation.y > 0)
                        {
                            transform.Rotate(new Vector3(0, -decreaseRotateSpeed * Time.deltaTime, 0));
                        }
                        return;
                    }
                    else if (currentmoveXSpeed < 0)
                    {
                        currentmoveXSpeed += decreaseMoveXSpeed;
                        if (transform.rotation.y < 0)
                        {
                            transform.Rotate(new Vector3(0, decreaseRotateSpeed * Time.deltaTime, 0));
                        }
                        return;
                    }
                }
            }
            else
            {
                if (currentmoveXSpeed > 0)
                {
                    currentmoveXSpeed -= decreaseMoveXSpeed;
                }
                else if (currentmoveXSpeed < 0)
                {
                    currentmoveXSpeed += decreaseMoveXSpeed;
                }


                if (transform.rotation.y > 0)
                {
                    transform.Rotate(new Vector3(0, -decreaseRotateSpeed * Time.deltaTime, 0));
                }
                else if (transform.rotation.y < 0)
                {
                    transform.Rotate(new Vector3(0, decreaseRotateSpeed * Time.deltaTime, 0));
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, transform.position.y, transform.position.z), 2.5f * Time.deltaTime);
            if (transform.rotation.y > 0)
            {
                transform.Rotate(new Vector3(0, -decreaseRotateSpeed * Time.deltaTime, 0));
            }
            else if (transform.rotation.y < 0)
            {
                transform.Rotate(new Vector3(0, decreaseRotateSpeed * Time.deltaTime, 0));
            }
        }
    }
    public void PowerUp()
	{
        PowerUpEffect.SetActive(true);
        streo.clip = clipUp;
        streo.Play();
		if (currentCar<carList.Length-1)
		{
            carList[currentCar + 1].SetActive(true);
            carList[currentCar].SetActive(false);
            currentCar++;
        }
        Invoke("effectDelayPU", 1.5f);
    }
    private void effectDelayPU()
	{
        PowerUpEffect.SetActive(false);
    }
    public void PowerDown()
	{
        PowerDownEffect.SetActive(true);
        streo.clip = clipDown;
        streo.Play();
		if (currentCar>0)
		{
            carList[currentCar - 1].SetActive(true);
            carList[currentCar].SetActive(false);
            currentCar--;
        }
        Invoke("effectDelayPD", 1.5f);
    }
    private void effectDelayPD()
    {
        PowerDownEffect.SetActive(false);
    }
    public void speedUp()
    {
        StartCoroutine("speedup");
    }
     IEnumerator speedup()
    {
        while (true)
        {
            moveSpeed += 0.1f;
            anim.SetTrigger("runFaster");
            if (moveSpeed>=15)
            {
                StopCoroutine("speedup");
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void stop()
    {
        moveSpeed = 0;
    }

    public void StartGame()
	{
        Destroy(startPanel);
        moveSpeed = moveSpeedDump;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("end"))
		{
            stop();
            streo.clip = clipEnd;
            streo.Play();
            endEffect.SetActive(true);
            finishPanel.SetActive(true);
		}
	}
}
