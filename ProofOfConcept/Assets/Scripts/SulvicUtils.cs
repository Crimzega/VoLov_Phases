namespace Sulvic.IO{

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Text;

	public static class FileStreamExt{

		public static byte[] ReadBytes(this FileStream self, int length){
			byte[] buffer = new byte[length];
			self.Read(buffer, 0, length);
			return buffer;
		}

		public static bool ReadBoolean(this FileStream self) => self.ReadByte() == 1;

		public static char ReadChar(this FileStream self) => (char)self.ReadByte();

		public static double ReadDouble(this FileStream self) => BitConverter.ToDouble(self.ReadBytes(8), 0);

		public static float ReadFloat(this FileStream self) => BitConverter.ToSingle(self.ReadBytes(4), 0);

		public static int ReadInt(this FileStream self) => BitConverter.ToInt32(self.ReadBytes(4), 0);

		public static long ReadLong(this FileStream self) => BitConverter.ToInt64(self.ReadBytes(8), 0);

		public static sbyte ReadSByte(this FileStream self) => (sbyte)self.ReadByte();

		public static short ReadShort(this FileStream self) => BitConverter.ToInt16(self.ReadBytes(2), 0);

		public static string ReadStringZ(this FileStream self){
			List<byte> list = new List<byte>();
			int b;
			while((b = self.ReadByte()) != 0) list.Add((byte)b);
			return Encoding.ASCII.GetString(list.ToArray());
		}

		public static uint ReadUInt(this FileStream self) => BitConverter.ToUInt32(self.ReadBytes(4), 0);

		public static ulong ReadULong(this FileStream self) => BitConverter.ToUInt64(self.ReadBytes(8), 0);

		public static ushort ReadUShort(this FileStream self) => BitConverter.ToUInt16(self.ReadBytes(2), 0);

		public static string ReadUTF(this FileStream self) => Encoding.UTF8.GetString(self.ReadBytes((int)self.ReadUShort()));

		public static void Write(this FileStream self, byte[] buffer) => self.Write(buffer, 0);

		public static void Write(this FileStream self, byte[] buffer, int count) => self.Write(buffer, count, buffer.Length - count);

		public static void WriteBoolean(this FileStream self, bool value) => self.WriteByte((byte)(value? 0: 1));

		public static void WriteChar(this FileStream self, char ch) => self.WriteByte((byte)ch);

		public static void WriteDouble(this FileStream self, double value) => self.Write(BitConverter.GetBytes(value));

		public static void WriteFloat(this FileStream self, float value) => self.Write(BitConverter.GetBytes(value));

		public static void WriteInt(this FileStream self, int value) => self.Write(BitConverter.GetBytes(value));

		public static void WriteLong(this FileStream self, long value) => self.Write(BitConverter.GetBytes(value));

		public static void WriteSByte(this FileStream self, sbyte value) => self.Write(BitConverter.GetBytes(value));

		public static void WriteShort(this FileStream self, short value) => self.Write(BitConverter.GetBytes(value));

		public static void WriteStirngZ(this FileStream self, string str){
			self.Write(Encoding.ASCII.GetBytes(str));
			self.WriteByte(0);
		}

		public static void WriteUInt(this FileStream self, uint value) => self.Write(BitConverter.GetBytes(value));

		public static void WriteULong(this FileStream self, ulong value) => self.Write(BitConverter.GetBytes(value));

		public static void WriteUShort(this FileStream self, ushort value) => self.Write(BitConverter.GetBytes(value));

		public static void WriteUTF(this FileStream self, string str){
			Encoding utf8 = Encoding.UTF8;
			self.WriteUShort((ushort)utf8.GetByteCount(str));
			self.Write(utf8.GetBytes(str));
		}

	}

}

namespace Sulvic.Lib{

	using System;
	using System.Collections.Generic;
	using System.Xml;
	using System.Xml.Schema;
	using System.Xml.Serialization;

	public static class MultiExt{

		public static void AddRange<T>(this List<T> self, params T[] array){ self.AddRange((IEnumerable<T>)array); }

		public static bool NextBoolean(this Random self) => Convert.ToBoolean(self.Next(2));

		public static byte NextByte(this Random self) => (byte)(self.NextSingle() * byte.MaxValue);

		public static byte NextByte(this Random self, byte maxValue) => self.NextByte((byte)0, maxValue);

