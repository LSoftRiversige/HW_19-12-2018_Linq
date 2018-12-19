using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomwWork_Linq
{
    // дописать Except, Intersect, Union, Distinct, First
    // ElementAt, Last, Single, * OrDefault, Any, All, Contains,
    // Concat, Reverse, SequenceEqual, DefaultIfEmpty, OfType,
    // Cast, ToArray, ToList, ToDictionary

    public static class MyEnumerable
    {
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            foreach (var valFirst in first)
            {
                if (!second.Contains(valFirst))
                {
                    yield return valFirst;
                }
            }
        }

        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            foreach (var val1 in first)
            {
                foreach (var val2 in second)
                {
                    if (val1.Equals(val2))
                    {
                        yield return val1;
                    }
                }
            }
        }

        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            foreach (var val in first)
            {
                yield return val;
            }

            foreach (var val in second)
            {
                yield return val;
            }
        }

        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
        {
            var unique = new HashSet<TSource>();
            foreach (var val in source)
            {
                if (!unique.Contains(val))
                {
                    unique.Add(val);
                    yield return val;
                }
            }
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source)
        {
            foreach (var val in source)
            {
                return val;
            }
            throw new InvalidOperationException();
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var val in source)
            {
                if (predicate(val))
                {
                    return val;
                }
            }
            throw new InvalidOperationException();
        }

        public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index)
        {
            int i = 0;
            foreach (var val in source)
            {
                if (i == index)
                {
                    return val;
                }
                i++;
            }
            throw new ArgumentOutOfRangeException();
        }

        public static TSource Single<TSource>(this IEnumerable<TSource> source)
        {
            bool first = true;
            TSource element=default(TSource);
            foreach (var val in source)
            {
                if (first)
                {
                    first = false;
                    element = val;
                }
                else
                {
                    throw new InvalidOperationException("more than one");
                }
            }
            return element;
        }

        public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            bool first = true;
            TSource element = default(TSource);
            foreach (var val in source)
            {
                if (predicate(val))
                {
                    if (first)
                    {
                        first = false;
                        element = val;
                    }
                    else
                    {
                        throw new InvalidOperationException("more than one");
                    }
                }
            }
            return element;
        }

        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            bool first = true;
            TSource element = default(TSource);
            foreach (var val in source)
            {
                if (predicate(val))
                {
                    if (first)
                    {
                        first = false;
                        element = val;
                    }
                    else
                    {
                        element = default(TSource);
                        break;
                    }
                }
            }
            return element;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source)
        {
            foreach (var val in source)
            {
                return true;
            }
            return false;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var val in source)
            {
                if (!predicate(val))
                {
                    return false;
                }
            }
            return true;
        }

        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            foreach (var val in first)
            {
                yield return val;
            }
            foreach (var val in second)
            {
                yield return val;
            }
        }

        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            foreach (var val in source)
            {
                if (val.Equals(value))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
        {
            foreach (var val in source)
            {
                if (comparer.Equals(val, value))
                {
                    return true;
                }
            }
            return false;
        }

        public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
        {
            List<TSource> buf = new List<TSource>();
            foreach (var val in source)
            {
                buf.Add(val);
            }
            for (int i = buf.Count-1; i > 0; i--)
            {
                yield return buf[i];
            }
        }

        public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            int i = 0;
            foreach (var val1 in first)
            {
                if (!(bool)second.ElementAtOrDefault(i++)?.Equals(val1))
                {
                    return false;
                }
            }
            return true;
        }

        public static IEnumerable<TResult> OfType<TResult>(this IEnumerable source)
        {
            foreach (var val in source)
            {
                if (val is TResult)
                {
                    yield return (TResult)val;
                }
            }
        }

        public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source)
        {
            foreach (var val in source)
            {
                yield return (TResult)val;
            }
        }

        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
        {
            var buf = new List<TSource>();
            foreach (var val in source)
            {
                buf.Add(val);
            }
            return buf.ToArray();
        }

        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
        {
            var buf = new List<TSource>();
            foreach (var val in source)
            {
                buf.Add(val);
            }
            return buf;
        }

        public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var dict = new Dictionary<TKey, TSource>();
            foreach (var val in source)
            {
                TKey key = keySelector(val);
                if (!dict.TryAdd(key, val))
                {
                    dict[key] = val;
                }
            }
            return dict;
        }
    }
}