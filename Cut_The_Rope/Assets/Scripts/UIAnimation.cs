using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour {

    [Space(10)]
    [Header("Bool")]

    public bool offset = false;

    [Space(10)]
    [Header("Timing")]

    public float delay = 0;
    public float effectTime = 1;

    [Space(10)]
    [Header("ImagePosition")]

    public Vector3 startPos;

    public AnimationCurve posCurve;

    [Space(10)]
    [Header("Rotation")]

    public Vector3 startRotation;

    public AnimationCurve rotationCurve;

    [Space(10)]
    [Header("Scale")]

    public Vector3 startScale = new Vector3(1,1,1);

    public AnimationCurve scaleCurve;



    private Vector3 endPos;

    private Vector3 endRotation;

    private Vector3 endScale;

    private bool latch = false;


    private void Start () {
        endScale = transform.localScale;
        endPos = transform.localPosition;
        endRotation = transform.localEulerAngles;
        if (offset)
        {
            startPos += endPos;
        }
    }

    private void Update()
    {
        if (!latch)
        {
            StopCoroutine(Animation());
            StartCoroutine(Animation());
            latch = true;
        }
    }

    private void OnDisable()
    {
        latch = false;
    }

    IEnumerator Animation()
    {
        transform.position = startPos;
        transform.localScale = startScale;
        transform.localEulerAngles = startRotation;
        yield return new WaitForSecondsRealtime(delay);

        float time = 0;
        float percent = 0;
        float lastTime = Time.realtimeSinceStartup;

        do
        {
            time += Time.realtimeSinceStartup - lastTime;
            lastTime = Time.realtimeSinceStartup;
            percent = Mathf.Clamp01(time / effectTime);

            Vector3 tempScale = Vector3.Lerp(startScale, endScale, scaleCurve.Evaluate(percent));
            Vector3 tempPos = Vector3.Lerp(startPos, endPos, posCurve.Evaluate(percent));
            Vector3 tempRotation = Vector3.Lerp(startRotation, endRotation, rotationCurve.Evaluate(percent));

            transform.localScale = tempScale;
            transform.localPosition = tempPos;
            transform.localEulerAngles = tempRotation;

            yield return null;
        } while (percent < 1);
        transform.localScale = endScale;
        transform.localPosition = endPos;
        transform.localEulerAngles = endRotation;
    }
}
