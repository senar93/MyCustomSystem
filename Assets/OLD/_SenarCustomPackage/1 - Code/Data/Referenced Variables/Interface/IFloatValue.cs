namespace OLD_SenarCustomSystem.Variables.Interface
{
	public interface IFloatValue
	{
		bool CanSetValue { get; }
		float FloatValue { get; set; }
	}
}