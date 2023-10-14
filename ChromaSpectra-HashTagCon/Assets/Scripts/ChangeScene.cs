using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneName;

     void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameManager.lastScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneName);
    }
}