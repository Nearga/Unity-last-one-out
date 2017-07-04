using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringExtensions
{
	public static string EnumSplit(this string str)
	{
		string upper = str.ToUpper();
		bool hasMoved = false;
		bool multiples = false;
		for (int i = str.Length - 1; i > 0; i--)
		{
			if ((char)str[i] == (char)upper[i])
			{
				if (hasMoved == false)
				{
					hasMoved = true;
					str = str.Insert(i, " ");
				}
				else
					multiples = true;
			}
			else
			{
				hasMoved = false;
				if (multiples)
				{
					str = str.Insert(i + 1, " ");
					multiples = false;
				}
			}
		}

		return str;
	}
}
