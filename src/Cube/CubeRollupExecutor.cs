using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Cube
{
    public class CubeRollupExecutor<TElement> where TElement : new()
    {

        private Func<IEnumerable<TElement>, TElement> aggregator;

        private IList<Func<TElement, object>> dimentionSelectors = new List<Func<TElement, object>>();


        public CubeRollupExecutor(string[] facts, string[] dimentions, Func<IEnumerable<TElement>, TElement> aggregator)
        {
            this.aggregator = aggregator;

            // create fact expressions
            var fe1 = CreateLambdaExpression<int>("Fact1");
            var fe2= CreateLambdaExpression<int>("Fact2");
            var fe3 = CreateLambdaExpression<int>("Fact3");

            // create dim expressions
            var dim1 = CreateLambdaExpression<string>("Dim1");
            var dim2 = CreateLambdaExpression<string>("Dim2");
            var dim3 = CreateLambdaExpression<string>("Dim3");

            dimentionSelectors.Add(dim1.Compile());
            dimentionSelectors.Add(dim2.Compile());
        }


        public GroupResult<TElement> Rollup(IEnumerable<TElement> rows){

            // create a top level rollup result
            GroupResult<TElement> total = new GroupResult<TElement>();
            total.Items = rows;
            total.Count = rows.Count();
            total.Key = ""; // total key
            total.RollupItem = aggregator(rows);
            total.SubGroups = RollupInternal(rows, dimentionSelectors);
            return total;
        }

        private IEnumerable<GroupResult<TElement>> RollupInternal(IEnumerable<TElement> elements, IList<Func<TElement, object>> groupSelectors){


            if (groupSelectors.Count > 0)
            {
                var selector = groupSelectors.First();
                var nextSelectors = groupSelectors.Skip(1).ToArray();

                return

                    elements.GroupBy(selector).Select(

                        g => new GroupResult<TElement>

                        {
                            Key = g.Key,

                            Count = g.Count(),

                            RollupItem = CreateRollupItem(g),

                            Items = g,

                            SubGroups = RollupInternal(g, nextSelectors)

                        });

            } else
            {
                return null;
            }
        }

        private TElement CreateRollupItem(IEnumerable<TElement> elements)
        {
            TElement rollup = aggregator(elements);

            return rollup;
        }





        /// <summary>
        /// Create a lambda expression that can be used to extract named fact property of type U of an item
        /// </summary>
        /// <returns>The fact lambda expression.</returns>
        /// <param name="propName">Property Name</param>
        private Expression<Func<TElement, U>> CreateLambdaExpression<U>(string propName){
            
            ParameterExpression pe = Expression.Parameter(typeof(TElement), "item");
            Expression selectorExpression = Expression.Property(pe, typeof(TElement).GetProperty(propName));

            try{
                Expression<Func<TElement, U>> lambdaSelector =
                Expression.Lambda<Func<TElement, U>>(
                    selectorExpression,
                    new ParameterExpression[] { pe });

                return lambdaSelector;
            } catch(Exception e){
                Console.WriteLine(e.StackTrace);
            }
            return null;
        }

    }
}
