using System.Collections;
using System.Collections.Generic;
using UnityEngine;


enum Team { A, B };

enum Bar { BAR1, BAR2, BAR3, BAR4 };

enum State { DOWN, UP, MOVING_DOWN, MOVING_UP, };
public class BarA1Controller : MonoBehaviour
{

    public Texture2D CURSOR_DRAG, CURSOR_OVER, CURSOR_OUT;

    private Team team;

    private Bar bar;

    private GameObject player;

    private Color color;
    public Vector3 offset;

    public bool dragging = false;

    private bool active = false;

    public bool blocking = true;

    public bool mouseOver = false;
    private State currentState;

    // Start is called before the first frame update
    void Start()
    {
        //coll = GetComponent<BoxCollider2D>() as Collider;
        dragging = false;
        mouseOver = false;
        active = false;
        blocking = true;
        color = GetComponent<SpriteRenderer>().color;
        Cursor.SetCursor(CURSOR_OUT, Vector2.zero, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {



        if (dragging && Input.GetKey(KeyCode.A)) // -> left
        {
            PerformKick(-10f);

        }
        else if (dragging && Input.GetKey(KeyCode.D)) // -> right
        {
            PerformKick(10f);
        }
    }

    private void PerformKick(float deltaY)
    {
        foreach (Transform player in transform)
        {
            if (player.name.StartsWith("Player"))
            {
                player.GetComponent<Rigidbody2D>().AddForce(new Vector2(deltaY, 0), ForceMode2D.Impulse);
            }
        }
    }
    private void OnMouseEnter()
    {
        mouseOver = true;
        if (!dragging)
        {
            Cursor.SetCursor(CURSOR_OVER, Vector2.zero, CursorMode.ForceSoftware);
            // highlight:
            Highlight(true);
        }
    }

    private void OnMouseExit()
    {
        mouseOver = false;
        /// only dehighlight if not currently dragging:
        if (!dragging)
        {
            Cursor.SetCursor(CURSOR_OUT, Vector2.zero, CursorMode.ForceSoftware);
            Highlight(false);
        }
    }


    private void OnMouseDown()
    {
        dragging = true;
        Cursor.SetCursor(CURSOR_DRAG, Vector2.zero, CursorMode.ForceSoftware);
        // Store offset = gameobject world pos - mouse world pos
        offset = GetMouseAsWorldPoint() - gameObject.transform.position;

    }

    private void OnMouseUp()
    {
        dragging = false;
        if (mouseOver)
        {
            Cursor.SetCursor(CURSOR_OVER, Vector2.zero, CursorMode.ForceSoftware);
        }
        else
        {
            Highlight(false);
            Cursor.SetCursor(CURSOR_OUT, Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    /// <summary>
    /// Called every frame while the mouse is over the GUIElement or Collider.
    /// </summary>
    void OnMouseOver()
    {
        if (dragging && Input.GetMouseButtonDown(0))
        {
            foreach (Transform player in transform)
            {
                if (player.name.StartsWith("Player"))
                {

                    //JointTranslationLimits2D limits = player.GetComponent<SliderJoint2D>().limits;//new JointTranslationLimits2D();


                    //if (mouseOver)
                    {
                        player.GetComponent<TargetJoint2D>().target = new Vector2(GetMouseAsWorldPoint().x, player.transform.position.y);
                        //limits.max = GetMouseAsWorldPoint().x - gameObject.transform.position.x;
                        //Debug.Log(limits.max);
                        //player.GetComponent<Rigidbody2D>().AddForce(new Vector2(7f, 0), ForceMode2D.Impulse);
                    }
                }
            }
        }
    }
    private void OnMouseDrag()
    {
        Vector2 drag = new Vector2(transform.position.x, (GetMouseAsWorldPoint() - offset).y);
        GetComponent<TargetJoint2D>().target = drag;

    }


    private IEnumerator PerformPlayer()
    {
        yield return null;
    }

    public Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;
        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void Highlight(bool active)
    {
        this.active = active;
        //GetComponent<SpriteRenderer>().color = color;
        foreach (SpriteRenderer player in GetComponentsInChildren<SpriteRenderer>())
        {
            //if (player.gameObject.name.StartsWith("Player"))
            player.color = active ? Color.white : color;
        }
    }
}
