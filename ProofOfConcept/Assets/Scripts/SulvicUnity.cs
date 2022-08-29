namespace Sulvic.Unity{

	using System.Linq;
	using System.Collections.Generic;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;
	using UnityEngine;
	using Sulvic.Lib;
	using Sulvic.Util;

	public static class DictionaryExt{

		public static XmlDictionary<K, V> AsXmlDictionary<K, V>(this Dictionary<K, V> dict){
			XmlDictionary<K, V> xmlDict = new XmlDictionary<K, V>();
			foreach(KeyValuePair<K, V> pair in dict) xmlDict.Add(pair.Key, pair.Value);
			dict.Clear();
			return xmlDict;
		}

	}

	public class UnityHelper{

		private static void SetActive(bool active, params GameObject[] objects){
			foreach(GameObject @object in objects) if (@object != null) @object.SetActive(active);
		}

		private static Transform GetTransform(Component component) => component.transform;

		public static void Activate(params GameObject[] objects) => SetActive(true, objects);

		public static void ActivateRange(GameObject[] objects, int start, int end) => Activate(SulvicCollections.SubArray(objects, start, end));

		public static void Deactivate(params GameObject[] objects) => SetActive(false, objects);

		public static void DeactivateRange(GameObject[] objects, int start, int end) => Deactivate(SulvicCollections.SubArray(objects, start, end));

		public static T Instantiate<T>(T obj, MonoBehaviour behaviour)
			where T: Object => Object.Instantiate<T>(obj, GetPosition(behaviour), Quaternion.identity);

		public static T Instantiate<T>(T obj, Transform transform)
			where T: Object => Object.Instantiate(obj, GetPosition(transform), Quaternion.identity);

		public static T GetComponent<T>(string name)
			where T: Object => GameObject.Find(name).GetComponent<T>();

		public static T FindInParent<T>(Component component)
			where T: Component => component != null? component.GetComponentInParent<T>(): (T)null;

		public static Quaternion GetLocalRotation(Component component) => GetLocalRotation(GetTransform(component.transform));

		public static Quaternion GetLocalRotation(Transform transform) => transform.localRotation;

		public static Quaternion GetRotation(Component component) => GetRotation(GetTransform(component));

		public static Quaternion GetRotation(Transform transform) => transform.rotation;

		public static Vector3 GetLocalPosition(Component component) => GetLocalPosition(GetTransform(component));

		public static Vector3 GetLocalPosition(Transform transform) => transform.localPosition;

		public static Vector3 GetLocalScale(Component component) => component.transform.localScale;

		public static Vector3 GetPosition(Component component) => GetPosition(GetTransform(component));

		public static Vector3 GetPosition(Transform transform) => transform.position;

		public static Vector3 MoveTowards(Transform from, Transform to, Vector3 offset) =>
			Vector3.MoveTowards(GetPosition(from), GetPosition(to) + offset, Time.deltaTime);

	}

	[XmlRoot("XmlSerialiable")]
	public class XmlDictionary<K, V>: Dictionary<K, V>{

		private readonly XmlSerializer KEY_SERIAL = new XmlSerialiable(typeof(K)), VALUE_SERIAL = new XmlSerializer(typeof(V));

		private void ReadXml(XmlReader reader){
			Clear();
			
		}

		private void WriteXml(XmlWriter writer){
			foreach(KeyValuePair<K, V> pair in this){
				KEY_SERIAL.Deserialize(writer, pair.Key);
				VALUE_SERIAL.Deserialize(writer, pair.Value);
			}
		}

		private XmlSchema GetSchema() => null;

	}

}
