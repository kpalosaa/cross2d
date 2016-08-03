using System;
using System.Collections.Generic;
using System.Collections;

namespace Cross2D
{
	internal class BiDictionary<T1, T2> : IEnumerable
	{
		private Dictionary<T1, T2> d1 = new Dictionary<T1, T2>();
		private Dictionary<T2, T1> d2 = new Dictionary<T2, T1>();

		public void Add(T1 v1, T2 v2)
		{
			d1.Add(v1, v2);
			d2.Add(v2, v1);
		}

		public T1 this[T2 i]
		{
			get
			{
				return d2[i];
			}
			set
			{
				d2[i] = value;
				d1[value] = i;
			}
		}

		public T2 this[T1 i]
		{
			get
			{
				return d1[i];
			}
			set
			{
				d1[i] = value;
				d2[value] = i;
			}
		}

		public bool TryGetValue(T1 key, out T2 value)
		{
			return d1.TryGetValue(key, out value);
		}

		public bool TryGetValue(T2 key, out T1 value)
		{
			return d2.TryGetValue(key, out value);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			yield break;
		}
	}
}
