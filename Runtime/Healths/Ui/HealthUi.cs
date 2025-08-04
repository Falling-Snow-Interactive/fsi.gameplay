using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fsi.Gameplay.Healths.Ui
{
	public class HealthUi : MonoBehaviour
	{
		[SerializeField]
		private Health health;

		[Range(0, 1)]
		[SerializeField]
		private float value = 0.7f;

		[SerializeField]
		private RangeInt range = new(0, 10);

		[SerializeField]
		private Gradient gradient = new();

		[SerializeField]
		private bool showMaxHealth;

		[Header("Ui References")]
		[SerializeField]
		private Slider slider;

		[SerializeField]
		private Image backgroundImage;

		[SerializeField]
		private Image fillImage;

		[SerializeField]
		private TMP_Text text;

		public float Value
		{
			get => value;
			set
			{
				this.value = value;
				if (slider) slider.value = value;

				if (fillImage) fillImage.color = gradient.Evaluate(value);

				if (text)
				{
					float hp = Mathf.Lerp(range.min, range.max, value);
					if (showMaxHealth)
						text.text = $"{(int)hp} / {range.max}";
					else
						text.text = $"{(int)hp}";
				}
			}
		}

		private void OnEnable()
		{
			if (health != null) health.Changed += OnHealthChanged;
		}

		private void OnDisable()
		{
			if (health != null) health.Changed -= OnHealthChanged;
		}

		private void OnValidate()
		{
			Value = value;
		}

		public void Initialize(Health health)
		{
			this.health = health;

			health.Changed += OnHealthChanged;

			range.min = 0;
			range.max = health.max;

			Value = health.Normalized;
		}

		private void OnHealthChanged()
		{
			range.max = health.max;
			Value = health.Normalized;
		}
	}
}