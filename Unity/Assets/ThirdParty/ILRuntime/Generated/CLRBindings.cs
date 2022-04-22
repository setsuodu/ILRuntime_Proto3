using System;
using System.Collections.Generic;
using System.Reflection;

namespace ILRuntime.Runtime.Generated
{
    class CLRBindings
    {

//will auto register in unity
#if UNITY_5_3_OR_NEWER
        [UnityEngine.RuntimeInitializeOnLoadMethod(UnityEngine.RuntimeInitializeLoadType.BeforeSceneLoad)]
#endif
        static private void RegisterBindingAction()
        {
            ILRuntime.Runtime.CLRBinding.CLRBindingUtils.RegisterBindingAction(Initialize);
        }

        internal static ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector3> s_UnityEngine_Vector3_Binding_Binder = null;
        internal static ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector2> s_UnityEngine_Vector2_Binding_Binder = null;
        internal static ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Quaternion> s_UnityEngine_Quaternion_Binding_Binder = null;

        /// <summary>
        /// Initialize the CLR binding, please invoke this AFTER CLR Redirection registration
        /// </summary>
        public static void Initialize(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            System_Object_Binding.Register(app);
            System_String_Binding.Register(app);
            System_Collections_Generic_Queue_1_Byte_Array_Binding.Register(app);
            System_Byte_Binding.Register(app);
            System_Array_Binding.Register(app);
            UnityEngine_Debug_Binding.Register(app);
            System_Threading_Interlocked_Binding.Register(app);
            UnityEngine_GameObject_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_MonoBehaviourAdapter_Binding_Adaptor_Binding.Register(app);
            UnityEngine_Object_Binding.Register(app);
            System_Type_Binding.Register(app);
            System_Char_Binding.Register(app);
            UnityEngine_Component_Binding.Register(app);
            UnityEngine_Transform_Binding.Register(app);
            UnityEngine_Vector3_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_String_MonoBehaviourAdapter_Binding_Adaptor_Binding_Enumerator_Binding.Register(app);
            System_Collections_Generic_KeyValuePair_2_String_MonoBehaviourAdapter_Binding_Adaptor_Binding.Register(app);
            System_IDisposable_Binding.Register(app);
            System_Collections_Generic_List_1_String_Binding.Register(app);
            System_IO_MemoryStream_Binding.Register(app);
            System_Diagnostics_Stopwatch_Binding.Register(app);
            System_Boolean_Binding.Register(app);
            System_Single_Binding.Register(app);
            UnityEngine_Quaternion_Binding.Register(app);
            UnityEngine_Vector2_Binding.Register(app);
            System_Collections_Generic_List_1_GameObject_Binding.Register(app);
            CoroutineDemo_Binding.Register(app);
            UnityEngine_Time_Binding.Register(app);
            UnityEngine_WaitForSeconds_Binding.Register(app);
            System_NotSupportedException_Binding.Register(app);
            CLRBindingTestClass_Binding.Register(app);
            System_Int32_Binding.Register(app);
            TestClassBase_Binding.Register(app);
            TestDelegateMethod_Binding.Register(app);
            TestDelegateFunction_Binding.Register(app);
            System_Action_1_String_Binding.Register(app);
            DelegateDemo_Binding.Register(app);
            System_Diagnostics_Debug_Binding.Register(app);
            System_ArgumentException_Binding.Register(app);
            System_Text_Encoding_Binding.Register(app);
            System_Math_Binding.Register(app);
            System_Threading_Thread_Binding.Register(app);
            System_Net_EndPoint_Binding.Register(app);
            System_Net_Sockets_Socket_Binding.Register(app);
            System_Net_Sockets_SocketAsyncEventArgs_Binding.Register(app);
            System_Net_Sockets_SocketException_Binding.Register(app);
            System_Threading_Monitor_Binding.Register(app);
            System_GC_Binding.Register(app);
            System_Net_IPEndPoint_Binding.Register(app);
            System_Net_IPAddress_Binding.Register(app);
            System_Guid_Binding.Register(app);
            UnityEngine_UI_Button_Binding.Register(app);
            UnityEngine_Events_UnityEvent_Binding.Register(app);
            System_Collections_Generic_Dictionary_2_Type_Queue_1_Object_Binding.Register(app);
            System_Activator_Binding.Register(app);
            System_Collections_Generic_Queue_1_Object_Binding.Register(app);
            ProtoBuf_Meta_RuntimeTypeModel_Binding.Register(app);
            ProtoBuf_Meta_TypeModel_Binding.Register(app);
            ProtoBuf_Serializer_Binding.Register(app);
            System_Reflection_MemberInfo_Binding.Register(app);
            System_Collections_Generic_List_1_Single_Binding.Register(app);
            System_Collections_Generic_List_1_Int32_Binding.Register(app);
            System_Collections_Generic_List_1_Int64_Binding.Register(app);
            System_Collections_Generic_List_1_ILTypeInstance_Binding.Register(app);
            LitJson_JsonMapper_Binding.Register(app);

            ILRuntime.CLR.TypeSystem.CLRType __clrType = null;
            __clrType = (ILRuntime.CLR.TypeSystem.CLRType)app.GetType (typeof(UnityEngine.Vector3));
            s_UnityEngine_Vector3_Binding_Binder = __clrType.ValueTypeBinder as ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector3>;
            __clrType = (ILRuntime.CLR.TypeSystem.CLRType)app.GetType (typeof(UnityEngine.Vector2));
            s_UnityEngine_Vector2_Binding_Binder = __clrType.ValueTypeBinder as ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Vector2>;
            __clrType = (ILRuntime.CLR.TypeSystem.CLRType)app.GetType (typeof(UnityEngine.Quaternion));
            s_UnityEngine_Quaternion_Binding_Binder = __clrType.ValueTypeBinder as ILRuntime.Runtime.Enviorment.ValueTypeBinder<UnityEngine.Quaternion>;
        }

        /// <summary>
        /// Release the CLR binding, please invoke this BEFORE ILRuntime Appdomain destroy
        /// </summary>
        public static void Shutdown(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            s_UnityEngine_Vector3_Binding_Binder = null;
            s_UnityEngine_Vector2_Binding_Binder = null;
            s_UnityEngine_Quaternion_Binding_Binder = null;
        }
    }
}
