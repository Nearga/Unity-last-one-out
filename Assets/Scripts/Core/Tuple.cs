public class Tuple<T1, T2> : System.IEquatable<Tuple<T1, T2>>
{
	public T1 First { get; private set; }
	public T2 Second { get; private set; }

	internal Tuple(T1 first, T2 second)
	{
		First = first;
		Second = second;
	}

	public override int GetHashCode()
	{
		return First.GetHashCode() ^ Second.GetHashCode();
	}

	public override bool Equals(object obj)
	{
		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}
		return Equals((Tuple<T1, T2>)obj);
	}

	public bool Equals(Tuple<T1, T2> other)
	{
		return other.First.Equals(First) && other.Second.Equals(Second);
	}
}

public static class Tuple
{
	public static Tuple<T1, T2> New<T1, T2>(T1 first, T2 second)
	{
		var tuple = new Tuple<T1, T2>(first, second);
		return tuple;
	}
}