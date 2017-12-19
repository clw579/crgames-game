using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour {

	public float minZoom, maxZoom;
    public float moveSpeed;
    public Camera cam1, cam2;

	float zoom = 1f;

	private const float WIDTH = 24.0f;
	private const float HEIGHT = 13.0f;
	private const float TILE_SIZE = 0.75f;

	private float maxX = WIDTH * TILE_SIZE + TILE_SIZE / 2;
	private float maxY = HEIGHT * TILE_SIZE - TILE_SIZE / 2;
	private float minX = -TILE_SIZE / 2;
	private float minY = TILE_SIZE / 2;

    void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;


    }

    void Update ()
    {
		float zoomDelta = Input.GetAxis("Mouse ScrollWheel");

        float xDelta = Input.GetAxis("Horizontal");
        float yDelta = Input.GetAxis("Vertical");

		if (zoomDelta != 0f) {
			AdjustZoom(zoomDelta);
		}

        if (xDelta != 0f || yDelta != 0f)
        {
            AdjustPosition(xDelta, yDelta);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            cam1.enabled = !cam1.enabled;
            cam2.enabled = !cam2.enabled;
        }
	}

	/// <summary>
	/// Adjusts the zoom of the cmaera by changing its orthographic size.
	/// </summary>
	/// <param name="delta">Delta - change in zoom value.</param>
	void AdjustZoom(float delta)
	{
		zoom = Mathf.Clamp01(zoom + delta);

		float distance = Mathf.Lerp(maxZoom, minZoom, zoom);
		cam1.orthographicSize = distance;
	}

	/// <summary>
	/// Adjusts the position of the camera.
	/// </summary>
	/// <param name="xDelta">X delta - change in x value.</param>
	/// <param name="yDelta">Y delta - change in y value.</param>
    void AdjustPosition(float xDelta, float yDelta)
    {
        Vector2 direction = new Vector2(xDelta, yDelta).normalized;
        float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(yDelta));
        float distance = moveSpeed * damping * Time.deltaTime;

        Vector2 position = transform.localPosition;
		position.x = Mathf.Clamp(position.x + direction.x * distance, minX, maxX);
		position.y = Mathf.Clamp(position.y + direction.y * distance, maxY, minY);

		transform.localPosition = position;

//        if (position.x > -17.0f && position.x < maxX - 17.0f && position.y < 10.0f && position.y > maxY + 10.0f){
//            transform.localPosition = position;
//        }        
    }

	/// <summary>
	/// Limits the maximum coordinates.
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
    public void SetMaxCoord(float x, float y){
        maxX = x;
        maxY = -y;
    }
}
