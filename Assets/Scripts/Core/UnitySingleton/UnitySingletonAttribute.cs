using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitySingletonAttribute : Attribute {

	public string Path { get; private set; }

	public UnitySingletonAttribute(string path)
	{
		Path = path;
	}
}
