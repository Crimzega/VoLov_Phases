using UnityEngine;

namespace Sulvic.VoLov.World{

    public class WorldTime: MonoBehaviour{

		public event TimeDelegate TimeEvent;

		private int hour;
		private int minute;

		private void Start(){
			Debug.Log("Logging Check");
		}

		private void Update(){
			TimeEvent.Invoke(hour, minute);
		}

	}

}