		public static byte NextByte(this Random self, byte minValue, byte maxValue) => (byte)(self.NextSingle() * (maxValue - minValue) + minValue);

		public static double NextDouble(this Random self, double maxValue) => self.NextDouble(0d, double.MaxValue);

		public static double NextDouble(this Random self, double minValue, double maxValue) => self.NextDouble() * (maxValue - minValue) + minValue;

		public static float NextSingle(this Random self) => (float)self.NextDouble();

		public static float NextSingle(this Random self, float maxValue) => self.NextSingle(0f, maxValue);

		public static float NextSingle(this Random self, float minValue, float maxValue) => self.NextSingle() * (maxValue - minValue) + minValue;

		public static long NextInt64(this Random self) => (long)(self.NextDouble() * long.MaxValue);

		public static long NextInt64(this Random self, long maxValue) => self.NextInt64(0L, maxValue);

		public static long NextInt64(this Random self, long minValue, long maxValue) => (long)(self.NextDouble() * (maxValue + minValue) + minValue);

		public static short NextInt16(this Random self) => (short)self.Next(short.MaxValue);

		public static short NextInt16(this Random self, short maxValue) => self.NextInt16((short)0, maxValue);

		public static short NextInt16(this Random self, short minValue, short maxValue) => (short)(self.NextSingle() * (maxValue - minValue) + minValue);

		public static sbyte NextSByte(this Random self) => (sbyte)(self.NextSingle() * sbyte.MaxValue);

		public static sbyte NextSByte(this Random self, sbyte maxValue) => self.NextSByte((sbyte)0, maxValue);

		public static sbyte NextSByte(this Random self, sbyte minValue, sbyte maxValue) => (sbyte)(self.NextSingle() * (maxValue - minValue) + minValue);

		public static uint NextUnsigned(this Random self) => (uint)(self.NextDouble() * uint.MaxValue);

		public static uint NextUnsigned(this Random self, uint maxValue) => self.NextUnsigned(0u, maxValue);

		public static uint NextUnsigned(this Random self, uint minValue, uint maxValue) => (uint)(self.NextDouble() * (maxValue - minValue) + minValue);

		public static ulong NextUInt64(this Random self) => (ulong)(self.NextDouble() * ulong.MaxValue);

		public static ulong NextUInt64(this Random self, ulong maxValue) => self.NextUInt64(0Lu, maxValue);

		public static ulong NextUInt64(this Random self, ulong minValue, ulong maxValue) => (ulong)(self.NextDouble() * (maxValue - minValue) + minValue);

		public static ushort NextUInt16(this Random self) => (ushort)self.Next(ushort.MaxValue);

		public static ushort NextUInt16(this Random self, ushort maxValue) => self.NextUInt16((ushort)0u, maxValue);

		public static ushort NextUInt16(this Random self, ushort minValue, ushort maxValue) => (ushort)(self.NextSingle() * (maxValue - minValue) - minValue);

	}

	public interface IDoubleKey<K, K1>: IXmlSerializable{

		K FirstKey{ get; }

		K1 SecondKey{ get; }

		bool HasKeys(K key, K1 key1);

		bool HasKeys(IDoubleKey<K, K1> keys);

	}

	public interface IDoubleValue<V, V1>: IXmlSerializable{

		V FirstValue{ get; set; }

		V1 SecondValue{ get; set; }

		bool HasValues(V value, V1 value1);

		bool HasValues(IDoubleValue<V, V1> values);

	}

	public interface IObjectRegistry<T>: IXmlSerializable{

		T GetObject(int id);

		T GetObject(string name);

		HashSet<int> IdSet();

		HashSet<string> NameSet();

		int GetId(T obj);

		int GetId(string name);

		string GetName(T obj);

		string GetName(int id);

		void AddObject(int id, string name, T obj);

	}

	[XmlRoot("KeyPair")]
	public class DoubleKey<K, K1>: IDoubleKey<K, K1>{

		private static readonly XmlSerializer SERIAL_K = new XmlSerializer(typeof(K)), SERIAL_K1 = new XmlSerializer(typeof(K1));
		private K firstKey;
		private K1 secondKey;

		public K FirstKey{ get => firstKey; }

		public K1 SecondKey{ get => secondKey; }

		public DoubleKey(){}

		public DoubleKey(K key, K1 key1){
			firstKey = key;
			secondKey = key1;
		}

