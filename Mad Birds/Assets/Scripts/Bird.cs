using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    Vector3 _initialPosition;
    private bool _birdWasLaunched;
    private float _timeStill;

    [SerializeField] private float _speed = 500;

    private void Awake()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        GetComponent<LineRenderer>().SetPosition(0, transform.position);
        GetComponent<LineRenderer>().SetPosition(1, _initialPosition);
        
        if (_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeStill += Time.deltaTime;

        }

        if(transform.position.y > 30 || transform.position.y < -30 || transform.position.x > 30 || transform.position.x < -30 || _timeStill > 3)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    
    private void OnMouseDown()
    {
        //Make red when clicking birdyboi
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp()
    {
        //Make white when releasing birdyboi
        GetComponent<SpriteRenderer>().color = Color.white;

        //make bird fly (and fall)
        Vector2 directionToInitialPosition = _initialPosition - transform.position;

        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _speed);
        GetComponent<Rigidbody2D>().gravityScale = 1;

        _birdWasLaunched = true;

        GetComponent<LineRenderer>().enabled = false;
    }

    //Make me able to drag birdyboi
    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }
}
