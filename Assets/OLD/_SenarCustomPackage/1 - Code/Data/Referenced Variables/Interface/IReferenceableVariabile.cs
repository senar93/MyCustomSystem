namespace OLD_SenarCustomSystem.Variables.Interface
{
	/// <summary>
	/// everyone that implement this interface are variabile and can be used for reference in NumericReference
	/// </summary>
	/// <typeparam name="T">type of variabile</typeparam>
	public interface IReferenceableVariabile<T>
	{
		T Value { get; set; }
		bool CanSetValue { get; }

	}
}