		public override bool Equals(object obj) => obj.GetType() == typeof(IDoubleKey<K, K1>)? HasKeys((IDoubleKey<K, K1>)obj): base.Equals(obj);

		public override int GetHashCode() => HashCore.Create(9, 25).Append(FirstKey).Append(SecondKey).HashResult;

		public bool HasKeys(K key, K1 key1) => FirstKey.Equals(key) && SecondKey.Equals(key1);

		public bool HasKeys(IDoubleKey<K, K1> keys) => HasKeys(keys.FirstKey, keys.SecondKey);

		public void ReadXml(XmlReader reader){
			firstKey = (K)SERIAL_K.Deserialize(reader);
			secondKey = (K1)SERIAL_K1.Deserialize(reader);
		}

		public void WriteXml(XmlWriter writer){
			SERIAL_K.Serialize(writer, FirstKey);
			SERIAL_K1.Serialize(writer, SecondKey);
		}

		public XmlSchema GetSchema() => null;

	}

	[XmlRoot("ValuePair")]
	public class DoubleValue<V, V1>: IDoubleValue<V, V1>{

		private static readonly XmlSerializer SERIAL_V = new XmlSerializer(typeof(V)), SERIAL_V1 = new XmlSerializer(typeof(V1));

		public V FirstValue{ get; set; }

		public V1 SecondValue{ get; set; }

		public DoubleValue(){}

		public DoubleValue(V value, V1 value1){
			FirstValue = value;
			SecondValue = value1;
		}

		public override bool Equals(object obj) => obj.GetType() == typeof(IDoubleValue<V, V1>)? HasValues((IDoubleValue<V, V1>)obj): base.Equals(obj);

		public override int GetHashCode() => HashCore.Create(11, 21).Append(FirstValue).Append(SecondValue).HashResult;

		public bool HasValues(V value, V1 value1) => FirstValue.Equals(value) && SecondValue.Equals(value1);

		public bool HasValues(IDoubleValue<V, V1> values) => HasValues(values.FirstValue, values.SecondValue);

		public void ReadXml(XmlReader reader){
			FirstValue = (V)SERIAL_V.Deserialize(reader);
			SecondValue = (V1)SERIAL_V1.Deserialize(reader);
		}

		public void WriteXml(XmlWriter writer){
			SERIAL_V.Serialize(writer, FirstValue);
			SERIAL_V1.Serialize(writer, SecondValue);
		}

		public XmlSchema GetSchema() => null;

	}

	public class HashCore{

		private static Random RANDOM = new Random(0x612E6AF8);

		private int hashResult, initOdd, multOdd;

		public int HashResult{ get => hashResult; }

		private HashCore(int init, int mult){
			hashResult = initOdd = init % 2 == 0? init + (RANDOM.NextBoolean()? -1: 1): init;
			multOdd = mult % 2 == 0? mult + (RANDOM.NextBoolean()? -1: 1): mult;
		}

		public static HashCore Create(int init, int mult) => new HashCore(init, mult);

		public HashCore Append(object obj){
			hashResult = multOdd * hashResult + (obj == null? 0: obj.GetHashCode());
			return this;
		}

	}

}

namespace Sulvic.Util{

	using Sulvic.Lib;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public static class NumberExt{

		public static int Swap(this int self) => (int)(
			(((uint)self & 0xFF000000u) >> 24) |
			(((uint)self & 0x00FF0000u) >>  8) |
			(((uint)self & 0x0000FF00u) <<  8) |
			(((uint)self & 0x000000FFu) << 24)
		);

		public static long Swap(this long self) => (long)(
			(((ulong)self & 0xFF00000000000000L) >> 56) |
			(((ulong)self & 0x00FF000000000000L) >> 40) |
			(((ulong)self & 0x0000FF0000000000L) >> 24) |
			(((ulong)self & 0x000000FF00000000L) >>  8) |
			(((ulong)self & 0x00000000FF000000L) <<  8) |
			(((ulong)self & 0x0000000000FF0000L) << 24) |
			(((ulong)self & 0x000000000000FF00L) << 40) |
			(((ulong)self & 0x00000000000000FFL) << 56)
		);

		public static long Swap(this short self) => (short)(
			((self & 0xFF00) >> 8) |
			((self & 0x00FF) << 8)
		);

		public static uint Swap(this uint self) => (uint)(
			((self & 0xFF000000u) >> 24) |
			((self & 0x00FF0000u) >>  8) |
			((self & 0x0000FF00u) <<  8) |
			((self & 0x000000FFu) << 24)
		);

		public static ulong Swap(this ulong self) => (ulong)(
			((self & 0xFF00000000000000Lu) >> 56) |
			((self & 0x00FF000000000000Lu) >> 40) |
			((self & 0x0000FF0000000000Lu) >> 24) |
			((self & 0x000000FF00000000Lu) >>  8) |
			((self & 0x00000000FF000000Lu) <<  8) |
			((self & 0x0000000000FF0000Lu) << 24) |
			((self & 0x000000000000FF00Lu) << 40) |
			((self & 0x00000000000000FFLu) << 56)
		);

		public static ushort Swap(this ushort self) => (ushort)(
			((self & 0xFF00u) >> 8) |
			((self & 0x00FFu) << 8)
		);

	}

