using System;
using System.Collections;
using UnityEngine;

namespace LastOneOut
{
	public class Timer : SingletonObject<Timer>
	{
		public void Once(float delay, Action action)
		{
			StartCoroutine(OnceEnumerator(delay, action));
		}

		public void Repeat(int loops, float delay, Action action)
		{
			StartCoroutine(RepeatEnumerator(loops, delay, action));
		}

		public void Repeat(int loops, float delay, Action<int> action)
		{
			StartCoroutine(RepeatEnumerator(loops, delay, action));
		}

		IEnumerator OnceEnumerator(float delay, Action action)
		{
			yield return new WaitForSeconds(delay);
			action.Invoke();
		}

		IEnumerator RepeatEnumerator(int loops, float delay, Action action)
		{
			var time = new WaitForSeconds(delay);
			for (int i = 0; i < loops; i++)
			{
				action.Invoke();
				yield return time;
			}
		}

		IEnumerator RepeatEnumerator(int loops, float delay, Action<int> action)
		{
			var time = new WaitForSeconds(delay);
			for (int i = 0; i < loops; i++)
			{
				action.Invoke(i);
				yield return time;
			}
		}
	}
}