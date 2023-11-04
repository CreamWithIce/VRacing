using UnityEngine;
using UnityEngine.SceneManagement;
public class endManager : MonoBehaviour {
    [SerializeField] private int mainMenuIndex;
    public void backToMainMenu(){
        SceneManager.LoadScene(mainMenuIndex);
    }
}