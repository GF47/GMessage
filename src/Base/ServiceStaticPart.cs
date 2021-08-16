using System;
using System.Collections.Generic;

namespace GMessage
{
    public partial class Service
    {
        private static readonly Dictionary<int, IService> _services = new Dictionary<int, IService>();

        public static void Register(int id, IService service)
        {
            if (_services.ContainsKey(id))
            {
                _services[id] = service;
                Console.WriteLine($"ID为{id}的服务已经被替换");
            }
            else
            {
                _services.Add(id, service);
            }
        }

        public static void UnRegister(int id)
        {
            if (_services.ContainsKey(id)) { _services.Remove(id); }
        }

        public static bool Contain(int id)
        {
            return _services.ContainsKey(id);
        }

        public static void Call<T1, T2, T3, T4>(int id, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (_services.TryGetValue(id, out IService service))
            {
                ((Action<T1, T2, T3, T4>)service.Serve)(arg1, arg2, arg3, arg4);
            }
        }

        public static void Call<T1, T2, T3>(int id, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_services.TryGetValue(id, out IService service))
            {
                ((Action<T1, T2, T3>)service.Serve)(arg1, arg2, arg3);
            }
        }

        public static void Call<T1, T2>(int id, T1 arg1, T2 arg2)
        {
            if (_services.TryGetValue(id, out IService service))
            {
                ((Action<T1, T2>)service.Serve)(arg1, arg2);
            }
        }

        public static void Call<T1>(int id, T1 arg1)
        {
            if (_services.TryGetValue(id, out IService service))
            {
                ((Action<T1>)service.Serve)(arg1);
            }
        }

        public static void Call(int id)
        {
            if (_services.TryGetValue(id, out IService service))
            {
                ((Action)service.Serve)();
            }
        }

        public static R Call<T1, T2, T3, T4, R>(int id, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            R result = default;
            if (_services.TryGetValue(id, out IService service))
            {
                result = ((Func<T1, T2, T3, T4, R>)service.Serve)(arg1, arg2, arg3, arg4);
            }
            return result;
        }

        public static R Call<T1, T2, T3, R>(int id, T1 arg1, T2 arg2, T3 arg3)
        {
            R result = default;
            if (_services.TryGetValue(id, out IService service))
            {
                result = ((Func<T1, T2, T3, R>)service.Serve)(arg1, arg2, arg3);
            }
            return result;
        }

        public static R Call<T1, T2, R>(int id, T1 arg1, T2 arg2)
        {
            R result = default;
            if (_services.TryGetValue(id, out IService service))
            {
                result = ((Func<T1, T2, R>)service.Serve)(arg1, arg2);
            }
            return result;
        }

        public static R Call<T1, R>(int id, T1 arg1)
        {
            R result = default;
            if (_services.TryGetValue(id, out IService service))
            {
                result = ((Func<T1, R>)service.Serve)(arg1);
            }
            return result;
        }

        public static R Call<R>(int id)
        {
            R result = default;
            if (_services.TryGetValue(id, out IService service))
            {
                result = ((Func<R>)service.Serve)();
            }
            return result;
        }

        public static T Get<T>(int id) where T : Delegate
        {
            if (_services.TryGetValue(id, out IService service))
            {
                return service.Serve as T;
            }
            return null;
        }
    }
}