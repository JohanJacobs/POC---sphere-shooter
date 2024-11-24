using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace SS
{
    public static class BuildingCard
    {
        public static Transform CreateCard(BuildingScriptableObject so, Transform template, int x, int y, UnityAction callback)
        {
            var parent = template.parent;

            //create the new object
            var t = GameObject.Instantiate(template, template.parent);
            t.gameObject.SetActive(true);
            t.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            var b = t.GetComponent<Button>();
            b.onClick.AddListener(callback);

            // update the labels and icons
            t.Find("Label").GetComponent<TextMeshProUGUI>().text = so.buildingLabel;
            t.Find("Icon").GetComponent<Image>().sprite = so.buildingIcon;

            return t;
        }
        
    }
}