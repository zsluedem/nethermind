// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Reflection.Emit;

namespace Evm.IL{
    public class Example7
    {
        public static void call()
        {
            AssemblyName aName = new AssemblyName("a");
            AssemblyBuilder ab = AssemblyBuilder.DefineDynamicAssembly(aName, AssemblyBuilderAccess.Run);

            ModuleBuilder mb =
                ab.DefineDynamicModule(aName.Name);

            TypeBuilder tb = mb.DefineType("t");

            MethodBuilder methodB = tb.DefineMethod("GetSquare", MethodAttributes.Public, typeof(Int32), new Type[]{typeof(Int32)});

            ILGenerator il = methodB.GetILGenerator();

            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Ldarg_1);
            il.Emit(OpCodes.Mul);
            il.Emit(OpCodes.Ret);
        }
    }
}