	public class SulvicCollections{

		public static T[] AddToArray<T>(T[] array, params T[] array1){
			T[] result = new T[array.Length + array1.Length];
			Array.Copy(array, 0, result, 0, array.Length);
			Array.Copy(array1, 0, result, array.Length, array.Length);
			return result;
		}

		public static T[] SubArray<T>(T[] array, int start, int end){
			if(array == null) return default(T[]);
			start = SulvicMath.Max(start, 0);
			end = SulvicMath.Min(end, array.Length - 1);
			int newSize = end + 1 - start;
			if(newSize < 0) return default(T[]);
			T[] result = new T[newSize];
			Array.Copy(array, start, result, 0, end);
			return result;
		}

		public static List<T> Sorted<T>(params T[] array){
			List<T> result = new List<T>();
			result.AddRange(array);
			return Sorted(result);
		}

		public static List<T> Sorted<T>(List<T> list){
			list.Sort();
			return list;
		}

		public static K GetRandomKey<K, V>(Dictionary<K, V> dict) => dict.Keys.ElementAt<K>(SulvicMath.Ranged(0, dict.Count - 1));

		public static V GetRandomValue<K, V>(Dictionary<K, V> dict) => dict.Values.ElementAt<V>(SulvicMath.Ranged(0, dict.Count - 1));

		public static T GetRandomObject<T>(T[] array) => array[SulvicMath.Ranged(0, array.Length - 1)];

		public static T GetRandomObject<T>(List<T> list) => list[SulvicMath.Ranged(0, list.Count - 1)];

		public static T ClampedObject<T>(T[] array, int index) => array[SulvicMath.Clamp(index, 0, array.Length - 1)];

		public static T ClampedObject<T>(List<T> list, int index) => list[SulvicMath.Clamp(index, 0, list.Count - 1)];

	}

	public static class SulvicMath{

		public const double ROTATION_BASE = Math.PI / 180d;
		private static Random RANDOM = new Random();

		public static byte Clamp(byte value, byte min, byte max) => Max(Min(value, max), min);

		public static byte Max(params byte[] array) => SulvicCollections.Sorted(array)[array.Length - 1];

		public static byte Max(List<byte> list) => SulvicCollections.Sorted(list)[list.Count - 1];

		public static byte Min(params byte[] array) => SulvicCollections.Sorted(array)[0];

		public static byte Min(List<byte> list) => SulvicCollections.Sorted(list)[0];

		public static byte Modulo(byte value, byte mod) => (byte)(value - Floor(value / mod) * mod);

		public static byte Ranged(byte min, byte max) => Ranged(RANDOM, min, max);

		public static byte Ranged(Random random, byte min, byte max){
			min = Min(min, max);
			max = Max(min, max);
			return random.NextByte(min, max);
		}

		public static byte Remap(byte value, byte min, byte max, byte min1, byte max1){
			min = Min(min, max);
			max = Max(min, max);
			min1 = Min(min1, max1);
			max1 = Max(min1, max1);
			return (byte)(((value - min) + (max1 - min1)) / (max - min) + min1);
		}

		public static byte Wrap(byte value, byte min, byte max){
			min = Min(min, max);
			max = Max(min, max);
			return (byte)((((value - min) % (max + 1 - min)) + (max + 1 - min)) % (max + 1 - min) + min);
		}

		public static double Clamp(double value, double min, double max) => Max(Min(value, max), min);

		public static double Max(params double[] array) => SulvicCollections.Sorted(array)[array.Length - 1];

		public static double Max(List<double> list) => SulvicCollections.Sorted(list)[list.Count - 1];

