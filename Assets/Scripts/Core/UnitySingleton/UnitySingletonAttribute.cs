using System;

public class UnitySingletonAttribute : Attribute {

	public string Path { get; private set; }

	public UnitySingletonAttribute(string path)
	{
		Path = path;
	}
}
