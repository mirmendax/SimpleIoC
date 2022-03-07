using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using SimpleIoC.Attributes;
using SimpleIoC.Models;
using SimpleIoC.Utils;

namespace SimpleIoC
{
    public static class SimpleFactory
    {
        private static List<Assembly> assemblies;
        private static List<TypeConfig> types;
        private static object _lock = new object();

        #region public Methods
        /// <summary>
        /// Инициализация IoC контейнера
        /// </summary>
        /// <param name="assembly">Ссылка на сборку в которой будет использоваться IoC контейнер</param>
        public static void Initialize(Assembly assembly)
        {
            if (assemblies == null)
                assemblies = new List<Assembly>();
            if (assemblies.Any(a => a.Equals(assembly))) return;
            assemblies.Add(assembly);
            ListBindings();
        }
        

        /// <summary>
        /// Регистрация <typeparamref name="TService"/> c реализацией <typeparamref name="TImplement"/>
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        /// <param name="isSingleton"><see langword="true"/> если нужно что бы класс инициализировался как синглтон</param>
        public static void Bind<TService, TImplement>(bool isSingleton = false)
        {
            Bind(typeof(TService), typeof(TImplement), $"{nameof(TService)}On{nameof(TImplement)}", isSingleton);
        }

        /// <summary>
        /// Регистрация реализации <typeparamref name="TImplement"/> в DI контейнер
        /// </summary>
        /// <typeparam name="TImplement"></typeparam>
        /// <param name="isSingleton"><see langword="true"/> если нужно что бы класс инициализировался как синглтон</param>
        public static void Register<TImplement>()
        {
            Bind(typeof(TImplement), typeof(TImplement), $"{nameof(TImplement)}");
        }

        /// <summary>
        /// Регистрация <typeparamref name="TService"/> как синглтон с реализацией <typeparamref name="TImplement"/> 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplement"></typeparam>
        public static void Singleton<TService, TImplement>()
        {
            Bind(typeof(TService), typeof(TImplement), $"{nameof(TService)}On{nameof(TImplement)}", true);
        }

        /// <summary>
        /// Регистрация синглтон реализации <typeparamref name="TImplement"/>
        /// </summary>
        /// <typeparam name="TImplement"></typeparam>
        public static void Singleton<TImplement>()
        {
            Bind(typeof(TImplement), typeof(TImplement), $"{nameof(TImplement)}asSingleton", true);
        }


        public static void ReBind<TOldImpl, TImpl>(bool isSingleton = false)
        {
            ReBind(typeof(TOldImpl), typeof(TOldImpl), null, isSingleton);
        }

        /// <summary>
        /// Получение экземпляра из контейнера
        /// </summary>
        /// <typeparam name="T">Тип выходного значения</typeparam>
        /// <param name="args">Параметры для конструктора</param>
        /// <returns>Инициализированный объект типа <typeparamref name="T"/></returns>
        public static T Get<T>(params object[] args)
        {
            if (assemblies == null)
                return default(T);
            try
            {
                var selectType = types.First(x => x.Service == typeof(T));
                return GetInstance<T>(selectType, args);
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// Получение экземпляра из контейнера
        /// </summary>
        /// <param name="nameService">Название</param>
        /// <param name="args">Параметры для конструктора</param>
        /// <returns>Инициализированный объект типа <see cref="object"/></returns>
        public static object Get(string nameService, params object[] args)
        {
            if (assemblies == null)
                return default;
            try
            {
                var selectType = types.First(x => x.Name == nameService);
                return GetInstance<object>(selectType, args);
            }
            catch
            {
                return default;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetAll<T>()
        {
            if (assemblies == null)
                return null;
            try
            {
                var selectTypes = types.Where(x => x.Service == typeof(T));
                var temp = new List<T>();
                foreach (var item in selectTypes)
                {
                    temp.Add(GetInstance<T>(item));
                }
                return temp;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region private Methods
        /// <summary>
        /// Связывание <paramref name="service"/> c реализацией <paramref name="impl"/>
        /// </summary>
        /// <param name="service">Сервис (Интерфейс)</param>
        /// <param name="impl">Реализация сервиса</param>
        /// <param name="name">Название этой сборки</param>
        private static void Bind(Type service, Type impl, string name, bool isSingleton = false)
        {
            lock (_lock)
            {
                if (types == null) return;
                if (types.Any(x => x.Service == service && x.Implementation == impl)) return;
                types.Add(new TypeConfig(service, impl, name) { IsSingleton = isSingleton });
            }
        }

        private static void ReBind(Type oldImpl, Type impl, string name, bool isSingleton = false)
        {
            lock (_lock)
            {
                if (types == null) return;
                if (!types.Any(x => x.Implementation == oldImpl)) return;
                else
                {
                    var findService = types.Find(x => x.Implementation == oldImpl);
                    findService.Implementation = impl;
                    findService.Name = $"{findService.Service.GetType().Name}On{findService.Implementation.GetType().Name}";
                    findService.IsSingleton = isSingleton;
                    if (isSingleton)
                        findService.Instance = null;
                }
            }
        }

        /// <summary>
        /// Получение экземпляра <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Тип выходного класса</typeparam>
        /// <param name="type">Ссылка на тип класса в контейнере</param>
        /// <param name="args">Аргументы для конструткора</param>
        /// <returns></returns>
        private static T GetInstance<T>(TypeConfig type, params object[] args)
        {
            if (!type.IsSingleton)
                return (T)Activator.CreateInstance(type.Implementation, args);
            else
            {
                if (type.Instance == null)
                    type.Instance = Activator.CreateInstance(type.Implementation);
                return (T)type.Instance;
            }
        }

        private static void ListBindings()
        {
            types = new List<TypeConfig>();
            lock (_lock)
            {
                Type binderAttr = typeof(SimpleBinderAttribute);
                foreach (var type in new AssemblyTypeLoader().GetTypes(assemblies))
                {
                    var tBind = type.GetCustomAttributes(binderAttr, false);
                    if (tBind.Length != 0)
                    {
                        var tAtrribute = tBind.GetValue(0) as SimpleBinderAttribute;
                        if (types.Any(x => x.Service.Equals(tAtrribute.Service) && x.Implementation.Equals(type))) return;
                        types.Add(new TypeConfig(tAtrribute.Service, type, tAtrribute.Name));
                    }
                }
                OverridedBinding();
            }
        }

        private static void OverridedBinding()
        {
            Type overrideAttr = typeof(SimpleOverridedAttribute);
            Type objType = typeof(object);
            foreach (var type in new AssemblyTypeLoader().GetTypes(assemblies))
            {
                var tOverr = type.GetCustomAttributes(overrideAttr, false);
                if (tOverr.Length != 0 && type.BaseType != objType)
                {
                    var findType = FindByBaseType(type.BaseType);
                    if (findType != null)
                        ReBind(findType.Implementation, type, "");
                }
            }
        }

        private static TypeConfig FindByBaseType(Type implement)
        {
            if (implement == null) return null;
            if (types == null) return null;
            foreach (var type in types)
            {
                if (type.Implementation.Equals(implement))
                    return type;
            }
            return null;
        }

        #endregion
    }
}
