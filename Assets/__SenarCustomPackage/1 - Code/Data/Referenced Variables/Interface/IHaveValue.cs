namespace MyCustomSystem.Variables.Interface
{
	public interface IHaveValue<T>
	{
		T Value { get; set; }
		bool CanSetValue { get; }

	}
}