		public static double Min(params double[] array) => SulvicCollections.Sorted(array)[0];

		public static double Min(List<double> list) => SulvicCollections.Sorted(list)[0];

		public static double Modulo(double value, double mod) => (double)(value - FloorLong(value / mod) * mod);

		public static double Ranged(double min, double max) => Ranged(RANDOM, min, max);

		public static double Ranged(Random random, double min, double max){
			min = Min(min, max);
			max = Max(min, max);
			return random.NextDouble(min, max);
		}

		public static double Remap(double value, double min, double max, double min1, double max1){
			min = Min(min, max);
			max = Max(min, max);
			min1 = Min(min1, max1);
			max1 = Max(min1, max1);
			return (double)(((value - min) + (max1 - min1)) / (max - min) + min1);
		}

		public static double Rotate(double angle) => ROTATION_BASE * Wrap(angle, -179d, 180d);

		public static double Wrap(double value, double min, double max){
			min = Min(min, max);
			max = Max(min, max);
			return (double)((((value - min) % (max + 1d - min)) + (max + 1d - min)) % (max + 1d - min) + min);
		}

		public static float Clamp(float value, float min, float max) => Max(Min(value, max), min);

		public static float Max(params float[] array) => SulvicCollections.Sorted(array)[array.Length - 1];

		public static float Max(List<float> list) => SulvicCollections.Sorted(list)[list.Count - 1];

		public static float Min(params float[] array) => SulvicCollections.Sorted(array)[0];

		public static float Min(List<float> list) => SulvicCollections.Sorted(list)[0];

		public static float Modulo(float value, float mod) => (float)(value - Floor(value / mod) * mod);

		public static float Ranged(float min, float max) => Ranged(RANDOM, min, max);

		public static float Ranged(Random random, float min, float max){
			min = Min(min, max);
			max = Max(min, max);
			return random.NextSingle(min, max);
		}

		public static float Remap(float value, float min, float max, float min1, float max1){
			min = Min(min, max);
			max = Max(min, max);
			min1 = Min(min1, max1);
			max1 = Max(min1, max1);
			return (float)(((value - min) + (max1 - min1)) / (max - min) + min1);
		}

		public static float Rotate(float angle) => (float)ROTATION_BASE * Wrap(angle, -179f, 180f);

		public static float Wrap(float value, float min, float max){
			min = Min(min, max);
			max = Max(min, max);
			return (float)((((value - min) % (max + 1f - min)) + (max + 1f - min)) % (max + 1f - min) + min);
		}

		public static int Clamp(int value, int min, int max) => Max(Min(value, max), min);

		public static int Ceil(double value){
			int temp = (int)value;
			return value < (double)temp? temp + 1: temp;
		}

		public static int Ceil(float value){
			int temp = (int)value;
			return value < (float)temp? temp + 1: temp;
		}

		public static int Floor(double value){
			int temp = (int)value;
			return value < (double)temp? temp - 1: temp;
		}

		public static int Floor(float value){
			int temp = (int)value;
			return value < (float)temp? temp - 1: temp;
		}

		public static int Max(params int[] array) => SulvicCollections.Sorted(array)[array.Length - 1];

		public static int Max(List<int> list) => SulvicCollections.Sorted(list)[list.Count - 1];

		public static int Min(params int[] array) => SulvicCollections.Sorted(array)[0];

		public static int Min(List<int> list) => SulvicCollections.Sorted(list)[0];

		public static int Modulo(int value, int mod) => (int)(value - Floor(value / mod) * mod);

		public static int Ranged(int min, int max) => Ranged(RANDOM, min, max);

		public static int Ranged(Random random, int min, int max){
			min = Min(min, max);
			max = Max(min, max);
			return random.Next(min, max);
		}

		public static int Remap(int value, int min, int max, int min1, int max1){
			min = Min(min, max);
			max = Max(min, max);
			min1 = Min(min1, max1);
			max1 = Max(min1, max1);
			return (int)(((value - min) + (max1 - min1)) / (max - min) + min1);
		}

		public static int Wrap(int value, int min, int max){
			min = Min(min, max);
			max = Max(min, max);
			return (((value - min) % (max + 1 - min)) + (max + 1 - min)) % (max + 1 - min) + min;
		}

		public static long Clamp(long value, long min, long max) => Max(Min(value, max), min);

		public static long CeilLong(double value){
			long temp = (long)value;
			return value < (double)temp? temp + 1L: temp;
		}

