using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gopuz2 : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.name == "Player")
        {
            SceneManager.LoadScene(5);
        }
    }

}