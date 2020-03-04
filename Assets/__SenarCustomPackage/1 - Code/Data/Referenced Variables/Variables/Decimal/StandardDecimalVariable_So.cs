namespace MyCustomSystem.Variables.Data
{
    using MyCustomSystem.Variables.Abstract;
    using MyCustomSystem.Variables.Interface;
    using UnityEngine;

	[CreateAssetMenu(menuName = "MyCustomSystem/Variables/Decimal/StandardVar",
					 fileName = "_NEW_Standard_Decimal")]
	public class StandardDecimalVariable_So : AbsStandardVariable_So<decimal>, IHaveValue<decimal>
	{

	}
}