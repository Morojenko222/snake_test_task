using UnityEngine;

namespace Views
{
	public class SceneView : MonoBehaviour
	{
		public Transform MapCenterPoint => _mapCenterPoint;
		public CoroutinesHelper CoroutinesHelper => _coroutinesHelper;

		[SerializeField] private Transform _mapCenterPoint;
		[SerializeField] private CoroutinesHelper _coroutinesHelper;
	}
}