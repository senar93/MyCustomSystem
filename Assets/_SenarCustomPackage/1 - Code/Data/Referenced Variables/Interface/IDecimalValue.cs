﻿namespace SenarCustomSystem.Variables.Interface
{
	public interface IDecimalValue
	{
		bool CanSetValue { get; }
		decimal DecimalValue { get; set; }
	}
}