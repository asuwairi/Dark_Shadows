using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject SelectedPiece;
    public Camera cam;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform.gameObject.CompareTag("Puzzle"))
            {
                if (hit.transform.gameObject.GetComponent<PieceScript>() != null)
                {
                    if (!hit.transform.gameObject.GetComponent<PieceScript>().isRight())
                    {
                        SelectedPiece = hit.transform.gameObject;
                        SelectedPiece.GetComponent<PieceScript>().Selected = true;
                    }
                }


            }
            else { return; }
        }
        if (SelectedPiece != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                SelectedPiece.GetComponent<PieceScript>().Selected = false;
                SelectedPiece = null;
            }
            if (SelectedPiece != null)
            {
                Vector3 MousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                SelectedPiece.transform.position = new Vector3(MousePoint.x, MousePoint.y, 0);
            }
        }

    }
}