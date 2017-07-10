﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;

public static class Texture2DExtensions
{
    public static Texture2D FlipTexture(Texture2D original)
    {
        Texture2D flipped = new Texture2D(original.width, original.height);

        int xN = original.width;
        int yN = original.height;


        for (int i = 0; i < xN; i++)
        {
            for (int j = 0; j < yN; j++)
            {
                flipped.SetPixel(xN - i - 1, j, original.GetPixel(i, j));
            }
        }
        flipped.Apply();

        return flipped;
    }
    public static bool isDifferentColor(this Texture2D image, int thisx, int thisy, int x, int y)
    {
        if (image.GetPixel(thisx, thisy) != image.GetPixel(x, y))
            return true;
        else
            return false;
    }
    public static void setColor(this Texture2D image, Color color)
    {
        for (int j = 0; j < image.height; j++) // cicle by province        
            for (int i = 0; i < image.width; i++)
                image.SetPixel(i, j, color);
    }

    public static void setAlphaToMax(this Texture2D image)
    {
        for (int j = 0; j < image.height; j++) // cicle by province        
            for (int i = 0; i < image.width; i++)
                // if (image.GetPixel(i, j) != Color.black)
                image.SetPixel(i, j, image.GetPixel(i, j).setAlphaToMax());
    }
    static void drawSpot(Texture2D image, int x, int y, Color color)
    {
        int straightBorderChance = 4;// 5;
        //if (x >= 0 && x < image.width && y >= 0 && y < image.height)

        if (image.coordinatesExist(x, y))
            if (Game.Random.Next(straightBorderChance) != 1)
                //if (image.GetPixel(x, y).a != 1f || image.GetPixel(x, y) == Color.black)
                if (image.GetPixel(x, y) == Color.black)
                    image.SetPixel(x, y, color.setAlphaToZero());
    }
    public static bool coordinatesExist(this Texture2D image, int x, int y)
    {
        return (x >= 0 && x < image.width && y >= 0 && y < image.height);
    }
    public static bool isRightTopCorner(this Texture2D image, int x, int y)
    {
        if (image.coordinatesExist(x + 1, y) && image.GetPixel(x + 1, y) != image.GetPixel(x, y)
            && image.coordinatesExist(x, y + 1) && image.GetPixel(x, y + 1) != image.GetPixel(x, y)
            && image.coordinatesExist(x, y - 1) && image.GetPixel(x, y - 1) == image.GetPixel(x, y)
            && image.coordinatesExist(x - 1, y) && image.GetPixel(x - 1, y) == image.GetPixel(x, y)
            )
            return true;
        else
            return false;
    }
    public static bool isRightBottomCorner(this Texture2D image, int x, int y)
    {
        if (image.coordinatesExist(x + 1, y) && image.GetPixel(x + 1, y) != image.GetPixel(x, y)
            && image.coordinatesExist(x, y - 1) && image.GetPixel(x, y - 1) != image.GetPixel(x, y)
            && image.coordinatesExist(x, y + 1) && image.GetPixel(x, y + 1) == image.GetPixel(x, y)
            && image.coordinatesExist(x - 1, y) && image.GetPixel(x - 1, y) == image.GetPixel(x, y)
            )
            return true;
        else
            return false;
    }
    public static bool isLeftTopCorner(this Texture2D image, int x, int y)
    {
        if (image.coordinatesExist(x - 1, y) && image.GetPixel(x - 1, y) != image.GetPixel(x, y)
            && image.coordinatesExist(x, y + 1) && image.GetPixel(x, y + 1) != image.GetPixel(x, y)
            && image.coordinatesExist(x, y - 1) && image.GetPixel(x, y - 1) == image.GetPixel(x, y)
            && image.coordinatesExist(x + 1, y) && image.GetPixel(x + 1, y) == image.GetPixel(x, y)
            )
            return true;
        else
            return false;
    }
    public static bool isLeftBottomCorner(this Texture2D image, int x, int y)
    {
        if (image.coordinatesExist(x - 1, y) && image.GetPixel(x - 1, y) != image.GetPixel(x, y)
            && image.coordinatesExist(x, y - 1) && image.GetPixel(x, y - 1) != image.GetPixel(x, y)
            && image.coordinatesExist(x, y + 1) && image.GetPixel(x, y + 1) == image.GetPixel(x, y)
            && image.coordinatesExist(x + 1, y) && image.GetPixel(x + 1, y) == image.GetPixel(x, y)
            )
            return true;
        else
            return false;
    }

