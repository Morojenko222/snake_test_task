using UnityEngine;
using UnityEngine.UI;

namespace Views
{
	public class MapTileView : MonoBehaviour
	{
		public RectTransform RectTf => _rectTf;
		public Image Image => _image;

		[SerializeField] private RectTransform _rectTf;
		[SerializeField] private Image _image;
	}
}