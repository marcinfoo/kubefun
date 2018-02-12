using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Cube
{
    class Program
    {
        static void Main(string[] args)
        {
            string selectorProp = "Fact1";
            // Compose the expression tree that represents the parameter to the predicate.  
            ParameterExpression pe = Expression.Parameter(typeof(Row), "row");
            Expression selectorExpression = Expression.Property(pe, typeof(Row).GetProperty(selectorProp));
            Expression<Func<Row, int>> lambdaSelector =  
                Expression.Lambda<Func<Row, int>>(
                    selectorExpression,  
                    new ParameterExpression[] { pe }); 

            Func<Row, int> selector = lambdaSelector.Compile();

            IList<Row> items = new List<Row>(){
                new Row(){ Fact1=1, Fact2=2, Fact3=3, Fact4=4, Dim1="A", Dim2="BB", Dim3="CC", Dim4="D" },
                new Row(){ Fact1=1, Fact2=2, Fact3=3, Fact4=4, Dim1="A", Dim2="BB", Dim3="C", Dim4="DDD" },
                new Row(){ Fact1=1, Fact2=2, Fact3=3, Fact4=4, Dim1="AA", Dim2="B", Dim3="CCC", Dim4="DD" },
                new Row(){ Fact1=1, Fact2=2, Fact3=3, Fact4=4, Dim1="AA", Dim2="BB", Dim3="C", Dim4="D" },
                new Row(){ Fact1=1, Fact2=2, Fact3=3, Fact4=4, Dim1="A", Dim2="BB", Dim3="C", Dim4="DDD" },
                new Row(){ Fact1=1, Fact2=2, Fact3=3, Fact4=4, Dim1="AA", Dim2="B", Dim3="CC", Dim4="DD" },
                new Row(){ Fact1=1, Fact2=2, Fact3=3, Fact4=4, Dim1="A", Dim2="B", Dim3="CCC", Dim4="D" }

          };

            Func<IEnumerable<Row>, IList<Expression<Func<Row, object>>>, Row> aggregator = (rs, ss) =>
            {
                var fact1 = rs.Select(x => x.Fact1).Sum();
                var fact2 = rs.Select(x => x.Fact2).Sum();
                var fact3 = rs.Select(x => x.Fact3).Sum();
                var fact4 = rs.Select(x => x.Fact4).Sum();

                var row = new Row()
                {
                    Fact1 = fact1,
                    Fact2 = fact2,
                    Fact3 = fact3,
                    Fact4 = fact4                        
                };

                Console.WriteLine("TEST");

                foreach (var s in ss)
                {
                    var vext = s.Compile();
                    var prop = (PropertyInfo)((MemberExpression)s.Body).Member;
                    prop.SetValue(row, vext(rs.First()), null);
                }

                return row;
            };


            //var result = items.GroupByMany(aggregator, i=>i.Dim1, i=>i.Dim2);



            //var xyz = result.First();
            //Console.WriteLine(xyz.ToString());
            // print recursively everything under rollup, up to leaf nodes

        }

    }
}
