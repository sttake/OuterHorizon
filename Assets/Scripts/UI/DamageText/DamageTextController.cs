using UnityEngine;

public class DamageTextController : MonoBehaviour {
    private static DamageText DamageTextPrefab;
    private static GameObject canvas;

    public static void Initialize() {
        canvas = GameObject.Find("GameEffectCanvas");
        if(!DamageTextPrefab) DamageTextPrefab = Resources.Load<DamageText>("Prefabs/DamageText/DamagetextParent");
    }

    public static void CreateDamageText(string text, Transform textTransform) {
        DamageText instance = Instantiate(DamageTextPrefab) as DamageText;

        // ワールド空間の position をスクリーン空間に変換。
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(textTransform.position);
        //Vector2 randomPosition = new Vector2(textTransform.position.x + Random.Range (-.1f, .1f), textTransform.position.y + Random.Range (-.1f, .1f));
        //Vector2 screenPosition = Camera.main.WorldToScreenPoint(randomPosition);
        
        instance.transform.SetParent(canvas.transform, false);
        instance.transform.position = screenPosition;
        Debug.Log(screenPosition + " ::: " + instance.transform.position);
        instance.setText(text);
    }

}