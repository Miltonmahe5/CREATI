﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickCamera : MonoBehaviour
{
    //public Transform player;
    //public float speed = 5.0f;
    private bool touchStart = false;
    private Vector2 pointA;
    private Vector2 pointB;
    public float radiousOffset = 1.0f;
    public RectTransform CenterImage;
    public Camera CameraUI;

    Vector3 m_StartPos;

    public Transform circle;
    public Transform outerCircle;

    public void InitPositions()
    {

        pointA = CenterImage.position;
        circle.position = pointA;
        outerCircle.position = pointA;
        print(pointA+"= PointA");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));

            //circle.transform.position = pointA * -1;
            //outerCircle.transform.position = pointA * -1;
            //circle.GetComponent<SpriteRenderer>().enabled = true;
            //outerCircle.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = CameraUI.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
        }
        else
        {
            touchStart = false;
        }

    }
    private void FixedUpdate()
    {
        if (touchStart)
        {
            Vector2 offset = pointB - pointA;
            Vector2 direction = Vector2.ClampMagnitude(offset, radiousOffset);
            moveCharacter(direction * -1);

            circle.transform.localPosition = new Vector2(pointA.x + direction.x, pointA.y + direction.y) * -1;
        }
        else
        {
            //circle.GetComponent<SpriteRenderer>().enabled = false;
            //outerCircle.GetComponent<SpriteRenderer>().enabled = false;
        }

    }
    void moveCharacter(Vector2 direction)
    {
        //player.Translate(direction * speed * Time.deltaTime);
    }
}

