using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Tooltip : MonoBehaviour {
    public static Tooltip Instance { get; set; }

    //Returns the transform that's current being looked at.
    static Transform currentTooltipTransform;

    Text textInfo;
    Image infoBackground;
    Vector2 originalBackgroundSize;
    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(this);

        textInfo = transform.Find("Info").GetComponent<Text>();
        infoBackground = transform.Find("Background").GetComponent<Image>();

        originalBackgroundSize = infoBackground.rectTransform.sizeDelta;
    }

    //Checks if an item has a tooltip.
    public void CheckForTooltip(Vector3 origin, Vector3 direction, float maxDistance) {

        currentTooltipTransform = null;

        //Gets rid of UI.
        textInfo.gameObject.SetActive(false);
        infoBackground.gameObject.SetActive(false);

        infoBackground.rectTransform.sizeDelta = originalBackgroundSize;

        //Checks for item with ray.
        RaycastHit hit;
        if (Physics.Raycast(origin, direction, out hit, maxDistance)) {

            if ((hit.transform.gameObject.CompareTag("StealablePainting") || hit.transform.gameObject.CompareTag("StealablePainting") || hit.transform.gameObject.CompareTag("ObjectiveItem")
                || hit.transform.gameObject.CompareTag("Interactable"))) {

                if (hit.transform.GetComponent<TooltipInfo>()) {
                    ShowInfo(hit.transform);

                    currentTooltipTransform = hit.transform;

                    textInfo.gameObject.SetActive(true);
                    infoBackground.gameObject.SetActive(true);
                }
                else {
                    Debug.LogWarning("No tooltip info.");
                }
            }
        }
    }

    int offsetPerSpace = 19;
    void ShowInfo(Transform item) {
        textInfo.text = item.GetComponent<TooltipInfo>().ItemInfo();

        //Adjust UI for amount of text.
        infoBackground.rectTransform.sizeDelta = new Vector2(originalBackgroundSize.x,
            originalBackgroundSize.y + offsetPerSpace * textInfo.text.Count(f => f == '\n') + ((originalBackgroundSize.y - offsetPerSpace) / 2));
    }

    public static Transform CurrentTooltipTransform {
        get {
            return currentTooltipTransform;
        }
    }
}
