namespace SenarCustomSystem.Variables.Interface
{
	public interface ILongValue
	{
		bool CanSetValue { get; }
		long LongValue { get; set; }
	}
}