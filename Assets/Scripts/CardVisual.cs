using Entity;
using UnityEngine;

[ExecuteInEditMode]
public class CardVisual : MonoBehaviour
{
    [Header("Gradient values")] [SerializeField]
    float rowEffect1;

    [SerializeField] float rowEffect2;
    [Header("")] [SerializeField] float frenselPower = 0.65f;
    [Header("")] [SerializeField] float holoTilling = 2f;
    [SerializeField] float holoIntensity = 1f;
    [SerializeField] float transitionPower = 1f;

    private Renderer cardRenderer;
    private MaterialPropertyBlock propertyBlock;

    void Start()
    {
        cardRenderer = GetComponent<Renderer>();
        propertyBlock = new MaterialPropertyBlock();

        updateShader();
    }

    public void setUpdated(CardConfig visual)
    {
        rowEffect1 = visual.RowEffect1;
        rowEffect2 = visual.RowEffect2;
        transitionPower = visual.TransitionPower;
        holoIntensity = visual.HoloIntensity;
        holoTilling = visual.HoloTilling;
        frenselPower = visual.FrenselPower;
        updateShader();
    }

    private void updateShader()
    {
        cardRenderer.GetPropertyBlock(propertyBlock);

        propertyBlock.SetFloat("_Row_Effect_1", rowEffect1);
        propertyBlock.SetFloat("_Row_Effect_2", rowEffect2);
        propertyBlock.SetFloat("_Transition_Power", transitionPower);
        propertyBlock.SetFloat("_Holo_Intensity", holoIntensity);
        propertyBlock.SetFloat("_Holo_Tilling", holoTilling);
        propertyBlock.SetFloat("_Fresnel_Power", frenselPower);

        cardRenderer.SetPropertyBlock(propertyBlock);
    }
}