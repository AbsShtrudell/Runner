using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DrawController : MonoBehaviour
{
    [SerializeField]
    private Draw draw;
    [SerializeField]
    public Canvas parentCanvas;
    [SerializeField]
    public RectTransform line;
    [SerializeField]
    private GameObject drawPanel;

    private bool drawing = false;

    public event Action<Vector2[]> onPictureDrawn;
    private void Update()
    {
        if (draw == null) return;

        if (Input.GetButton("Fire1"))
        {
            if (IsMouseOverDrawPanel())
            {
                drawing = true;

                Vector2 movePos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    line,
                    Input.mousePosition, parentCanvas.worldCamera,
                    out movePos);
                draw.SetPoint(movePos);
            }
        }
        else
        {
            if(drawing)
            {
                onPictureDrawn?.Invoke(draw.Clear());
                drawing = false;
            }
        }
    }

    private bool IsMouseOverDrawPanel()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;


        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);

        for(int i = 0; i < raycastResults.Count; i++)
        {
            if(raycastResults[i].gameObject == drawPanel) return true;
        }
        return false;
    }
}
