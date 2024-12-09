using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
   /*  public void Play(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    } */

    public void Play(){
        SceneManager.LoadScene("Level1");
    }

    public void Exit(){
        Debug.Log("Salir");
        Application.Quit();
    }
}
