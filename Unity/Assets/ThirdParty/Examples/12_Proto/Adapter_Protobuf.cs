//适配文件放到主程序中
using System;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.CLR.Method;
using System.IO;
using System.Collections.Generic;
using System.Collections;

public class Adapter_Protobuf : CrossBindingAdaptor
{
    public override Type BaseCLRType
    {
        get
        {
            return null;
        }
    }

    public override Type[] BaseCLRTypes
    {
        get
        {
            return new Type[] { typeof(IEquatable<ILTypeInstance>), typeof(IComparable<ILTypeInstance>), typeof(IEnumerable<System.Byte>), typeof(IDisposable), typeof(IOException) };
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(Adaptor);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adaptor(appdomain, instance);
    }

    internal class Adaptor : IOException, IEquatable<ILTypeInstance>, IComparable<ILTypeInstance>, IEnumerable<System.Byte>, IDisposable, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        public Adaptor()
        {

        }

        public Adaptor(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }

        public object[] data1 = new object[1];

        public ILTypeInstance ILInstance { get { return instance; } }

        IMethod mEquals = null;
        bool mEqualsGot = false;
        public bool Equals(ILTypeInstance other)
        {
            if (!mEqualsGot)
            {
                mEquals = instance.Type.GetMethod("Equals", 1);
                if (mEquals == null)
                {
                    mEquals = instance.Type.GetMethod("System.IEquatable.Equals", 1);
                }
                mEqualsGot = true;
            }
            if (mEquals != null)
            {
                data1[0] = other;
                return (bool)appdomain.Invoke(mEquals, instance, data1);
            }
            return false;
        }

        IMethod mCompareTo = null;
        bool mCompareToGot = false;
        public int CompareTo(ILTypeInstance other)
        {
            if (!mCompareToGot)
            {
                mCompareTo = instance.Type.GetMethod("CompareTo", 1);
                if (mCompareTo == null)
                {
                    mCompareTo = instance.Type.GetMethod("System.IComparable.CompareTo", 1);
                }
                mCompareToGot = true;
            }
            if (mCompareTo != null)
            {
                data1[0] = other;
                return (int)appdomain.Invoke(mCompareTo, instance, data1);
            }
            return 0;
        }

        public IEnumerator<byte> GetEnumerator()
        {
            IMethod method = null;
            method = instance.Type.GetMethod("GetEnumerator", 0);
            if (method == null)
            {
                method = instance.Type.GetMethod("System.Collections.IEnumerable.GetEnumerator", 0);
            }
            if (method != null)
            {
                var res = appdomain.Invoke(method, instance, null);
                return (IEnumerator<byte>)res;
            }
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            IMethod method = null;
            method = instance.Type.GetMethod("GetEnumerator", 0);
            if (method == null)
            {
                method = instance.Type.GetMethod("System.Collections.IEnumerable.GetEnumerator", 0);
            }
            if (method != null)
            {
                var res = appdomain.Invoke(method, instance, null);
                return (IEnumerator)res;
            }
            return null;
        }

        IMethod mDisposeMethod;
        bool mDisposeMethodGot;
        public void Dispose()
        {
            if (!mDisposeMethodGot)
            {
                mDisposeMethod = instance.Type.GetMethod("Dispose", 0);
                mDisposeMethodGot = true;
            }

            if (mDisposeMethod != null)
            {
                appdomain.Invoke(mDisposeMethod, instance, null);
            }
        }
    }
}