		public static long CeilLong(float value){
			long temp = (long)value;
			return value < (float)temp? temp + 1L: temp;
		}

		public static long FloorLong(double value){
			long temp = (long)value;
			return value < (double)temp? temp - 1L: temp;
		}

		public static long FloorLong(float value){
			long temp = (long)value;
			return value < (float)temp? temp - 1L: temp;
		}

		public static long Max(params long[] array) => SulvicCollections.Sorted(array)[array.Length - 1];

		public static long Max(List<long> list) => SulvicCollections.Sorted(list)[list.Count - 1];

		public static long Min(params long[] array) => SulvicCollections.Sorted(array)[0];

		public static long Min(List<long> list) => SulvicCollections.Sorted(list)[0];

		public static long Modulo(long value, long mod) => (long)(value - FloorLong(value / mod) * mod);

		public static long Ranged(long min, long max) => Ranged(RANDOM, min, max);

		public static long Ranged(Random random, long min, long max){
			min = Min(min, max);
			max = Max(min, max);
			return random.NextInt64(min, max);
		}

		public static long Remap(long value, long min, long max, long min1, long max1){
			min = Min(min, max);
			max = Max(min, max);
			min1 = Min(min1, max1);
			max1 = Max(min1, max1);
			return (long)(((value - min) + (max1 - min1)) / (max - min) + min1);
		}

		public static long Wrap(long value, long min, long max){
			min = Min(min, max);
			max = Max(min, max);
			return (long)((((value - min) % (max + 1L - min)) + (max + 1L - min)) % (max + 1L - min) + min);
		}

		public static sbyte Clamp(sbyte value, sbyte min, sbyte max) => Max(Min(value, max), min);

		public static sbyte Max(params sbyte[] array) => SulvicCollections.Sorted(array)[array.Length - 1];

		public static sbyte Max(List<sbyte> list) => SulvicCollections.Sorted(list)[list.Count - 1];

		public static sbyte Min(params sbyte[] array) => SulvicCollections.Sorted(array)[0];

		public static sbyte Min(List<sbyte> list) => SulvicCollections.Sorted(list)[0];

		public static sbyte Modulo(sbyte value, sbyte mod) => (sbyte)(value - Floor(value / mod) * mod);

		public static sbyte Ranged(sbyte min, sbyte max) => Ranged(RANDOM, min, max);

		public static sbyte Ranged(Random random, sbyte min, sbyte max){
			min = Min(min, max);
			max = Max(min, max);
			return random.NextSByte(min, max);
		}

		public static sbyte Remap(sbyte value, sbyte min, sbyte max, sbyte min1, sbyte max1){
			min = Min(min, max);
			max = Max(min, max);
			min1 = Min(min1, max1);
			max1 = Max(min1, max1);
			return (sbyte)(((value - min) + (max1 - min1)) / (max - min) + min1);
		}

		public static sbyte Wrap(sbyte value, sbyte min, sbyte max){
			min = Min(min, max);
			max = Max(min, max);
			return (sbyte)((((value - min) % (max + 1 - min)) + (max + 1 - min)) % (max + 1 - min) + min);
		}

		public static short Clamp(short value, short min, short max) => Max(Min(value, max), min);

		public static short Max(params short[] array) => SulvicCollections.Sorted(array)[array.Length - 1];

		public static short Max(List<short> list) => SulvicCollections.Sorted(list)[list.Count - 1];

		public static short Min(params short[] array) => SulvicCollections.Sorted(array)[0];

		public static short Min(List<short> list) => SulvicCollections.Sorted(list)[0];

		public static short Modulo(short value, short mod) => (short)(value - Floor(value / mod) * mod);

		public static short Ranged(short min, short max) => Ranged(RANDOM, min, max);

		public static short Ranged(Random random, short min, short max){
			min = Min(min, max);
			max = Max(min, max);
			return random.NextInt16(min, max);
		}

		public static short Remap(short value, short min, short max, short min1, short max1){
			min = Min(min, max);
			max = Max(min, max);
			min1 = Min(min1, max1);
			max1 = Max(min1, max1);
			return (short)(((value - min) + (max1 - min1)) / (max - min) + min1);
		}

		public static short Wrap(short value, short min, short max){
			min = Min(min, max);
			max = Max(min, max);
			return (short)((((value - min) % (max + 1 - min)) + (max + 1 - min)) % (max + 1 - min) + min);
		}

