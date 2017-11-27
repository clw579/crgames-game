﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour {

	public float minZoom, maxZoom;
    public float moveSpeed;
    public Camera cam1, cam2;

	float zoom = 1f;

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

	void AdjustZoom(float delta)
	{
		zoom = Mathf.Clamp01(zoom + delta);

		float distance = Mathf.Lerp(minZoom, maxZoom, zoom);
		cam1.orthographicSize = distance;
	}

    void AdjustPosition(float xDelta, float yDelta)
    {
        Vector2 direction = new Vector2(xDelta, yDelta).normalized;
        float damping = Mathf.Max(Mathf.Abs(xDelta), Mathf.Abs(yDelta));
        float distance = moveSpeed * damping * Time.deltaTime;

        Vector2 position = transform.localPosition;
        position += direction * distance;
        transform.localPosition = position;
    }
}
