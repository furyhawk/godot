using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Godot.NativeInterop;

#nullable enable

public partial class VariantUtils
{
    private static Exception UnsupportedType<T>() => new InvalidOperationException(
        $"The type is not supported for conversion to/from Variant: '{typeof(T).FullName}'");

    internal static class GenericConversion<T>
    {
        public static unsafe godot_variant ToVariant(in T from) =>
            ToVariantCb != null ? ToVariantCb(from) : throw UnsupportedType<T>();

        public static unsafe T FromVariant(in godot_variant variant) =>
            FromVariantCb != null ? FromVariantCb(variant) : throw UnsupportedType<T>();

        // ReSharper disable once StaticMemberInGenericType
        internal static unsafe delegate*<in T, godot_variant> ToVariantCb;

        // ReSharper disable once StaticMemberInGenericType
        internal static unsafe delegate*<in godot_variant, T> FromVariantCb;

        [SuppressMessage("ReSharper", "RedundantNameQualifier")]
        static GenericConversion()
        {
            RuntimeHelpers.RunClassConstructor(typeof(T).TypeHandle);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    public static godot_variant CreateFrom<[MustBeVariant] T>(in T from)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static TTo UnsafeAs<TTo>(in T f) => Unsafe.As<T, TTo>(ref Unsafe.AsRef(f));

        // `typeof(T) == typeof(X)` is optimized away. We cannot cache `typeof(T)` in a local variable, as it's not optimized when done like that.

        if (typeof(T) == typeof(bool))
            return CreateFromBool(UnsafeAs<bool>(from));

        if (typeof(T) == typeof(char))
            return CreateFromInt(UnsafeAs<char>(from));

        if (typeof(T) == typeof(sbyte))
            return CreateFromInt(UnsafeAs<sbyte>(from));

        if (typeof(T) == typeof(short))
            return CreateFromInt(UnsafeAs<short>(from));

        if (typeof(T) == typeof(int))
            return CreateFromInt(UnsafeAs<int>(from));

        if (typeof(T) == typeof(long))
            return CreateFromInt(UnsafeAs<long>(from));

        if (typeof(T) == typeof(byte))
            return CreateFromInt(UnsafeAs<byte>(from));

        if (typeof(T) == typeof(ushort))
            return CreateFromInt(UnsafeAs<ushort>(from));

        if (typeof(T) == typeof(uint))
            return CreateFromInt(UnsafeAs<uint>(from));

        if (typeof(T) == typeof(ulong))
            return CreateFromInt(UnsafeAs<ulong>(from));

        if (typeof(T) == typeof(float))
            return CreateFromFloat(UnsafeAs<float>(from));

        if (typeof(T) == typeof(double))
            return CreateFromFloat(UnsafeAs<double>(from));

        if (typeof(T) == typeof(Vector2))
            return CreateFromVector2(UnsafeAs<Vector2>(from));

        if (typeof(T) == typeof(Vector2i))
            return CreateFromVector2i(UnsafeAs<Vector2i>(from));

        if (typeof(T) == typeof(Rect2))
            return CreateFromRect2(UnsafeAs<Rect2>(from));

        if (typeof(T) == typeof(Rect2i))
            return CreateFromRect2i(UnsafeAs<Rect2i>(from));

        if (typeof(T) == typeof(Transform2D))
            return CreateFromTransform2D(UnsafeAs<Transform2D>(from));

        if (typeof(T) == typeof(Vector3))
            return CreateFromVector3(UnsafeAs<Vector3>(from));

        if (typeof(T) == typeof(Vector3i))
            return CreateFromVector3i(UnsafeAs<Vector3i>(from));

        if (typeof(T) == typeof(Basis))
            return CreateFromBasis(UnsafeAs<Basis>(from));

        if (typeof(T) == typeof(Quaternion))
            return CreateFromQuaternion(UnsafeAs<Quaternion>(from));

        if (typeof(T) == typeof(Transform3D))
            return CreateFromTransform3D(UnsafeAs<Transform3D>(from));

        if (typeof(T) == typeof(Vector4))
            return CreateFromVector4(UnsafeAs<Vector4>(from));

        if (typeof(T) == typeof(Vector4i))
            return CreateFromVector4i(UnsafeAs<Vector4i>(from));

        if (typeof(T) == typeof(AABB))
            return CreateFromAABB(UnsafeAs<AABB>(from));

        if (typeof(T) == typeof(Color))
            return CreateFromColor(UnsafeAs<Color>(from));

        if (typeof(T) == typeof(Plane))
            return CreateFromPlane(UnsafeAs<Plane>(from));

        if (typeof(T) == typeof(Callable))
            return CreateFromCallable(UnsafeAs<Callable>(from));

        if (typeof(T) == typeof(Signal))
            return CreateFromSignal(UnsafeAs<Signal>(from));

        if (typeof(T) == typeof(string))
            return CreateFromString(UnsafeAs<string>(from));

        if (typeof(T) == typeof(byte[]))
            return CreateFromPackedByteArray(UnsafeAs<byte[]>(from));

        if (typeof(T) == typeof(int[]))
            return CreateFromPackedInt32Array(UnsafeAs<int[]>(from));

        if (typeof(T) == typeof(long[]))
            return CreateFromPackedInt64Array(UnsafeAs<long[]>(from));

        if (typeof(T) == typeof(float[]))
            return CreateFromPackedFloat32Array(UnsafeAs<float[]>(from));

        if (typeof(T) == typeof(double[]))
            return CreateFromPackedFloat64Array(UnsafeAs<double[]>(from));

        if (typeof(T) == typeof(string[]))
            return CreateFromPackedStringArray(UnsafeAs<string[]>(from));

        if (typeof(T) == typeof(Vector2[]))
            return CreateFromPackedVector2Array(UnsafeAs<Vector2[]>(from));

        if (typeof(T) == typeof(Vector3[]))
            return CreateFromPackedVector3Array(UnsafeAs<Vector3[]>(from));

        if (typeof(T) == typeof(Color[]))
            return CreateFromPackedColorArray(UnsafeAs<Color[]>(from));

        if (typeof(T) == typeof(StringName[]))
            return CreateFromSystemArrayOfStringName(UnsafeAs<StringName[]>(from));

        if (typeof(T) == typeof(NodePath[]))
            return CreateFromSystemArrayOfNodePath(UnsafeAs<NodePath[]>(from));

        if (typeof(T) == typeof(RID[]))
            return CreateFromSystemArrayOfRID(UnsafeAs<RID[]>(from));

        if (typeof(T) == typeof(StringName))
            return CreateFromStringName(UnsafeAs<StringName>(from));

        if (typeof(T) == typeof(NodePath))
            return CreateFromNodePath(UnsafeAs<NodePath>(from));

        if (typeof(T) == typeof(RID))
            return CreateFromRID(UnsafeAs<RID>(from));

        if (typeof(T) == typeof(Godot.Collections.Dictionary))
            return CreateFromDictionary(UnsafeAs<Godot.Collections.Dictionary>(from));

        if (typeof(T) == typeof(Godot.Collections.Array))
            return CreateFromArray(UnsafeAs<Godot.Collections.Array>(from));

        if (typeof(T) == typeof(Variant))
            return NativeFuncs.godotsharp_variant_new_copy((godot_variant)UnsafeAs<Variant>(from).NativeVar);

        // More complex checks here at the end, to avoid screwing the simple ones in case they're not optimized away.

        // `typeof(X).IsAssignableFrom(typeof(T))` is optimized away

        if (typeof(Godot.Object).IsAssignableFrom(typeof(T)))
            return CreateFromGodotObject(UnsafeAs<Godot.Object>(from));

        // `typeof(T).IsValueType` is optimized away
        // `typeof(T).IsEnum` is NOT optimized away: https://github.com/dotnet/runtime/issues/67113
        // Fortunately, `typeof(System.Enum).IsAssignableFrom(typeof(T))` does the job!

        if (typeof(T).IsValueType && typeof(System.Enum).IsAssignableFrom(typeof(T)))
        {
            // `Type.GetTypeCode(typeof(T).GetEnumUnderlyingType())` is not optimized away.
            // Fortunately, `Unsafe.SizeOf<T>()` works and is optimized away.
            // We don't need to know whether it's signed or unsigned.

            if (Unsafe.SizeOf<T>() == 1)
                return CreateFromInt(UnsafeAs<sbyte>(from));

            if (Unsafe.SizeOf<T>() == 2)
                return CreateFromInt(UnsafeAs<short>(from));

            if (Unsafe.SizeOf<T>() == 4)
                return CreateFromInt(UnsafeAs<int>(from));

            if (Unsafe.SizeOf<T>() == 8)
                return CreateFromInt(UnsafeAs<long>(from));

            throw UnsupportedType<T>();
        }

        return GenericConversion<T>.ToVariant(from);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
    [SuppressMessage("ReSharper", "RedundantNameQualifier")]
    public static T ConvertTo<[MustBeVariant] T>(in godot_variant variant)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static T UnsafeAsT<TFrom>(TFrom f) => Unsafe.As<TFrom, T>(ref Unsafe.AsRef(f));

        if (typeof(T) == typeof(bool))
            return UnsafeAsT(ConvertToBool(variant));

        if (typeof(T) == typeof(char))
            return UnsafeAsT(ConvertToChar(variant));

        if (typeof(T) == typeof(sbyte))
            return UnsafeAsT(ConvertToInt8(variant));

        if (typeof(T) == typeof(short))
            return UnsafeAsT(ConvertToInt16(variant));

        if (typeof(T) == typeof(int))
            return UnsafeAsT(ConvertToInt32(variant));

        if (typeof(T) == typeof(long))
            return UnsafeAsT(ConvertToInt64(variant));

        if (typeof(T) == typeof(byte))
            return UnsafeAsT(ConvertToUInt8(variant));

        if (typeof(T) == typeof(ushort))
            return UnsafeAsT(ConvertToUInt16(variant));

        if (typeof(T) == typeof(uint))
            return UnsafeAsT(ConvertToUInt32(variant));

        if (typeof(T) == typeof(ulong))
            return UnsafeAsT(ConvertToUInt64(variant));

        if (typeof(T) == typeof(float))
            return UnsafeAsT(ConvertToFloat32(variant));

        if (typeof(T) == typeof(double))
            return UnsafeAsT(ConvertToFloat64(variant));

        if (typeof(T) == typeof(Vector2))
            return UnsafeAsT(ConvertToVector2(variant));

        if (typeof(T) == typeof(Vector2i))
            return UnsafeAsT(ConvertToVector2i(variant));

        if (typeof(T) == typeof(Rect2))
            return UnsafeAsT(ConvertToRect2(variant));

        if (typeof(T) == typeof(Rect2i))
            return UnsafeAsT(ConvertToRect2i(variant));

        if (typeof(T) == typeof(Transform2D))
            return UnsafeAsT(ConvertToTransform2D(variant));

        if (typeof(T) == typeof(Vector3))
            return UnsafeAsT(ConvertToVector3(variant));

        if (typeof(T) == typeof(Vector3i))
            return UnsafeAsT(ConvertToVector3i(variant));

        if (typeof(T) == typeof(Basis))
            return UnsafeAsT(ConvertToBasis(variant));

        if (typeof(T) == typeof(Quaternion))
            return UnsafeAsT(ConvertToQuaternion(variant));

        if (typeof(T) == typeof(Transform3D))
            return UnsafeAsT(ConvertToTransform3D(variant));

        if (typeof(T) == typeof(Vector4))
            return UnsafeAsT(ConvertToVector4(variant));

        if (typeof(T) == typeof(Vector4i))
            return UnsafeAsT(ConvertToVector4i(variant));

        if (typeof(T) == typeof(AABB))
            return UnsafeAsT(ConvertToAABB(variant));

        if (typeof(T) == typeof(Color))
            return UnsafeAsT(ConvertToColor(variant));

        if (typeof(T) == typeof(Plane))
            return UnsafeAsT(ConvertToPlane(variant));

        if (typeof(T) == typeof(Callable))
            return UnsafeAsT(ConvertToCallableManaged(variant));

        if (typeof(T) == typeof(Signal))
            return UnsafeAsT(ConvertToSignalManaged(variant));

        if (typeof(T) == typeof(string))
            return UnsafeAsT(ConvertToStringObject(variant));

        if (typeof(T) == typeof(byte[]))
            return UnsafeAsT(ConvertAsPackedByteArrayToSystemArray(variant));

        if (typeof(T) == typeof(int[]))
            return UnsafeAsT(ConvertAsPackedInt32ArrayToSystemArray(variant));

        if (typeof(T) == typeof(long[]))
            return UnsafeAsT(ConvertAsPackedInt64ArrayToSystemArray(variant));

        if (typeof(T) == typeof(float[]))
            return UnsafeAsT(ConvertAsPackedFloat32ArrayToSystemArray(variant));

        if (typeof(T) == typeof(double[]))
            return UnsafeAsT(ConvertAsPackedFloat64ArrayToSystemArray(variant));

        if (typeof(T) == typeof(string[]))
            return UnsafeAsT(ConvertAsPackedStringArrayToSystemArray(variant));

        if (typeof(T) == typeof(Vector2[]))
            return UnsafeAsT(ConvertAsPackedVector2ArrayToSystemArray(variant));

        if (typeof(T) == typeof(Vector3[]))
            return UnsafeAsT(ConvertAsPackedVector3ArrayToSystemArray(variant));

        if (typeof(T) == typeof(Color[]))
            return UnsafeAsT(ConvertAsPackedColorArrayToSystemArray(variant));

        if (typeof(T) == typeof(StringName[]))
            return UnsafeAsT(ConvertToSystemArrayOfStringName(variant));

        if (typeof(T) == typeof(NodePath[]))
            return UnsafeAsT(ConvertToSystemArrayOfNodePath(variant));

        if (typeof(T) == typeof(RID[]))
            return UnsafeAsT(ConvertToSystemArrayOfRID(variant));

        if (typeof(T) == typeof(StringName))
            return UnsafeAsT(ConvertToStringNameObject(variant));

        if (typeof(T) == typeof(NodePath))
            return UnsafeAsT(ConvertToNodePathObject(variant));

        if (typeof(T) == typeof(RID))
            return UnsafeAsT(ConvertToRID(variant));

        if (typeof(T) == typeof(Godot.Collections.Dictionary))
            return UnsafeAsT(ConvertToDictionaryObject(variant));

        if (typeof(T) == typeof(Godot.Collections.Array))
            return UnsafeAsT(ConvertToArrayObject(variant));

        if (typeof(T) == typeof(Variant))
            return UnsafeAsT(Variant.CreateCopyingBorrowed(variant));

        // More complex checks here at the end, to avoid screwing the simple ones in case they're not optimized away.

        // `typeof(X).IsAssignableFrom(typeof(T))` is optimized away

        if (typeof(Godot.Object).IsAssignableFrom(typeof(T)))
            return (T)(object)ConvertToGodotObject(variant);

        // `typeof(T).IsValueType` is optimized away
        // `typeof(T).IsEnum` is NOT optimized away: https://github.com/dotnet/runtime/issues/67113
        // Fortunately, `typeof(System.Enum).IsAssignableFrom(typeof(T))` does the job!

        if (typeof(T).IsValueType && typeof(System.Enum).IsAssignableFrom(typeof(T)))
        {
            // `Type.GetTypeCode(typeof(T).GetEnumUnderlyingType())` is not optimized away.
            // Fortunately, `Unsafe.SizeOf<T>()` works and is optimized away.
            // We don't need to know whether it's signed or unsigned.

            if (Unsafe.SizeOf<T>() == 1)
                return UnsafeAsT(ConvertToInt8(variant));

            if (Unsafe.SizeOf<T>() == 2)
                return UnsafeAsT(ConvertToInt16(variant));

            if (Unsafe.SizeOf<T>() == 4)
                return UnsafeAsT(ConvertToInt32(variant));

            if (Unsafe.SizeOf<T>() == 8)
                return UnsafeAsT(ConvertToInt64(variant));

            throw UnsupportedType<T>();
        }

        return GenericConversion<T>.FromVariant(variant);
    }
}