    public static void drawRandomSpot(this Texture2D image, int x, int y, Color color)
    {
        //draw 4 points around x, y
        //int chance = 90;
        drawSpot(image, x - 1, y, color);
        drawSpot(image, x + 1, y, color);
        drawSpot(image, x, y - 1, color);
        drawSpot(image, x, y + 1, color);

    }
    public static int getRandomX(this Texture2D image)
    {
        return Game.Random.Next(0, image.width);
    }
    public static Color getRandomPixel(this Texture2D image)
    {
        return image.GetPixel(image.getRandomX(), image.getRandomY());
    }
    public static int getRandomY(this Texture2D image)
    {
        return Game.Random.Next(0, image.height);
    }
}
public static class CollectionExtensions
{
    public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Action<TKey, TValue> invokeMe)
    {
        foreach (var keyValue in dictionary)
        {
            invokeMe(keyValue.Key, keyValue.Value);
        }
    }
    //public static void AddMy(this Dictionary<object, int> dictionary, object what, int size)
    //{
    //    if (dictionary.ContainsKey(what))
    //        dictionary[what] += size;
    //    else
    //        dictionary.Add(what, size);
    //}
    public static void AddMy<T>(this Dictionary<T, int> dictionary, T what, int size)
    {
        if (dictionary.ContainsKey(what))
            dictionary[what] += size;
        else
            dictionary.Add(what, size);
    }
    public static void move<T>(this List<T> source, T item, List<T> destination)
    {
        if (source.Remove(item)) // don't remove this
            destination.Add(item);

    }


    public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
    {
        return source.MinBy(selector, null);
    }
    public static void FindAndDo<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, Action<TSource> action)
    {
        foreach (var item in source.Where(predicate))
            action(item);
    }
    public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> selector, IComparer<TKey> comparer)
    {
        //if (source == null) throw new ArgumentNullException("source");
        //if (selector == null) throw new ArgumentNullException("selector");
        //todo fix exception
        comparer = comparer ?? Comparer<TKey>.Default;

        using (var sourceIterator = source.GetEnumerator())
        {
            if (!sourceIterator.MoveNext())
            {
                //throw new InvalidOperationException("Sequence contains no elements");
                return default(TSource);
            }
            var min = sourceIterator.Current;
            var minKey = selector(min);
            while (sourceIterator.MoveNext())
            {
                var candidate = sourceIterator.Current;
                var candidateProjected = selector(candidate);
                if (comparer.Compare(candidateProjected, minKey) < 0)
                {
                    min = candidate;
                    minKey = candidateProjected;
                }
            }
            return min;
        }
    }
    public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
           Func<TSource, TKey> selector)
    {
        return source.MaxBy(selector, null);
    }
    //public static string ToString<TSource, TKey>(this IEnumerable<TSource> source,
    //       Func<TSource, TKey> selector)
    //{
    //    return source.MaxBy(selector, null).ToString();
    //}

    /// <summary>
    /// Returns the maximal element of the given sequence, based on
    /// the given projection and the specified comparer for projected values. 
    /// </summary>
    /// <remarks>
    /// If more than one element has the maximal projected value, the first
    /// one encountered will be returned. This operator uses immediate execution, but
    /// only buffers a single result (the current maximal element).
    /// </remarks>
    /// <typeparam name="TSource">Type of the source sequence</typeparam>
    /// <typeparam name="TKey">Type of the projected element</typeparam>
    /// <param name="source">Source sequence</param>
    /// <param name="selector">Selector to use to pick the results to compare</param>
    /// <param name="comparer">Comparer to use to compare projected values</param>
    /// <returns>The maximal element, according to the projection.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/>, <paramref name="selector"/> 
    /// or <paramref name="comparer"/> is null</exception>
    /// <exception cref="InvalidOperationException"><paramref name="source"/> is empty</exception>

    public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> selector, IComparer<TKey> comparer)
    {
        //if (source == null) throw new ArgumentNullException(nameof(source)); //todo fix exception
        //if (selector == null) throw new ArgumentNullException(nameof(selector)); 
        //todo fix exception
        comparer = comparer ?? Comparer<TKey>.Default;

        using (var sourceIterator = source.GetEnumerator())
        {
            if (!sourceIterator.MoveNext())
            {
                return default(TSource);
            }
            var max = sourceIterator.Current;
            var maxKey = selector(max);
            while (sourceIterator.MoveNext())
            {
                var candidate = sourceIterator.Current;
                var candidateProjected = selector(candidate);
                if (comparer.Compare(candidateProjected, maxKey) > 0)
                {
                    max = candidate;
                    maxKey = candidateProjected;
                }
            }
            return max;
        }
    }
    public static TSource MaxByRandom<TSource, TKey>(this IEnumerable<TSource> source,
        Func<TSource, TKey> selector)
    {
        IComparer<TKey> comparer = Comparer<TKey>.Default;
        TKey maxKey;
        using (var sourceIterator = source.GetEnumerator())
        {
            if (!sourceIterator.MoveNext())
            {
                return default(TSource);
            }
            var max = sourceIterator.Current;
            maxKey = selector(max);
            while (sourceIterator.MoveNext())
            {
                var candidate = sourceIterator.Current;
                var candidateProjected = selector(candidate);
                if (comparer.Compare(candidateProjected, maxKey) > 0)
                {
                    max = candidate;
                    maxKey = candidateProjected;
                }
            }
        }
        var reslist = new List<TSource>();
        //var foundMaximum = source.MaxBy(selector);
        using (var sourceIterator = source.GetEnumerator())
        {
            if (!sourceIterator.MoveNext())
            {
                return default(TSource);
            }
            do
            {
                var candidate = sourceIterator.Current;
                var candidateProjected = selector(candidate);
                if (maxKey.Equals(candidateProjected))
                    reslist.Add(candidate);
            }
            while (sourceIterator.MoveNext());
        }
        return reslist.PickRandom();
    }
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    //public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
    //{
    //    if (target == null)
    //        throw new ArgumentNullException("target");
    //    if (source == null)
    //        throw new ArgumentNullException("source");
    //    foreach (var element in source)
    //        //if (target)
    //        target.Add(element);
    //}
    /// <summary>
    /// returns default(T) if fails
    /// </summary>    
    public static T PickRandom<T>(this List<T> source)
    {
        //return source.ElementAt(Game.random.Next(source.Count));
        if (source == null || source.Count == 0)
            return default(T);
        return source[Game.Random.Next(source.Count)];

    }
    /// <summary>
    ///returns an empty List<T> if didn't find anything
    /// </summary>    
    public static T PickRandom<T>(this List<T> source, Predicate<T> predicate)
    {
        return source.FindAll(predicate).PickRandom();
        //return source.ElementAt(Game.random.Next(source.Count));

    }
    public static string getString(this List<Storage> list, string lineBreaker)
    {
        if (list.Count > 0)
        {
            var sb = new StringBuilder();
            bool isFirstRow = true;
            bool haveAnyNonZeroItem = false;
            foreach (var item in list)
                if (item.isExist())
                {
                    haveAnyNonZeroItem = true;
                    if (!isFirstRow)
                    {
                        sb.Append(lineBreaker);
                    }
                    isFirstRow = false;
                    sb.Append(item);
                }
            if (haveAnyNonZeroItem)
                return sb.ToString();
            else
                return "none";
        }
        else
            return "none";
    }

    public static string getString<TValue>(this List<TValue> list, string lineBreaker)
    {
        if (list.Count > 0)
        {
            var sb = new StringBuilder();
            bool isFirstRow = true;
            foreach (var item in list)
            {
                if (!isFirstRow)
                {
                    sb.Append(lineBreaker);
                }
                isFirstRow = false;
                sb.Append(item);
            }
            return sb.ToString();
        }
        else
            return "none";
    }
    public static string getString<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, string intermediateString, string lineBreaker)
    {
        var sb = new StringBuilder();
        bool isFirstRow = true;
        foreach (var item in dictionary)
        {
            if (!isFirstRow)
            {
                sb.Append(lineBreaker);
            }
            isFirstRow = false;
            sb.Append(item.Key).Append(intermediateString).Append(item.Value);
        }
        return sb.ToString();
    }
    public static string getString(Dictionary<Mod, DateTime> dictionary)
    {
        var sb = new StringBuilder();
        bool isFirstRow = true;
        foreach (var item in dictionary)
        {
            if (!isFirstRow)
            {
                sb.Append("\n");
            }
            isFirstRow = false;
            if (item.Value == default(DateTime))
                sb.Append(item.Key).Append(" (permanent)");
            else
                sb.Append(item.Key).Append(" expires in ").Append(item.Value.getYearsUntill()).Append(" years");



        }
        return sb.ToString();
    }

    public static void RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dic,
        Func<TKey, TValue, bool> predicate)
    {
        var keys = dic.Keys.Where(k => predicate(k, dic[k])).ToList();
        foreach (var key in keys)
        {
            dic.Remove(key);
        }
    }

}
public static class ColorExtensions
{
    public static Color getNegative(this Color color)
    {
        return new Color(1.0f - color.r, 1.0f - color.g, 1.0f - color.b);
    }

