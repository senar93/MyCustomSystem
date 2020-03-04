namespace MyCustomSystem.Variables.Data
{
	using MyCustomSystem.Variables.Abstract;
	using MyCustomSystem.Variables.Interface;
	using UnityEngine;

	[CreateAssetMenu(menuName = "MyCustomSystem/Variables/Int/StandardVar",
					 fileName = "_NEW_Standard_Int")]
	public class StandardIntVariable_So : AbsStandardVariable_So<int>, IHaveValue<int>
	{

	}
}