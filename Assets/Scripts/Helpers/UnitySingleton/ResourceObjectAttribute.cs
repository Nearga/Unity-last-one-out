using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceObjectAttribute : Attribute {

	public string Path { get; private set; }

	public ResourceObjectAttribute(string path)
	{
		Path = path;
	}
}