    public static Color getRandomColor()
    {
        return new Color((float)Game.Random.NextDouble(), (float)Game.Random.NextDouble(), (float)Game.Random.NextDouble(), 1f);
    }
    public static Color setAlphaToZero(this Color color)
    {
        color.a = 0f;
        return color;
    }
    public static Color getAlmostSameColor(this Color color)
    {
        float maxDeviation = 0.02f;//not including

        var result = new Color();
        float deviation = maxDeviation - Game.Random.getFloat(0f, maxDeviation * 2);
        result.r = color.r + deviation;
        result.g = color.g + deviation;
        result.b = color.b + deviation;


        return result;
    }
    public static bool isSameColorsWithoutAlpha(this Color colorA, Color colorB)
    {
        if (colorA.b == colorB.b && colorA.g == colorB.g && colorA.r == colorB.r)
            return true;
        else
            return false;

    }
    public static Color setAlphaToMax(this Color color)
    {
        color.a = 1f;
        return color;
    }
}
public static class MeshExtensions
{
    public static bool hasDuplicateOfEdge(this Mesh mesh, int pointA, int pointB)
    {
        //getAllTriangles
        //    getAlledge
        //        check every Edge with a- b
        int foundEdgeDuplicates = 0;
        for (int i = 0; i < mesh.triangles.Count(); i += 3)
        {
            if (mesh.isSameEdge(pointA, pointB, mesh.triangles[i + 0], mesh.triangles[i + 1]))
                foundEdgeDuplicates++;
            if (mesh.isSameEdge(pointA, pointB, mesh.triangles[i + 1], mesh.triangles[i + 2]))
                foundEdgeDuplicates++;
            if (mesh.isSameEdge(pointA, pointB, mesh.triangles[i + 2], mesh.triangles[i + 0]))
                foundEdgeDuplicates++;
            if (foundEdgeDuplicates > 1) // 1 is this edge itself
                return true;
        }
        return false;
    }
    public static List<EdgeHelpers.Edge> getBorders(this Mesh mesh, List<EdgeHelpers.Edge> edges)
    {
        List<EdgeHelpers.Edge> res = new List<EdgeHelpers.Edge>();
        foreach (var checkingEdge in edges)
        {
            //if (!mesh.hasDuplicateOfEdge(item.v1, item.v2))
            // check only in edges!
            // need vector by vector comprasion
            int foundDuplicates = 0;
            foreach (var comparingEdge in edges)
            {
                //if (checkingEdge == comparingEdge)
                if (mesh.isSameEdge(checkingEdge.v1, checkingEdge.v2, comparingEdge.v1, comparingEdge.v2))
                {
                    foundDuplicates++;
                    if (foundDuplicates > 1) // 1 - is edge itself
                        break;
                }
            }
            if (foundDuplicates < 2)
                res.Add(checkingEdge);
        }
        return res;
    }

