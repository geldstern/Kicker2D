using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BarController : MonoBehaviour
{

    private Team team;

    private Bar bar;
    private Color color;
    private Vector3 offset;

    private bool dragging = false;

    private State currentState;

    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = Color.white;


        foreach (SpriteRenderer player in GetComponentsInChildren<SpriteRenderer>())
        {
            if (player.gameObject.name.StartsWith("Player"))
                player.color = Color.white;
        }

    }


    private void OnMouseOver()
    {
        //offset = gameObject.transform.position - GetMouseAsWorldPoint();
        transform.position = new Vector3(transform.position.x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y ,0);
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = color;
        foreach (SpriteRenderer player in GetComponentsInChildren<SpriteRenderer>())
        {
            if (player.gameObject.name.StartsWith("Player"))
                player.color = color;
        }
    }


    private void OnMouseDown()
    {
        dragging = true;
        // Store offset = gameobject world pos - mouse world pos
        offset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    private void OnMouseUp()
    {
        // Store offset = gameobject world pos - mouse world pos
        offset = gameObject.transform.position - GetMouseAsWorldPoint();
        dragging = false;
    }



    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;
        // z coordinate of game object on screen
        mousePoint.z = 0;
        mousePoint.x = 0;
        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseAsWorldPoint() + offset;

    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.DOWN;
        color = GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {


        if (dragging && Input.GetKeyDown(KeyCode.Space) && currentState == State.DOWN)
        {

            currentState = State.UP;
            //StartCoroutine(PerformPlayer());
            foreach (Transform player in GetComponentsInChildren<Transform>())
            {
                if (player.gameObject.name.StartsWith("Player"))
                {
                    player.transform.position = player.transform.position + Vector3.right * 0.5f;
                }
            }

        }
        else if (dragging && Input.GetKeyUp(KeyCode.Space) && currentState == State.UP)
        {
            currentState = State.DOWN;
            //StartCoroutine(PerformPlayer());
            foreach (Transform player in GetComponentsInChildren<Transform>())
            {
                if (player.gameObject.name.StartsWith("Player"))
                {
                    player.transform.position = player.transform.position + Vector3.left * 0.5f;
                }
            }

        }
    }


    private IEnumerator PerformPlayer()
    {

        yield return null;
    }
}
