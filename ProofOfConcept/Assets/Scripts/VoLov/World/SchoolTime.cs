using UnityEngine;
using Sulvic.Unity;

namespace Sulvic.VoLov.World{

	public class SchoolTime: MonoBehaviour{

		private WorldTime worldTime;

		private void OnEnable(){
			if(worldTime == null) worldTime = UnityHelper.GetComponent<WorldTime>("Directional Light");
			worldTime.TimeEvent += UpdateTime;
		}

		private void OnDisable(){
			worldTime.TimeEvent -= UpdateTime;
		}

		private void Start(){
			
		}

        private void UpdateTime(int hour, int minute){
            Debug.Log($"{hour}:{minute}");
        }

	}

}