using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelScroll : MonoBehaviour, IBeginDragHandler, IEndDragHandler {

    public Toggle[] toggles;

    public float smoothSpeed = 5f;

    private ScrollRect scrollRect;

    private float[] pageArray = new float[] {0f, 0.334f, 0.667f, 1f};

    private float targetHorizontalPosition;

    private bool isDraging = false;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    private void Update()
    {
        if (!isDraging)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(scrollRect.horizontalNormalizedPosition, targetHorizontalPosition, Time.deltaTime * smoothSpeed);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDraging = false;
        float posX = scrollRect.horizontalNormalizedPosition;
        int index = 0;
        float offset = Mathf.Abs(pageArray[index] - posX);

        for (int i=0;i<pageArray.Length;i++)
        {
            float offsetTemp = Mathf.Abs(pageArray[i] - posX);
            if (offsetTemp < offset)
            {
                index = i;
                offset = offsetTemp;
            }
        }
        toggles[index].isOn = true;
        targetHorizontalPosition = pageArray[index];
    }

    public void TurnToPage(int index)
    {
        targetHorizontalPosition = pageArray[index];
    }
}
