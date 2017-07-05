using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace LastOneOut
{
	public class DependencyContainer : ObjectSingleton<DependencyContainer>
	{
		static BindingFlags bindingAttr = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy;

		private Dictionary<Type, System.Object> dependenciesMap = new Dictionary<Type, System.Object>();

		bool bindCompleted;
		
		public void ClearBindings()
		{
			dependenciesMap = new Dictionary<Type, System.Object>();
			bindCompleted = false;
		}

		public void Bind<T>(System.Object injectable)
		{
			if (bindCompleted)
				throw new Exception("Binding has already been completed");

			if (dependenciesMap.ContainsKey(typeof(T)))
				Debug.LogWarning("Already containts dependency bound on that key : " + typeof(T));

			dependenciesMap[typeof(T)] = injectable;
		}

		public void BindCompleted()
		{
			foreach (System.Object obj in dependenciesMap.Values)
			{
				if (obj is IInjectable)
					((IInjectable)obj).BindCompleted();
			}

			bindCompleted = true;
		}

		public void Inject(object target)
		{
			Type type = target.GetType();

			IEnumerable<FieldInfo> fields = type.GetFields(bindingAttr).Where(field => field.IsDefined(typeof(Inject), false));

			foreach (FieldInfo field in fields)
			{
				System.Object value;
				if (dependenciesMap.TryGetValue(field.FieldType, out value))
					field.SetValue(target, value);
				else
					throw new Exception("Type not found : " + field.FieldType);
			}

		}

		public T Find<T>()
		{
			System.Object value;
			if (!dependenciesMap.TryGetValue(typeof(T), out value))
				Debug.LogWarning("Type not found: " + typeof(T));

			return (T)value;
		}


		public Dictionary<Type, System.Object> DependenciesMap
		{
			get
			{
				return dependenciesMap;
			}
		}
	}
}