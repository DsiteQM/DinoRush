using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField]
    private Transform camera;

    public GameManager gameManager;
    
    public float moveSpeed;

    private void Update()
    {
        if(gameManager.parallaxController ==true)
        {
            MoveParallax();
        }
    }
    public void MoveParallax()
    {
        transform.Translate(-1 * moveSpeed * Time.deltaTime, 0f, 0f);

        if (camera.position.x >= transform.position.x + 26.07f)
        {
            transform.position = new Vector2(camera.position.x + 26.07f, transform.position.y);
        }
    }
}
