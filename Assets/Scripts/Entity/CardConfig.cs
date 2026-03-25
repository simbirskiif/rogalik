using UnityEngine;

namespace Entity
{
    public class CardConfig : ScriptableObject
    {
        [SerializeField] float rowEffect1 = 3;

        public float RowEffect1 => rowEffect1;

        public float RowEffect2 => rowEffect2;

        public float FrenselPower => frenselPower;

        public float HoloTilling => holoTilling;

        public float HoloIntensity => holoIntensity;

        public float TransitionPower => transitionPower;

        [SerializeField] float rowEffect2 = 4;
        [Header("")] [SerializeField] float frenselPower = 0.65f;
        [Header("")] [SerializeField] float holoTilling = 2f;
        [SerializeField] float holoIntensity = 1f;
        [SerializeField] float transitionPower = 1f;

        public CardConfig()
        {
        }

        public CardConfig(float rowEffect1, float rowEffect2, float frenselPower, float holoTilling,
            float holoIntensity, float transitionPower)
        {
            this.rowEffect1 = rowEffect1;
            this.rowEffect2 = rowEffect2;
            this.frenselPower = frenselPower;
            this.holoTilling = holoTilling;
            this.holoIntensity = holoIntensity;
            this.transitionPower = transitionPower;
        }
    }
}