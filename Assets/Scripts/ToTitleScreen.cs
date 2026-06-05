using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ToTitleScreen : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(co());
    }
    IEnumerator co()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }
}
