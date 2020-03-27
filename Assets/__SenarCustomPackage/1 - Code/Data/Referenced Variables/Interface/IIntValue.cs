namespace MyCustomSystem.Variables.Interface
{
	public interface IIntValue
	{
		bool CanSetValue { get; }
		int IntValue { get; set; }
	}
}