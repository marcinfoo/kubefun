using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public static class EnumerableExtensions

{

    public static IEnumerable<GroupResult<TElement>> GroupByMany<TElement>(

        this IEnumerable<TElement> elements,


        // idea is that the aggregator internally needs to set the dimentions used so far in aggregation
        Func<IEnumerable<TElement>, IList<Expression<Func<TElement, object>>>, TElement> aggregator, 


        // group selectors used so far???
        IList<Expression<Func<TElement, object>>> selectorsUsedSoFar,

        params Expression<Func<TElement, object>>[] groupSelectors
    )

    {

        if (groupSelectors.Length > 0)
        {
            var selector = groupSelectors.First();

            //reduce the list recursively until zero

            var nextSelectors = groupSelectors.Skip(1).ToArray();

            var selectorsSoFar = new List<Expression<Func<TElement, object>>>();

            selectorsSoFar.Add(selector);

            return

                elements.GroupBy(selector.Compile()).Select(

                    g => new GroupResult<TElement>

                    {

                        Key = g.Key,

                        Count = g.Count(),

                RollupItem = aggregator(g, nextSelectors),

                        Items = g,

                SubGroups = g.GroupByMany(aggregator, nextSelectors)

                    });

        }

        else

            return null;

    }

}