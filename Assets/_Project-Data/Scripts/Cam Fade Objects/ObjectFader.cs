using UnityEngine;

public class ObjectFader : MonoBehaviour
{

    public float fadeSpeed; // Speed at which the GameObject will fade when
    public float fadeOpacity; // Opacity of the GameObject when it fully fades

    private float originalOpacity; // Original opacity value of the GameObject's material

    private Material Material;
    public bool DoFade = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Material = GetComponent<Renderer>().material;
        originalOpacity = Material.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if (DoFade)
        {
            FadeNow();
        }
        else
        {
            ResetFade();
        }
    }


    public void FadeNow()
    {
        Color currentColor = Material.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
            Mathf.Lerp(currentColor.a, fadeOpacity, fadeSpeed * Time.deltaTime));
        Material.color = smoothColor;
    }

    public void ResetFade()
    {
        Color currentColor = Material.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b,
            Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed * Time.deltaTime));
        Material.color = smoothColor;
    }
}
