using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;
    
    private void OnCollisionEnter(Collision collision)
    {
        GameManager.lastScene = SceneManager.GetActiveScene().buildIndex;
        SceneTransition transition = FindObjectOfType<SceneTransition>();
        transition.NextLevelFade(sceneName);
    }
}