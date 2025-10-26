using TMPro;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

		private void ApplyVisuals(float v, bool includeSlider)
		{
			if (includeSlider && slider)
				slider.SetValueWithoutNotify(v);

			if (fillImage)
				fillImage.color = gradient.Evaluate(v);

			if (text)
			{
				float hp = Mathf.Lerp(range.min, range.max, v);
				if (showMaxHealth)
					text.text = $"{(int)hp} / {range.max}";
				else
					text.text = $"{(int)hp}";
			}
		}

		public float Value
		{
			get => value;
			set
			{
				this.value = value;
				ApplyVisuals(value, includeSlider: true);
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
			// Clamp and update non-Slider visuals immediately (safe during validation)
			value = Mathf.Clamp01(value);
			ApplyVisuals(value, includeSlider: false);

			// Defer Slider update until after validation to avoid SendMessage errors
			#if UNITY_EDITOR
			if (!EditorApplication.isPlayingOrWillChangePlaymode)
			{
				EditorApplication.delayCall += () =>
				{
					if (this == null) return; // object might have been destroyed
					ApplyVisuals(value, includeSlider: true);
				};
			}
			#endif
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