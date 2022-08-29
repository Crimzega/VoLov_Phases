namespace Sulvic.VoLov.Characters{

	using System;
	using UnityEngine;

	public abstract class CharacterBase: MonoBehaviour{}

	public abstract class FacultyBase: CharacterBase{}

	public abstract class RivalBase: StudentBase{}

	public abstract class StudentBase: CharacterBase{}

}