using UnityEngine.SceneManagement;
using UnityEngine;

public class NextRoomDoor : MonoBehaviour
{
   [SerializeField] private int Roomnumber;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (Roomnumber == 0)
                SceneManager.LoadSceneAsync("Scene2");
            //else
            //    SceneManager.LoadSceneAsync("Scene2");

        }
    }
}
