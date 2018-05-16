using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public GameObject circleCursor;

    Vector3 lastFramePosition;
    Vector3 currentFramePosition;
    Camera mainCamera;

    Vector3 dragStartPosition;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        currentFramePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        currentFramePosition.z = 0;

        // Update the circle position
        Tile tileUnderMouse = GetTileAtWorldCoord(currentFramePosition);
        if (tileUnderMouse != null)
        {
            Vector3 circlePosition = new Vector3(tileUnderMouse.X, tileUnderMouse.Y, 0);
            circleCursor.transform.position = circlePosition;
            circleCursor.SetActive(true);
        }
        else
        {
            circleCursor.SetActive(false);
        }

        // Start drag
        if (Input.GetMouseButtonDown(0))
        {
            dragStartPosition = currentFramePosition;
        }

        // End drag
        if (Input.GetMouseButtonUp(0))
        {
            int start_x = Mathf.FloorToInt(dragStartPosition.x);
            int end_x = Mathf.FloorToInt(currentFramePosition.x);
            if (end_x < start_x)
            {
                int tmp = end_x;
                end_x = start_x;
                start_x = tmp;
            }

            int start_y = Mathf.FloorToInt(dragStartPosition.y);
            int end_y = Mathf.FloorToInt(currentFramePosition.y);
            if (end_y < start_y)
            {
                int tmp = end_y;
                end_y = start_y;
                start_y = tmp;
            }

            for (int x = start_x; x <= end_x; x++)
            {
                for (int y = start_y; y <= end_y; y++)
                {
                    Tile tile = WorldController.Instance.World.GetTileAt(x, y);
                    if (tile != null)
                    {
                        tile.Type = Tile.TileType.Floor;
                    }
                }
            }
        }

        // Handle screen dragging
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {
            Vector3 diff = lastFramePosition - currentFramePosition;
            mainCamera.transform.Translate(diff);
        }
        // We need to recheck the position because the mouse is moving.
        lastFramePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }

    Tile GetTileAtWorldCoord(Vector3 coord)
    {
        int x = Mathf.FloorToInt(coord.x);
        int y = Mathf.FloorToInt(coord.y);
        return WorldController.Instance.World.GetTileAt(x, y);
    }
}
