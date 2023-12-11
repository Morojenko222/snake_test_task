using UnityEngine;

namespace Factories
{
	public class ObjectFactory<T> where T : MonoBehaviour
	{
		private T _prefab;
		
		public ObjectFactory (T prefab)
		{
			_prefab = prefab;
		}

		public T Create (Vector3 position, Transform parent)
		{
			return Object.Instantiate(_prefab, position, Quaternion.identity, parent);
		}
	}
}