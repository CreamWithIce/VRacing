using UnityEngine;
using UnityEngine.SceneManagement;
public class InputTutorial : MonoBehaviour
{

    void Update()
    {
        if(Input.GetButtonDown("XRI_Right_PrimaryButton")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
