﻿namespace OLD_SenarCustomSystem.Variables.Interface
{
	public interface IDoubleValue
	{
		bool CanSetValue { get; }
		double DoubleValue { get; set; }
	}
}