    public static int isAnyPointOnLine(this Mesh mesh, Vector3 a, Vector3 b)
    {
        int result = -1;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            if (isPointLiesOnLine(mesh.vertices[i], a, b))
            {
                result = i;
                break;
            }
        }
        return result;
    }
    public static bool isSameEdge(this Mesh mesh, int a, int b, int c, int d)
    {
        //if ( (mesh.vertices[a] == mesh.vertices[c] && mesh.vertices[b] == mesh.vertices[d])
        //    || (mesh.vertices[a] == mesh.vertices[c] && mesh.vertices[d] == mesh.vertices[b]))
        if ((a == c && b == d)
            || (a == d && b == c)
            || isTwoLinesTouchEachOther(mesh.vertices[a], mesh.vertices[b], mesh.vertices[c], mesh.vertices[d]))
        {
            //var point = mesh.isAnyPointOnLine(mesh.vertices[c], mesh.vertices[d]);
            //if (point > 0)
            //{
            //    vertexNumbers.Add(a);
            //    vertexNumbers.Add(point);
            //}
            //if (
            //    isPointLiesOnLine(mesh.vertices[a], mesh.vertices[c], mesh.vertices[d])
            //   //|| isPointLiesOnLine(mesh.vertices[c], mesh.vertices[a], mesh.vertices[b]))
            //   && !(isPointLiesOnLine(mesh.vertices[a], mesh.vertices[c], mesh.vertices[d]) && isPointLiesOnLine(mesh.vertices[b], mesh.vertices[c], mesh.vertices[d])))
            ////isTwoLinesTouchEachOther(mesh.vertices[a], mesh.vertices[d], mesh.vertices[a], mesh.vertices[c]))
            //{
            //    vertexNumbers.Add(a);
            //    vertexNumbers.Add(d);
            //}
            //else
            //if (isPointLiesOnLine(mesh.vertices[b], mesh.vertices[c], mesh.vertices[d])
            //   && !(isPointLiesOnLine(mesh.vertices[b], mesh.vertices[c], mesh.vertices[d]) && isPointLiesOnLine(mesh.vertices[a], mesh.vertices[c], mesh.vertices[d])))
            ////isTwoLinesTouchEachOther(mesh.vertices[a], mesh.vertices[d], mesh.vertices[a], mesh.vertices[c]))
            //{
            //    vertexNumbers.Add(b);
            //    vertexNumbers.Add(c);
            //}

            //if (isPointLiesOnLine(mesh.vertices[c], mesh.vertices[a], mesh.vertices[b])
            //  && !(isPointLiesOnLine(mesh.vertices[b], mesh.vertices[c], mesh.vertices[d]) && isPointLiesOnLine(mesh.vertices[a], mesh.vertices[c], mesh.vertices[d])))
            ////isTwoLinesTouchEachOther(mesh.vertices[a], mesh.vertices[d], mesh.vertices[a], mesh.vertices[c]))
            //{
            //    vertexNumbers.Add(c);
            //    vertexNumbers.Add(a);
            //}
            return true;
        }
        else
        {

            return false;
        }
    }
    //public static List<int> getPerimeterVertexNumbers2(this Mesh mesh)
    //{
    //    //for (int i = 0; i < mesh.triangles.Count(); i += 6)

    //}
    public static List<int> getPerimeterVertexNumbers(this Mesh mesh)
    {
        List<int> vertexNumbers = new List<int>();
        for (int i = 0; i < mesh.triangles.Count(); i += 6)
        //for (int i = 0; i < 17; i += 6)
        //int i = 0;        
        {
            if (!mesh.hasDuplicateOfEdge(
            mesh.triangles[i + 5],
            mesh.triangles[i + 1]))
            {
                vertexNumbers.Add(mesh.triangles[i + 5]);
                vertexNumbers.Add(mesh.triangles[i + 1]);
            }


            if (!mesh.hasDuplicateOfEdge(
            mesh.triangles[i + 1],
            mesh.triangles[i + 2]))
            {
                vertexNumbers.Add(mesh.triangles[i + 1]);
                vertexNumbers.Add(mesh.triangles[i + 2]);
            }

            if (!mesh.hasDuplicateOfEdge(
            mesh.triangles[i + 2],
            mesh.triangles[i + 0]))
            {
                vertexNumbers.Add(mesh.triangles[i + 2]);
                vertexNumbers.Add(mesh.triangles[i + 0]);
            }


            if (!mesh.hasDuplicateOfEdge(
           mesh.triangles[i + 0],
           mesh.triangles[i + 5]))
            {
                vertexNumbers.Add(mesh.triangles[i + 0]);
                vertexNumbers.Add(mesh.triangles[i + 5]);
            }


        }
        //List<int> realVertexNumbers = new List<int>();
        //for (int i = 1; i < vertexNumbers.Count - 1; i += 2)
        //{
        //    if (Mathf.Abs(vertexNumbers[i] - vertexNumbers[i + 1]) > 1)
        //    {
        //        realVertexNumbers.Add(vertexNumbers[i] + 1);
        //        realVertexNumbers.Add(vertexNumbers[i] + 2);
        //    }
        //}
        //vertexNumbers.AddRange(realVertexNumbers);
        return vertexNumbers;
    }
    public static bool isTwoLinesTouchEachOther(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        if (isLinesParallel(a, b, c, d))
            //return isPointLiesOnLine(a, c, d) || isPointLiesOnLine(b, c, d)
            //    || isPointLiesOnLine(c, a, b) || isPointLiesOnLine(d, a, b);
            return (a == c && b == d) || (a == d && b == c);
        // || (a == b && c == d) || (a == d && b == c);
        else
            return false;
    }
    public static float getLineSlope2D(Vector3 a, Vector3 b)
    {
        return (b.y - a.y) / (b.x - a.x);
    }
    public static bool isLinesParallel(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        var slope1 = getLineSlope2D(a, b);
        var slope2 = getLineSlope2D(c, d);
        //return Mathf.Abs(slope1 - slope2) < 0.001f;
        return slope1 == slope2 || (float.IsInfinity(slope1) && float.IsInfinity(slope2));

        //if 
    }
    public static bool isPointLiesOnLine(Vector3 point, Vector3 a, Vector3 b)
    {

        float AB = Mathf.Sqrt((b.x - a.x) * (b.x - a.x) + (b.y - a.y) * (b.y - a.y) + (b.z - a.z) * (b.z - a.z));
        float AP = Mathf.Sqrt((point.x - a.x) * (point.x - a.x) + (point.y - a.y) * (point.y - a.y) + (point.z - a.z) * (point.z - a.z));
        float PB = Mathf.Sqrt((b.x - point.x) * (b.x - point.x) + (b.y - point.y) * (b.y - point.y) + (b.z - point.z) * (b.z - point.z));

        //if (AB == AP + PB)
        if (Mathf.Abs(AB - AP - PB) < 0.001f)
        {
            //vertexNumbers.Add()
            return true;
        }
        else
            return false;
    }
    //public static Vector3[] getPerimeterVerices2(this Mesh mesh, bool removeDuplicates)
    //{
    //    take each edge from this mesh
    //        compare it eash edge of other meshs
    //        if same add it in collection
    //    for (int i=0; i< mesh.vertexCount; i++)

    //}
    public static Vector3[] getPerimeterVerices(this Mesh mesh, bool removeDuplicates)
    {
        var edges = mesh.getPerimeterVertexNumbers();

        List<Vector3> res = new List<Vector3>();

        for (int i = 0; i < edges.Count - 1; i++)
        {
            if (removeDuplicates)
            {
                if (edges[i] != edges[i + 1])
                    res.Add(mesh.vertices[edges[i]]);
            }
            else
                res.Add(mesh.vertices[edges[i]]);
        }
        res.Add(mesh.vertices[edges[edges.Count - 1]]);

        return res.ToArray();
    }
    public static Vector3 makeArrow(Vector3 arrowStart, Vector3 arrowEnd, float arrowBaseWidth) // true - water
    {
        //Vector3 directionPoint, leftBasePoint, rightBasePoint;
        Vector3 leftBasePoint;
        // Vector3[] result = new Vector3[3];

        //if (value > 0f)
        Vector3 arrowDirection = arrowEnd - arrowStart;
        //else
        //    arrowDirection = a.getTotalVertex() - b.getTotalVertex();

        leftBasePoint = Vector3.Cross(arrowDirection, Vector3.forward);
        leftBasePoint.Normalize();
        leftBasePoint = leftBasePoint * arrowBaseWidth;



        //rightBasePoint = leftBasePoint * -1f;
        //rightBasePoint += arrowStart;
        leftBasePoint += arrowStart;
        //directionPoint = arrowStart + (arrowDirection.normalized * value * 250f * arrowMuliplier);

        return leftBasePoint;
    }

}
public static class DateExtensions
{
    //public static int getYearsSince(this DateTime source, DateTime date2)
    //{
    //    return source.Year - date2.Year;
    //}
    public static int getYearsSince(this DateTime date2)
    {
        return Game.date.Year - date2.Year;
    }
    public static int getYearsUntill(this DateTime date2)
    {
        return date2.Year - Game.date.Year;
    }
    public static bool isYearsPassed(this DateTime date, int years)
    {
        return date.Year % years == 0;
    }
    public static bool isDatePassed(this DateTime date)
    {
        return date.CompareTo(Game.date) < 1;
    }
}