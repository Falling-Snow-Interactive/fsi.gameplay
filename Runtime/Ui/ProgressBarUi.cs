using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Fsi.Gameplay.Ui
{
	public class ProgressBarUi : MonoBehaviour
	{
		[Range(0, 1)]
		[SerializeField]
		private float value = 0.7f;

		[SerializeField]
		private RangeInt range = new(0, 10);

		[SerializeField]
		private Gradient gradient = new();

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

				if (fillImage)
				{
					fillImage.color = gradient.Evaluate(value);
					fillImage.fillAmount = value;
				}

				if (text)
				{
					float hp = Mathf.Lerp(range.min, range.max, value);
					text.text = $"{(int)hp}/{range.max}";
				}
			}
		}

		public RangeInt Range
		{
			get => range;
			set => range = value;
		}

		private void OnValidate()
		{
			Value = value;
		}
	}
}