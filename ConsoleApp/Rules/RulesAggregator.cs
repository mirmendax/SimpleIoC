using ConsoleApp.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp.Model;
using SimpleIoC;

namespace ConsoleApp.Rules
{
    public enum Order{
        ASC,
        DESC      
    }

    public static class RulesAggregator<TSource, TDestination>
    {


        //public static List<IRuleConvert<TSource, TDestination>> GetRules()
        //{
        //    List<IRuleConvert<TSource, TDestination>> result;
        //    IEnumerable<Type> types;
        //    result = new List<IRuleConvert<TSource, TDestination>>();
        //    types = typeof(IRuleConvert<TSource, TDestination>).GetTypeInfo().Assembly.GetTypes()
        //        .Where(t => t.GetTypeInfo().GetInterfaces().Contains(typeof(IRuleConvert<TSource, TDestination>)));
        //    foreach (var type in types)
        //    {
        //        var rule = Activator.CreateInstance(type) as IRuleConvert<TSource, TDestination>;
        //        result.Add(rule);
        //    }
        //    return result;
        //}
        //

        private static List<IRuleConvert<TSource, TDestination>> GetRules()
        {
            var rules = SimpleFactory.GetAll<IRuleConvert<TSource, TDestination>>();
            return rules.ToList();
        }

        //private static List<IRuleOrderConvert<TSource, TDestination>> GetRules(bool isOrder, Order order = Order.ASC)
        //{
        //    List<IRuleOrderConvert<TSource, TDestination>> tempList;
        //    IEnumerable<Type> types;
        //
        //    tempList = new List<IRuleOrderConvert<TSource, TDestination>>();
        //    types = typeof(IRuleOrderConvert<TSource, TDestination>).GetTypeInfo().Assembly.GetTypes()
        //        .Where(t => t.GetTypeInfo().GetInterfaces().Contains(typeof(IRuleOrderConvert<TSource, TDestination>)));
        //    foreach (var type in types)
        //    {
        //        var rule = Activator.CreateInstance(type) as IRuleOrderConvert<TSource, TDestination>;
        //        tempList.Add(rule);
        //    }
        //    List<IRuleOrderConvert<TSource, TDestination>> result = new List<IRuleOrderConvert<TSource, TDestination>>();
        //    if (order == Order.ASC)
        //        result = tempList.OrderBy(x => x.OrderRule).ToList();
        //    else
        //        result = tempList.OrderByDescending(x=> x.OrderRule).ToList();
        //    return result;
        //}

        private static List<IRuleOrderConvert<TSource, TDestination>> GetRules(bool isOrder, Order order = Order.ASC)
        {
            var rules = SimpleFactory.GetAll<IRuleOrderConvert<TSource, TDestination>>();
            if (!isOrder)
                return rules.ToList();
            else
            {
                if (order == Order.ASC)
                    return rules.OrderBy(x => x.OrderRule).ToList();
                else
                    return rules.OrderByDescending(x => x.OrderRule).ToList();
            }
        }

        public static void Evaluate(TSource source, TDestination destionation)
        {
            var rules = GetRules();
            foreach (var rule in rules)
                rule.Convert(source, destionation);

        }
        public static void EvaluateOrder(TSource source, TDestination destionation, bool isOrder, Order order = Order.ASC)
        {
            
            var rules = GetRules(isOrder, order);
            foreach (var rule in rules)
                rule.Convert(source, destionation);

        }
    }
}