		public static uint Clamp(uint value, uint min, uint max) => Max(Min(value, max), min);

		public static uint Max(params uint[] array) => SulvicCollections.Sorted(array)[array.Length - 1];

		public static uint Max(List<uint> list) => SulvicCollections.Sorted(list)[list.Count - 1];

		public static uint Min(params uint[] array) => SulvicCollections.Sorted(array)[0];

		public static uint Min(List<uint> list) => SulvicCollections.Sorted(list)[0];

		public static uint Modulo(uint value, uint mod) => (uint)(value - Floor(value / mod) * mod);

		public static uint Ranged(uint min, uint max) => Ranged(RANDOM, min, max);

		public static uint Ranged(Random random, uint min, uint max){
			min = Min(min, max);
			max = Max(min, max);
			return random.NextUnsigned(min, max);
		}

		public static uint Remap(uint value, uint min, uint max, uint min1, uint max1){
			min = Min(min, max);
			max = Max(min, max);
			min1 = Min(min1, max1);
			max1 = Max(min1, max1);
			return (uint)(((value - min) + (max1 - min1)) / (max - min) + min1);
		}

		public static uint Wrap(uint value, uint min, uint max){
			min = Min(min, max);
			max = Max(min, max);
			return (uint)((((value - min) % (max + 1u - min)) + (max + 1u - min)) % (max + 1u - min) + min);
		}

		public static ulong Clamp(ulong value, ulong min, ulong max) => Max(Min(value, max), min);

		public static ulong Max(params ulong[] array) => SulvicCollections.Sorted(array)[array.Length - 1];

		public static ulong Max(List<ulong> list) => SulvicCollections.Sorted(list)[list.Count - 1];

		public static ulong Min(params ulong[] array) => SulvicCollections.Sorted(array)[0];

		public static ulong Min(List<ulong> list) => SulvicCollections.Sorted(list)[0];

		public static ulong Modulo(ulong value, ulong mod) => (ulong)(value - (ulong)FloorLong(value / mod) * mod);

		public static ulong Ranged(ulong min, ulong max) => Ranged(RANDOM, min, max);

		public static ulong Ranged(Random random, ulong min, ulong max){
			min = Min(min, max);
			max = Max(min, max);
			return random.NextUInt64(min, max);
		}

		public static ulong Remap(ulong value, ulong min, ulong max, ulong min1, ulong max1){
			min = Min(min, max);
			max = Max(min, max);
			min1 = Min(min1, max1);
			max1 = Max(min1, max1);
			return (ulong)(((value - min) + (max1 - min1)) / (max - min) + min1);
		}

		public static ulong Wrap(ulong value, ulong min, ulong max){
			min = Min(min, max);
			max = Max(min, max);
			return (ulong)((((value - min) % (max + 1Lu - min)) + (max + 1Lu - min)) % (max + 1Lu - min) + min);
		}

		public static ushort Clamp(ushort value, ushort min, ushort max) => Max(Min(value, max), min);

		public static ushort Max(params ushort[] array) => SulvicCollections.Sorted(array)[array.Length - 1];

		public static ushort Max(List<ushort> list) => SulvicCollections.Sorted(list)[list.Count - 1];

		public static ushort Min(params ushort[] array) => SulvicCollections.Sorted(array)[0];

		public static ushort Min(List<ushort> list) => SulvicCollections.Sorted(list)[0];

		public static ushort Modulo(ushort value, ushort mod) => (ushort)(value - FloorLong(value / mod) * mod);

		public static ushort Ranged(ushort min, ushort max) => Ranged(RANDOM, min, max);

		public static ushort Ranged(Random random, ushort min, ushort max){
			min = Min(min, max);
			max = Max(min, max);
			return random.NextUInt16(min, max);
		}

		public static ushort Remap(ushort value, ushort min, ushort max, ushort min1, ushort max1){
			min = Min(min, max);
			max = Max(min, max);
			min1 = Min(min1, max1);
			max1 = Max(min1, max1);
			return (ushort)(((value - min) + (max1 - min1)) / (max - min) + min1);
		}

		public static ushort Wrap(ushort value, ushort min, ushort max){
			min = Min(min, max);
			max = Max(min, max);
			return (ushort)((((value - min) % (max + 1u - min)) + (max + 1u - min)) % (max + 1u - min) + min);
		}

	}

}
