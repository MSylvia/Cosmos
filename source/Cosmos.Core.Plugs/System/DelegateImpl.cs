﻿using System;

using Cosmos.IL2CPU;
using Cosmos.IL2CPU.Plugs;

namespace Cosmos.Core.Plugs.System
{
    [Plug(Target = typeof(Delegate))]
    //[PlugField(FieldId = "$$Method$$", FieldType = typeof(object))]
    //[PlugField(FieldId = "$$Object$$", FieldType = typeof(object))]
    [PlugField(FieldType = typeof(int), FieldId = "$$ReturnsValue$$")]
    public static class DelegateImpl
    {

        [PlugMethod(Signature = "System_MulticastDelegate__System_Delegate_InternalAllocLike_System_Delegate_")]
        public unsafe static uint InternalAllocLike(uint* aDelegate)
        {
            uint xNeededSize = 1024; // 24 is needed fields for Multicast Delegate
            xNeededSize += 12;
            uint xResultAddr = GCImplementation.AllocNewObject(xNeededSize);
            byte* xResult = (byte*)xResultAddr;
            byte* xDelegateAsByte = (byte*)aDelegate;
            for (int i = 0; i < 1024; i++)
            {
                xResult[i] = xDelegateAsByte[i];
            }
            return xResultAddr;
        }

        //[PlugMethod(Signature = "System_Void__System_Delegate__ctor___")]
        //public static void Ctor(Delegate d, [FieldAccess(Name = "$$ReturnsValue$$")] ref bool hasReturnvalue)
        //{
        //    hasReturnvalue = d.Method.ReturnType != typeof(void);
        //}

        public static IntPtr GetInvokeMethod(Delegate aThis)
        {
            return IntPtr.Zero;
        }

        public static bool Equals(Delegate aThis, object aThat)
        {
            // todo: implement proper Delegate.Equals(object)
            return false;
        }

        public static unsafe bool InternalEqualTypes([ObjectPointerAccess] uint** a, [ObjectPointerAccess] uint** b)
        {
            var xTypeA = a[0][0];
            var xTypeB = b[0][0];

            return xTypeA == xTypeB;
        }
    }
}
