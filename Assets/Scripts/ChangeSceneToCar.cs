using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneToCar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Change();
    }
    public void Change()
    {
        SceneManager.LoadScene(1);
    }
}
