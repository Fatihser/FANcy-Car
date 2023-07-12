using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneWorks : MonoBehaviour
{
    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
