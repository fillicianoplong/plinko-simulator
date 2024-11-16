using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject m_objectToCenter;
    private Camera m_orthographicCamera;
    private float m_xPos;
    private float m_yPos;
    private float m_width;
    private float m_height;
    private float m_aspect;
    private float m_worldHeight;
    private float m_worldWidth;

    // Start is called before the first frame update
    void Start()
    {
        m_orthographicCamera = GetComponent<Camera>();

        m_xPos = 0;
        m_yPos = 0.455f;
        m_width = 1.0f;
        m_height = 0.5f;

        if(m_orthographicCamera != null )
        {
            // Set camera as orthographic
            m_orthographicCamera.orthographic = true;

            // Find orthographic camera world height and width
            m_aspect = (float)Screen.width / Screen.height;
            m_worldHeight = m_orthographicCamera.orthographicSize * 2;
            m_worldWidth = m_worldHeight * m_aspect;


            // Find the center of a parent object
            Vector3 cameraCenter = getCenter(m_objectToCenter.GetComponent<Transform>());
            cameraCenter.z = -1.0f;

            // Center camera
            m_orthographicCamera.transform.position = cameraCenter;

            // Adjust camera size based on object width
            resizeCamera(m_aspect, 3, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Find orthographic camera world height and width
        m_aspect = (float)Screen.width / Screen.height;
        m_worldHeight = m_orthographicCamera.orthographicSize * 2;
        m_worldWidth = m_worldHeight * m_aspect;


        // Adjust camera size based on object width
        resizeCamera(m_aspect, 3, 0.5f);
    }

    private Vector3 getCenter(Transform obj)
    {
        // Initialize center variable
        Vector3 center = new Vector3();

        // Determine if object has a renderer element
        if(obj.GetComponent<Renderer>() != null)
        {
            // Get the center of the renderer element
            center = obj.GetComponent<Renderer>().bounds.center;
        }
        else
        {
            // Access sub objects in parent
            foreach(Transform subObj in obj)
            {
                // Recursively traverse sub objects to find renderer center
                center += getCenter(subObj);
            }
            
            // Average out the center point of all children
            center /= obj.childCount;
        }

        return center;
    }

    private float getWidth(Transform obj)
    {
        float xPosMax = 0.0f;
        
        foreach (Transform subObj in obj)
        {
            foreach(Transform subSubObj in subObj)
            {
                float xPosCurrent = subSubObj.position.x;
                if (xPosCurrent > xPosMax)
                {
                    xPosMax = xPosCurrent;
                }
            }
        }

        return xPosMax * 2;
    }

    private void resizeCamera(float currentAspect, float size, float maxAspect)
    {
        if (m_aspect <= maxAspect)
        {
            m_orthographicCamera.orthographicSize = getWidth(m_objectToCenter.GetComponent<Transform>()) / (currentAspect * size);
            m_orthographicCamera.rect = new Rect(m_xPos, m_yPos, m_width, m_height);
        }
        else
        {
            m_orthographicCamera.orthographicSize = getWidth(m_objectToCenter.GetComponent<Transform>()) / (maxAspect * size);
            m_orthographicCamera.rect = new Rect(m_xPos, m_yPos, m_width, m_height);
        }
    }
}
