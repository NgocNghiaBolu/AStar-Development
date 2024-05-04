using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sensor : MonoBehaviour
{
    const float sensorLength = 2.5f;

    const float frontSensorStartingPoint = 1;
    const float frontSideSensorStartingPoint = 0.5f;
    const float frontSensorAngle = 30f;

    public float Check()
    {
        RaycastHit hit;
        float avoidDirection = 0;

        Vector3 frontPosition = transform.position + (transform.forward * frontSensorStartingPoint);

        if (DrawSensors(frontPosition, Vector3.forward, sensorLength * 2, out hit))
        {
            Debug.Log(hit.normal.x);
            if (hit.normal.x < 0)
            {
                avoidDirection = -0.25f;
            }
            else
            {
                avoidDirection = 0.25f;
            }
        }
        avoidDirection -= FrontSideSensors(frontPosition, out hit, -1);
        avoidDirection += FrontSideSensors(frontPosition, out hit, 1);
        return avoidDirection;
    }

    bool DrawSensors(Vector3 sensorPosition, Vector3 direction, float length, out RaycastHit hit)
    {
        if(Physics.Raycast(sensorPosition, direction, out hit, length))
        {
            Debug.DrawLine(sensorPosition, hit.point, Color.black);
            return true;
        }
        return false;
    }

    float FrontSideSensors(Vector3 frontPosition, out RaycastHit hit, float sensorDirection)
    {
        float avoidDirection = 0;
        Vector3 sensorPosition = frontPosition + (transform.right * frontSideSensorStartingPoint * sensorDirection); 
        Vector3 sensorAngle = Quaternion.AngleAxis(frontSensorAngle * sensorDirection, transform.up) * transform.forward; 
        if (Physics.Raycast(sensorPosition, transform.forward, out hit, sensorLength))
        {
            avoidDirection = 1;
            Debug.DrawLine(sensorPosition, hit.point, Color.black);
        }
        
        if (Physics.Raycast(sensorPosition, sensorAngle, out hit, sensorLength))
        {
            avoidDirection = 0.5f;
            Debug.DrawLine(sensorPosition, hit.point, Color.black);
        }
        return avoidDirection;
    }
}