using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMovement : MonoBehaviour
{
    [Header("Min/Max X Rotation Angles (Degrees)")]
    [SerializeField] float _maxRotationAngle = 90;
    [SerializeField] float _minRotationAngle = -90;

    [Header("Sun Rotation Animation Curve")]
    [SerializeField] AnimationCurve _sunRotationCurve;

    [Header("Time Length of a Rotation Cycle (Seconds)")]
    [SerializeField] float _sunRotationPeriod = 10f;
    [SerializeField] Transform _light;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SunRotateRoutine(_sunRotationPeriod, _maxRotationAngle, _minRotationAngle));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator SunRotateRoutine(float period, float maxAngle, float minAngle)
    {
        //Inilitialise the time variable at 0, this will keep track of the time in game.
        //Calculate the difference between the min and max angle values.
        float time = 0;
        float xRotationDelta = maxAngle - minAngle;
        while (true)
        {
            //If the time variable has exceeded the cycle period, break the loop and restart the coroutine.
            if (time >= period)
            {
                StartCoroutine(SunRotateRoutine(period, maxAngle, minAngle));
                break;
            }
            //Else, calculate the suns current rotation on the X axis using the animation curve 
            //which is evalutated at the timeRatio and multiplied by the xRotationDelta variable.
            //Set the sun's local euler angles to a new vector3 which has the x value set to the current angle calculated.
            //Increment the time variable by the time elapsed and yield.
            else
            {
                float timeRatio = time / period;
                float currentAngleX = _maxRotationAngle - (_sunRotationCurve.Evaluate(timeRatio) * xRotationDelta);
                transform.eulerAngles = new Vector3(currentAngleX, 0, 0);
                _light.LookAt(transform.position);
                time += Time.deltaTime;
                yield return null;
            }
        }
    }
}
